namespace General.MSSQL.DAL
{
    using General.Data;
    using General.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.MSSQL;

    /// <summary>
    /// Represents a data access layer for managing candidate experiences
    /// </summary>4
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 07/ Octubre / 2023
    /// </remarks>
    public class CandidateExperienceDAL : ICandidateExperienceDAL
    {
        private readonly ApplicationDbContextTemp _dbContext;

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public CandidateExperienceDAL(ApplicationDbContextTemp dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds a candidate experience to the database asynchronously.
        /// </summary>
        /// <param name="experience">The candidate experience to add.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public async Task<bool> AddExperienceAsync(CandidateExperienceSQL experience)
        {
            try
            {
                // Add the candidate experience to the database
                _dbContext.CandidateExperience.Add(experience);
                // Save changes to the database
                return await this.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a candidate experience by its unique identifier asynchronously.
        /// </summary>
        /// <param name="experienceId">The unique identifier of the candidate experience to delete.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public async Task<bool> DeleteExperienceAsync(int experienceId)
        {
            var candidateSearch = await this.GetExperienceByIdAsync(experienceId);

            if (candidateSearch == null)
            {
                return false;
            }

            _dbContext.CandidateExperience.Remove(candidateSearch);
            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves all candidate experiences from the database asynchronously.
        /// </summary>
        /// <returns>A collection of candidate experiences.</returns>
        public async Task<IEnumerable<CandidateExperienceSQL>> GetAllExperiencesAsync()
        {
            return await _dbContext.CandidateExperience.Include(experience => experience.Candidate).ToListAsync(); ;
        }

        /// <summary>
        /// Retrieves a candidate experience by the candidate's unique identifier asynchronously.
        /// </summary>
        /// <param name="candidateId">The unique identifier of the candidate whose experience you want to retrieve.</param>
        /// <returns>The candidate's experience if found, otherwise null.</returns>
        public async Task<CandidateExperienceSQL> GetExperienceByIdAsync(int candidateId)
        {
            return await _dbContext.CandidateExperience.Where(e => e.IdCandidateExperience == candidateId).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Retrieves all candidate experiences of a specific candidate asynchronously.
        /// </summary>
        /// <param name="candidateId">The unique identifier of the candidate.</param>
        /// <returns>A collection of candidate experiences for the specified candidate.</returns>
        public async Task<IEnumerable<CandidateExperienceSQL>> GetExperiencesByCandidateIdAsync(int candidateId)
        {
            return await _dbContext.CandidateExperience.Where(e => e.IdCandidate == candidateId).ToListAsync();
        }


        /// <summary>
        /// Saves changes made to the database asynchronously.
        /// </summary>
        /// <returns>True if changes were successfully saved, otherwise false.</returns>
        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() > 0);
        }

        /// <summary>
        /// Updates a candidate experience in the database asynchronously.
        /// </summary>
        /// <param name="experience">The candidate experience with updated information.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public async Task<bool> UpdateExperienceAsync(CandidateExperienceSQL experience)
        {
            var candidateSearch = await this.GetExperienceByIdAsync(experience.IdCandidateExperience);

            if (candidateSearch == null)
            {
                return false;
            }
            candidateSearch.Company = experience.Company;
            candidateSearch.Job = experience.Job;
            candidateSearch.Salary = experience.Salary;
            candidateSearch.IdCandidate = experience.IdCandidate;
            candidateSearch.Description = experience.Description;
            candidateSearch.BeginDate = experience.BeginDate;
            candidateSearch.EndDate = experience.EndDate;
            candidateSearch.ModifyDate = DateTime.Now;

            return await this.SaveChangesAsync();
        }
    }
}
