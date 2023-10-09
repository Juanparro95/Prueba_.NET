namespace Applications.Handler.CandidateExperiences
{
    using PandaPeUtilidades.Exceptions;
    using MediatR;
    using Entities;
    using General.Interfaces;
    using global::Models.MSSQL;
    using Applications.Commands.CandidatesExperiences;

    /// <summary>
    /// Handles the creation of a new candidate experience.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 08/ Octubre / 2023
    /// </remarks>
    public class CreateCandidateExperience : IRequestHandler<CreateExperienceCommand, CandidateExperience>
    {
        private readonly ICandidateExperienceDAL _candidateExperienceDAL;

        public CreateCandidateExperience(ICandidateExperienceDAL candidateExperienceDAL)
        {
            _candidateExperienceDAL = candidateExperienceDAL;
        }

        /// <summary>
        /// Handles the creation of a new candidate experience based on the provided command.
        /// </summary>
        /// <param name="request">The command containing information for creating a new experience.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created candidate experience.</returns>
        public async Task<CandidateExperience> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
        {
            if (request.EndDate != null && request.EndDate < request.BeginDate)
            {
                throw new Base("La fecha final de la experiencia no puede ser inferior a la fecha de inicio.");
            }

            var candidateExperienceItem = new CandidateExperienceSQL
            {
                Company = request.Company,
                Job = request.Job,
                Salary = request.Salary,
                IdCandidate = request.IdCandidate,
                Description = request.Description,
                BeginDate = request.BeginDate,
                EndDate = (DateTime)request.EndDate,
                InsertDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };

            bool addNewCandidate = await _candidateExperienceDAL.AddExperienceAsync(candidateExperienceItem);

            return (!addNewCandidate) ? throw new Base("Hubo un error al registrar un nuevo candidato, por favor intentalo de nuevo.") :                 
                new CandidateExperience
                    {
                        IdCandidateExperience = candidateExperienceItem.IdCandidateExperience,
                        Company = candidateExperienceItem.Company,
                        Job = candidateExperienceItem.Job, 
                        IdCandidate = candidateExperienceItem.IdCandidate,
                        Description = candidateExperienceItem.Description, 
                        BeginDate = candidateExperienceItem.BeginDate,  
                        EndDate = candidateExperienceItem.EndDate,  
                    };
        }
    }
}
