using PonguGame.game;

namespace PonguGame
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var game = new Pongu();
            game.Run();
        }
    }
}