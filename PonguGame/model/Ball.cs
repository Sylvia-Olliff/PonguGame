using PonguGame.lib;
using PonguGame.resources;
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

        public override void UpdateCurrent(Time deltaTime)
        {
            var windowBorder = ResourceRegistry.GetSingleton<WorldBorder>();
            
            
        }
    }
}