using System;

namespace LeonSutedja.BookingSystem.Shared
{
    public class TimeRange
    {
        public static TimeRange Create(DateTime startTime, DateTime endTime)
        {
            try
            {
                return new TimeRange(startTime, endTime);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DateTime StartTime { get; }

        public DateTime EndTime { get; }

        public TimeRange(DateTime startTime, DateTime endTime)
        {
            if (endTime < startTime) throw new Exception("Invalid time");
            StartTime = startTime;
            EndTime = endTime;
        }

        public bool IsInside(DateTime timeInside)
            => StartTime <= timeInside && EndTime >= timeInside;

        public bool IsInside(TimeRange timeRange)
            => IsInside(timeRange.StartTime) && IsInside(timeRange.EndTime);

        public bool IsOverlap(TimeRange timeRange)
        {
            var isStartTimeInside = IsInside(timeRange.StartTime) && timeRange.StartTime != EndTime;
            var isEndTimeInside = IsInside(timeRange.EndTime) && timeRange.EndTime != StartTime;
            return isStartTimeInside || isEndTimeInside || timeRange.IsInside(this);
        } 
    }
}