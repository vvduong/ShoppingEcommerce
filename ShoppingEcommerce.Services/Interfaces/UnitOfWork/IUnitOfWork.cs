using System;

namespace ShoppingEcommerce.Services
{
    public partial interface IUnitOfWork : IDisposable
    {
        //TService GetService<TService>();
        TContext GetContext<TContext>() where TContext : class;
        void BeginTransaction();
        void Commit();

        bool ChangeConnection(string connectionString);
    }

    public partial interface IDocUnitOfWork : IDisposable
    {
        //TService GetService<TService>();
        TContext GetContext<TContext>() where TContext : class;
        void BeginTransaction();
        void Commit();

        bool ChangeConnection(string connectionString);
    }
}
