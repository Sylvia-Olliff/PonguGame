using System;
using PonguGame.model;
using SFML.Graphics;

namespace PonguGame.game
{
    public class Pongu : GameLoop
    {
        private const uint DEFAULT_WIDTH = 800;
        private const uint DEFAULT_HEIGHT = 600;
        private const string DEFAULT_TITLE = "Pongu Game";
        private static readonly Color DEFAULT_CLEAR_COLOR = Color.Black;
        
        public Pongu(uint windowWidth, uint windowHeight, string windowTitle, Color windowClearColor) : base(windowWidth, windowHeight, windowTitle, windowClearColor)
        {
        }

        public Pongu() : base(DEFAULT_WIDTH, DEFAULT_HEIGHT, DEFAULT_TITLE, DEFAULT_CLEAR_COLOR)
        {
            
        }

        public override void LoadContent()
        {
            
        }

        public override void Initialize()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            
        }
    }
}