using System;

namespace ShoppingEcommerce.Core.Business.Schedules
{
    public interface IExpectStartDate
    {
        IExpectEndDate WithStartDate(DateTime startDate);

        IExpectEndDate WithDefaultStartDate();
    }
}