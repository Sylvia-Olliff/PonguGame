using PonguGame.lib;
using SFML.Graphics;
using SFML.System;

namespace PonguGame.model
{
    public class WorldBorder : SceneNode
    {
        private readonly RectangleShape _border;

        public WorldBorder(Layer layer, Vector2f size, Color containerColor, Color borderColor) : base(layer)
        {
            _border = new RectangleShape(size)
            {
                FillColor = containerColor, 
                OutlineColor = borderColor, 
                OutlineThickness = 0.3f
            };
        }

        public WorldBorder(Layer layer, Vector2f size, Color containerColor, Color borderColor, float thickness) : base (layer)
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