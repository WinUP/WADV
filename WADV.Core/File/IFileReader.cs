using System.IO;
using WADV.Core.Utility;

namespace WADV.Core.File {
    /// <summary>
    /// File reader
    /// </summary>
    public interface IFileReader {
        /// <summary>
        /// Supported extension
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Read file as stream
        /// </summary>
        /// <param name="file">File stream</param>
        /// <param name="innerPath">Inner path in this file</param>
        /// <returns></returns>
        Stream ReadAsStream(Stream file, string innerPath);

        /// <summary>
        /// Read file as resource
        /// </summary>
        /// <param name="file">File stream</param>
        /// <param name="innerPath">Inner path in this file</param>
        /// <returns></returns>
        Resource ReadAsResource(Stream file, string innerPath);

        /// <summary>
        /// Read file as resource
        /// </summary>
        /// <typeparam name="T">Resource type</typeparam>
        /// <param name="file">File stream</param>
        /// <param name="innerPath">Inner path in this file</param>
        /// <returns></returns>
        Resource<T> ReadAsResource<T>(Stream file, string innerPath);

        /// <summary>
        /// Read file as chain
        /// </summary>
        /// <param name="file">File stream</param>
        /// <param name="innerPath">Inner path in this file</param>
        /// <returns></returns>
        Chain ReadAsChain(Stream file, string innerPath);

        /// <summary>
        /// Read file as chain
        /// </summary>
        /// <typeparam name="T">Chain's initial value type</typeparam>
        /// <param name="file">File stream</param>
        /// <param name="innerPath">Inner path in this file</param>
        /// <returns></returns>
        Chain<T, T> ReadAsChain<T>(Stream file, string innerPath) where T : class;
    }
}