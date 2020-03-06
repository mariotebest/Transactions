using System.IO;

namespace TransactionalFileManager.Operations
{
    /// <summary>
    /// Rollbackable operation which copies a file.
    /// </summary>
    internal sealed class Copy : SingleFileOperation
    {
        private readonly string _sourceFileName;
        private readonly bool _overwrite;

        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="sourceFileName">The file to copy.</param>
        /// <param name="destFileName">The name of the destination file.</param>
        /// <param name="overwrite">true if the destination file can be overwritten, otherwise false.</param>
        public Copy(string sourceFileName, string destFileName, bool overwrite)
            : base(destFileName)
        {
            _sourceFileName = sourceFileName;
            _overwrite = overwrite;
        }

        public override void Execute()
        {
            CreateSnapshot();

            File.Copy(_sourceFileName, Path, _overwrite);
        }
    }
}
