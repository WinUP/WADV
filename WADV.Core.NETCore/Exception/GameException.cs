using System;
using System.IO;
using System.Text;

namespace WADV.Core.NETCore.Exception
{
    public abstract class GameException : global::System.Exception
    {
        protected GameException(string message, string errorType) : base(message)
        {
            if (!string.IsNullOrEmpty(Configuration.Location.Log))
                File.AppendAllText(Path.Combine(Configuration.Location.Log, "error.log"),
                                   $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {errorType}: {message}",
                                   Encoding.UTF8);
        }
    }
}
