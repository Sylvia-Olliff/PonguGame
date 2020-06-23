using System.Diagnostics;
using PonguGame.model;

namespace PonguGame.resources
{
    public class SingletonRegistry<T> : IRegistryItem
    {
        private T _self;
        
        public SingletonRegistry(T instance)
        {
            _self = instance;
        }

        public ref T Get()
        {
            return ref _self;
        }
    }
}