using System.Text;
using System.IO;
using Core.Exceptions;

namespace Core
{
    public class FileChecker
    {
        public static bool CheckEncoding(StreamReader stream) 
        {
            Encoding enc = stream.CurrentEncoding;
            if (enc == Encoding.UTF8 || enc == Encoding.ASCII)
                return true;
            throw new EncodingNotSupportedException($"Encoding {enc.EncodingName} is not supported");
        }
    }
}