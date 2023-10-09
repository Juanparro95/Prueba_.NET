namespace Applications.Commands.Candidates
{
    using MediatR;
    using Entities;

    /// <summary>
    /// Represents a command to create a new candidate.
    /// </summary>
    public record CreateCandidateCommand(
        string Name,
        string Surname,
        DateTime Birthday,
        string Email
    ) : IRequest<Candidate>;
}
