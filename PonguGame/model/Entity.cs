using PonguGame.lib;
using SFML.Graphics;
using SFML.System;

namespace PonguGame.model
{
    public abstract class Entity<T> : SceneNode where T : Transformable, Drawable
    {
        protected Sprite _model;
        protected T _boundingBox;
        protected Vector2f _velocity;

        public Entity(Layer layer, Sprite model, T boundingBox) : base(layer)
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
            // Move the bounding box first, just in case
            _boundingBox.Position += _velocity * deltaTime.AsSeconds();
            _model.Position += _velocity * deltaTime.AsSeconds();
        }
    }
}