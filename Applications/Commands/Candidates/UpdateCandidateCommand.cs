namespace Applications.Commands.Candidates
{
    using Entities;
    using MediatR;

    /// <summary>
    /// Represents a command to update candidate information.
    /// </summary>
    public record UpdateCandidateCommand(
        int Id,
        string Name,
        string Surname,
        DateTime Birthday,
        string Email,
        bool IsCompleted
    ) : IRequest<Candidate>;
}
