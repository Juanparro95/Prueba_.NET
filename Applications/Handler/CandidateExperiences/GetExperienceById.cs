namespace Applications.Handler.CandidateExperiences
{
    using Entities;
    using MediatR;
    using General.Interfaces;
    using PandaPeUtilidades.Exceptions;
    using Applications.Queries.CandidateExperiences;

    /// <summary>
    /// Handles the retrieval of a candidate's experience by its unique identifier.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 08/ Octubre / 2023
    /// </remarks>
    public class GetExperienceById : IRequestHandler<GetExperienceByIdQuery, CandidateExperience>
    {
        private readonly ICandidateExperienceDAL _candidateExperienceDAL;

        public GetExperienceById(ICandidateExperienceDAL candidateExperienceDAL)
        {
            _candidateExperienceDAL = candidateExperienceDAL;
        }

        /// <summary>
        /// Handles the retrieval of a candidate's experience based on the provided query.
        /// </summary>
        /// <param name="request">The query containing the ID of the experience to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The candidate's experience if found; otherwise, throws an exception.</returns>
        public async Task<CandidateExperience> Handle(GetExperienceByIdQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateExperienceDAL.GetExperienceByIdAsync(request.Id);

            return candidate == null
                ? throw new PandaPeUtilidadesException($"La experiencia consultada no está registrada, intenta con otro código.")
                : new CandidateExperience
                {
                    Company = candidate.Company,
                    Job = candidate.Job,
                    Salary = candidate.Salary,
                    IdCandidate = candidate.IdCandidate,
                    Description = candidate.Description,
                    BeginDate = candidate.BeginDate,
                    EndDate = candidate.EndDate,
                };
        }
    }
}
