namespace Applications.Handler.CandidateExperiences
{
    using Entities;
    using MediatR;
    using General.Interfaces;
    using System.Linq;
    using Applications.Queries.CandidateExperiences;

    /// <summary>
    /// Handles the retrieval of all candidate experiences.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 08/ Octubre / 2023
    /// </remarks>
    public class GetAllExperiences : IRequestHandler<GetAllExperiencesXCandidatesQuery, IEnumerable<CandidateExperience>>
    {
        private readonly ICandidateExperienceDAL _candidateExperienceDAL;

        public GetAllExperiences(ICandidateExperienceDAL candidateExperienceDAL)
        {
            _candidateExperienceDAL = candidateExperienceDAL;
        }

        /// <summary>
        /// Handles the retrieval of all candidate experiences.
        /// </summary>
        /// <param name="request">The query to retrieve all candidate experiences.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of candidate experiences.</returns>
        public async Task<IEnumerable<CandidateExperience>> Handle(GetAllExperiencesXCandidatesQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _candidateExperienceDAL.GetAllExperiencesAsync();

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
                ModifyDate = experience.ModifyDate,
                Candidate = new Candidate
                {
                    // Aquí asigna las propiedades del candidato desde experience.Candidate
                    IdCandidate = experience.Candidate.IdCandidate,
                    Name = experience.Candidate.Name,
                    Surname = experience.Candidate.Surname,
                    Email = experience.Candidate.Email,
                    Birthday = experience.Candidate.Birthday
                }
            }).ToList();
        }
    }
}
