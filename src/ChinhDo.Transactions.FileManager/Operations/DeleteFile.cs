using System.IO;
using TxFileManager.Utils;

namespace TxFileManager.Operations
{
    /// <summary>
    /// Rollbackable operation which deletes a file. An exception is not thrown if the file does not exist.
    /// </summary>
    internal sealed class DeleteFile : SingleFileOperation
    {
        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="path">The file to be deleted.</param>
        public DeleteFile(string path)
            : base(path)
        {
        }

        public override void Execute()
        {
            CreateSnapshot();

            File.Delete(Path);
        }
    }
}
