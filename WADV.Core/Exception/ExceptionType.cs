namespace WADV.Core.Exception {
    public partial class ExceptionType {
        public static string IndexOutOfRange => "IndexOutOfRange";
        public static string ArgumentOutOfRange => "ArgumentOutOfRange";
        public static string MessageDequeueFailed => "MessageDequeueFailed";
        public static string FileCannotFound => "FileCannotFound";
        public static string PluginInitializeFailed => "PluginInitializeFailed";
        public static string FullScreenNotAllowed => "FullScreenNotAllowed";
        public static string CloseNotAllowed => "FullScreenNotAllowed";
        public static string TryInverstUninvertibleMatrix => "TryInverseUninvertibleMatrix";
    }
}