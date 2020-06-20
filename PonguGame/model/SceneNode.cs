using System.Collections.Generic;

namespace PonguGame.model
{
    public class SceneNode
    {
        private Dictionary<uint, SceneNode> _children = new Dictionary<uint, SceneNode>();
        private SceneNode _parent = null;

        public SceneNode()
        {
            
        }

        public void AttachChild(KeyValuePair<uint, SceneNode> child)
        {
            _children.Add(child.Key, child.Value);
        }

        public SceneNode DetachChild(uint id)
        {
            if (!_children.ContainsKey(id)) return null;
            
            var child = _children[id];
            _children.Remove(id);
            return child;
        }
    }
}