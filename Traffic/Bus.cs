using System;
using System.Collections.Generic;

namespace Traffic
{
    public class Bus
    {
        public struct BusTravel
        {
            public int FromBusStopId, ToBusStopId;
            public TimeSpan TravelTime;
        }
        public int Id { get; }
        public int Cost { get; }
        public TimeSpan StartTime { get; }
        public TimeSpan TotalRouteTime { get; }
        public List<BusTravel> BusTravels { get; }
        public Bus(int id, int cost, TimeSpan startTime, List<BusTravel> busTravels)
        {
            Id = id;
            Cost = cost;
            StartTime = startTime;
            BusTravels = busTravels;
            foreach (var t in busTravels)
                TotalRouteTime += t.TravelTime;
        }
        public TimeSpan GetNextBusStopTime(TimeSpan fromTime, int busStopId)
        {
            TimeSpan busStopTime = StartTime;
            foreach (var travel in BusTravels)
            {
                if (travel.FromBusStopId != busStopId)
                {
                    if (busStopTime.TotalMinutes + travel.TravelTime.TotalMinutes > 24 * 60)
                        throw new TimeElapsedException();
                    busStopTime += travel.TravelTime;
                }
                else break;
            }
            while (busStopTime < fromTime)
            {
                if (busStopTime.TotalMinutes + TotalRouteTime.TotalMinutes > 24 * 60)
                    throw new TimeElapsedException();
                busStopTime += TotalRouteTime;
            }
            return busStopTime;
        }
    }
}

