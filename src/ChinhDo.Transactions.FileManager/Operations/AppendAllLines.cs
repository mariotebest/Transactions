using System.Collections.Generic;
using System.IO;
using System.Text;
using TxFileManager.Utils;

namespace TxFileManager.Operations
{
    /// <summary>
    /// Rollbackable operation which appends a string to an existing file, or creates the file if it doesn't exist.
    /// </summary>
    internal sealed class AppendAllLines : SingleFileOperation
    {
        private readonly IEnumerable<string> _contents;
        private readonly Encoding _encoding;

        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="path">The file to append the string to.</param>
        /// <param name="contents">The string to append to the file.</param>
        /// <param name="encoding">The encoding to use</param>
        public AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
            : base(path)
        {
            _contents = contents;
            _encoding = encoding;
        }

        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="path">The file to append the string to.</param>
        /// <param name="contents">The string to append to the file.</param>
        public AppendAllLines(string path, IEnumerable<string> contents)
            : base(path)
        {
            _contents = contents;
        }

        public override void Execute()
        {
            CreateSnapshot();

            File.AppendAllLines(Path, _contents, _encoding ?? Encoding.Default);
        }
    }
}
