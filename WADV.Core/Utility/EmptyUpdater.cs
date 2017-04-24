namespace WADV.Core.Utility {
    internal sealed class EmptyUpdater : Receiver.IUpdater {
        private int _count;

        public EmptyUpdater(int count) {
            _count = count;
        }

        public bool Update(int frame, out bool abort) {
            abort = false;
            if (_count == 0) return false;
            _count--;
            return true;
        }
    }
}