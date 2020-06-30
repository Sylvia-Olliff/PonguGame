using System;
using PonguGame.model.scenes;
using PonguGame.util;
using PonguGame.resources;
using SFML.Graphics;
using SFML.System;

namespace PonguGame.model.entities
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
            _boundingBox.Position = startingPos;
            return ref _self;
        }
        
        public override void UpdateCurrent(Time deltaTime)
        {
            var windowBorder = ResourceRegistry.GetSingleton<WorldBorder>();

            var destination = _boundingBox.Position + _velocity * deltaTime.AsSeconds();

            
            var borderBounds = windowBorder.Border.GetGlobalBounds();
            
            if (destination.Y - (_boundingBox.GetGlobalBounds().Height / 2f) <=
                borderBounds.Top + windowBorder.Border.OutlineThickness)
            {
                SetVelocity(new Vector2f());
            }
            
            if (destination.Y + (_boundingBox.GetGlobalBounds().Height / 2f) >=
                borderBounds.Height - windowBorder.Border.OutlineThickness)
            {
                SetVelocity(new Vector2f());
            }
            
            _model.Position += _velocity * deltaTime.AsSeconds();
            _boundingBox.Position += _velocity * deltaTime.AsSeconds();
        }

        public override void DrawCurrent(RenderTarget target, RenderStates states)
        {
            _model.Draw(target, states);
        }

        public Vector2f BallBounce(CircleShape ballBB, Vector2f currentVelocity, Time deltaTime)
        {
            var ballPosPrev = ballBB.Position;
            
            ballBB.Position += currentVelocity * deltaTime.AsSeconds();
            
            var ballLeft = ballBB.Position.X - ballBB.Radius;
            var ballRight = ballBB.Position.X + ballBB.Radius;
            var ballTop = ballBB.Position.Y - ballBB.Radius;
            var ballBottom = ballBB.Position.Y + ballBB.Radius;
            
            var bbLeft = _boundingBox.Position.X - _boundingBox.Size.X / 2f;
            var bbRight = _boundingBox.Position.X + _boundingBox.Size.X / 2f;
            var bbTop = _boundingBox.Position.Y - _boundingBox.Size.Y / 2f;
            var bbBottom = _boundingBox.Position.Y + _boundingBox.Size.Y / 2f;

            var shouldProceed = false;

            if (ballPosPrev.X >= bbRight)
            {
                shouldProceed = ballLeft <= bbRight && ballBottom >= bbTop && ballTop <= bbBottom;
            }

            if (ballPosPrev.X <= bbLeft)
            {
                shouldProceed = ballRight >= bbLeft && ballBottom >= bbTop && ballTop <= bbBottom;
            }

            if (!shouldProceed)
                return currentVelocity;

            if ((ballPosPrev.Y <= bbTop && ballPosPrev.X <= bbRight) || (ballPosPrev.Y >= bbBottom && ballPosPrev.X <= bbRight)) // Ball is coming from above or below
            {
                return new Vector2f(currentVelocity.X, -currentVelocity.Y); // Every paddle strike speeds up the ball a little
            }
            
            if (ballPosPrev.X >= bbRight || ballPosPrev.X <= bbLeft) // Ball is coming from the left or the right
            {
                return new Vector2f(-currentVelocity.X, currentVelocity.Y); // Every paddle strike speeds up the ball a little
            }

            return currentVelocity; // Shouldn't ever reach here but just in case, return the current velocity
        }

        public void Logic()
        {
            var gameBall = ResourceRegistry.GetEntity<Ball>(Entites.GameBall);

            if (!gameBall.IsEnabled)
            {
                SetVelocity(new Vector2f());
                return;
            }

            var xDist = Math.Abs(gameBall.BbModel.Position.X - _boundingBox.Position.X);
            var isAbove = gameBall.BbModel.Position.Y < _boundingBox.Position.Y;
            
            if (xDist <= 200f)
            {
                SetVelocity(
                    isAbove ? new Vector2f(0f, -Settings.PADDLE_SPEED) : new Vector2f(0f, Settings.PADDLE_SPEED));
            }
        }
    }
}