using System.Collections.Generic;
using PonguGame.lib;
using SFML.Graphics;

namespace PonguGame.model
{
    public class Menu : Entity<RectangleShape>
    {
        private Text _title;
        private List<Option> _options;
        private bool _isOpen;
        
        public Menu(Sprite model, bool isOpen) : base(Layer.Menu, model, new RectangleShape())
        {
            _isOpen = isOpen;
        }
    }
}