using System;
using System.Collections.Generic;
using System.IO;
using WADV.Core.Utility;

namespace WADV.Core.File {
    /// <summary>
    /// File manager
    /// </summary>
    internal class FileManager {
        private static readonly FileManager SingleInstance = new FileManager();
        private readonly Dictionary<string, IProtocol> _protocols = new Dictionary<string, IProtocol>();
        private readonly Dictionary<string, IFileReader> _fileReaders = new Dictionary<string, IFileReader>();

        private FileManager() {
        }

        /// <summary>
        /// Instance of FileManager
        /// </summary>
        /// <returns></returns>
        internal static FileManager Instance() {
            return SingleInstance;
        }

        /// <summary>
        /// Add new protocol to manager
        /// </summary>
        /// <param name="target">Target protocol</param>
        internal void AddProtocol(IProtocol target) {
            if (_protocols.ContainsKey(target.Name)) return;
            _protocols.Add(target.Name, target);
        }

        /// <summary>
        /// Add new reader to manager
        /// </summary>
        /// <param name="target">Target reader</param>
        internal void AddFileReader(IFileReader target) {
            if (_fileReaders.ContainsKey(target.Extension)) return;
            _fileReaders.Add(target.Extension, target);
        }

        /// <summary>
        /// Read file as stream
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns></returns>
        internal Stream ReadAsStream(string path) {
            var reader = FindFile(path);
            return reader.reader.ReadAsStream(reader.file, reader.innerPath);
        }

        /// <summary>
        /// Read file as resource
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns></returns>
        internal Resource ReadAsResource(string path) {
            var reader = FindFile(path);
            return reader.reader.ReadAsResource(reader.file, reader.innerPath);
        }

        /// <summary>
        /// Read file as resource
        /// </summary>
        /// <typeparam name="T">Resource type</typeparam>
        /// <param name="path">File path</param>
        /// <returns></returns>
        internal Resource<T> ReadAsResource<T>(string path) {
            var reader = FindFile(path);
            return reader.reader.ReadAsResource<T>(reader.file, reader.innerPath);
        }

        /// <summary>
        /// Read file as chain
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns></returns>
        internal Chain ReadAsChain(string path) {
            var reader = FindFile(path);
            return reader.reader.ReadAsChain(reader.file, reader.innerPath);
        }

        /// <summary>
        /// Read file as chain
        /// </summary>
        /// <typeparam name="T">Chain's initial value type</typeparam>
        /// <param name="path">File path</param>
        /// <returns></returns>
        internal Chain<T, T> ReadAsChain<T>(string path) where T : class {
            var reader = FindFile(path);
            return reader.reader.ReadAsChain<T>(reader.file, reader.innerPath);
        }

        private (IFileReader reader, Stream file, string innerPath) FindFile(string path) {
            var splitterIndex = path.IndexOf(":", StringComparison.Ordinal);
            if (splitterIndex < 1) return (null, null, null);
            var protocalType = path.Substring(0, splitterIndex);
            if (!_protocols.ContainsKey(protocalType)) return (null, null, null);
            var protocal = _protocols[protocalType];
            var fileInfo = protocal.GetFileStream(path.Substring(splitterIndex + 1));
            return _fileReaders.ContainsKey(fileInfo.fileExt) 
                ? (_fileReaders[fileInfo.fileExt], fileInfo.file, fileInfo.innerPath)
                : (null, null, null);
        }
    }
}