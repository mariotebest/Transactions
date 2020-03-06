using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using TxFileManager.Operations;
using TxFileManager.Utils;

namespace TxFileManager
{
    /// <summary>
    /// File Resource Manager. Allows inclusion of file system operations in transactions.
    /// http://www.chinhdo.com/20080825/transactional-file-manager/
    /// </summary>
    public class TxFileManager : IFileManager
    {
        /// <summary>
        /// Initializes the <see cref="TxFileManager"/> class.
        /// </summary>
        public TxFileManager()
        {
            FileUtils.EnsureTempFolderExists();
        }

        #region IFileOperations

        public void AppendAllLines(string path, IEnumerable<string> contents)
        {
            if (IsInTransaction())
                EnlistOperation(new AppendAllLines(path, contents));
            else
                File.AppendAllLines(path, contents);
        }

        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            if (IsInTransaction())
                EnlistOperation(new AppendAllLines(path, contents, encoding));
            else
                File.AppendAllLines(path, contents, encoding);
        }

        public async Task AppendAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding,
            CancellationToken cancellationToken = default)
        {
            await Task.Run(() => AppendAllLines(path, contents, encoding), cancellationToken);
        }

        public async Task AppendAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => AppendAllLines(path, contents), cancellationToken);
        }

        /// <summary>Appends the specified string the file, creating the file if it doesn't already exist.</summary>
        /// <param name="path">The file to append the string to.</param>
        /// <param name="contents">The string to append to the file.</param>
        public void AppendAllText(string path, string contents)
        {
            if (IsInTransaction())
                EnlistOperation(new AppendAllText(path, contents));
            else
                File.AppendAllText(path, contents);
        }

        public void AppendAllText(string path, string contents, Encoding encoding)
        {
            if (IsInTransaction())
                EnlistOperation(new AppendAllText(path, contents, encoding));
            else
                File.AppendAllText(path, contents, encoding);
        }

        public async Task AppendAllTextAsync(string path, string contents, Encoding encoding, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => AppendAllText(path, contents, encoding), cancellationToken);
        }

        public async Task AppendAllTextAsync(string path, string contents, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => AppendAllText(path, contents), cancellationToken);
        }

        public void Copy(string sourceFileName, string destFileName)
        {
            Copy(sourceFileName, destFileName, false);
        }

        /// <summary>Copies the specified <paramref name="sourceFileName"/> to <paramref name="destFileName"/>.</summary>
        /// <param name="sourceFileName">The file to copy.</param>
        /// <param name="destFileName">The name of the destination file.</param>
        /// <param name="overwrite">true if the destination file can be overwritten, otherwise false.</param>
        public void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            if (IsInTransaction())
                EnlistOperation(new Copy(sourceFileName, destFileName, overwrite));
            else
                File.Copy(sourceFileName, destFileName, overwrite);
        }

        /// <summary>Creates all directories in the specified path.</summary>
        /// <param name="path">The directory path to create.</param>
        public void CreateDirectory(string path)
        {
            if (IsInTransaction())
                EnlistOperation(new CreateDirectory(path));
            else
                Directory.CreateDirectory(path);
        }

        /// <summary>Deletes the specified file. An exception is not thrown if the file does not exist.</summary>
        /// <param name="path">The file to be deleted.</param>
        public void Delete(string path)
        {
            if (IsInTransaction())
                EnlistOperation(new DeleteFile(path));
            else
                File.Delete(path);
        }


        /// <summary>Deletes the specified directory and all its contents. An exception is not thrown if the directory does not exist.</summary>
        /// <param name="path">The directory to be deleted.</param>
        public void DeleteDirectory(string path)
        {
            if (IsInTransaction())
                EnlistOperation(new DeleteDirectory(path));
            else
                Directory.Delete(path, true);
        }

        /// <summary>Moves the specified file to a new location.</summary>
        /// <param name="sourceFileName">The name of the file to move.</param>
        /// <param name="destFileName">The new path for the file.</param>
        public void Move(string sourceFileName, string destFileName)
        {
            Move(sourceFileName, destFileName, false);
        }

        public void Move(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!overwrite && File.Exists(destFileName)) throw new InvalidOperationException("Cannot move file. File exists!");
            if (IsInTransaction())
                EnlistOperation(new Move(sourceFileName, destFileName));
            else
                File.Move(sourceFileName, destFileName);
        }


        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
        {
            Replace(sourceFileName, destinationFileName, destinationBackupFileName, false);
        }

        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName,
            bool ignoreMetadataErrors)
        {
            if (IsInTransaction())
                EnlistOperation(new Replace(sourceFileName, destinationFileName, destinationBackupFileName,
                    ignoreMetadataErrors));
            else
                File.Replace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
        }


        /// <summary>Take a snapshot of the specified file. The snapshot is used to rollback the file later if needed.</summary>
        /// <param name="fileName">The file to take a snapshot for.</param>
        public void Snapshot(string fileName)
        {
            if (IsInTransaction())
                EnlistOperation(new Snapshot(fileName));
        }

        public async Task WriteAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => WriteAllLines(path, contents), cancellationToken);
        }

        /// <summary>Creates a file, write the specified <paramref name="contents"/> to the file.</summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The string to write to the file.</param>
        public void WriteAllText(string path, string contents)
        {
            if (IsInTransaction())
                EnlistOperation(new WriteAllText(path, contents));
            else
                File.WriteAllText(path, contents);
        }

        public void WriteAllText(string path, string contents, Encoding encoding)
        {
            if (IsInTransaction())
                EnlistOperation(new WriteAllText(path, contents));
            else
                File.WriteAllText(path, contents, encoding);
        }

        public async Task WriteAllTextAsync(string path, string contents, Encoding encoding, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => WriteAllText(path, contents, encoding), cancellationToken);
        }

        public async Task WriteAllTextAsync(string path, string contents, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => WriteAllText(path, contents), cancellationToken);
        }

        /// <summary>Creates a file, write the specified <paramref name="contents"/> to the file.</summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The bytes to write to the file.</param>
        public void WriteAllBytes(string path, byte[] contents)
        {
            if (IsInTransaction())
            {
                EnlistOperation(new WriteAllBytes(path, contents));
            }
            else
            {
                File.WriteAllBytes(path, contents);
            }
        }

        public async Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => WriteAllBytes(path, bytes), cancellationToken);
        }

        public void WriteAllLines(string path, IEnumerable<string> contents)
        {
            if (IsInTransaction())
                EnlistOperation(new WriteAllLines(path, contents));
            else
                File.WriteAllLines(path, contents);
        }

        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            if (IsInTransaction())
                EnlistOperation(new WriteAllLines(path, contents, encoding));
            else
                File.WriteAllLines(path, contents, encoding);
        }

        public void WriteAllLines(string path, string[] contents)
        {
            if (IsInTransaction())
                EnlistOperation(new WriteAllLines(path, contents));
            else
                File.WriteAllLines(path, contents);
        }

        public void WriteAllLines(string path, string[] contents, Encoding encoding)
        {
            if (IsInTransaction())
                EnlistOperation(new WriteAllLines(path, contents, encoding));
            else
                File.WriteAllLines(path, contents, encoding);
        }

        public async Task WriteAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding,
            CancellationToken cancellationToken = default)
        {
            await Task.Run(() => WriteAllLines(path, contents, encoding), cancellationToken);
        }

        #endregion

        /// <summary>Determines whether the specified path refers to a directory that exists on disk.</summary>
        /// <param name="path">The directory to check.</param>
        /// <returns>True if the directory exists.</returns>
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>Determines whether the specified file exists.</summary>
        /// <param name="path">The file to check.</param>
        /// <returns>True if the file exists.</returns>
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>Gets the files in the specified directory.</summary>
        /// <param name="path">The directory to get files.</param>
        /// <param name="handler">The <see cref="FileEventHandler"/> object to call on each file found.</param>
        /// <param name="recursive">if set to <c>true</c>, include files in sub directories recursively.</param>
        public void GetFiles(string path, FileEventHandler handler, bool recursive)
        {
            foreach (var fileName in Directory.GetFiles(path))
            {
                var cancel = false;
                handler(fileName, ref cancel);
                if (cancel) return;
            }

            // Check subdirs
            if (!recursive) return;

            foreach (var folderName in Directory.GetDirectories(path))
            {
                GetFiles(folderName, handler, true);
            }
        }

        /// <summary>Creates a temporary file name. File is not automatically created.</summary>
        /// <param name="extension">File extension (with the dot).</param>
        public string GetTempFileName(string extension)
        {
            var retVal = FileUtils.GetTempFileName(extension);

            Snapshot(retVal);

            return retVal;
        }

        /// <summary>Creates a temporary file name. File is not automatically created.</summary>
        public string GetTempFileName()
        {
            return GetTempFileName(".tmp");
        }

        /// <summary>Gets a temporary directory.</summary>
        /// <returns>The path to the newly created temporary directory.</returns>
        public string GetTempDirectory()
        {
            return GetTempDirectory(Path.GetTempPath(), string.Empty);
        }

        /// <summary>Gets a temporary directory.</summary>
        /// <param name="parentDirectory">The parent directory.</param>
        /// <param name="prefix">The prefix of the directory name.</param>
        /// <returns>Path to the temporary directory. The temporary directory is created automatically.</returns>
        public string GetTempDirectory(string parentDirectory, string prefix)
        {
            var g = Guid.NewGuid();
            var dirName = Path.Combine(parentDirectory, prefix + g.ToString().Substring(0, 16));

            CreateDirectory(dirName);

            return dirName;
        }

        #region Private

        /// <summary>Dictionary of transaction enlistment objects for the current thread.</summary>
        [ThreadStatic]
        private static Dictionary<string, TxEnlistment> _enlistments;

        private static readonly object EnlistmentsLock = new object();

        private static bool IsInTransaction()
        {
            return Transaction.Current != null;
        }

        private static void EnlistOperation(IRollbackableOperation operation)
        {
            var tx = Transaction.Current;

            lock (EnlistmentsLock)
            {
                if (_enlistments == null)
                {
                    _enlistments = new Dictionary<string, TxEnlistment>();
                }

                if (!_enlistments.TryGetValue(tx.TransactionInformation.LocalIdentifier, out var enlistment))
                {
                    enlistment = new TxEnlistment(tx);
                    _enlistments.Add(tx.TransactionInformation.LocalIdentifier, enlistment);
                }
                enlistment.EnlistOperation(operation);
            }
        }

        #endregion
    }
}
