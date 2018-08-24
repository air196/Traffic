using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traffic
{
    public class WayInfo
    {
        public struct Travel
        { public int FromId, ToId, BusId, Cost; }
        public List<Travel> Travels { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }
        public TimeSpan TravelLength => EndTime - StartTime;
        public int TotalCost => Travels.Sum(t => t.Cost);
        public string WayDescription => GetWayDescription();

        public WayInfo(WayInfo info)
        {
            Travels = new List<Travel>();
            Travels.AddRange(info.Travels);
            StartTime = info.StartTime;
            EndTime = info.EndTime;
        }
        public WayInfo(Travel busStop, TimeSpan startTime, TimeSpan travelTime)
        {
            if (startTime.TotalMinutes + travelTime.TotalMinutes > 24 * 60)
                throw new TimeElapsedException();
            Travels = new List<Travel> { busStop };
            StartTime = startTime;
            EndTime = startTime + travelTime;
        }
        public WayInfo AddTravel(Travel travel, TimeSpan travelTime)
        {
            if (EndTime.TotalMinutes + travelTime.TotalMinutes > 24 * 60)
                throw new TimeElapsedException();
            WayInfo res = new WayInfo(this);
            res.Travels.Add(travel);
            res.EndTime += travelTime;
            return res;
        }
        private string GetWayDescription()
        {
            var sb = new StringBuilder(StartTime.ToString().Substring(0, StartTime.ToString().Length - 3) + " ");
            for (var i = 0; i < Travels.Count; i++)
            {
                if (i == 0 || Travels[i].BusId != Travels[i - 1].BusId)
                    sb.Append(" (автобус №" + Travels[i].BusId + " " + Travels[i].Cost + " у.е.) " + Travels[i].FromId);
                sb.Append("->" + Travels[i].ToId);
            }
            sb.Append(" " + EndTime.ToString().Substring(0, EndTime.ToString().Length - 3));
            sb.Append("\nОбщая длительность поездки: " + TravelLength.ToString().Substring(0, TravelLength.ToString().Length - 3));
            sb.Append("\nОбщая стоимость поездки: " + TotalCost + " у.е.");
            return sb.ToString();
        }
    }
}
