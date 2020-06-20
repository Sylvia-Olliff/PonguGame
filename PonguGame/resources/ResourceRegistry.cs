using System;
using System.Collections.Concurrent;
using SFML.Graphics;

namespace PonguGame.resources
{
    public static class ResourceRegistry
    {
        private static bool _registriesLoaded = false;
        private static readonly ConcurrentDictionary<string, Sprite> EntityRegistry = new ConcurrentDictionary<string, Sprite>();
        private static readonly ConcurrentDictionary<string, Shape> ObjectRegistry = new ConcurrentDictionary<string, Shape>();
        private static readonly ResourceLoader _resourceLoader = new ResourceLoader(new Uri(Environment.CurrentDirectory));

        public static bool RegistriesLoaded => _registriesLoaded;

        public static void RegisterEntity(string name, Sprite sprite)
        {
            if (_registriesLoaded)
                throw new AccessViolationException($"Attempt to register an entity after Registration phase! Name: {name}");
            
            try
            {
                sprite.Texture = _resourceLoader.LoadTexture(name);
                EntityRegistry.TryAdd(name, sprite);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error adding Entity {name} to entity registry!");
                Console.Error.WriteLine(e);
            }
        }

        public static void RegisterShape(string name, Shape shape)
        {
            if (_registriesLoaded)
                throw new AccessViolationException($"Attempt to register an object after Registration phase! Name: {name}");
            
            try
            {
                shape.Texture = _resourceLoader.LoadTexture(name);
                ObjectRegistry.TryAdd(name, shape);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error adding Entity {name} to object registry!");
                Console.Error.WriteLine(e);
            }
        }

        public static void SetInitialized()
        {
            if (!_registriesLoaded)
                _registriesLoaded = true;
        }

        public static Sprite GetEntityByName(string name)
        {
            try
            {
                return EntityRegistry.TryGetValue(name, out var value) ? value : null;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error retrieving Entity: {name}");
                Console.Error.WriteLine(e);
                return null;
            }
        }

        public static T GetObjectByName<T>(string name) where T : Shape, new()
        {
            try
            {
                return (T) (ObjectRegistry.TryGetValue(name, out var value) ? value : new T());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error retrieving Object: {name}");
                Console.Error.WriteLine(e);
                return null;
            }
        }
    }
}