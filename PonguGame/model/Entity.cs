using SFML.Graphics;
using SFML.System;

namespace PonguGame.model
{
    public abstract class Entity : SceneNode
    {
        protected Sprite _model;
        protected Shape _boundingBox;
        protected Vector2f _velocity;

        public Entity(Sprite model, Shape boundingBox)
        {
            _model = model;
            _boundingBox = boundingBox;

            var modelBounds = _model.GetLocalBounds();
            _model.Origin = new Vector2f(modelBounds.Width / 2.0f, modelBounds.Height / 2.0f);
        }

        public void SetVelocity(Vector2f velocity)
        {
            _velocity = velocity;
        }

        public override void UpdateCurrent(Time deltaTime)
        {
            // Avoids recalculating position when not moving, and removes jitter from tiny velocity.
            if ((int) _velocity.X == 0 && (int) _velocity.Y == 0)
                return;
            
            // Move the bounding box first, just in case
            _boundingBox.Position += _velocity * deltaTime.AsSeconds();
            _model.Position += _velocity * deltaTime.AsSeconds();
        }
    }
}