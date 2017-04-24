using System.Collections.Generic;

namespace WADV.Core.Script {
    /// <summary>
    /// Cross Language Linking Field
    /// </summary>
    public class Field {
        /// <summary>
        /// Name of this field
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Children of this field
        /// </summary>
        public Dictionary<string, object> Content { get; }

        public Field(string name) {
            Name = name;
            Content = new Dictionary<string, object>();
        }
    }
}