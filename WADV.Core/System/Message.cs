namespace WADV.Core.System {
    /// <summary>
    /// System message
    /// </summary>
    public struct Message {
        /// <summary>
        /// Message content
        /// </summary>
        public string Content;

        /// <summary>
        /// Message mask
        /// </summary>
        /// <example>
        /// Mask will be conpared by bit. If receiver.Mask & this.Mask != 0, it can be proceed by that receiver.
        /// </example>
        public int Mask;

        /// <summary>
        /// Parameter of this message
        /// </summary>
        public object Parameter;

        /// <summary>
        /// Float type parameter of this message
        /// </summary>
        public float NumericalParameter;
    }
}