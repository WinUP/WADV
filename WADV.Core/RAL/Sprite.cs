using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WADV.Core.Utility;

namespace WADV.Core.RAL {
    public abstract class Sprite : RenderNode<Sprite> {
        protected Sprite(string name) : base(name) {
            Components = new ComponentList(this);
        }

        public ComponentList Components { get; }
        public Scene Scene { get; internal set; }
        public Sprite Parent { get; private set; }
        public abstract Rect2 DisplayArea { get; set; }
        public abstract bool Visibility { get; set; }

        internal bool SetParent(Sprite target, List<Sprite> chain = null) {
            var list = chain;
            if (list == null) {
                var delegates = BeforeChangeParent?.GetInvocationList();
                if (delegates != null &&
                    delegates.Cast<BeforeChangeParentEventHandler>().Any(e => !e.Invoke(target, this))) {
                    target.BeforeChangeParentRollback?.Invoke(target, this);
                    return false;
                }
                Parent = target;
                list = new List<Sprite> {target};
            }
            var abortTransmit = true;
            AfterChangeParent?.Invoke(target, list.AsReadOnly(), ref abortTransmit);
            if (!abortTransmit) return true;
            list.Add(this);
            foreach (var e in ChildrenDictionary.Values) {
                e.SetParent(this, list);
            }
            list.Remove(list.Last());
            return true;
        }

        public void RemoveChild(Sprite target) {
            ChildrenDictionary.Remove(target.Name);
            target.SetParent(null);
        }

        public bool AddChild(Sprite target) {
            var answer = target.SetParent(this);
            if (!answer) return false;
            ChildrenDictionary.Add(target.Name, target);
            return true;
        }

        public bool ChangeParent(Sprite newParent) {
            var answer = SetParent(newParent);
            if (!answer) return false;
            Parent.ChildrenDictionary.Remove(Name);
            newParent.ChildrenDictionary.Add(Name, this);
            return true;
        }

        public event BeforeChangeParentEventHandler BeforeChangeParent;

        public delegate bool BeforeChangeParentEventHandler(Sprite sprite, Sprite newParent);

        //BeforeChangeParentRollback will be called if one of handlers of BeforeChangeParent returned false
        public event BeforeChangeParentRollbackEventHandler BeforeChangeParentRollback;

        public delegate void BeforeChangeParentRollbackEventHandler(Sprite sprite, Sprite newParent);

        //AfterCangeParent has 2 situations to be called:
        //1. After this sprite finished its change parent
        //2. After one of this sprite's child or its child's child or... finished its change parent
        public event AfterChangeParentEventHandler AfterChangeParent;

        public delegate void AfterChangeParentEventHandler(
            Sprite sprite, ReadOnlyCollection<Sprite> parentList, ref bool transmiteToChildren);

    }
}