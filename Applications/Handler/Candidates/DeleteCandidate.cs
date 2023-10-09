namespace Applications.Handler.Candidates
{
    using MediatR;
    using General.Interfaces;
    using PandaPeUtilidades.Exceptions;
    using Applications.Commands.Candidates;

    /// <summary>
    /// Handles the deletion of a candidate.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 08/ Octubre / 2023
    /// </remarks>
    public class DeleteCandidate : IRequestHandler<DeleteCandidateCommand, bool>
    {
        private readonly ICandidateDAL _candidateDAL;

        public DeleteCandidate(ICandidateDAL candidateDAL)
        {
            _candidateDAL = candidateDAL;
        }

        /// <summary>
        /// Handles the deletion of a candidate based on the provided command.
        /// </summary>
        /// <param name="request">The command containing the candidate's ID to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the deletion was successful; otherwise, throws an exception.</returns>
        public async Task<bool> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            bool deleteCandidate = await _candidateDAL.DeleteCandidateAsync(request.Id);
            return (!deleteCandidate) ? throw new Base($"Hubo un error al eliminar al candidato, por favor intentalo de nuevo.") : true;
        }
    }
}
