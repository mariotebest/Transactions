namespace TxFileManager
{
    /// <summary>
    /// Classes implementing this interface provide methods to work with files.
    /// </summary>
    public interface IFileManager : IFileOperations
    {
        /// <summary>
        /// Determines whether the specified path refers to a directory that exists on disk.
        /// </summary>
        /// <param name="path">The directory to check.</param>
        /// <returns>True if the directory exists.</returns>
        bool DirectoryExists(string path);

        /// <summary>
        /// Determines whether the specified file exists.
        /// </summary>
        /// <param name="path">The file to check.</param>
        /// <returns>True if the file exists.</returns>
        bool FileExists(string path);

        /// <summary>
        /// Creates a temporary file name. The file is not automatically created.
        /// </summary>
        /// <param name="extension">File extension (with the dot).</param>
        string GetTempFileName(string extension);

        /// <summary>
        /// Gets a temporary filename. The file is not automatically created.
        /// </summary>
        string GetTempFileName();

        /// <summary>Deletes the specified directory and all its contents. An exception is not thrown if the directory does not exist.</summary>
        /// <param name="path">The directory to be deleted.</param>
        void DeleteDirectory(string path);

        /// <summary>Creates all directories in the specified path.</summary>
        /// <param name="path">The directory path to create.</param>
        void CreateDirectory(string path);

        /// <summary>Gets a temporary directory.</summary>
        /// <returns>The path to the newly created temporary directory.</returns>
        string GetTempDirectory();

        /// <summary>Take a snapshot of the specified file. The snapshot is used to rollback the file later if needed.</summary>
        /// <param name="fileName">The file to take a snapshot for.</param>
        void Snapshot(string fileName);
    }
}
