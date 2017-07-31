using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WADV.Core.Utility;

namespace WADV.Core.RAL {
    public abstract class Sprite2D : RenderNode<Sprite2D> {
        private Sprite2D _parent = null;

        public Sprite2D(string name) : base(name) { }

        public Sprite2D Parent {
            get => _parent;
            set {
                _parent?.ChildrenDictionary.Remove(Name);
                value.AddChild(this);
                _parent = value;
            }
        }

        public Matrix2 Transform { get; set; } = Matrix2.IdentityMatrix;

        public abstract Rect2 OriginalDisplayArea { get; set; }

        public abstract bool Visibility { get; set; }
        
        internal void SetParent(Sprite2D target, List<Sprite2D> chain = null) {
            var list = chain ?? new List<Sprite2D> {target};
            _parent = target;
            var abortTransmit = false;
            AfterChangeParent?.Invoke(target, list.AsReadOnly(), ref abortTransmit);
            if (abortTransmit) return;
            list.Add(this);
            foreach (var e in ChildrenDictionary.Values) {
                e.SetParent(this, list);
            }
            list.Remove(list.Last());
        }
        
        public void RemoveChild(Sprite2D target) {
            ChildrenDictionary.Remove(target.Name);
            target.SetParent(null);
        }
        
        public bool AddChild(Sprite2D target) {
            target.SetParent(this);
            ChildrenDictionary.Add(target.Name, target);
            return true;
        }
        
        public event AfterChangeParentEventHandler AfterChangeParent;
        
        public delegate void AfterChangeParentEventHandler(Sprite2D sprite, ReadOnlyCollection<Sprite2D> parentList, ref bool transmiteToChildren);
    }
}
