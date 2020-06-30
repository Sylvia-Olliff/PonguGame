using System;
using System.Collections.Generic;
using PonguGame.model.entities;
using PonguGame.util;
using PonguGame.resources;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PonguGame.model.scenes
{
    public class World
    {
        private RenderWindow _window;
        private View _worldView;
        private Dictionary<Layer, SceneNode> _sceneMatrix;
        private FloatRect _worldBounds;
        private Vector2f _spawnPosition;
        
        private Paddle _player;
        private Paddle _opponent;

        public World(ref RenderWindow window)
        {
            _sceneMatrix = new Dictionary<Layer, SceneNode>();
            
            _window = window;
            _worldView = new View(_window.DefaultView);
            _worldBounds = new FloatRect(0f, 0f, _worldView.Size.X, _worldView.Size.Y);
            _spawnPosition = new Vector2f((_worldBounds.Width / 50f) * 2f, _worldBounds.Height / 2f);

            _player = ResourceRegistry.GetEntity<Paddle>(Entites.PlayerPaddle);
            _opponent = ResourceRegistry.GetEntity<Paddle>(Entites.OpponentPaddle);
            BuildScene();
        }
        
        private void BuildScene()
        {
            // First Initialization pass
            foreach (var layer in (Layer[]) Enum.GetValues(typeof(Layer)))
                _sceneMatrix.Add(layer, new SceneNode(layer));

            // Add Game border
            _sceneMatrix[Layer.Background]
                .AttachChild(ref
                    ResourceRegistry.GetSingleton<WorldBorder>()
                        .Init(ref _window, new Vector2f(_worldBounds.Width - 20f, _worldBounds.Height - 20f), Color.Black,
                            Color.White)
                )
                // Add Scoreboard - sub to game border
                .AttachChild(ref
                    ResourceRegistry.GetSingleton<ScoreBoard>()
                        .Init(ref _window)
                );

            // Add Player paddle
            _sceneMatrix[Layer.Player]
                .AttachChild(ref 
                    ResourceRegistry.GetEntity<Paddle>(Entites.PlayerPaddle)
                        .Init(_spawnPosition)
                );
            
            // Add opponent paddle
            _sceneMatrix[Layer.Player]
                .AttachChild(ref 
                    ResourceRegistry.GetEntity<Paddle>(Entites.OpponentPaddle)
                        .Init(new Vector2f((_worldBounds.Width / 50f) * 48f, _spawnPosition.Y))
                );
            
            // Add ball entity
            _sceneMatrix[Layer.Player]
                .AttachChild(ref 
                    ResourceRegistry.GetEntity<Ball>(Entites.GameBall)
                        .Init()
                );
            
            Mouse.SetPosition((Vector2i) _worldView.Center, _window);
        }

        public void Update(Time deltaTime)
        {

            var windowCenter = (Vector2i) (_window.Size / 2u);
            var mousePos = Mouse.GetPosition(_window);
            var delta = (Vector2f) (windowCenter - mousePos);
            Mouse.SetPosition(windowCenter, _window);
            delta.X = 0;
            delta.Y *= 3f;
            _player.SetVelocity(delta);
            
            _opponent.Logic();
            _sceneMatrix[Layer.Background].Update(deltaTime);
            _sceneMatrix[Layer.Player].Update(deltaTime);
            _sceneMatrix[Layer.Effects].Update(deltaTime);
            _sceneMatrix[Layer.Menu].Update(deltaTime);
        }

        public void Draw()
        {
            _window.SetView(_worldView);
            _window.Draw(_sceneMatrix[Layer.Background]);
            _window.Draw(_sceneMatrix[Layer.Player]);
            _window.Draw(_sceneMatrix[Layer.Effects]);
            _window.Draw(_sceneMatrix[Layer.Menu]);
        }
    }
}