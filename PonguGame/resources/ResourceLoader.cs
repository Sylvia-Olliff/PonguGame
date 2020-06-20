using System;
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

        public Uri GameDirectoryTextures { get; }

        public Uri GameDirectoryComponents { get; }

        public Uri GameDirectoryFonts { get; }

        public ResourceLoader(Uri pathToRoot)
        {
            var gameDirectoryRoot = new Uri(Path.Combine(pathToRoot.LocalPath, RESOURCE_PATH_ROOT));
            GameDirectoryComponents = new Uri(Path.Combine(gameDirectoryRoot.LocalPath, RESOURCE_PATH_COMPONENTS));
            GameDirectoryTextures = new Uri(Path.Combine(gameDirectoryRoot.LocalPath, RESOURCE_PATH_TEXTURES));
            GameDirectoryFonts = new Uri(Path.Combine(gameDirectoryRoot.LocalPath, RESOURCE_PATH_FONTS));
        }

        public Texture LoadTexture(string name)
        {
            return new Texture(Path.Combine(GameDirectoryTextures.LocalPath, $"{name}.png"));
        }

        public Font LoadFont(string name)
        {
            var filePathTtf = Path.Combine(GameDirectoryFonts.LocalPath, $"{name}.TTF");
            if (File.Exists(filePathTtf))
                return new Font(filePathTtf);
            
            var filePathFon = Path.Combine(GameDirectoryFonts.LocalPath, $"{name}.fon");
            if (File.Exists(filePathFon))
                return new Font(filePathFon);
            
            throw new FileNotFoundException("Error loading font file!", name);
        }
    }
}