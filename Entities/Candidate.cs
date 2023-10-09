namespace Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents DTO class Candidate
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 07/ Octubre / 2023
    /// </remarks>
    public class Candidate
    {
        /// <summary>
        /// Gets or sets the unique identifier for the candidate.
        /// </summary>
        public int IdCandidate { get; set; }

        /// <summary>
        /// Gets or sets the name of the candidate.
        /// </summary>
        [Display(Name = "Nombre del Candidato")]
        [StringLength(50, ErrorMessage = "El nombre del candidato no puede tener más de 50 caracteres.")]
        [Required(ErrorMessage = "Por favor ingresa el nombre del candidato.")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname of the candidate.
        /// </summary>
        [Display(Name = "Apellidos del Candidato")]
        [StringLength(150, ErrorMessage = "Los apellidos del candidato no pueden tener más de 150 caracteres.")]
        [Required(ErrorMessage = "Por favor ingresa los apellidos del candidato.")]
        public required string Surname { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the candidate.
        /// </summary>
        [Display(Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "Por favor ingresa la fecha de cumpleaños del candidato.")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Gets or sets the email address of the candidate.
        /// </summary>
        [Display(Name = "Correo Electrónico")]
        [StringLength(250, ErrorMessage = "El correo electrónico del candidato no puede tener más de 250 caracteres.")]
        [Required(ErrorMessage = "Por favor ingresa el correo electrónico del candidato.")]
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
        /// Gets or sets the list of experiences associated with the candidate.
        /// </summary>
        [Display(Name = "Experiences")]
        public List<CandidateExperience> Experiences { get; set; }
    }
}