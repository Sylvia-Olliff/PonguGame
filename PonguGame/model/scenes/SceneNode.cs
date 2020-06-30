using System.Collections.Generic;
using PonguGame.util;
using SFML.Graphics;
using SFML.System;

namespace PonguGame.model.scenes
{
    public class SceneNode : Transformable, Drawable
    {
        private List<SceneNode> _children = new List<SceneNode>();
        private SceneNode _parent;
        
        public Layer SceneLayer { get; }

        public SceneNode(Layer sceneLayer)
        {
            SceneLayer = sceneLayer;
        }
        
        public SceneNode AttachChild(SceneNode child)
        {
            child._parent = this;
            _children.Add(child);
            return child;
        }

        public T AttachChild<T>(ref T child) where T : SceneNode
        {
            child._parent = this;
            _children.Add(child);
            return child;
        }

        public SceneNode DetachChild(ref SceneNode child)
        {
            if (!_children.Contains(child))
                return null;
            
            _children.Remove(child);
            return child;
        }

        public Transform GetWorldTransform()
        {
            var transform = Transform.Identity;
            for (var node = this; node != null; node = _parent)
                transform *= node.Transform;

            return transform;
        }

        public Vector2f GetWorldPosition()
        {
            return GetWorldTransform() * new Vector2f();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            DrawCurrent(target, states);
            foreach (var child in _children)
            {
                child.Draw(target, states);
            }
        }

        public virtual void DrawCurrent(RenderTarget target, RenderStates states) {}

        public void Update(Time deltaTime)
        {
            UpdateCurrent(deltaTime);
            UpdateChildren(deltaTime);
        }

        public virtual void UpdateCurrent(Time deltaTime) {}

        public void UpdateChildren(Time deltaTime)
        {
            foreach (var child in _children)
            {
                child.Update(deltaTime);
            }
        }
    }
}