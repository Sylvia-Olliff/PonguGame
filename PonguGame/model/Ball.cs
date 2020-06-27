using PonguGame.lib;
using SFML.Graphics;
using SFML.System;

namespace PonguGame.model
{
    public class Ball : Entity<CircleShape>
    {
        private Ball _self;
        public Ball(Sprite model, CircleShape boundingBox) : base(Layer.Player, model, boundingBox)
        {
            _self = this;
        }

        public ref Ball Init(Vector2f startingPos, Vector2f startingVelocity)
        {
            _model.Position = startingPos;
            _velocity = startingVelocity;
            return ref _self;
        } 
    }
}