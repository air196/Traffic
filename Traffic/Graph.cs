using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Traffic
{
    public class Graph
    {
        private Dictionary<int, Dictionary<int, List<TravelInfo>>> graph 
            = new Dictionary<int, Dictionary<int, List<TravelInfo>>>();
        public List<Bus> Buses { get; private set; }
        public Graph(List<Bus> buses)
        {
            Buses = buses;
            foreach (var b in buses)
                foreach (var t in b.BusTravels)
                {
                    if (!graph.ContainsKey(t.FromBusStopId))
                    {
                        graph.Add(t.FromBusStopId, new Dictionary<int, List<TravelInfo>>());
                        graph[t.FromBusStopId].Add(t.ToBusStopId, new List<TravelInfo>());
                        graph[t.FromBusStopId][t.ToBusStopId].Add(new TravelInfo()
                        { BusId = b.Id, Cost = b.Cost, Time = t.TravelTime });
                    }
                    else
                    {
                        if (!graph[t.FromBusStopId].ContainsKey(t.ToBusStopId))
                            graph[t.FromBusStopId].Add(t.ToBusStopId, new List<TravelInfo>());
                        graph[t.FromBusStopId][t.ToBusStopId].Add(new TravelInfo()
                        { BusId = b.Id, Cost = b.Cost, Time = t.TravelTime });
                    }
                }
        }

        private TimeSpan GetNextBusStopTime(int busId, int targetBusStopId, TimeSpan fromTime)
        {
            return Buses.Where((b) => { return b.Id == busId ? true : false; }).
                ElementAt(0).GetNextBusStopTime(fromTime, targetBusStopId);
        }

        private List<WayInfo> GetWayInfos(int startBusStopStopId, int targetBusStopId, 
            TimeSpan travelStartTime)
        {
            if (startBusStopStopId == targetBusStopId)
                return null;
            if (!graph.ContainsKey(targetBusStopId) || !graph.ContainsKey(startBusStopStopId))
                return null;
            var res = new List<WayInfo>();
            var temp = new List<WayInfo>();
            var parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount * 4;
            Parallel.ForEach(graph[startBusStopStopId], parallelOptions,
                (g) =>
                {
                    foreach (var travelinfo in g.Value)
                    {
                        try
                        {
                            var wayInfo = new WayInfo(new WayInfo.Travel()
                            {
                                BusId = travelinfo.BusId,
                                FromId = startBusStopStopId,
                                ToId = g.Key,
                                WaitTime = new TimeSpan(0,0,0),
                                TravelTime = travelinfo.Time,
                                Cost = travelinfo.Cost
                            }, GetNextBusStopTime(travelinfo.BusId, startBusStopStopId, travelStartTime));
                            if (wayInfo.Travels.Where(b => (b.ToId == g.Key ? true : false)).Count() == 1)
                                if (wayInfo.Travels.Last().ToId == targetBusStopId)
                                    res.Add(new WayInfo(wayInfo));
                                else
                                    temp.Add(new WayInfo(wayInfo));
                        }
                        catch(TimeElapsedException)
                        { }
                    }
                });
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;
            while (temp.Count > 0)
            {
                var tempCount = temp.Count;
                for(var i = 0; i < tempCount; i++)
                {
                    var fromBusStopId = temp.ElementAt(i).Travels.Last().ToId;
                    Parallel.ForEach(graph[fromBusStopId], parallelOptions, (g) =>
                    {
                        Parallel.ForEach(g.Value, parallelOptions, (travelinfo) =>
                        {
                            try
                            {
                                var wayInfo = temp.ElementAt(i).AddTravel(new WayInfo.Travel()
                                    {
                                        BusId = travelinfo.BusId,
                                        FromId = fromBusStopId,
                                        ToId = g.Key,
                                        WaitTime = GetNextBusStopTime(travelinfo.BusId, fromBusStopId, temp.ElementAt(i).EndTime) - temp.ElementAt(i).EndTime,
                                        TravelTime = travelinfo.Time,
                                        Cost = temp.ElementAt(i).Travels.Last().BusId == travelinfo.BusId
                                            ? 0
                                            : travelinfo.Cost
                                    });
                                if (wayInfo.Travels.Where(b => (b.FromId == g.Key ? true : false)).Count() == 0)
                                    if (wayInfo.Travels.Last().ToId == targetBusStopId)
                                        res.Add(new WayInfo(wayInfo));
                                    else if (wayInfo.Travels
                                                 .Where(travel => travel.ToId == targetBusStopId ? true : false)
                                                 .Count() == 0)
                                        temp.Add(new WayInfo(wayInfo));
                            }
                            catch (TimeElapsedException)
                            { }
                        });
                    });
                }
                temp.RemoveRange(0, tempCount);
            }
            return res.ToList();
        }

        public Task<WayInfo> GetCheapestWay(int startBusStopStopId, int targetBusStopId, 
            TimeSpan travelStartTime)
        {
            return Task.Run(() => {
                List<WayInfo> waysInfo = GetWayInfos(startBusStopStopId, targetBusStopId, travelStartTime);
                if (waysInfo.Count > 0)
                {
                    var allCheapest = waysInfo.Where(w => w.TotalCost == 
                                                          waysInfo.Min(wi => wi.TotalCost) ? true : false);
                    return allCheapest.Where(wayinf => 
                        wayinf.EndTime == allCheapest.Min(wayinfo => wayinfo.EndTime)).ElementAt(0);
                }
                else
                    return null;
            });
        }

        public Task<WayInfo> GetFastestWay(int startBusStopStopId, int targetBusStopId, 
            TimeSpan travelStartTime)
        {
            return Task.Run(() => {
                List<WayInfo> waysInfo = GetWayInfos(startBusStopStopId, targetBusStopId, travelStartTime);
                if (waysInfo.Count > 0)
                {
                    var fastest = waysInfo.Where(wayinf => wayinf.EndTime == 
                                                           waysInfo.Min(wayinfo => wayinfo.EndTime));
                    return fastest.Where(w => w.TotalCost == 
                                              fastest.Min(wi => wi.TotalCost) ? true : false).ElementAt(0);
                }  
                else
                    return null;
            });
        }
    }
}
