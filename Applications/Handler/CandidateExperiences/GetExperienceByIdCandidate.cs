namespace Applications.Handler.CandidateExperiences
{
    using Applications.Queries.CandidateExperiences;
    using Entities;
    using General.Interfaces;
    using MediatR;

    /// <summary>
    /// This class handles the GetExperiencesByIdCandidate query and retrieves candidate experiences by ID.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 08/ Octubre / 2023
    /// </remarks>
    public class GetExperienceByIdCandidate : IRequestHandler<GetExperiencesByIdCandidate, IEnumerable<CandidateExperience>>
    {
        private readonly ICandidateExperienceDAL _candidateExperienceDAL;

        /// <summary>
        /// Initializes a new instance of the GetExperienceByIdCandidate class with the specified ICandidateExperienceDAL.
        /// </summary>
        /// <param name="candidateExperienceDAL">The data access layer for candidate experiences.</param>
        public GetExperienceByIdCandidate(ICandidateExperienceDAL candidateExperienceDAL)
        {
            _candidateExperienceDAL = candidateExperienceDAL;
        }

        /// <summary>
        /// Handles the GetExperiencesByIdCandidate query and retrieves candidate experiences by candidate ID asynchronously.
        /// </summary>
        /// <param name="request">The GetExperiencesByIdCandidate request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of candidate experiences.</returns>
        public async Task<IEnumerable<CandidateExperience>> Handle(GetExperiencesByIdCandidate request, CancellationToken cancellationToken)
        {
            // Use the injected ICandidateExperienceDAL to retrieve experiences by candidate ID asynchronously.
            var candidates = await _candidateExperienceDAL.GetExperiencesByCandidateIdAsync(request.Id);

            // Map the retrieved experiences to a list of CandidateExperience objects.
            return candidates.Select(experience => new CandidateExperience
            {
                IdCandidateExperience = experience.IdCandidateExperience,
                IdCandidate = experience.IdCandidate,
                Company = experience.Company,
                Job = experience.Job,
                Description = experience.Description,
                Salary = experience.Salary,
                BeginDate = experience.BeginDate,
                EndDate = experience.EndDate,
                InsertDate = experience.InsertDate,
                ModifyDate = experience.ModifyDate
            }).ToList();
        }
    }
}
