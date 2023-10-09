namespace Applications.Queries.Candidates
{
    using Entities;
    using MediatR;

    /// <summary>
    /// Represents a query to retrieve all candidates.
    /// </summary>
    public class GetAllCandidatesQuery : IRequest<IEnumerable<Candidate>> { }
}
