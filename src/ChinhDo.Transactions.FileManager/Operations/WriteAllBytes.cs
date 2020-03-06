using System.IO;
using TxFileManager.Utils;

namespace TxFileManager.Operations
{
    /// <summary>
    /// Creates a file, and writes the specified contents to it.
    /// </summary>
    internal sealed class WriteAllBytes : SingleFileOperation
    {
        private readonly byte[] _contents;

        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The string to write to the file.</param>
        public WriteAllBytes(string path, byte[] contents)
            : base(path)
        {
            _contents = contents;
        }

        public override void Execute()
        {
            CreateSnapshot();

            File.WriteAllBytes(Path, _contents);
        }
    }
}