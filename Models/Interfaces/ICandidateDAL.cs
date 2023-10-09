namespace General.Interfaces
{
    using Models.MSSQL;

    /// <summary>
    /// Interface for accessing and managing candidate data.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano
    ///     Date: 07/ Octubre / 2023
    /// </remarks>
    public interface ICandidateDAL
    {
        /// <summary>
        /// Retrieves all candidates asynchronously.
        /// </summary>
        /// <returns>A collection of candidates.</returns>
        Task<IEnumerable<CandidateSQL>> GetAllCandidatesAsync();

        /// <summary>
        /// Retrieves a candidate by their unique identifier asynchronously.
        /// </summary>
        /// <param name="candidateId">The unique identifier of the candidate.</param>
        /// <returns>The candidate.</returns>
        Task<CandidateSQL> GetCandidateByIdAsync(int candidateId);

        /// <summary>
        /// Adds a new candidate asynchronously.
        /// </summary>
        /// <param name="candidate">The candidate to add.</param>
        /// <returns>The Boolean of affected rows (usually 1 if successful).</returns>
        Task<bool> AddCandidateAsync(CandidateSQL candidate);

        /// <summary>
        /// Updates an existing candidate asynchronously.
        /// </summary>
        /// <param name="candidate">The candidate to update.</param>
        /// <returns>The number of affected rows (usually 1 if successful).</returns>
        Task<bool> UpdateCandidateAsync(CandidateSQL candidate);

        /// <summary>
        /// Deletes a candidate by their unique identifier asynchronously.
        /// </summary>
        /// <param name="candidateId">The unique identifier of the candidate to delete.</param>
        /// <returns>The number of affected rows (usually 1 if successful).</returns>
        Task<bool> DeleteCandidateAsync(int candidateId);

        /// <summary>
        /// Saves changes made to the database asynchronously.
        /// </summary>
        /// <returns>True if changes were successfully saved, otherwise false.</returns>
        Task<bool> SaveChangesAsync();
    }
}
