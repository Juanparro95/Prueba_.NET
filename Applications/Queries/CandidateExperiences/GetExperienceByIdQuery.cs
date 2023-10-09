﻿namespace Applications.Queries.CandidateExperiences
{
    using Entities;
    using MediatR;

    /// <summary>
    /// Represents a query to retrieve a candidate experience by its unique identifier.
    /// </summary>
    public record class GetExperienceByIdQuery(int Id) : IRequest<CandidateExperience>;
}
