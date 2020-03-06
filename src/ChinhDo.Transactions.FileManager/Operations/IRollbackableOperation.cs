namespace TxFileManager.Operations
{
    /// <summary>
    /// Represents a transactional file operation.
    /// </summary>
    internal interface IRollbackableOperation
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        void Execute();

        /// <summary>
        /// Rolls back the operation, restores the original state.
        /// </summary>
        void Rollback();
    }
}
