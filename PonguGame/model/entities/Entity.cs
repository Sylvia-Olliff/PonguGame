using PonguGame.model.scenes;
using PonguGame.util;
using SFML.Graphics;
using SFML.System;

namespace PonguGame.model.entities
{
    public abstract class Entity<T> : SceneNode where T : Transformable, Drawable
    {
        protected Sprite _model;
        protected T _boundingBox;
        protected Vector2f _velocity;

        public Vector2f Velocity => _velocity;

        protected Entity(Layer layer, Sprite model, T boundingBox) : base(layer)
        {
            _model = model;
            _boundingBox = boundingBox;

            var modelBounds = _model.GetLocalBounds();
            _model.Origin = new Vector2f(modelBounds.Width / 2.0f, modelBounds.Height / 2.0f);
            _boundingBox.Origin = _model.Origin;
        }

        public virtual void SetVelocity(Vector2f velocity)
        {
            _velocity = velocity;
        }

        public override void DrawCurrent(RenderTarget target, RenderStates states)
        {
            _model.Draw(target, states);
        }
    }
}