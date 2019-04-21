using System;
using System.Collections.Generic;
using ShoppingEcommerce.Core.Abstraction;

namespace ShoppingEcommerce.Core.Business.Schedules
{
    public class ScheduleSpecification : ValueObject
        , IExpectModuleId
        , IExpectObjectId
        , IExpectStartDate
        , IExpectEndDate
        , IExpectIntervalFrequently
        , IBuildingSpecification<Schedule>
    {
        private ScheduleSpecification()
        {
        }

        public static IExpectModuleId Initialize() => new ScheduleSpecification();

        private Guid ModuleId { get; set; }

        private string ObjectId { get; set; }

        private DateTime StartDate { get; set; }

        private DateTime EndDate { get; set; }

        private int IntervalFrequently { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ModuleId;

            yield return ObjectId;

            yield return StartDate;

            yield return EndDate;

            yield return IntervalFrequently;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public IExpectObjectId WithModuleId(Guid moduleId)
        {
            if (moduleId == Guid.Empty)
            {
                throw new ArgumentException(nameof(moduleId));
            }

            return new ScheduleSpecification
            {
                ModuleId = moduleId
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public IExpectStartDate WithObjectId(string objectId)
        {
            if (string.IsNullOrEmpty(objectId))
            {
                throw new ArgumentException(nameof(objectId));
            }

            return new ScheduleSpecification
            {
                ModuleId = ModuleId,
                ObjectId = objectId
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public IExpectEndDate WithStartDate(DateTime startDate)
        {
            return new ScheduleSpecification
            {
                ModuleId = ModuleId,
                ObjectId = ObjectId,
                StartDate = startDate
            };
        }

        public IExpectEndDate WithDefaultStartDate()
        {
            return new ScheduleSpecification
            {
                ModuleId = ModuleId,
                ObjectId = ObjectId,
                StartDate = DateTime.Now
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IExpectIntervalFrequently WithEndDate(DateTime endDate)
        {
            if (endDate < StartDate)
            {
                throw new ArgumentException(nameof(endDate));
            }

            return new ScheduleSpecification
            {
                ModuleId = ModuleId,
                ObjectId = ObjectId,
                StartDate = StartDate,
                EndDate = endDate
            };
        }

        public IExpectIntervalFrequently WithDefaultEndDate()
        {
            return new ScheduleSpecification
            {
                ModuleId = ModuleId,
                ObjectId = ObjectId,
                StartDate = StartDate,
                EndDate = StartDate.AddMonths(1)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intervalFrequently"></param>
        /// <returns></returns>
        public IBuildingSpecification<Schedule> WithIntervalFrequently(int intervalFrequently)
        {
            if (intervalFrequently < 1)
            {
                throw new ArgumentException(nameof(intervalFrequently));
            }

            return new ScheduleSpecification
            {
                ModuleId = ModuleId,
                ObjectId = ObjectId,
                StartDate = StartDate,
                EndDate = EndDate,
                IntervalFrequently = intervalFrequently
            };
        }

        public Schedule Build()
        {
            return new Schedule
            {
                ModuleId = ModuleId,
                ObjectId = ObjectId,
                StartDate = StartDate,
                EndDate = EndDate,
                IntervalFrequently = IntervalFrequently
            };
        }
    }
}