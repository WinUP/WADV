using System.IO;
using WADV.Core.Exception;

namespace WADV.Core.API {
    public class Location {
        public static string Resource {
            get { return Configuration.Location.Resource; }
            set { Configuration.Location.Resource = value; }
        }

        public static string Skin {
            get { return Configuration.Location.Skin; }
            set { Configuration.Location.Skin = value; }
        }

        public static string Script {
            get { return Configuration.Location.Script; }
            set { Configuration.Location.Script = value; }
        }

        public static string Player {
            get { return Configuration.Location.Player; }
            set { Configuration.Location.Player = value; }
        }

        public static string Module {
            get { return Configuration.Location.Module; }
            set { Configuration.Location.Module = value; }
        }

        public static string Log {
            get { return Configuration.Location.Log; }
            set { Configuration.Location.Log = value; }
        }

        public static string GamePath => Configuration.Location.GamePath;

        public static string Combine(Type type, string path) {
            var mainPath = "";
            switch (type) {
                case Type.GamePath:
                    mainPath = GamePath;
                    break;
                case Type.Log:
                    mainPath = Log;
                    break;
                case Type.Module:
                    mainPath = Module;
                    break;
                case Type.Player:
                    mainPath = Player;
                    break;
                case Type.Resource:
                    mainPath = Resource;
                    break;
                case Type.Script:
                    mainPath = Script;
                    break;
                case Type.Skin:
                    mainPath = Skin;
                    break;
                default:
                    throw GameException.New(ExceptionType.ArgumentOutOfRange)
                        .Value(type.ToString())
                        .At("API.Location.Combine")
                        .Message("Location type is an unknow type")
                        .Save()
                        .AsThrowable();
            }
            return Path.Combine(mainPath, path);
        }

        public enum Type {
            GamePath,
            Log,
            Module,
            Player,
            Resource,
            Script,
            Skin
        }
    }
}