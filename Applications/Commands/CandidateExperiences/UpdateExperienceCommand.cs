namespace Applications.Commands.CandidatesExperiences
{
    using Entities;
    using MediatR;

    /// <summary>
    /// Represents a command to update a candidate experience.
    /// </summary>
    public record UpdateExperienceCommand(
        int Id,
        int IdCandidate,
        string Company,
        string Job,
        string Description,
        decimal Salary,
        DateTime BeginDate,
        DateTime EndDate
    ) : IRequest<CandidateExperience>;
}
