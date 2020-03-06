using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TxFileManager.Utils;

namespace TxFileManager.Operations
{
    /// <summary>
    /// Creates a file, and writes the specified contents to it.
    /// </summary>
    internal sealed class WriteAllLines : SingleFileOperation
    {
        private readonly IEnumerable<string> _contents;
        private readonly Encoding _encoding;

        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The string to write to the file.</param>
        public WriteAllLines(string path, IEnumerable<string> contents)
            : base(path)
        {
            _contents = contents;
        }

        public WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding) : base(path)
        {
            _contents = contents;
            _encoding = encoding;
        }

        public WriteAllLines(string path, string[] contents) : base(path)
        {
            _contents = contents.ToList();
        }

        public WriteAllLines(string path, string[] contents, Encoding encoding) : base(path)
        {
            _encoding = encoding;
            _contents = contents.ToList();
        }


        public override void Execute()
        {
            CreateSnapshot();

            File.WriteAllLines(Path, _contents, _encoding ?? Encoding.Default);
        }
    }
}
