using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TransactionalFileManager
{
    /// <summary>
    /// Classes implementing this interface provide methods to manipulate files.
    /// </summary>
    public interface IFileOperations
    {
        void AppendAllLines(string path, IEnumerable<string> contents);
        //
        // Summary:
        //     Appends lines to a file by using a specified encoding, and then closes the file.
        //     If the specified file does not exist, this method creates a file, writes the
        //     specified lines to the file, and then closes the file.
        //
        // Parameters:
        //   path:
        //     The file to append the lines to. The file is created if it doesn't already exist.
        //
        //   contents:
        //     The lines to append to the file.
        //
        //   encoding:
        //     The character encoding to use.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains one more
        //     invalid characters defined by the System.IO.Path.GetInvalidPathChars method.
        //
        //   T:System.ArgumentNullException:
        //     Either path, contents, or encoding is null.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     path is invalid (for example, the directory doesn't exist or it is on an unmapped
        //     drive).
        //
        //   T:System.IO.FileNotFoundException:
        //     The file specified by path was not found.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred while opening the file.
        //
        //   T:System.IO.PathTooLongException:
        //     path exceeds the system-defined maximum length.
        //
        //   T:System.NotSupportedException:
        //     path is in an invalid format.
        //
        //   T:System.Security.SecurityException:
        //     The caller does not have the required permission.
        //
        //   T:System.UnauthorizedAccessException:
        //     path specifies a file that is read-only. -or- This operation is not supported
        //     on the current platform. -or- path is a directory. -or- The caller does not have
        //     the required permission.
        void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding);
        //
        // Summary:
        //     Asynchronously appends lines to a file by using a specified encoding, and then
        //     closes the file. If the specified file does not exist, this method creates a
        //     file, writes the specified lines to the file, and then closes the file.
        //
        // Parameters:
        //   path:
        //     The file to append the lines to. The file is created if it doesn't already exist.
        //
        //   contents:
        //     The lines to append to the file.
        //
        //   encoding:
        //     The character encoding to use.
        //
        //   cancellationToken:
        //     The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.
        //
        // Returns:
        //     A task that represents the asynchronous append operation.
        Task AppendAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding, CancellationToken cancellationToken = default);
        //
        // Summary:
        //     Asynchronously appends lines to a file, and then closes the file. If the specified
        //     file does not exist, this method creates a file, writes the specified lines to
        //     the file, and then closes the file.
        //
        // Parameters:
        //   path:
        //     The file to append the lines to. The file is created if it doesn't already exist.
        //
        //   contents:
        //     The lines to append to the file.
        //
        //   cancellationToken:
        //     The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.
        //
        // Returns:
        //     A task that represents the asynchronous append operation.
        Task AppendAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default);
        //
        // Summary:
        //     Opens a file, appends the specified string to the file, and then closes the file.
        //     If the file does not exist, this method creates a file, writes the specified
        //     string to the file, then closes the file.
        //
        // Parameters:
        //   path:
        //     The file to append the specified string to.
        //
        //   contents:
        //     The string to append to the file.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains one or more
        //     invalid characters as defined by System.IO.Path.InvalidPathChars.
        //
        //   T:System.ArgumentNullException:
        //     path is null.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     The specified path is invalid (for example, the directory doesn't exist or it
        //     is on an unmapped drive).
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred while opening the file.
        //
        //   T:System.UnauthorizedAccessException:
        //     path specified a file that is read-only. -or- This operation is not supported
        //     on the current platform. -or- path specified a directory. -or- The caller does
        //     not have the required permission.
        //
        //   T:System.NotSupportedException:
        //     path is in an invalid format.
        //
        //   T:System.Security.SecurityException:
        //     The caller does not have the required permission.
        void AppendAllText(string path, string contents);
        //
        // Summary:
        //     Appends the specified string to the file using the specified encoding, creating
        //     the file if it does not already exist.
        //
        // Parameters:
        //   path:
        //     The file to append the specified string to.
        //
        //   contents:
        //     The string to append to the file.
        //
        //   encoding:
        //     The character encoding to use.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains one or more
        //     invalid characters as defined by System.IO.Path.InvalidPathChars.
        //
        //   T:System.ArgumentNullException:
        //     path is null.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     The specified path is invalid (for example, the directory doesn't exist or it
        //     is on an unmapped drive).
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred while opening the file.
        //
        //   T:System.UnauthorizedAccessException:
        //     path specified a file that is read-only. -or- This operation is not supported
        //     on the current platform. -or- path specified a directory. -or- The caller does
        //     not have the required permission.
        //
        //   T:System.NotSupportedException:
        //     path is in an invalid format.
        //
        //   T:System.Security.SecurityException:
        //     The caller does not have the required permission.
        void AppendAllText(string path, string contents, Encoding encoding);
        //
        // Summary:
        //     Asynchronously opens a file or creates the file if it does not already exist,
        //     appends the specified string to the file using the specified encoding, and then
        //     closes the file.
        //
        // Parameters:
        //   path:
        //     The file to append the specified string to.
        //
        //   contents:
        //     The string to append to the file.
        //
        //   encoding:
        //     The character encoding to use.
        //
        //   cancellationToken:
        //     The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.
        //
        // Returns:
        //     A task that represents the asynchronous append operation.
        Task AppendAllTextAsync(string path, string contents, Encoding encoding, CancellationToken cancellationToken = default);
        //
        // Summary:
        //     Asynchronously opens a file or creates a file if it does not already exist, appends
        //     the specified string to the file, and then closes the file.
        //
        // Parameters:
        //   path:
        //     The file to append the specified string to.
        //
        //   contents:
        //     The string to append to the file.
        //
        //   cancellationToken:
        //     The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.
        //
        // Returns:
        //     A task that represents the asynchronous append operation.
        Task AppendAllTextAsync(string path, string contents, CancellationToken cancellationToken = default);

        //
        // Summary:
        //     Copies an existing file to a new file. Overwriting a file of the same name is
        //     not allowed.
        //
        // Parameters:
        //   sourceFileName:
        //     The file to copy.
        //
        //   destFileName:
        //     The name of the destination file. This cannot be a directory or an existing file.
        //
        // Exceptions:
        //   T:System.UnauthorizedAccessException:
        //     The caller does not have the required permission.
        //
        //   T:System.ArgumentException:
        //     sourceFileName or destFileName is a zero-length string, contains only white space,
        //     or contains one or more invalid characters as defined by System.IO.Path.InvalidPathChars.
        //     -or- sourceFileName or destFileName specifies a directory.
        //
        //   T:System.ArgumentNullException:
        //     sourceFileName or destFileName is null.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     The path specified in sourceFileName or destFileName is invalid (for example,
        //     it is on an unmapped drive).
        //
        //   T:System.IO.FileNotFoundException:
        //     sourceFileName was not found.
        //
        //   T:System.IO.IOException:
        //     destFileName exists. -or- An I/O error has occurred.
        //
        //   T:System.NotSupportedException:
        //     sourceFileName or destFileName is in an invalid format.
        void Copy(string sourceFileName, string destFileName);
        //
        // Summary:
        //     Copies an existing file to a new file. Overwriting a file of the same name is
        //     allowed.
        //
        // Parameters:
        //   sourceFileName:
        //     The file to copy.
        //
        //   destFileName:
        //     The name of the destination file. This cannot be a directory.
        //
        //   overwrite:
        //     true if the destination file can be overwritten; otherwise, false.
        //
        // Exceptions:
        //   T:System.UnauthorizedAccessException:
        //     The caller does not have the required permission. -or- destFileName is read-only.
        //     -or- overwrite is true, destFileName exists and is hidden, but sourceFileName
        //     is not hidden.
        //
        //   T:System.ArgumentException:
        //     sourceFileName or destFileName is a zero-length string, contains only white space,
        //     or contains one or more invalid characters as defined by System.IO.Path.InvalidPathChars.
        //     -or- sourceFileName or destFileName specifies a directory.
        //
        //   T:System.ArgumentNullException:
        //     sourceFileName or destFileName is null.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     The path specified in sourceFileName or destFileName is invalid (for example,
        //     it is on an unmapped drive).
        //
        //   T:System.IO.FileNotFoundException:
        //     sourceFileName was not found.
        //
        //   T:System.IO.IOException:
        //     destFileName exists and overwrite is false. -or- An I/O error has occurred.
        //
        //   T:System.NotSupportedException:
        //     sourceFileName or destFileName is in an invalid format.
        void Copy(string sourceFileName, string destFileName, bool overwrite);

        //
        // Summary:
        //     Deletes the specified file.
        //
        // Parameters:
        //   path:
        //     The name of the file to be deleted. Wildcard characters are not supported.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains one or more
        //     invalid characters as defined by System.IO.Path.InvalidPathChars.
        //
        //   T:System.ArgumentNullException:
        //     path is null.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     The specified path is invalid (for example, it is on an unmapped drive).
        //
        //   T:System.IO.IOException:
        //     The specified file is in use. -or- There is an open handle on the file, and the
        //     operating system is Windows XP or earlier. This open handle can result from enumerating
        //     directories and files. For more information, see How to: Enumerate Directories
        //     and Files.
        //
        //   T:System.NotSupportedException:
        //     path is in an invalid format.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.UnauthorizedAccessException:
        //     The caller does not have the required permission. -or- The file is an executable
        //     file that is in use. -or- path is a directory. -or- path specified a read-only
        //     file.
        void Delete(string path);

        //
        // Summary:
        //     Gets the System.IO.FileAttributes of the file on the path.
        //
        // Parameters:
        //   path:
        //     The path to the file.
        //
        // Returns:
        //     The System.IO.FileAttributes of the file on the path.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is empty, contains only white spaces, or contains invalid characters.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.NotSupportedException:
        //     path is in an invalid format.
        //
        //   T:System.IO.FileNotFoundException:
        //     path represents a file and is invalid, such as being on an unmapped drive, or
        //     the file cannot be found.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     path represents a directory and is invalid, such as being on an unmapped drive,
        //     or the directory cannot be found.
        //
        //   T:System.IO.IOException:
        //     This file is being used by another process.
        //
        //   T:System.UnauthorizedAccessException:
        //     The caller does not have the required permission.

        //
        // Summary:
        //     Moves a specified file to a new location, providing the option to specify a new
        //     file name.
        //
        // Parameters:
        //   sourceFileName:
        //     The name of the file to move. Can include a relative or absolute path.
        //
        //   destFileName:
        //     The new path and name for the file.
        //
        // Exceptions:
        //   T:System.IO.IOException:
        //     The destination file already exists. -or- sourceFileName was not found.
        //
        //   T:System.ArgumentNullException:
        //     sourceFileName or destFileName is null.
        //
        //   T:System.ArgumentException:
        //     sourceFileName or destFileName is a zero-length string, contains only white space,
        //     or contains invalid characters as defined in System.IO.Path.InvalidPathChars.
        //
        //   T:System.UnauthorizedAccessException:
        //     The caller does not have the required permission.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     The path specified in sourceFileName or destFileName is invalid, (for example,
        //     it is on an unmapped drive).
        //
        //   T:System.NotSupportedException:
        //     sourceFileName or destFileName is in an invalid format.
        void Move(string sourceFileName, string destFileName);
        //
        // Summary:
        //     Moves a specified file to a new location, providing the options to specify a
        //     new file name and to overwrite the destination file if it already exists.
        //
        // Parameters:
        //   sourceFileName:
        //     The name of the file to move. Can include a relative or absolute path.
        //
        //   destFileName:
        //     The new path and name for the file.
        //
        //   overwrite:
        //     true to overwrite the destination file if it already exists; false otherwise.
        //
        // Exceptions:
        //   T:System.IO.IOException:
        //     sourceFileName was not found.
        //
        //   T:System.ArgumentNullException:
        //     sourceFileName or destFileName is null.
        //
        //   T:System.ArgumentException:
        //     sourceFileName or destFileName is a zero-length string, contains only white space,
        //     or contains invalid characters as defined in System.IO.Path.InvalidPathChars.
        //
        //   T:System.UnauthorizedAccessException:
        //     The caller does not have the required permission.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     The path specified in sourceFileName or destFileName is invalid, (for example,
        //     it is on an unmapped drive).
        //
        //   T:System.NotSupportedException:
        //     sourceFileName or destFileName is in an invalid format.
        void Move(string sourceFileName, string destFileName, bool overwrite);


        //
        // Summary:
        //     Replaces the contents of a specified file with the contents of another file,
        //     deleting the original file, and creating a backup of the replaced file.
        //
        // Parameters:
        //   sourceFileName:
        //     The name of a file that replaces the file specified by destinationFileName.
        //
        //   destinationFileName:
        //     The name of the file being replaced.
        //
        //   destinationBackupFileName:
        //     The name of the backup file.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The path described by the destinationFileName parameter was not of a legal form.
        //     -or- The path described by the destinationBackupFileName parameter was not of
        //     a legal form.
        //
        //   T:System.ArgumentNullException:
        //     The destinationFileName parameter is null.
        //
        //   T:System.IO.DriveNotFoundException:
        //     An invalid drive was specified.
        //
        //   T:System.IO.FileNotFoundException:
        //     The file described by the current System.IO.FileInfo object could not be found.
        //     -or- The file described by the destinationBackupFileName parameter could not
        //     be found.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred while opening the file. -or- The sourceFileName and destinationFileName
        //     parameters specify the same file.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.PlatformNotSupportedException:
        //     The operating system is Windows 98 Second Edition or earlier and the files system
        //     is not NTFS.
        //
        //   T:System.UnauthorizedAccessException:
        //     The sourceFileName or destinationFileName parameter specifies a file that is
        //     read-only. -or- This operation is not supported on the current platform. -or-
        //     Source or destination parameters specify a directory instead of a file. -or-
        //     The caller does not have the required permission.
        void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName);
        //
        // Summary:
        //     Replaces the contents of a specified file with the contents of another file,
        //     deleting the original file, and creating a backup of the replaced file and optionally
        //     ignores merge errors.
        //
        // Parameters:
        //   sourceFileName:
        //     The name of a file that replaces the file specified by destinationFileName.
        //
        //   destinationFileName:
        //     The name of the file being replaced.
        //
        //   destinationBackupFileName:
        //     The name of the backup file.
        //
        //   ignoreMetadataErrors:
        //     true to ignore merge errors (such as attributes and access control lists (ACLs))
        //     from the replaced file to the replacement file; otherwise, false.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The path described by the destinationFileName parameter was not of a legal form.
        //     -or- The path described by the destinationBackupFileName parameter was not of
        //     a legal form.
        //
        //   T:System.ArgumentNullException:
        //     The destinationFileName parameter is null.
        //
        //   T:System.IO.DriveNotFoundException:
        //     An invalid drive was specified.
        //
        //   T:System.IO.FileNotFoundException:
        //     The file described by the current System.IO.FileInfo object could not be found.
        //     -or- The file described by the destinationBackupFileName parameter could not
        //     be found.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred while opening the file. -or- The sourceFileName and destinationFileName
        //     parameters specify the same file.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.PlatformNotSupportedException:
        //     The operating system is Windows 98 Second Edition or earlier and the files system
        //     is not NTFS.
        //
        //   T:System.UnauthorizedAccessException:
        //     The sourceFileName or destinationFileName parameter specifies a file that is
        //     read-only. -or- This operation is not supported on the current platform. -or-
        //     Source or destination parameters specify a directory instead of a file. -or-
        //     The caller does not have the required permission.
        void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);

        //
        // Summary:
        //     Creates a new file, writes the specified byte array to the file, and then closes
        //     the file. If the target file already exists, it is overwritten.
        //
        // Parameters:
        //   path:
        //     The file to write to.
        //
        //   bytes:
        //     The bytes to write to the file.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains one or more
        //     invalid characters as defined by System.IO.Path.InvalidPathChars.
        //
        //   T:System.ArgumentNullException:
        //     path is null or the byte array is empty.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     The specified path is invalid (for example, it is on an unmapped drive).
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred while opening the file.
        //
        //   T:System.UnauthorizedAccessException:
        //     path specified a file that is read-only. -or- path specified a file that is hidden.
        //     -or- This operation is not supported on the current platform. -or- path specified
        //     a directory. -or- The caller does not have the required permission.
        //
        //   T:System.NotSupportedException:
        //     path is in an invalid format.
        //
        //   T:System.Security.SecurityException:
        //     The caller does not have the required permission.
        void WriteAllBytes(string path, byte[] bytes);
        //
        // Summary:
        //     Asynchronously creates a new file, writes the specified byte array to the file,
        //     and then closes the file. If the target file already exists, it is overwritten.
        //
        // Parameters:
        //   path:
        //     The file to write to.
        //
        //   bytes:
        //     The bytes to write to the file.
        //
        //   cancellationToken:
        //     The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = default);
        //
        // Summary:
        //     Creates a new file, writes a collection of strings to the file, and then closes
        //     the file.
        //
        // Parameters:
        //   path:
        //     The file to write to.
        //
        //   contents:
        //     The lines to write to the file.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains one or more
        //     invalid characters defined by the System.IO.Path.GetInvalidPathChars method.
        //
        //   T:System.ArgumentNullException:
        //     Either path or contents is null.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     path is invalid (for example, it is on an unmapped drive).
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred while opening the file.
        //
        //   T:System.IO.PathTooLongException:
        //     path exceeds the system-defined maximum length.
        //
        //   T:System.NotSupportedException:
        //     path is in an invalid format.
        //
        //   T:System.Security.SecurityException:
        //     The caller does not have the required permission.
        //
        //   T:System.UnauthorizedAccessException:
        //     path specified a file that is read-only. -or- path specified a file that is hidden.
        //     -or- This operation is not supported on the current platform. -or- path is a
        //     directory. -or- The caller does not have the required permission.
        void WriteAllLines(string path, IEnumerable<string> contents);
        //
        // Summary:
        //     Creates a new file by using the specified encoding, writes a collection of strings
        //     to the file, and then closes the file.
        //
        // Parameters:
        //   path:
        //     The file to write to.
        //
        //   contents:
        //     The lines to write to the file.
        //
        //   encoding:
        //     The character encoding to use.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains one or more
        //     invalid characters defined by the System.IO.Path.GetInvalidPathChars method.
        //
        //   T:System.ArgumentNullException:
        //     Either path, contents, or encoding is null.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     path is invalid (for example, it is on an unmapped drive).
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred while opening the file.
        //
        //   T:System.IO.PathTooLongException:
        //     path exceeds the system-defined maximum length.
        //
        //   T:System.NotSupportedException:
        //     path is in an invalid format.
        //
        //   T:System.Security.SecurityException:
        //     The caller does not have the required permission.
        //
        //   T:System.UnauthorizedAccessException:
        //     path specified a file that is read-only. -or- path specified a file that is hidden.
        //     -or- This operation is not supported on the current platform. -or- path is a
        //     directory. -or- The caller does not have the required permission.
        void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding);
        //
        // Summary:
        //     Creates a new file, write the specified string array to the file, and then closes
        //     the file.
        //
        // Parameters:
        //   path:
        //     The file to write to.
        //
        //   contents:
        //     The string array to write to the file.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains one or more
        //     invalid characters as defined by System.IO.Path.InvalidPathChars.
        //
        //   T:System.ArgumentNullException:
        //     Either path or contents is null.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     The specified path is invalid (for example, it is on an unmapped drive).
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred while opening the file.
        //
        //   T:System.UnauthorizedAccessException:
        //     path specified a file that is read-only. -or- path specified a file that is hidden.
        //     -or- This operation is not supported on the current platform. -or- path specified
        //     a directory. -or- The caller does not have the required permission.
        //
        //   T:System.NotSupportedException:
        //     path is in an invalid format.
        //
        //   T:System.Security.SecurityException:
        //     The caller does not have the required permission.
        void WriteAllLines(string path, string[] contents);
        //
        // Summary:
        //     Creates a new file, writes the specified string array to the file by using the
        //     specified encoding, and then closes the file.
        //
        // Parameters:
        //   path:
        //     The file to write to.
        //
        //   contents:
        //     The string array to write to the file.
        //
        //   encoding:
        //     An System.Text.Encoding object that represents the character encoding applied
        //     to the string array.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains one or more
        //     invalid characters as defined by System.IO.Path.InvalidPathChars.
        //
        //   T:System.ArgumentNullException:
        //     Either path or contents is null.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     The specified path is invalid (for example, it is on an unmapped drive).
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred while opening the file.
        //
        //   T:System.UnauthorizedAccessException:
        //     path specified a file that is read-only. -or- path specified a file that is hidden.
        //     -or- This operation is not supported on the current platform. -or- path specified
        //     a directory. -or- The caller does not have the required permission.
        //
        //   T:System.NotSupportedException:
        //     path is in an invalid format.
        //
        //   T:System.Security.SecurityException:
        //     The caller does not have the required permission.
        void WriteAllLines(string path, string[] contents, Encoding encoding);
        //
        // Summary:
        //     Asynchronously creates a new file, write the specified lines to the file by using
        //     the specified encoding, and then closes the file.
        //
        // Parameters:
        //   path:
        //     The file to write to.
        //
        //   contents:
        //     The lines to write to the file.
        //
        //   encoding:
        //     The character encoding to use.
        //
        //   cancellationToken:
        //     The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        Task WriteAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding, CancellationToken cancellationToken = default);
        //
        // Summary:
        //     Asynchronously creates a new file, writes the specified lines to the file, and
        //     then closes the file.
        //
        // Parameters:
        //   path:
        //     The file to write to.
        //
        //   contents:
        //     The lines to write to the file.
        //
        //   cancellationToken:
        //     The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        Task WriteAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default);
        //
        // Summary:
        //     Creates a new file, writes the specified string to the file, and then closes
        //     the file. If the target file already exists, it is overwritten.
        //
        // Parameters:
        //   path:
        //     The file to write to.
        //
        //   contents:
        //     The string to write to the file.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains one or more
        //     invalid characters as defined by System.IO.Path.InvalidPathChars.
        //
        //   T:System.ArgumentNullException:
        //     path is null.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     The specified path is invalid (for example, it is on an unmapped drive).
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred while opening the file.
        //
        //   T:System.UnauthorizedAccessException:
        //     path specified a file that is read-only. -or- path specified a file that is hidden.
        //     -or- This operation is not supported on the current platform. -or- path specified
        //     a directory. -or- The caller does not have the required permission.
        //
        //   T:System.NotSupportedException:
        //     path is in an invalid format.
        //
        //   T:System.Security.SecurityException:
        //     The caller does not have the required permission.
        void WriteAllText(string path, string contents);
        //
        // Summary:
        //     Creates a new file, writes the specified string to the file using the specified
        //     encoding, and then closes the file. If the target file already exists, it is
        //     overwritten.
        //
        // Parameters:
        //   path:
        //     The file to write to.
        //
        //   contents:
        //     The string to write to the file.
        //
        //   encoding:
        //     The encoding to apply to the string.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     path is a zero-length string, contains only white space, or contains one or more
        //     invalid characters as defined by System.IO.Path.InvalidPathChars.
        //
        //   T:System.ArgumentNullException:
        //     path is null.
        //
        //   T:System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length.
        //
        //   T:System.IO.DirectoryNotFoundException:
        //     The specified path is invalid (for example, it is on an unmapped drive).
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred while opening the file.
        //
        //   T:System.UnauthorizedAccessException:
        //     path specified a file that is read-only. -or- path specified a file that is hidden.
        //     -or- This operation is not supported on the current platform. -or- path specified
        //     a directory. -or- The caller does not have the required permission.
        //
        //   T:System.NotSupportedException:
        //     path is in an invalid format.
        //
        //   T:System.Security.SecurityException:
        //     The caller does not have the required permission.
        void WriteAllText(string path, string contents, Encoding encoding);
        //
        // Summary:
        //     Asynchronously creates a new file, writes the specified string to the file using
        //     the specified encoding, and then closes the file. If the target file already
        //     exists, it is overwritten.
        //
        // Parameters:
        //   path:
        //     The file to write to.
        //
        //   contents:
        //     The string to write to the file.
        //
        //   encoding:
        //     The encoding to apply to the string.
        //
        //   cancellationToken:
        //     The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        Task WriteAllTextAsync(string path, string contents, Encoding encoding, CancellationToken cancellationToken = default);
        //
        // Summary:
        //     Asynchronously creates a new file, writes the specified string to the file, and
        //     then closes the file. If the target file already exists, it is overwritten.
        //
        // Parameters:
        //   path:
        //     The file to write to.
        //
        //   contents:
        //     The string to write to the file.
        //
        //   cancellationToken:
        //     The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        Task WriteAllTextAsync(string path, string contents, CancellationToken cancellationToken = default);
    }
}
