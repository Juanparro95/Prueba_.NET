using Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Applications.Commands.Candidates;
using Applications.Queries.Candidates;
using Applications.Queries.CandidateExperiences;
/// <summary>
/// Represents a controller for managing candidate-related operations through HTTP endpoints.
/// </summary>
/// <remarks>
///     Author: Juan David Parroquiano 
///     Date: 08/ Octubre / 2023
/// </remarks>
[Route("api/[controller]")]
[ApiController]
public class CandidateController : Controller
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CandidateController"/> class.
    /// </summary>
    /// <param name="mediator">The Mediator service for handling application requests and responses.</param>
    public CandidateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a list of all candidates.
    /// </summary>
    /// <returns>An asynchronous operation that returns a collection of candidate data.</returns>
    [HttpGet]
    public async Task<IEnumerable<Candidate>> GetAll() => await _mediator.Send(new GetAllCandidatesQuery());

    /// <summary>
    /// Gets a candidate by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the candidate to retrieve.</param>
    /// <returns>An asynchronous operation that returns the candidate data or a NotFound response if the candidate does not exist.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Candidate>> GetById(int id)
    {
        var query = new GetCandidateByIdQuery(id);
        var taskCandidate = await _mediator.Send(query);

        if (taskCandidate == null)
        {
            return NotFound();
        }

        return taskCandidate;
    }

    /// <summary>
    /// Creates a new candidate.
    /// </summary>
    /// <param name="createCandidateCommand">The command for creating a new candidate.</param>
    /// <returns>An asynchronous operation that returns the created candidate data and a CreatedAtAction response with the candidate's unique identifier.</returns>
    [HttpPost]
    public async Task<ActionResult<Candidate>> Create(CreateCandidateCommand createCandidateCommand)
    {
        var candidateItem = await _mediator.Send(createCandidateCommand);
        return CreatedAtAction(nameof(candidateItem), new { id = candidateItem.IdCandidate }, candidateItem);
    }

    /// <summary>
    /// Updates an existing candidate.
    /// </summary>
    /// <param name="id">The unique identifier of the candidate to update.</param>
    /// <param name="updateCandidateCommand">The command for updating an existing candidate.</param>
    /// <returns>An asynchronous operation that returns a NoContent response for a successful update, BadRequest for invalid input, or NotFound for a candidate that does not exist.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCandidateCommand updateCandidateCommand)
    {
        if (id != updateCandidateCommand.Id)
        {
            return BadRequest();
        }

        var candidateItem = await _mediator.Send(updateCandidateCommand);

        if (candidateItem == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Deletes a candidate by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the candidate to delete.</param>
    /// <returns>An asynchronous operation that returns a NoContent response for a successful deletion or NotFound for a candidate that does not exist.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteCandidateCommand(id));

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
