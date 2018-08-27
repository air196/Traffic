using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traffic
{
    public class WayInfo
    {
        public struct Travel
        {
            public int FromId, ToId, BusId, Cost;
            public TimeSpan WaitTime, TravelTime;
        }
        public List<Travel> Travels { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime => GetEndTime();
        
        private TimeSpan GetEndTime()
        {
            TimeSpan res = StartTime;
            foreach (var t in Travels)
                res += t.WaitTime + t.TravelTime;
            return res;
        }

        public TimeSpan TravelLength => EndTime - StartTime;
        public int TotalCost => Travels.Sum(t => t.Cost);
        public string WayDescription => GetWayDescription();

        public WayInfo(WayInfo info)
        {
            Travels = new List<Travel>();
            Travels.AddRange(info.Travels);
            StartTime = info.StartTime;
        }
        public WayInfo(Travel travel, TimeSpan startTime)
        {
            if (startTime.TotalMinutes + travel.TravelTime.TotalMinutes > 24 * 60)
                throw new TimeElapsedException();
            Travels = new List<Travel> { travel };
            StartTime = startTime;
        }
        public WayInfo AddTravel(Travel travel)
        {
            if (EndTime.TotalMinutes + travel.TravelTime.TotalMinutes > 24 * 60)
                throw new TimeElapsedException();
            WayInfo res = new WayInfo(this);
            res.Travels.Add(travel);
            return res;
        }
        private string GetWayDescription()
        {
            var sb = new StringBuilder(StartTime.ToString().Substring(0, StartTime.ToString().Length - 3) + " ");
            for (var i = 0; i < Travels.Count; i++)
            {
                if (i == 0 || Travels[i].BusId != Travels[i - 1].BusId)
                    sb.Append("\nАвтобус №" + Travels[i].BusId + " (" + Travels[i].Cost + " руб.) " + Travels[i].FromId);
                sb.Append("->" + Travels[i].ToId);
            }
            sb.Append('\n' + EndTime.ToString().Substring(0, EndTime.ToString().Length - 3));
            sb.Append("\nОбщая длительность поездки: " + TravelLength.ToString().Substring(0, TravelLength.ToString().Length - 3));
            sb.Append("\nОбщая стоимость поездки: " + TotalCost + " руб.");
            return sb.ToString();
        }
    }
}
