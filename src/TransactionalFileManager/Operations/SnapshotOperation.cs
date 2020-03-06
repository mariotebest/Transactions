namespace TransactionalFileManager.Operations
{
    /// <summary>
    /// Rollbackable operation which takes a snapshot of a file. The snapshot is used to rollback the file later if needed.
    /// </summary>
    internal sealed class Snapshot: SingleFileOperation
    {
        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="path">The file to take a snapshot for.</param>
        public Snapshot(string path) : base(path)
        {
        }

        public override void Execute()
        {
            CreateSnapshot();
        }
    }
}
