using System.IO;

namespace TransactionalFileManager.Operations
{
    /// <summary>
    /// Rollbackable operation which moves a file to a new location.
    /// </summary>
    internal sealed class Move : IRollbackableOperation
    {
        private readonly string _sourceFileName;
        private readonly string _destFileName;

        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="sourceFileName">The name of the file to move.</param>
        /// <param name="destFileName">The new path for the file.</param>
        public Move(string sourceFileName, string destFileName)
        {
            _sourceFileName = sourceFileName;
            _destFileName = destFileName;
        }

        public void Execute()
        {
            File.Move(_sourceFileName, _destFileName);
        }

        public void Rollback()
        {
            File.Move(_destFileName, _sourceFileName);
        }
    }
}
