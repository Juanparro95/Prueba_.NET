namespace Applications.Handler.Candidates
{
    using Entities;
    using MediatR;
    using General.Interfaces;
    using PandaPeUtilidades.Exceptions;
    using Applications.Queries.Candidates;

    /// <summary>
    /// Handles the retrieval of a candidate by their unique identifier.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 08/ Octubre / 2023
    /// </remarks>
    public class GetCandidateById : IRequestHandler<GetCandidateByIdQuery, Candidate>
    {
        private readonly ICandidateDAL _candidateDAL;

        public GetCandidateById(ICandidateDAL candidateDAL)
        {
            _candidateDAL = candidateDAL;
        }

        /// <summary>
        /// Handles the retrieval of a candidate by their unique identifier based on the provided query.
        /// </summary>
        /// <param name="request">The query to retrieve a candidate by their unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The candidate with the specified unique identifier, or throws an exception if not found.</returns>
        public async Task<Candidate> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateDAL.GetCandidateByIdAsync(request.Id);

            return (candidate == null) ? throw new PandaPeUtilidadesException($"El candidato consultado no está registrado, intenta con otro código.") :
            
                new Candidate
                    {
                        IdCandidate = candidate.IdCandidate,
                        Name = candidate.Name,
                        Surname = candidate.Surname,
                        Birthday = candidate.Birthday,
                        Email = candidate.Email,
                        InsertDate = candidate.InsertDate
                    };
        }
    }
}
