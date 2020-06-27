
namespace PonguGame.resources
{
    public class RegistryObject<T> : IRegistryItem
    {
        private T _self;
        
        public RegistryObject(T instance)
        {
            _self = instance;
        }

        public ref T Get()
        {
            return ref _self;
        }
    }
}