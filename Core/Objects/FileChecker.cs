using System.Text;
using System.IO;
using Core.Exceptions;
/// <summary>
/// The namespace of all core objectss
/// </summary>
namespace Core.Objects
{
    /// <summary>
    /// The class responsible for checking if a given file is correctly encoded.
    /// </summary>
    public class FileChecker
    {
        /// <summary>
        /// The method for checking if a file is ecoded as either UTF8 or ASCII
        /// </summary>
        /// <param name="stream">The inputstream of the file being compiled</param>
        /// <returns>True if the file is either UTF8 or ASCII</returns>
        public static bool CheckEncoding(StreamReader stream) 
        {
            Encoding enc = stream.CurrentEncoding;
            if (enc == Encoding.UTF8 || enc == Encoding.ASCII)
                return true;
            throw new EncodingNotSupportedException($"Encoding {enc.EncodingName} is not supported");
        }
    }
}