namespace Applications.Handler.CandidateExperiences
{
    using MediatR;
    using global::Models.MSSQL;
    using System;
    using System.Threading.Tasks;
    using Entities;
    using PandaPeUtilidades.Exceptions;
    using General.Interfaces;
    using Applications.Commands.CandidatesExperiences;

    /// <summary>
    /// Handles the updating of a candidate's experience.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 08/ Octubre / 2023
    /// </remarks>
    public class UpdateCandidateExperience : IRequestHandler<UpdateExperienceCommand, CandidateExperience>
    {
        private readonly ICandidateExperienceDAL _candidateExperienceDAL;

        public UpdateCandidateExperience(ICandidateExperienceDAL candidateExperienceDAL)
        {
            _candidateExperienceDAL = candidateExperienceDAL;
        }

        /// <summary>
        /// Handles the updating of a candidate's experience based on the provided command.
        /// </summary>
        /// <param name="request">The command containing the updated experience information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated candidate's experience if successful; otherwise, throws an exception.</returns>
        public async Task<CandidateExperience> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
        {
             var candidateItem = new CandidateExperienceSQL
            {
                IdCandidateExperience = request.Id,
                Company = request.Company,
                Job = request.Job,
                IdCandidate = request.IdCandidate,
                Description = request.Description,
                BeginDate = request.BeginDate,
                EndDate = request.EndDate,
                Salary = request.Salary,
                ModifyDate = DateTime.Now
            };

            bool addNewCandidate = await _candidateExperienceDAL.UpdateExperienceAsync(candidateItem);

            return (!addNewCandidate) ? throw new PandaPeUtilidadesException($"Hubo un error al actualizar la experiencia, por favor intentalo de nuevo.") :
    
            new CandidateExperience
                {
                    Company = candidateItem.Company,
                    Job = candidateItem.Job,
                    IdCandidate = candidateItem.IdCandidate,
                    Description = candidateItem.Description,
                    BeginDate = candidateItem.BeginDate,
                    EndDate = candidateItem.EndDate,
                };
        }    
    }
}
