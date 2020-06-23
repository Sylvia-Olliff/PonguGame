﻿using System;
using System.Collections.Generic;
using PonguGame.lib;
using PonguGame.resources;
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

        private void RegisterSingletons()
        {
            ResourceRegistry.RegisterSingleton(new ScoreBoard(ref _window));
            ResourceRegistry.RegisterSingleton(new WorldBorder(Layer.Background, new Vector2f(_worldBounds.Left + 10f, _worldBounds.Height - 10f), Color.Black, Color.White));
        }
        
        private void BuildScene()
        {
            // First Initialization pass
            foreach (var layer in (Layer[]) Enum.GetValues(typeof(Layer)))
            {
                _sceneMatrix.Add(layer, new SceneNode(layer));
            }

            // Add Game border (don't need to keep a reference to this as it never changes or updates)
            _sceneMatrix[Layer.Background].AttachChild(ResourceRegistry.GetSingleton<WorldBorder>());
            
            // Add Score board
            _sceneMatrix[Layer.Background].AttachChild(ResourceRegistry.GetSingleton<ScoreBoard>());
        }

        public void Update(Time deltaTime)
        {
            
        }

        public void Draw()
        {
            
        }
    }
}