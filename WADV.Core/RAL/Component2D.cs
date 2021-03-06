﻿using System;
using System.Collections.Generic;

namespace WADV.Core.RAL {
    /// <summary>
    /// Component is a structure that update sprites in a static way, like do animation, change picture, etc.
    /// </summary>
    public abstract class Component2D : IDisposable {
        protected readonly List<Sprite2D> Sprites = new List<Sprite2D>();

        /// <summary>
        /// Bind a sprite to this component
        /// </summary>
        /// <param name="target">Target sprite</param>
        /// <returns></returns>
        public bool Bind(Sprite2D target) {
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
        public bool Unbind(Sprite2D target) {
            if (!UnbindingCheck(target)) return false;
            Sprites.Remove(target);
            Unbinding(target);
            return true;
        }

        /// <summary>
        /// Unbind a sprite from this component without checking
        /// </summary>
        /// <param name="target">Target component</param>
        public void Remove(Sprite2D target) {
            Sprites.Remove(target);
            Removing(target);
        }

        /// <summary>
        /// Update all binded sprites
        /// </summary>
        public abstract void Update();

        protected abstract bool BindingCheck(Sprite2D value);
        protected abstract bool UnbindingCheck(Sprite2D value);
        protected abstract void Binding(Sprite2D value);
        protected abstract void Unbinding(Sprite2D value);
        protected abstract void Removing(Sprite2D target);

        public abstract void Dispose();
    }
}
