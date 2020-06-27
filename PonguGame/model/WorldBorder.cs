using System.Runtime.CompilerServices;
using PonguGame.lib;
using SFML.Graphics;
using SFML.System;

namespace PonguGame.model
{
    public class WorldBorder : SceneNode
    {
        private RectangleShape _border;
        private WorldBorder _self;

        public RectangleShape Border => _border;

        public WorldBorder() : base(Layer.Background)
        {
            _self = this;
        }

        public ref WorldBorder Init(ref RenderWindow window, Vector2f size, Color containerColor, Color borderColor)
        {
            _border = new RectangleShape(size)
            {
                FillColor = containerColor, 
                OutlineColor = borderColor, 
                OutlineThickness = 5f
            };
            
            _border.Origin = new Vector2f(_border.Size.X / 2f, _border.Size.Y / 2f);
            _border.Position = window.DefaultView.Center;
            
            return ref _self;
        }
        
        public WorldBorder Init(ref RenderWindow window, Vector2f size, Color containerColor, Color borderColor, float thickness)
        {
            _border = new RectangleShape(size)
            {
                FillColor = containerColor, 
                OutlineColor = borderColor, 
                OutlineThickness = thickness,
            };
            
            _border.Origin = new Vector2f(_border.Size.X / 2f, _border.Size.Y / 2f);
            _border.Position = window.DefaultView.Center;
            
            return this;
        }

        public bool Intersects(Vector2f position)
        {
            if (position.Y <= Border.GetGlobalBounds().Top + Border.OutlineThickness)
                return true;
            if (position.Y >= Border.GetGlobalBounds().Height - Border.OutlineThickness)
                return true;
            if (position.X <= Border.GetGlobalBounds().Left + Border.OutlineThickness)
                return true;
            if (position.X >= Border.GetGlobalBounds().Width - Border.OutlineThickness)
                return true;

            return false;
        }

        public bool Intersects(Vector2f position, out FloatRect overlap)
        {
            overlap = new FloatRect();
            if (!Intersects(position))
                return false;

            if (position.Y <= Border.GetGlobalBounds().Top + Border.OutlineThickness)
            {
                overlap.Top = position.Y;
                overlap.Height = position.Y + (Border.GetGlobalBounds().Top + Border.OutlineThickness);
            }
            if (position.Y >= Border.GetGlobalBounds().Height - Border.OutlineThickness)
            {
                overlap.Top = position.Y - (Border.GetGlobalBounds().Height - Border.OutlineThickness);
                overlap.Height = position.Y;
            }
            if (position.X <= Border.GetGlobalBounds().Left + Border.OutlineThickness)
            {
                overlap.Left = position.X;
                overlap.Width = position.X + (Border.GetGlobalBounds().Left + Border.OutlineThickness);
            }
            if (position.X >= Border.GetGlobalBounds().Width - Border.OutlineThickness)
            {
                overlap.Left = position.X - (Border.GetGlobalBounds().Width - Border.OutlineThickness);
                overlap.Width = position.X;
            }

            return true;
        }

        public override void DrawCurrent(RenderTarget target, RenderStates states)
        {
            _border.Draw(target, states);
        }
    }
}