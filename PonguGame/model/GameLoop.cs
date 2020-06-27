using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PonguGame.model
{
    public abstract class GameLoop
    {
        public const int TARGET_FPS = 60;
        public const float TIME_UNTIL_UPDATE = 1f / TARGET_FPS;

        public RenderWindow Window;

        public GameTime GameTime
        {
            get;
            protected set;
        }

        public Color WindowClearColor
        {
            get;
            protected set;
        }

        protected GameLoop(uint windowWidth, uint windowHeight, string windowTitle, Color windowClearColor)
        {
            WindowClearColor = windowClearColor;
            Window = new RenderWindow(new VideoMode(windowWidth, windowHeight), windowTitle);
            GameTime = new GameTime();
            Window.Closed += WindowOnClosed;
        }


        public void Run()
        {
            LoadContent();
            Initialize();

            var totalTimeBeforeUpdate = 0f;
            var previousTimeElapsed = 0f;
            var deltaTime = 0f;
            var totalTimeElapsed = 0f;
            
            var clock = new Clock();

            while (Window.IsOpen)
            {
                Window.DispatchEvents();

                totalTimeElapsed = clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previousTimeElapsed;
                previousTimeElapsed = totalTimeElapsed;

                totalTimeBeforeUpdate += deltaTime;

                if (!(totalTimeBeforeUpdate >= TIME_UNTIL_UPDATE)) continue;
                
                GameTime.Update(totalTimeBeforeUpdate, clock.ElapsedTime.AsSeconds());
                totalTimeBeforeUpdate= 0f;
                Update(GameTime);
                    
                Window.Clear(WindowClearColor);
                Draw(GameTime);
                Window.Display();
            }
        }

        public abstract void LoadContent();
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        
        private void WindowOnClosed(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}
