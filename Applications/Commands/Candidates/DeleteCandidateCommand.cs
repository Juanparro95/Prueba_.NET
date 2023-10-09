namespace Applications.Commands.Candidates
{
    using MediatR;

    /// <summary>
    /// Represents a command to delete a candidate.
    /// </summary>
    public record DeleteCandidateCommand(int Id) : IRequest<bool>;
}
