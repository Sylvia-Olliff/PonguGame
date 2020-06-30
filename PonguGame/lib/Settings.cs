using SFML.Graphics;

namespace PonguGame.util
{
    public static class Settings
    {
        // Game Window
        public const uint DEFAULT_WIDTH = 800;
        public const uint DEFAULT_HEIGHT = 600;
        public const string DEFAULT_TITLE = "Pongu Game";
        public static readonly Color DEFAULT_CLEAR_COLOR = Color.Black;
        
        // Opponent settings
        public const float PADDLE_SPEED = 225f;
        
        // Ball Settings
        public const float BALL_SPEED_INCREASE = 1.002f;
    }
}