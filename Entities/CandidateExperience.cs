namespace Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents DTO class CandidateExperience
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 07/ Octubre / 2023
    /// </remarks>
    public class CandidateExperience
    {
        /// <summary>
        /// Gets or sets the unique identifier for the candidate experience.
        /// </summary>
        public int IdCandidateExperience { get; set; }

        /// <summary>
        /// Gets or sets the candidate to whom the experience is related.
        /// </summary>
        [Required(ErrorMessage = "Por favor ingresa el candidato.")]
        public int IdCandidate { get; set; }
        public Candidate Candidate { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        [Display(Name = "Empresa")]
        [StringLength(100, ErrorMessage = "El nombre de la empresa no puede tener más de 100 caracteres.")]
        [Required(ErrorMessage = "Por favor ingresa el nombre de la empresa.")]
        public required string Company { get; set; }

        /// <summary>
        /// Gets or sets the job position.
        /// </summary>
        [Display(Name = "Puesto de Trabajo")]
        [StringLength(100, ErrorMessage = "El puesto de trabajo no puede tener más de 100 caracteres.")]
        [Required(ErrorMessage = "Por favor ingresa el puesto de trabajo.")]
        public required string Job { get; set; }

        /// <summary>
        /// Gets or sets a description of the experience.
        /// </summary>
        [Display(Name = "Descripción")]
        [StringLength(4000, ErrorMessage = "La descripción no puede tener más de 4000 caracteres.")]
        [Required(ErrorMessage = "Por favor ingresa una descripción.")]
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the salary for the experience (numeric format 8,2).
        /// </summary>
        [Display(Name = "Salario")]
        [RegularExpression(@"^\d{1,6}(\.\d{1,2})?$", ErrorMessage = "El salario debe ser un número con hasta 6 dígitos y 2 decimales.")]
        [Required(ErrorMessage = "Por favor ingresa el salario.")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Salary { get; set; }

        /// <summary>
        /// Gets or sets the start date of the experience.
        /// </summary>
        [Display(Name = "Fecha de Inicio")]
        [Required(ErrorMessage = "Por favor ingresa la fecha de inicio.")]
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the experience.
        /// </summary>
        [Display(Name = "Fecha de Finalización")]
        [Required(ErrorMessage = "Por favor ingresa la fecha de finalización.")]
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
