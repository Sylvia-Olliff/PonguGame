using PonguGame.lib;
using SFML.Graphics;

namespace PonguGame.model
{
    public class Paddle : Entity<RectangleShape>
    {
        public Paddle(Sprite model, RectangleShape boundingBox) : base(Layer.Player, model, boundingBox)
        {
        }
    }
}