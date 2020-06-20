using System.Collections.Generic;
using SFML.Graphics;

namespace PonguGame.model
{
    public class SceneNode : Transformable, Drawable
    {
        private Dictionary<uint, SceneNode> _children = new Dictionary<uint, SceneNode>();
        private SceneNode _parent = null;

        public SceneNode()
        {
            
        }

        public void AttachChild(KeyValuePair<uint, SceneNode> child)
        {
            child.Value._parent = this;
            _children.Add(child.Key, child.Value);
        }

        public SceneNode DetachChild(uint id)
        {
            if (!_children.ContainsKey(id)) return null;
            
            var child = _children[id];
            _children.Remove(id);
            return child;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            DrawCurrent(target, states);
            foreach (var child in _children)
            {
                child.Value.Draw(target, states);
            }
        }

        public virtual void DrawCurrent(RenderTarget target, RenderStates states)
        {
            
        }
    }
}