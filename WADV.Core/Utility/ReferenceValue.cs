namespace WADV.Core.Utility {
    /// <summary>
    /// Box any value to reference type
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    public sealed class ReferenceValue<T> {
        public ReferenceValue() { }

        public ReferenceValue(T value) {
            Value = value;
        }

        /// <summary>
        /// Get or set value
        /// </summary>
        public T Value { get; set; }
    }
}
