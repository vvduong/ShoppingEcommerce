using ShoppingEcommerce.Core.Abstraction;

namespace ShoppingEcommerce.Core.Business.Schedules
{
    public interface IExpectIntervalFrequently
    {
        IBuildingSpecification<Schedule> WithIntervalFrequently(int intervalFrequently);
    }
}