namespace General.Interfaces
{
    using Models.MSSQL;

    /// <summary>
    /// Interface for accessing and managing candidate experiences.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 07/ Octubre / 2023
    /// </remarks>
    public interface ICandidateExperienceDAL
    {
        /// <summary>
        /// Retrieves all candidate experiences asynchronously.
        /// </summary>
        /// <returns>A collection of candidate experiences.</returns>
        Task<IEnumerable<CandidateExperienceSQL>> GetAllExperiencesAsync();

        /// <summary>
        /// Retrieves a candidate experience by its unique identifier asynchronously.
        /// </summary>
        /// <param name="experienceId">The unique identifier of the experience.</param>
        /// <returns>The candidate experience.</returns>
        Task<CandidateExperienceSQL> GetExperienceByIdAsync(int experienceId);

        /// <summary>
        /// Adds a new candidate experience asynchronously.
        /// </summary>
        /// <param name="experience">The candidate experience to add.</param>
        /// <returns>The number of affected rows (usually 1 if successful).</returns>
        Task<bool> AddExperienceAsync(CandidateExperienceSQL experience);

        /// <summary>
        /// Updates an existing candidate experience asynchronously.
        /// </summary>
        /// <param name="experience">The candidate experience to update.</param>
        /// <returns>The number of affected rows (usually 1 if successful).</returns>
        Task<bool> UpdateExperienceAsync(CandidateExperienceSQL experience);

        /// <summary>
        /// Deletes a candidate experience by its unique identifier asynchronously.
        /// </summary>
        /// <param name="experienceId">The unique identifier of the experience to delete.</param>
        /// <returns>The number of affected rows (usually 1 if successful).</returns>
        Task<bool> DeleteExperienceAsync(int experienceId);

        /// <summary>
        /// Saves changes made to the database asynchronously.
        /// </summary>
        /// <returns>True if changes were successfully saved, otherwise false.</returns>
        Task<bool> SaveChangesAsync();

        /// <summary>
        /// Retrieves all candidate experiences of a specific candidate asynchronously.
        /// </summary>
        /// <param name="candidateId">The unique identifier of the candidate.</param>
        /// <returns>A collection of candidate experiences for the specified candidate.</returns>
        Task<IEnumerable<CandidateExperienceSQL>> GetExperiencesByCandidateIdAsync(int candidateId);
    }
}
