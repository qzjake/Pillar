#region

using System.IO;
using System.Text;

#endregion

namespace KataBankOCR.Business
{
    public interface IFileUtilities
    {
        string ReadFileToString(Stream file);
    }

    public class FileUtilities : IFileUtilities
    {
        public string ReadFileToString(Stream file)
        {
            var target = new MemoryStream();

            file.CopyTo(target);

            return Encoding.UTF8.GetString(target.ToArray());
        }
    }
}