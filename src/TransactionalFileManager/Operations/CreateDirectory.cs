using System.IO;

namespace TransactionalFileManager.Operations
{
    /// <summary>
    /// Creates all directories in the specified path.
    /// </summary>
    internal sealed class CreateDirectory : IRollbackableOperation
    {
        private readonly string _path;
        private string _backupPath;

        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="path">The directory path to create.</param>
        public CreateDirectory(string path)
        {
            _path = path;
        }

        public void Execute()
        {
            // find the topmost directory which must be created
            var children = Path.GetFullPath(_path).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var parent = Path.GetDirectoryName(children);
            while (parent != null /* children is a root directory */
                && !Directory.Exists(parent))
            {
                children = parent;
                parent = Path.GetDirectoryName(children);
            }

            if (Directory.Exists(children)) return;

            Directory.CreateDirectory(_path);
            _backupPath = children;
        }

        public void Rollback()
        {
            if (_backupPath != null)
            {
                Directory.Delete(_backupPath, true);
            }
        }
    }
}
