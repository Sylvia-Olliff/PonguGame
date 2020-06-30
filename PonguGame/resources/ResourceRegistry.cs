using System;
using System.Collections.Concurrent;
using PonguGame.util;
using PonguGame.model.entities;
using PonguGame.model.scenes;
using SFML.Graphics;

namespace PonguGame.resources
{
    public static class ResourceRegistry
    {
        private static bool _registriesLoaded = false;
        private static readonly ConcurrentDictionary<Entites, IRegistryItem> EntityRegistry = new ConcurrentDictionary<Entites, IRegistryItem>();
        private static readonly ConcurrentDictionary<string, Texture> TextureRegistry = new ConcurrentDictionary<string, Texture>();
        private static readonly ConcurrentDictionary<string, Font> FontRegistry = new ConcurrentDictionary<string, Font>();
        private static readonly ConcurrentDictionary<Type, IRegistryItem> SingletonRegistry = new ConcurrentDictionary<Type, IRegistryItem>();
        
        private static readonly ResourceLoader Loader = new ResourceLoader(new Uri(Environment.CurrentDirectory));

        static ResourceRegistry()
        {
            foreach (var fontType in (Fonts[]) Enum.GetValues(typeof(Fonts)))
            {
                var fontIdStr = fontType.ToDescriptionString();
                FontRegistry.TryAdd(fontIdStr, Loader.LoadFont(fontIdStr));
            }

            foreach (var texture in (Textures[]) Enum.GetValues(typeof(Textures)))
            {
                var textureFileName = texture.ToDescriptionString();
                TextureRegistry.TryAdd(textureFileName, Loader.LoadTexture(textureFileName));
            }
        }
        
        public static bool RegistriesLoaded => _registriesLoaded;

        private static void CheckRegistryLoadedStatus()
        {
            if (!_registriesLoaded)
                throw new AccessViolationException($"Attempt to access registry before Registration phase is complete!");
        }

        public static void RegisterEntity<T>(Entites entity, T instance) where T : SceneNode
        {
            if (_registriesLoaded)
                throw new AccessViolationException($"Attempt to register an entity after Registration phase! Name: {entity}");
            
            try
            {
                EntityRegistry.AddOrUpdate(entity, s => new RegistryObject<T>(instance),
                    (s, item) => new RegistryObject<T>(instance));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error adding Entity {entity} to entity registry!");
                Console.Error.WriteLine(e);
            }
        }

        public static void RegisterSingleton<T>(T instance) where T : SceneNode
        {
            if (_registriesLoaded)
                throw new AccessViolationException($"Attempt to register an object after Registration phase! Name: {typeof(T)}");

            try
            {
                SingletonRegistry.AddOrUpdate(typeof(T), type => new RegistryObject<T>(instance), (type, item) => new RegistryObject<T>(instance));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error registering Singleton {typeof(T)}");
                Console.Error.WriteLine(e);
            }
        }

        public static void SetInitialized()
        {
            if (!_registriesLoaded)
                _registriesLoaded = true;
        }

        public static ref T GetEntity<T>(Entites entity) where T : SceneNode
        {
            try
            {
                CheckRegistryLoadedStatus();
                EntityRegistry.TryGetValue(entity, out var value);
                
                if (value == null)
                    throw new MissingMemberException($"Entity id: {entity} isn't registered!'");

                return ref ((RegistryObject<T>) value).Get();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error retrieving Entity: {entity}");
                Console.Error.WriteLine(e);
                throw;
            }
        }

        public static Texture GetTexture(Textures texture)
        {
            try
            {
                return TextureRegistry.TryGetValue(texture.ToDescriptionString(), out var value) ? value : null;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error retrieving Texture: {texture.ToDescriptionString()}");
                Console.Error.WriteLine(e);
                return null;
            }
        }

        public static ref T GetSingleton<T>() where T : SceneNode
        {
            try
            {
                CheckRegistryLoadedStatus();
                SingletonRegistry.TryGetValue(typeof(T), out var value);
                
                if (value == null)
                    throw new MissingMemberException($"Singleton {typeof(T)} not registered!");
                
                return ref ((RegistryObject<T>) value).Get();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error Retrieving singleton: {typeof(T)}");
                throw e;
            }
        }

        public static Font GetFont(Fonts fontType)
        {
            FontRegistry.TryGetValue(fontType.ToDescriptionString(), out var value);
            return value;
        }
    }
}