using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.MSSQL;

namespace Prueba_Panda_Pe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CandidateSQL> Candidates { get; set; }
        public DbSet<CandidateExperienceSQL> CandidateExperience { get; set; }
    }
}