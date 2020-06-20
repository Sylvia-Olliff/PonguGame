using SFML.Graphics;
using SFML.System;

namespace PonguGame.model
{
    public class WorldBorder : SceneNode
    {
        private readonly RectangleShape _border;

        public WorldBorder(Vector2f size, Color containerColor, Color borderColor)
        {
            _border = new RectangleShape(size)
            {
                FillColor = containerColor, 
                OutlineColor = borderColor, 
                OutlineThickness = 0.3f
            };
        }

        public WorldBorder(Vector2f size, Color containerColor, Color borderColor, float thickness)
        {
            _border = new RectangleShape(size)
            {
                FillColor = containerColor, 
                OutlineColor = borderColor, 
                OutlineThickness = thickness
            };
        }

        public override void DrawCurrent(RenderTarget target, RenderStates states)
        {
            _border.Draw(target, states);
        }
    }
}