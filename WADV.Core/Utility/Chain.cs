using System;
using System.Threading;
using WADV.Core.Exception;

namespace WADV.Core.Utility {
    /// <summary>
    /// Asynchronized code chain
    /// </summary>
    public abstract class Chain {
        public Chain Parent { get; }
        public Chain Child { get; protected set; }

        protected Chain(Chain parent) {
            Parent = parent;
        }

        /// <summary>
        /// Run this chain in a new thread from its root
        /// </summary>
        public abstract void Fire();

        internal abstract void Fire(object value, GameException exception);

        /// <summary>
        /// Create a new chain
        /// </summary>
        /// <typeparam name="T">Input value's type</typeparam>
        /// <param name="value">Input value</param>
        /// <returns></returns>
        public static Chain<T, T> From<T>(T value) where T : class {
            return new Chain<T, T>(null, null, null, value);
        }
    }

    /// <summary>
    /// Asynchronized code chain
    /// </summary>
    /// <typeparam name="T">Input value's type</typeparam>
    /// <typeparam name="TU">Output value's type</typeparam>
    public class Chain<T, TU> : Chain where T : class where TU : class {
        private readonly Func<T, TU> _runner;
        private Func<GameException, TU> _catcher;
        private readonly T _initialValue;

        internal Chain(Chain parent, Func<T, TU> runner = null, Func<GameException, TU> catcher = null, T initialValue = null)
            : base(parent) {
            _runner = runner;
            _catcher = catcher;
            _initialValue = initialValue;
        }

        /// <summary>
        /// Add another chain to receive the output of this chain
        /// </summary>
        /// <typeparam name="TK">Output value's type of next chain</typeparam>
        /// <param name="method">Process function of next chain</param>
        /// <returns></returns>
        public Chain<TU, TK> Then<TK>(Func<TU, TK> method) where TK : class {
            var child = new Chain<TU, TK>(this, method);
            Child = child;
            return child;
        }

        /// <summary>
        /// Add a function to catch GameException of this chain's processor.
        /// If more than one catcher was registered to this chain, only the last one will be run.
        /// </summary>
        /// <param name="method">Catcher function</param>
        /// <returns></returns>
        public Chain<T, TU> Catch(Func<GameException, TU> method) {
            _catcher = method;
            return this;
        }

        /// <summary>
        /// Get the root chain of this chain
        /// </summary>
        /// <returns></returns>
        public Chain Root() {
            if (Parent == null) return this;
            var root = Parent;
            while (root.Parent != null) root = root.Parent;
            return root;
        }

        /// <summary>
        /// Run this chain in a new thred from its root
        /// </summary>
        public override void Fire() {
            new Thread(() => {
                Root().Child?.Fire(_initialValue, null);
            }) { Name = $"Chain {Guid.NewGuid().ToString().ToUpper()}" }
            .Start();
        }

        internal override void Fire(object value, GameException exception) {
            if (exception != null) {
                if (_catcher == null)
                    Child?.Fire(value, exception);
                else
                    Child?.Fire(_catcher.Invoke(exception), null);
            } else {
                TU result = null;
                GameException ex = null;
                try {
                    result = _runner.Invoke((T)value);
                } catch (GameException e) {
                    ex = e;
                }
                if (ex != null) {
                    if (_catcher != null) {
                        result = _catcher.Invoke(ex);
                        ex = null;
                    }
                }
                Child?.Fire(result, ex);
            }
        }
    }
}