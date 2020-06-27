using System;
using PonguGame.lib;
using PonguGame.model;
using PonguGame.resources;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PonguGame.game
{
    public class Pongu : GameLoop
    {
        private World _world = null;
        
        public Pongu(uint windowWidth, uint windowHeight, string windowTitle, Color windowClearColor) : base(windowWidth, windowHeight, windowTitle, windowClearColor)
        {
        }

        public Pongu() : base(Settings.DEFAULT_WIDTH, Settings.DEFAULT_HEIGHT, Settings.DEFAULT_TITLE, Settings.DEFAULT_CLEAR_COLOR)
        {
        }

        private void RegisterSingletons()
        {
            ResourceRegistry.RegisterSingleton(new ScoreBoard());
            ResourceRegistry.RegisterSingleton(new WorldBorder());
        }

        private void RegisterEntities()
        {
            ResourceRegistry.RegisterEntity(Entites.PlayerPaddle, new Paddle(new Sprite(ResourceRegistry.GetTexture(Textures.Paddle)), new RectangleShape(new Vector2f(Settings.PADDLE_WIDTH, Settings.PADDLE_HEIGHT))));
            ResourceRegistry.RegisterEntity(Entites.OpponentPaddle, new Paddle(new Sprite(ResourceRegistry.GetTexture(Textures.Paddle)), new RectangleShape(new Vector2f(Settings.PADDLE_WIDTH, Settings.PADDLE_HEIGHT))));
            ResourceRegistry.RegisterEntity(Entites.GameBall, new Ball(new Sprite(ResourceRegistry.GetTexture(Textures.Ball)), new CircleShape(Settings.BALL_RADIUS)));
        }

        public override void LoadContent()
        {
            RegisterSingletons();
            RegisterEntities();
            ResourceRegistry.SetInitialized();
        }

        public override void Initialize()
        {
            //TODO: Set state values like random seeds and starting ball velocities.
            //TODO: Move the Init calls from BuildScene to here
            _world = new World(ref Window);
        }

        public override void Update(GameTime gameTime)
        {
            _world.Update(Time.FromSeconds(gameTime.DeltaTime));
        }

        public override void Draw(GameTime gameTime)
        {
            _world.Draw();
        }
    }
}