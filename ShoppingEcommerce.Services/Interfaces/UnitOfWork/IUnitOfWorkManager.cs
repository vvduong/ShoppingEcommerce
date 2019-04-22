using System;

namespace ShoppingEcommerce.Services
{
    public partial interface IUnitOfWorkManager : IDisposable
    {     
        IUnitOfWork NewUnitOfWork();
    }
}
