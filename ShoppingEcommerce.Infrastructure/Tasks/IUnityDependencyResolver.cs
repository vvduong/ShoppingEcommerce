using Unity;

namespace ShoppingEcommerce.Tasks
{
    public interface IUnityDependencyResolver
    {
        void ResolveDependency(IUnityContainer unityContainer);
    }
}