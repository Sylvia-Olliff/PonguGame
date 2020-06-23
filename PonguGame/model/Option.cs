using System;
using PonguGame.lib;
using PonguGame.resources;
using SFML.Graphics;

namespace PonguGame.model
{
    public class Option : Entity<RectangleShape>
    {
        private readonly Text _label;
        private readonly Action _clickAction;
        
        public Option(Sprite model, string label) : base(Layer.Menu, model, new RectangleShape())
        {
            _label = new Text(label, ResourceRegistry.GetFont(Fonts.Title));
            _clickAction = DefaultClickAction();
        }

        public Option(Sprite model, string label, Action clickAction) : base(Layer.Menu, model, new RectangleShape())
        {
            _label = new Text(label, ResourceRegistry.GetFont(Fonts.Title));
            _clickAction = clickAction;
        }

        private Action DefaultClickAction()
        {
            return () =>
            {
                Console.Out.WriteLine($"Button {_label} was clicked!");                
            };
        }

        public void OnClick()
        {
            _clickAction();
        }
    }
}