using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingEcommerce.Core.Abstraction;

namespace ShoppingEcommerce.Core.Business.Schedules
{
    public class Schedule : AggregateRoot
    {
        internal Schedule()
        {
        }

        public Guid ModuleId { get; internal set; }

        public string ObjectId { get; internal set; }

        public ScheduleType ScheduleType { get; internal set; }

        public DateTime StartDate { get; internal set; }

        public DateTime? EndDate { get; internal set; }

        public int IntervalFrequently { get; internal set; }

        public bool IntervalInWeekday { get; internal set; }

        public string IntervalInDayOfWeek { get; internal set; }

        public int? IntervalInDateOfMonth { get; internal set; }

        public ScheduleMonthOfYear? IntervalInMonthOfYear { get; internal set; }

        public int? IntervalOrdinalNumber { get; internal set; }

        public int? IntervalOrdinalNumberInDayOfWeek { get; internal set; }

        // builder is just temporary solution
        // further it will be constructed by specification for fully encapsulation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="objectId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="intervalFrequently"></param>
        /// <returns></returns>
        private static Schedule BuildSchedule(Guid moduleId
            , string objectId
            , DateTime? startDate
            , DateTime? endDate
            , int intervalFrequently)
        {
            if (moduleId == Guid.Empty)
            {
                throw new ArgumentException(nameof(moduleId));
            }

            if (string.IsNullOrEmpty(objectId))
            {
                throw new ArgumentException(nameof(objectId));
            }

            if (endDate < startDate)
            {
                throw new ArgumentException(nameof(endDate));
            }

            if (intervalFrequently < 1)
            {
                throw new ArgumentException(nameof(intervalFrequently));
            }

            var schedule = new Schedule
            {
                ModuleId = moduleId,
                ObjectId = objectId,
                StartDate = startDate ?? DateTime.Now,
                EndDate = endDate,
                IntervalFrequently = intervalFrequently
            };

            return schedule;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="objectId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="intervalFrequently"></param>
        /// <returns></returns>
        public static Schedule BuildDailySchedule(Guid moduleId
            , string objectId
            , DateTime? startDate
            , DateTime? endDate
            , int intervalFrequently)
        {
            var schedule = BuildSchedule(moduleId, objectId, startDate, endDate, intervalFrequently);

            schedule.ScheduleType = ScheduleType.Daily;

            return schedule;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="objectId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="intervalFrequently"></param>
        /// <param name="intervalInDayOfWeek"></param>
        /// <returns></returns>
        public static Schedule BuildWeeklySchedule(Guid moduleId
            , string objectId
            , DateTime? startDate
            , DateTime? endDate
            , int intervalFrequently
            , IEnumerable<ScheduleDayOfWeek> intervalInDayOfWeek)
        {
            if (intervalInDayOfWeek == null)
            {
                throw new ArgumentNullException(nameof(intervalInDayOfWeek));
            }

            var intervalInDayOfWeekList = intervalInDayOfWeek
                .Select(selector => (int) selector)
                .ToList();

            if (!intervalInDayOfWeekList.Any())
            {
                throw new ArgumentException(nameof(intervalInDayOfWeek));
            }

            var schedule = BuildSchedule(moduleId, objectId, startDate, endDate, intervalFrequently);

            schedule.ScheduleType = ScheduleType.Weekly;
            schedule.IntervalInDayOfWeek = string.Join(";", intervalInDayOfWeekList);

            return schedule;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="objectId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="intervalFrequently"></param>
        /// <param name="intervalInDateOfMonth"></param>
        /// <returns></returns>
        public static Schedule BuildMonthlySchedule(Guid moduleId
            , string objectId
            , DateTime? startDate
            , DateTime? endDate
            , int intervalFrequently
            , int intervalInDateOfMonth)
        {
            if (intervalInDateOfMonth < 1 || intervalInDateOfMonth > 31)
            {
                throw new ArgumentException(nameof(intervalInDateOfMonth));
            }

            var schedule = BuildSchedule(moduleId, objectId, startDate, endDate, intervalFrequently);

            schedule.ScheduleType = ScheduleType.Monthly;
            schedule.IntervalInDateOfMonth = intervalInDateOfMonth;

            return schedule;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="objectId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="intervalFrequently"></param>
        /// <param name="intervalInDateOfMonth"></param>
        /// <param name="intervalInMonthOfYear"></param>
        /// <returns></returns>
        public static Schedule BuildYearlySchedule(Guid moduleId
            , string objectId
            , DateTime? startDate
            , DateTime? endDate
            , int intervalFrequently
            , int intervalInDateOfMonth
            , ScheduleMonthOfYear intervalInMonthOfYear)
        {
            var schedule = BuildMonthlySchedule(moduleId, objectId, startDate, endDate, intervalFrequently,
                intervalInDateOfMonth);

            schedule.ScheduleType = ScheduleType.Yearly;
            schedule.IntervalInMonthOfYear = intervalInMonthOfYear;

            return schedule;
        }
    }
}