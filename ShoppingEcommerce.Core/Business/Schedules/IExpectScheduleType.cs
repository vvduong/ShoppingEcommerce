using ShoppingEcommerce.Core.Abstraction;

namespace ShoppingEcommerce.Core.Business.Schedules
{
    public interface IExpectScheduleType
    {
        IBuildingSpecification<Schedule> ForDailySchedule();

        IBuildingSpecification<Schedule> ForWeeklySchedule();

        IBuildingSpecification<Schedule> ForMonthlySchedule();

        IBuildingSpecification<Schedule> ForYearlySchedule();
    }
}