namespace Applications.Handler.Candidates
{
    using Entities;
    using MediatR;
    using General.Interfaces;
    using Applications.Queries.Candidates;

    /// <summary>
    /// Handles the retrieval of all candidates.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 08/ Octubre / 2023
    /// </remarks>
    public class GetAllCandidates : IRequestHandler<GetAllCandidatesQuery, IEnumerable<Candidate>>
    {
        private readonly ICandidateDAL _candidateDAL;

        public GetAllCandidates(ICandidateDAL candidateDAL)
        {
            _candidateDAL = candidateDAL;
        }

        /// <summary>
        /// Handles the retrieval of all candidates based on the provided query.
        /// </summary>
        /// <param name="request">The query to retrieve all candidates.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An enumerable collection of candidates with their associated experiences.</returns>
        public async Task<IEnumerable<Candidate>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _candidateDAL.GetAllCandidatesAsync();

            return candidates.Select(candidate => new Candidate
            {
                IdCandidate = candidate.IdCandidate,
                Name = candidate.Name,
                Surname = candidate.Surname,
                Birthday = candidate.Birthday,
                Email = candidate.Email,
                InsertDate = candidate.InsertDate,
                Experiences = candidate.Experiences.Select(experience => new CandidateExperience
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
                    ModifyDate = experience.ModifyDate,
                }).ToList()
            });
        }
    }
}
