using PonguGame.lib;
using PonguGame.resources;
using SFML.Graphics;
using SFML.System;

namespace PonguGame.model
{
    public class Paddle : Entity<RectangleShape>
    {
        private Paddle _self;
        public Paddle(Sprite model, RectangleShape boundingBox) : base(Layer.Player, model, boundingBox)
        {
            _self = this;
        }

        public override void SetVelocity(Vector2f velocity)
        {
            velocity.X = 0f;
            base.SetVelocity(velocity);
        }

        public ref Paddle Init(Vector2f startingPos)
        {
            _model.Position = startingPos;
            return ref _self;
        }
        
        public override void UpdateCurrent(Time deltaTime)
        {
            var windowBorder = ResourceRegistry.GetSingleton<WorldBorder>();
            
            _model.Position += _velocity * deltaTime.AsSeconds();

            var bounds = _model.GetGlobalBounds();
            var borderBounds = windowBorder.Border.GetGlobalBounds();
            
            if (bounds.Top <=
                borderBounds.Top + windowBorder.Border.OutlineThickness)
            {
                SetVelocity(new Vector2f());
                _model.Position = new Vector2f(_model.Position.X, _model.Position.Y + borderBounds.Top + windowBorder.Border.OutlineThickness);
            }
            
            if (bounds.Top + bounds.Height >=
                borderBounds.Height - windowBorder.Border.OutlineThickness)
            {
                SetVelocity(new Vector2f());
                _model.Position = new Vector2f(_model.Position.X, _model.Position.Y - (borderBounds.Top + windowBorder.Border.OutlineThickness));
            }
        }
    }
}