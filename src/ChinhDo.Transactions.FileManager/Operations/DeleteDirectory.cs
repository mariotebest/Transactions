using System;
using System.IO;
using TxFileManager.Utils;

namespace TxFileManager.Operations
{
    /// <summary>
    /// Deletes the specified directory and all its contents.
    /// </summary>
    internal sealed class DeleteDirectory : IRollbackableOperation, IDisposable
    {
        private readonly string _path;
        private string _backupPath;
        // tracks whether Dispose has been called
        private bool _disposed;

        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="path">The directory path to delete.</param>
        public DeleteDirectory(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Disposes the resources used by this class.
        /// </summary>
        ~DeleteDirectory()
        {
            InnerDispose();
        }

        public void Execute()
        {
            if (!Directory.Exists(_path)) return;

            var temp = FileUtils.GetTempFileName(string.Empty);
            MoveDirectory(_path, temp);
            _backupPath = temp;
        }

        public void Rollback()
        {
            if (!Directory.Exists(_backupPath)) return;

            var parentDirectory = Path.GetDirectoryName(_path);
            if (!Directory.Exists(parentDirectory))
            {
                Directory.CreateDirectory(parentDirectory);
            }
            MoveDirectory(_backupPath, _path);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            InnerDispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Moves a directory, recursively, from one path to another.
		/// This is a version of <see cref="Directory.Move"/> that works across volumes.
        /// </summary>
        private static void MoveDirectory(string sourcePath, string destinationPath)
        {
            if (Directory.GetDirectoryRoot(sourcePath) == Directory.GetDirectoryRoot(destinationPath))
            {
                // The source and destination volumes are the same, so we can do the much less expensive Directory.Move.
                Directory.Move(sourcePath, destinationPath);
            }
            else
            {
                // The source and destination volumes are different, so we have to resort to a copy/delete.
                CopyDirectory(new DirectoryInfo(sourcePath), new DirectoryInfo(destinationPath));
                Directory.Delete(sourcePath, true);
            }
        }

        private static void CopyDirectory(DirectoryInfo sourceDirectory, DirectoryInfo destinationDirectory)
        {
            if (!destinationDirectory.Exists)
            {
                destinationDirectory.Create();
            }

            foreach (var sourceFile in sourceDirectory.GetFiles())
            {
                sourceFile.CopyTo(Path.Combine(destinationDirectory.FullName, sourceFile.Name));
            }

            foreach (var sourceSubDirectory in sourceDirectory.GetDirectories())
            {
                var destinationSubDirectoryPath = Path.Combine(destinationDirectory.FullName, sourceSubDirectory.Name);
                CopyDirectory(sourceSubDirectory, new DirectoryInfo(destinationSubDirectoryPath));
            }
        }
		
        /// <summary>
        /// Disposes the resources of this class.
        /// </summary>
        private void InnerDispose()
        {
            if (_disposed) return;

            if (Directory.Exists(_backupPath))
            {
                Directory.Delete(_backupPath, true);
            }

            _disposed = true;
        }
    }
}
