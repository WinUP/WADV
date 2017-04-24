using System;
using System.Threading;
using WADV.Core.Exception;

namespace WADV.Core.Utility {
    public abstract class Chain {
        public virtual Chain Parent { get; }
        public Chain Child { get; protected set; }

        protected Chain(Chain parent) {
            Parent = parent;
        }

        public abstract void Fire();

        internal abstract void Fire(object value, GameException exception);

        public static Chain<T, T> From<T>(T value) where T : class {
            return new Chain<T, T>(null, null, null, value);
        }
    }

    public class Chain<T, TU> : Chain where T : class where TU : class {
        private readonly Func<T, TU> _runner = null;
        private Func<GameException, TU> _catcher = null;
        private readonly T _initialValue;

        internal Chain(Chain parent, Func<T, TU> runner = null, Func<GameException, TU> catcher = null, T initialValue = null)
            : base(parent) {
            _runner = runner;
            _catcher = catcher;
            _initialValue = initialValue;
        }

        public Chain<TU, TK> Then<TK>(Func<TU, TK> method) where TK : class {
            var child = new Chain<TU, TK>(this, method);
            Child = child;
            return child;
        }

        public Chain<T, TU> Catch(Func<GameException, TU> method) {
            _catcher = method;
            return this;
        }

        public Chain Root() {
            if (Parent == null) return this;
            var root = Parent;
            while (root.Parent != null) root = root.Parent;
            return root;
        }

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