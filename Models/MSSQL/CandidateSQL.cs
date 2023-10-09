namespace Models.MSSQL
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Represents a candidate's in the database.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 07/ Octubre / 2023
    /// </remarks>
    public class CandidateSQL
    {
        /// <summary>
        /// Gets or sets the unique identifier for the candidate.
        /// </summary>
        [Key]
        public int IdCandidate { get; set; }

        /// <summary>
        /// Gets or sets the name of the candidate.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname of the candidate.
        /// </summary>
        public required string Surname { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the candidate.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Gets or sets the email address of the candidate.
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(250)]
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the candidate record was inserted.
        /// </summary>
        public DateTime InsertDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the candidate record was last modified.
        /// </summary>
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// Gets or sets a collection of a candidate's work experiences.
        /// </summary>
        public ICollection<CandidateExperienceSQL> Experiences { get; set; } = new List<CandidateExperienceSQL>();
    }
}