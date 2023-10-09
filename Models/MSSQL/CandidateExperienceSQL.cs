namespace Models.MSSQL
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    ///     Represents a candidate's work experience in the database.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 07/ Octubre / 2023
    /// </remarks>
    public class CandidateExperienceSQL
    {
        /// <summary>
        /// Gets or sets the unique identifier for the candidate experience.
        /// </summary>
        [Key]
        public int IdCandidateExperience { get; set; }

        /// <summary>
        /// Gets or sets the candidate to whom the experience is related.
        /// </summary>
        [ForeignKey("Candidate")]
        public int IdCandidate { get; set; }        
        public CandidateSQL Candidate { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        [Required]
        [StringLength(100)]
        public required string Company { get; set; }

        /// <summary>
        /// Gets or sets the job position.
        /// </summary>
        [Required]
        [StringLength(100)]
        public required string Job { get; set; }

        /// <summary>
        /// Gets or sets a description of the experience.
        /// </summary>
        [Required]
        [StringLength(4000)]
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the salary for the experience (numeric format 8,2).
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Salary { get; set; }

        /// <summary>
        /// Gets or sets the start date of the experience.
        /// </summary>
        [Required]
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the experience.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the experience record was inserted.
        /// </summary>
        public DateTime InsertDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the experience record was last modified.
        /// </summary>
        public DateTime ModifyDate { get; set; }
    }

}
