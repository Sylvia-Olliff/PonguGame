using System;
using System.Collections.Generic;
using PonguGame.lib;
using SFML.Graphics;
using SFML.System;

namespace PonguGame.model
{
    public class World
    {
        private RenderWindow _window;
        private View _worldView;
        private Dictionary<Layer, SceneNode> _sceneMatrix;
        private FloatRect _worldBounds;
        private Vector2f _spawnPosition;
        
        private Paddle _player;

        public World(ref RenderWindow window)
        {
            _sceneMatrix = new Dictionary<Layer, SceneNode>();
            
            _window = window;
            _worldView = new View(_window.DefaultView);
            _worldBounds = new FloatRect(0f, 0f, _worldView.Size.X, _worldView.Size.Y);
            _spawnPosition = new Vector2f(20f, _worldBounds.Height / 2f);
            
            BuildScene();
        }
        
        private void BuildScene()
        {
            // First Initialization pass
            foreach (var layer in (Layer[]) Enum.GetValues(typeof(Layer)))
            {
                _sceneMatrix.Add(layer, new SceneNode());
            }

            _sceneMatrix[Layer.Background].AttachChild(new WorldBorder(new Vector2f(_worldBounds.Left + 10f, _worldBounds.Height - 10f), Color.Black, Color.White));
        }

        public void Update(Time deltaTime)
        {
            
        }

        public void Draw()
        {
            
        }
    }
}