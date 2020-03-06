using System.IO;
using System.Text;
using TxFileManager.Utils;

namespace TxFileManager.Operations
{
    /// <summary>
    /// Creates a file, and writes the specified contents to it.
    /// </summary>
    internal sealed class WriteAllText : SingleFileOperation
    {
        private readonly string _contents;
        private readonly Encoding _encoding;

        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The string to write to the file.</param>
        public WriteAllText(string path, string contents)
            : base(path)
        {
            _contents = contents;
        }

        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The string to write to the file.</param>
        public WriteAllText(string path, string contents, Encoding encoding)
            : base(path)
        {
            _contents = contents;
            _encoding = encoding;
        }

        public override void Execute()
        {
            CreateSnapshot();
            File.WriteAllText(Path, _contents, _encoding ?? Encoding.Default);
        }
    }
}
