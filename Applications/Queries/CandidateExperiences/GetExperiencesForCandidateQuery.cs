namespace Applications.Queries.CandidateExperiences
{
    using Entities;
    using MediatR;

    /// <summary>
    /// Represents a query to retrieve all candidate experiences.
    /// </summary>
    public record GetAllExperiencesXCandidatesQuery : IRequest<IEnumerable<CandidateExperience>>;
}
