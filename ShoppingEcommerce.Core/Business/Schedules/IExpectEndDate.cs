using System;

namespace ShoppingEcommerce.Core.Business.Schedules
{
    public interface IExpectEndDate
    {
        IExpectIntervalFrequently WithEndDate(DateTime endDate);

        IExpectIntervalFrequently WithDefaultEndDate();
    }
}