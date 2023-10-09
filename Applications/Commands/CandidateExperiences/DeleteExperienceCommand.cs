namespace Applications.Commands.CandidatesExperiences
{
    using MediatR;

    /// <summary>
    /// Represents a command to delete a candidate experience by its unique identifier.
    /// </summary>
    public record DeleteExperienceCommand(int Id) : IRequest<bool>;
}
