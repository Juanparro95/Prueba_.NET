namespace Applications.Queries.Candidates
{
    using Entities;
    using MediatR;

    /// <summary>
    /// Represents a query to retrieve a candidate by their unique identifier.
    /// </summary>
    public record class GetCandidateByIdQuery(int Id) : IRequest<Candidate>;
}
