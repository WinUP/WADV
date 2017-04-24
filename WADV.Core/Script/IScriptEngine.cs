using System;

namespace WADV.Core.Script {
    public interface IScriptEngine : IDisposable {
        /// <summary>
        /// Initialize engine
        /// </summary>
        void Initialize();

        /// <summary>
        /// Run script file in new thread
        /// </summary>
        /// <param name="filePath">Script file location</param>
        void RunFileAsync(string filePath);

        /// <summary>
        /// Run script file
        /// </summary>
        /// <param name="filePath">Script file location</param>
        /// <returns></returns>
        object RunFile(string filePath);

        /// <summary>
        /// Run script code in new thread
        /// </summary>
        /// <param name="content">Code content</param>
        void RunStringAsync(string content);

        /// <summary>
        /// Run script code
        /// </summary>
        /// <param name="content">Code content</param>
        /// <returns></returns>
        object RunString(string content);

        /// <summary>
        /// Set value of a global object
        /// </summary>
        /// <param name="name">Object name</param>
        /// <param name="value">Object value</param>
        void Set(string name, object value);

        /// <summary>
        /// Get value of a global object
        /// </summary>
        /// <param name="name">Object name</param>
        /// <returns></returns>
        object Get(string name);

        /// <summary>
        /// Get value of a global object
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="name">Object name</param>
        /// <returns></returns>
        T Get<T>(string name);

        /// <summary>
        /// Register Cross Language Linking Field
        /// </summary>
        /// <param name="target">Target field</param>
        void Register(Field target);

        /// <summary>
        /// Clear all data in this engine
        /// </summary>
        void ClearStatus();
    }
}
