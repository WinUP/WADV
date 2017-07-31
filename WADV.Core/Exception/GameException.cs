using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WADV.Core.Exception {
    /// <summary>
    /// Exception for game system
    /// </summary>
    public class GameException : global::System.Exception {
        public const string NoValue = "NULL";

        /// <summary>
        /// Get a new GameException
        /// </summary>
        /// <param name="type">Type of this exception</param>
        /// <returns></returns>
        public static GameException New(string type) {
            return new GameException(type);
        }

        private GameException(string type) {
            Type = type;
            Data = new Dictionary<string, string>();
        }

        public string Type { get; }

        public GameException How(string value) {
            Message = value;
            return this;
        }

        /// <summary>
        /// Sets the message of current exception
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public new string Message { get; private set; }

        /// <summary>
        /// Sets the value which caused this exception
        /// </summary>
        public GameException Value(string key, string value) {
            if (Data.ContainsKey(key)) Data[key] = value;
            else Data.Add(key, value);
            return this;
        }

        public new IDictionary<string, string> Data { get; }

        /// <summary>
        /// Sets the name of structure which this exception happend in
        /// </summary>
        public GameException At(string value) {
            Source = value;
            return this;
        }

        public new string Source { get; private set; }

        /// <summary>
        /// Save information of this exception to file
        /// </summary>
        public GameException Save() {
            if (!string.IsNullOrEmpty(Configuration.Location.Log))
                global::System.IO.File.AppendAllText(Path.Combine(Configuration.Location.Log, $"error_{DateTime.Now:yyyy_MM_dd}.log"),
                    $"[{DateTime.Now:HH:mm:ss}] {Type}@{Source}: {Message}\n{string.Join("\n", Data.Select(e => e.Key + " => " + e.Value))}",
                    Encoding.UTF8);
            return this;
        }
    }
}