using System;
using PonguGame.model.scenes;
using PonguGame.util;
using PonguGame.resources;
using SFML.Graphics;
using SFML.System;

namespace PonguGame.model.entities
{
    public class Ball : Entity<CircleShape>
    {
        private Ball _self;
        private bool _shouldDraw;
        private float _ballSpawnCountdown;

        private float _ballSpawnMinX;
        private float _ballSpawnMaxX;
        private float _ballSpawnMinY;
        private float _ballSpawnMaxY;
        
        private int _ballSpawnVelocityMin;
        private int _ballSpawnVelocityMax;

        public CircleShape BbModel => _boundingBox;
        public bool IsEnabled => _shouldDraw;

        public Ball(Sprite model, CircleShape boundingBox) : base(Layer.Player, model, boundingBox)
        {
            _self = this;
            _shouldDraw = false;
            _ballSpawnCountdown = 3f;
            _boundingBox.FillColor = Color.White;
            _boundingBox.OutlineColor = Color.White;
        }

        public ref Ball Init()
        {
            var windowBorder = ResourceRegistry.GetSingleton<WorldBorder>();
            var intervalX = windowBorder.Border.GetGlobalBounds().Width / 10f;
            var intervalY = windowBorder.Border.GetGlobalBounds().Height / 10f;
            var centerX = windowBorder.Border.GetGlobalBounds().Width / 2f;
            var centerY = windowBorder.Border.GetGlobalBounds().Height / 2f;

            _ballSpawnMinX = centerX - intervalX;
            _ballSpawnMaxX = centerX + intervalX;
            _ballSpawnMinY = centerY - (intervalY * 3f);
            _ballSpawnMaxY = centerY + (intervalY * 3f);

            _ballSpawnVelocityMin = 250;
            _ballSpawnVelocityMax = 800;
            
            ResetBall();
            return ref _self;
        }

        public override void UpdateCurrent(Time deltaTime)
        {
            if (!_shouldDraw)
            {
                ResetBall(deltaTime);
                return;
            }

            _velocity *= Settings.BALL_SPEED_INCREASE;
            
            var windowBorder = ResourceRegistry.GetSingleton<WorldBorder>();
            var destination = _boundingBox.Position + _velocity * deltaTime.AsSeconds();
            
            var borderBounds = windowBorder.Border.GetGlobalBounds();

            if (CheckWallBounce(destination, ref windowBorder, borderBounds))
            {
                _model.Position += _velocity * deltaTime.AsSeconds();
                _boundingBox.Position += _velocity * deltaTime.AsSeconds();
                return;
            }

            if (CheckScore(destination, ref windowBorder, borderBounds))
            {
                ResetBall();
                return;
            }

            CheckPaddleBounce(deltaTime);
            _model.Position += _velocity * deltaTime.AsSeconds();
            _boundingBox.Position += _velocity * deltaTime.AsSeconds();
        }

        public override void DrawCurrent(RenderTarget target, RenderStates states)
        {
            if (_shouldDraw)
                _boundingBox.Draw(target, states);
        }

        private bool CheckWallBounce(Vector2f destination, ref WorldBorder windowBorder, FloatRect borderBounds)
        {
            if (!(destination.Y - (_boundingBox.GetGlobalBounds().Height / 2f) <=
                  borderBounds.Top + windowBorder.Border.OutlineThickness) &&
                !(destination.Y + (_boundingBox.GetGlobalBounds().Height / 2f) >=
                  borderBounds.Height - windowBorder.Border.OutlineThickness)) return false;
            
            SetVelocity(new Vector2f(_velocity.X, -_velocity.Y)); // invert Y axis velocity
            return true;
        }

        private bool CheckScore(Vector2f destination, ref WorldBorder windowBorder, FloatRect borderBounds)
        {
            if (destination.X - (_boundingBox.GetGlobalBounds().Width / 2f) <=
                borderBounds.Left + windowBorder.Border.OutlineThickness)
            {
                SetVelocity(new Vector2f());
                ResourceRegistry.GetSingleton<ScoreBoard>().ScorePoint(false);
                return true;
            }
            
            if (destination.X + (_boundingBox.GetGlobalBounds().Width / 2f) >=
                borderBounds.Width - windowBorder.Border.OutlineThickness)
            {
                SetVelocity(new Vector2f());
                ResourceRegistry.GetSingleton<ScoreBoard>().ScorePoint(true);
                return true;
            }

            return false;
        }

        private void CheckPaddleBounce(Time deltaTime)
        {
            var player = ResourceRegistry.GetEntity<Paddle>(Entites.PlayerPaddle);
            var opponent = ResourceRegistry.GetEntity<Paddle>(Entites.OpponentPaddle);

            SetVelocity(player.BallBounce(_boundingBox, _velocity, deltaTime));
            SetVelocity(opponent.BallBounce(_boundingBox, _velocity, deltaTime));
        }

        private void ResetBall()
        {
            _ballSpawnCountdown = 3f;
            _shouldDraw = false;
            SetVelocity(new Vector2f());
        }

        private void ResetBall(Time deltaTime)
        {
            _ballSpawnCountdown -= deltaTime.AsSeconds();
            if (!(_ballSpawnCountdown <= 0f)) return;
            
            _shouldDraw = true;
            var rand = new Random();
            _boundingBox.Position = new Vector2f(rand.Next((int) _ballSpawnMinX, (int) _ballSpawnMaxX), rand.Next((int) _ballSpawnMinY, (int) _ballSpawnMaxY));
            _model.Position = _boundingBox.Position;
            if (!ResourceRegistry.GetSingleton<ScoreBoard>().PlayerWasLastToScore)
            {
                var coinFlip = rand.Next(0, 100);
                    
                SetVelocity(coinFlip >= 50
                    ? new Vector2f(-rand.Next(_ballSpawnVelocityMin, _ballSpawnVelocityMax) / 10f,
                        -rand.Next(_ballSpawnVelocityMin, _ballSpawnVelocityMax) / 10f)
                    : new Vector2f(-rand.Next(_ballSpawnVelocityMin, _ballSpawnVelocityMax) / 10f,
                        rand.Next(_ballSpawnVelocityMin, _ballSpawnVelocityMax) / 10f));
            }
            else
            {
                var coinFlip = rand.Next(0, 100);
                    
                SetVelocity(coinFlip >= 50
                    ? new Vector2f(rand.Next(_ballSpawnVelocityMin, _ballSpawnVelocityMax) / 10f,
                        -rand.Next(_ballSpawnVelocityMin, _ballSpawnVelocityMax) / 10f)
                    : new Vector2f(rand.Next(_ballSpawnVelocityMin, _ballSpawnVelocityMax) / 10f,
                        rand.Next(_ballSpawnVelocityMin, _ballSpawnVelocityMax) / 10f));
            }

            _ballSpawnCountdown = 3f;
        }
    }
}