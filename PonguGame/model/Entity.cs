using SFML.Graphics;
using SFML.System;

namespace PonguGame.model
{
    public abstract class Entity
    {
        protected Sprite _model;
        protected Shape _boundingBox;
        protected Vector2f _velocity;

        public Entity(Sprite model, Shape boundingBox)
        {
            _model = model;
            _boundingBox = boundingBox;
        }

        public void SetVelocity(Vector2f velocity)
        {
            _velocity = velocity;
        }

        public void SetVelocity(float vx, float vy)
        {
            _velocity = new Vector2f(vx, vy);
        }

        public Vector2f getVelocity()
        {
            return _velocity;
        }
    }
}