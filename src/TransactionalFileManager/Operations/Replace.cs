using System.IO;
using TransactionalFileManager.Utils;

namespace TransactionalFileManager.Operations
{
    /// <summary>
    /// Rollbackable operation which moves a file to a new location.
    /// </summary>
    internal sealed class Replace : IRollbackableOperation
    {
        private readonly string _sourceFileName;
        private readonly string _destFileName;
        private readonly string _destinationBackupFileName;
        private readonly bool _ignoreMetadataErrors;
        private string _backupPath;

        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="sourceFileName">The name of the file to move.</param>
        /// <param name="destinationFileName">The new path for the file.</param>
        /// <param name="destinationBackupFileName">Then name of the backup file</param>
        /// <param name="ignoreMetadataErrors">true to ignore merge errors (such as attributes and access control lists (ACLs)) //     from the replaced file to the replacement file; otherwise, false.</param>
        public Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            _sourceFileName = sourceFileName;
            _destFileName = destinationFileName;
            _destinationBackupFileName = destinationBackupFileName;
            _ignoreMetadataErrors = ignoreMetadataErrors;
        }

        public void Execute()
        {
            if (!File.Exists(_destFileName)) return;

            var temp = FileUtils.GetTempFileName(System.IO.Path.GetExtension(_destFileName));
            File.Copy(_destFileName, temp);
            _backupPath = temp;


            File.Replace(_sourceFileName, _destFileName, _destinationBackupFileName, _ignoreMetadataErrors);
        }

        public void Rollback()
        {
            File.Replace(_backupPath, _destFileName, null, true);
        }
    }
}
