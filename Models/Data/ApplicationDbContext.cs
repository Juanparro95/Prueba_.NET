using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.MSSQL;

namespace General.Data
{
    /// <summary>
    /// Represents the application's database context, extending IdentityDbContext for user authentication.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 07/ Octubre / 2023
    /// </remarks>
    public class ApplicationDbContextTemp : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="options">The DbContext options to configure the database context.</param>
        public ApplicationDbContextTemp(DbContextOptions<ApplicationDbContextTemp> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets a DbSet for accessing candidate data.
        /// </summary>
        public DbSet<CandidateSQL> Candidates { get; set; }

        /// <summary>
        /// Gets or sets a DbSet for accessing candidate experience data.
        /// </summary>
        public DbSet<CandidateExperienceSQL> CandidateExperience { get; set; }
    }

}