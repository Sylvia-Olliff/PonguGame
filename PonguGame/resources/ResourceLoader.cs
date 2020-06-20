using System;
using System.Collections.Generic;
using System.IO;
using SFML.Graphics;

namespace PonguGame.resources
{
    public class ResourceLoader
    {
        private const string RESOURCE_PATH_ROOT = "assets/";
        private const string RESOURCE_PATH_TEXTURES = "textures/";
        private const string RESOURCE_PATH_COMPONENTS = "components/";

        private Uri _gameDirectoryRoot;
        private Uri _gameDirectoryTextures;
        private Uri _gameDirectoryComponents;

        public Uri GameDirectoryRoot => _gameDirectoryRoot;

        public Uri GameDirectoryTextures => _gameDirectoryTextures;

        public Uri GameDirectoryComponents => _gameDirectoryComponents;

        public ResourceLoader(Uri pathToRoot)
        {
            _gameDirectoryRoot = new Uri(Path.Combine(pathToRoot.LocalPath, RESOURCE_PATH_ROOT));
            _gameDirectoryComponents = new Uri(Path.Combine(_gameDirectoryRoot.LocalPath, RESOURCE_PATH_COMPONENTS));
            _gameDirectoryTextures = new Uri(Path.Combine(_gameDirectoryRoot.LocalPath, RESOURCE_PATH_TEXTURES));
        }

        public Texture LoadTexture(string name)
        {
            return new Texture(Path.Combine(_gameDirectoryTextures.LocalPath, $"{name}.png"));
        }
    }
}