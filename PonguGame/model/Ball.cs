using PonguGame.lib;
using SFML.Graphics;

namespace PonguGame.model
{
    public class Ball : Entity<CircleShape>
    {
        public Ball(Sprite model, CircleShape boundingBox) : base(Layer.Player, model, boundingBox)
        {
        }
    }
}