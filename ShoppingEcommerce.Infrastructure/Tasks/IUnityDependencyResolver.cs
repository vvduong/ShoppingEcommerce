using Unity;

namespace ShoppingEcommerce.Infrastructure.Tasks
{
    public interface IUnityDependencyResolver
    {
        void ResolveDependency(IUnityContainer unityContainer);
    }
}