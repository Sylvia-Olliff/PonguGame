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
        private const string RESOURCE_PATH_FONTS = "fonts/";

        private Uri _gameDirectoryRoot;
        private Uri _gameDirectoryTextures;
        private Uri _gameDirectoryComponents;
        private Uri _gameDirectoryFonts;

        public Uri GameDirectoryRoot => _gameDirectoryRoot;

        public Uri GameDirectoryTextures => _gameDirectoryTextures;

        public Uri GameDirectoryComponents => _gameDirectoryComponents;

        public Uri GameDirectoryFonts => _gameDirectoryFonts;

        public ResourceLoader(Uri pathToRoot)
        {
            _gameDirectoryRoot = new Uri(Path.Combine(pathToRoot.LocalPath, RESOURCE_PATH_ROOT));
            _gameDirectoryComponents = new Uri(Path.Combine(_gameDirectoryRoot.LocalPath, RESOURCE_PATH_COMPONENTS));
            _gameDirectoryTextures = new Uri(Path.Combine(_gameDirectoryRoot.LocalPath, RESOURCE_PATH_TEXTURES));
            _gameDirectoryFonts = new Uri(Path.Combine(_gameDirectoryFonts.LocalPath, RESOURCE_PATH_FONTS));
        }

        public Texture LoadTexture(string name)
        {
            return new Texture(Path.Combine(GameDirectoryTextures.LocalPath, $"{name}.png"));
        }

        public Font LoadFont(string name)
        {
            return new Font(Path.Combine(GameDirectoryFonts.LocalPath, $"{name}.ttf"));
        }
    }
}