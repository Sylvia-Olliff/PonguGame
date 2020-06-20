using System.Collections.Generic;
using SFML.Graphics;

namespace PonguGame.model
{
    public class Menu : Entity
    {
        private Text _title;
        private List<Option> _options;
        private bool _isOpen;
        
        public Menu(Sprite model, bool isOpen) : base(model, new RectangleShape())
        {
            _isOpen = isOpen;
        }
    }
}