namespace Applications.Commands.CandidatesExperiences
{
    using Entities;
    using MediatR;

    /// <summary>
    /// Represents a command to create a new candidate experience.
    /// </summary>
    public record CreateExperienceCommand(int IdCandidate, string Company, string Job, string Description, decimal Salary, DateTime BeginDate, DateTime? EndDate) : IRequest<CandidateExperience>;
}
