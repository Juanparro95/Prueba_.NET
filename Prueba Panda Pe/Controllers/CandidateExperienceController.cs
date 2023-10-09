using Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Applications.Commands.CandidatesExperiences;
using Applications.Queries.CandidateExperiences;
/// <summary>
/// Represents a controller for managing candidate experiences-related operations through HTTP endpoints.
/// <remarks>
///     Author: Juan David Parroquiano 
///     Date: 08/ Octubre / 2023
/// </remarks>
[Route("api/[controller]")]
[ApiController]
public class CandidateExperienceController : Controller
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CandidateExperienceController"/> class.
    /// </summary>
    /// <param name="mediator">The Mediator service for handling application requests and responses.</param>
    public CandidateExperienceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a list of all candidate experiences.
    /// </summary>
    /// <returns>An asynchronous operation that returns a collection of candidate experience data.</returns>
    [HttpGet]
    public async Task<IEnumerable<CandidateExperience>> GetAll() => await _mediator.Send(new GetAllExperiencesXCandidatesQuery());

    /// <summary>
    /// Gets a candidate experience by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the candidate experience to retrieve.</param>
    /// <returns>An asynchronous operation that returns the candidate experience data or a NotFound response if the candidate experience does not exist.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CandidateExperience>> GetById(int id)
    {
        var query = new GetExperienceByIdQuery(id);
        var taskCandidate = await _mediator.Send(query);

        if (taskCandidate == null)
        {
            return NotFound();
        }

        return taskCandidate;
    }

    /// <summary>
    /// Gets a candidate by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the candidate to retrieve.</param>
    /// <returns>An asynchronous operation that returns the candidate data or a NotFound response if the candidate does not exist.</returns>
    [HttpGet("experience/{id}")]
    public async Task<IEnumerable<CandidateExperience>> GetByIdCandidate(int id)
    {
        var query = new GetExperiencesByIdCandidate(id);
        var taskCandidate = await _mediator.Send(query);

        return (taskCandidate == null) ? null : taskCandidate;
    }

    /// <summary>
    /// Creates a new candidate experience.
    /// </summary>
    /// <param name="createExperienceCommand">The command for creating a new candidate experience.</param>
    /// <returns>An asynchronous operation that returns the created candidate experience data and a CreatedAtAction response.</returns>
    [HttpPost]
    public async Task<ActionResult<Candidate>> Create(CreateExperienceCommand createExperienceCommand)
    {
        var candidateItem = await _mediator.Send(createExperienceCommand);
        return CreatedAtAction(nameof(candidateItem), candidateItem);
    }

    /// <summary>
    /// Updates an existing candidate experience.
    /// </summary>
    /// <param name="id">The unique identifier of the candidate experience to update.</param>
    /// <param name="updateExperienceCommand">The command for updating an existing candidate experience.</param>
    /// <returns>An asynchronous operation that returns a NoContent response for a successful update, BadRequest for invalid input, or NotFound for a candidate experience that does not exist.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateExperienceCommand updateExperienceCommand)
    {
        if (id != updateExperienceCommand.Id)
        {
            return BadRequest();
        }

        var candidateItem = await _mediator.Send(updateExperienceCommand);

        if (candidateItem == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Deletes a candidate experience by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the candidate experience to delete.</param>
    /// <returns>An asynchronous operation that returns a NoContent response for a successful deletion or NotFound for a candidate experience that does not exist.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteExperienceCommand(id));

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
