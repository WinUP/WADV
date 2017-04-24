using System;
using System.Collections.Generic;

namespace WADV.Core.RAL {
    /// <summary>
    /// Component is a structure that update sprites in a static way, like do animation, change picture, etc.
    /// </summary>
    public abstract class Component : IDisposable {
        protected readonly List<Sprite> Sprites = new List<Sprite>();

        /// <summary>
        /// Bind a sprite to this component
        /// </summary>
        /// <param name="target">Target sprite</param>
        /// <returns></returns>
        public bool Bind(Sprite target) {
            if (!BindingCheck(target)) return false;
            Sprites.Add(target);
            Binding(target);
            return true;
        }

        /// <summary>
        /// Unbind a sprite from this component
        /// </summary>
        /// <param name="target">Target sprite</param>
        /// <returns></returns>
        public bool Unbind(Sprite target) {
            if (!UnbindingCkeck(target)) return false;
            Sprites.Remove(target);
            Unbinding(target);
            return true;
        }

        /// <summary>
        /// Unbind a sprite from this component without checking
        /// </summary>
        /// <param name="target">Target component</param>
        public void Remove(Sprite target) {
            Sprites.Remove(target);
            Removing(target);
        }

        /// <summary>
        /// Update all binded sprites
        /// </summary>
        public abstract void Update();

        protected abstract bool BindingCheck(Sprite value);
        protected abstract bool UnbindingCkeck(Sprite value);
        protected abstract void Binding(Sprite value);
        protected abstract void Unbinding(Sprite value);
        protected abstract void Removing(Sprite target);

        public abstract void Dispose();
    }
}