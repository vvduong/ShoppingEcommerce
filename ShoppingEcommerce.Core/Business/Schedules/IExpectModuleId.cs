using System;

namespace ShoppingEcommerce.Core.Business.Schedules
{
    public interface IExpectModuleId
    {
        IExpectObjectId WithModuleId(Guid moduleId);
    }
}