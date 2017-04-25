using System.IO;

namespace WADV.Core.File {
    /// <summary>
    /// File protocol
    /// </summary>
    public interface IProtocol {
        /// <summary>
        /// Protocol name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Read top-level file as stream
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns></returns>
        (Stream file, string fileExt, string innerPath) GetFileStream(string path);
    }
}