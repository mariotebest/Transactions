using System;
using System.IO;
using TransactionalFileManager.Utils;

namespace TransactionalFileManager.Operations
{
    /// <summary>
    /// Class that contains common code for those rollbackable file operations which need
    /// to backup a single file and restore it when Rollback() is called.
    /// </summary>
    internal abstract class SingleFileOperation : IRollbackableOperation, IDisposable
    {
        protected readonly string Path;
        protected string BackupPath;
        // tracks whether Dispose has been called
        private bool _disposed;

        protected SingleFileOperation(string path)
        {
            Path = path;
        }

        protected void CreateSnapshot()
        {
            if (!File.Exists(Path)) return;

            var temp = FileUtils.GetTempFileName(System.IO.Path.GetExtension(Path));
            File.Copy(Path, temp);
            BackupPath = temp;
        }

        /// <summary>
        /// Disposes the resources used by this class.
        /// </summary>
        ~SingleFileOperation()
        {
            InnerDispose();
        }

        public abstract void Execute();

        public void Rollback()
        {
            if (BackupPath != null)
            {
                var directory = System.IO.Path.GetDirectoryName(Path);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.Copy(BackupPath, Path, true);
            }
            else
            {
                if (File.Exists(Path))
                {
                    File.Delete(Path);
                }
            }
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
        /// Disposes the resources of this class.
        /// </summary>
        private void InnerDispose()
        {
            if (_disposed) return;

            if (BackupPath != null)
            {
                var fi = new FileInfo(BackupPath);
                if (fi.IsReadOnly)
                {
                    fi.Attributes = FileAttributes.Normal;
                }
                File.Delete(BackupPath);
            }

            _disposed = true;
        }
    }
}
