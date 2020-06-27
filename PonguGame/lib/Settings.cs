using SFML.Graphics;

namespace PonguGame.lib
{
    public static class Settings
    {
        // Paddles
        public const float PADDLE_WIDTH = 10f;
        public const float PADDLE_HEIGHT = 50f;
        
        // Balls
        public const float BALL_RADIUS = 8f;
        
        // Game Window
        public const uint DEFAULT_WIDTH = 800;
        public const uint DEFAULT_HEIGHT = 600;
        public const string DEFAULT_TITLE = "Pongu Game";
        public static readonly Color DEFAULT_CLEAR_COLOR = Color.Black;
    }
}