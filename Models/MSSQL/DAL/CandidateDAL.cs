namespace General.MSSQL.DAL
{
    using General.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.MSSQL;
    using General.Data;

    /// <summary>
    /// Represents a data access layer for managing candidate
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 07/ Octubre / 2023
    /// </remarks>
    public class CandidateDAL : ICandidateDAL
    {
        private readonly ApplicationDbContextTemp _dbContext;

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public CandidateDAL(ApplicationDbContextTemp dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds a candidate to the database asynchronously.
        /// </summary>
        /// <param name="candidate">The candidate to add.</param>
        /// <returns>True if added successfully, otherwise, false.</returns>
        public async Task<bool> AddCandidateAsync(CandidateSQL candidate)
        {
            try
            {
                // Add the candidate to the database
                _dbContext.Candidates.Add(candidate);
                // Save changes to the database
                return await this.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a candidate from the database by their unique identifier asynchronously.
        /// </summary>
        /// <param name="candidateId">The unique identifier of the candidate to delete.</param>
        /// <returns>True if the candidate is deleted successfully, otherwise, false.</returns>
        public async Task<bool> DeleteCandidateAsync(int candidateId)
        {
            var candidateSearch = await this.GetCandidateByIdAsync(candidateId);

            if (candidateSearch == null)
            {
                return false;
            }

            _dbContext.Candidates.Remove(candidateSearch);
            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all candidates from the database asynchronously.
        /// </summary>
        /// <returns>A collection of candidates.</returns>
        public async Task<IEnumerable<CandidateSQL>> GetAllCandidatesAsync()
        {
            return await _dbContext.Candidates.Include(candidate => candidate.Experiences).ToListAsync();
        }

        /// <summary>
        /// Gets a candidate from the database by their unique identifier asynchronously.
        /// </summary>
        /// <param name="candidateId">The unique identifier of the candidate.</param>
        /// <returns>The corresponding candidate or null if not found.</returns>
        public async Task<CandidateSQL> GetCandidateByIdAsync(int candidateId)
        {
            return await _dbContext.Candidates.FindAsync(new object[] { candidateId });
        }

        /// <summary>
        /// Saves changes made to the database asynchronously.
        /// </summary>
        /// <returns>True if changes are saved successfully, otherwise, false.</returns>
        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() > 0);
        }

        /// <summary>
        /// Updates a candidate in the database asynchronously.
        /// </summary>
        /// <param name="candidate">The candidate with changes to update.</param>
        /// <returns>True if the candidate is updated successfully, otherwise, false.</returns>
        public async Task<bool> UpdateCandidateAsync(CandidateSQL candidate)
        {
            var candidateSearch = await this.GetCandidateByIdAsync(candidate.IdCandidate);

            if (candidateSearch == null)
            {
                return false;
            }

            candidateSearch.Name = candidate.Name;
            candidateSearch.Surname = candidate.Surname;
            candidateSearch.Email = candidate.Email;
            candidateSearch.Birthday = candidate.Birthday;
            candidateSearch.ModifyDate = DateTime.Now;

            return await this.SaveChangesAsync();
        }
    }
}
