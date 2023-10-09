namespace Applications.Handler.CandidateExperiences
{
    using MediatR;
    using General.Interfaces;
    using PandaPeUtilidades.Exceptions;
    using Applications.Commands.CandidatesExperiences;

    /// <summary>
    /// Handles the deletion of a candidate's experience.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 08/ Octubre / 2023
    /// </remarks>
    public class DeleteCandidateExperience : IRequestHandler<DeleteExperienceCommand, bool>
    {
        private readonly ICandidateExperienceDAL _candidateExperienceDAL;

        public DeleteCandidateExperience(ICandidateExperienceDAL candidateExperienceDAL)
        {
            _candidateExperienceDAL = candidateExperienceDAL;
        }

        /// <summary>
        /// Handles the deletion of a candidate's experience based on the provided command.
        /// </summary>
        /// <param name="request">The command containing the ID of the experience to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the experience is successfully deleted; otherwise, false.</returns>
        public async Task<bool> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
        {
            bool deleteCandidateExperience = await _candidateExperienceDAL.DeleteExperienceAsync(request.Id);

            return (!deleteCandidateExperience) ? throw new Base($"Hubo un error al eliminar al candidato, por favor inténtalo de nuevo.") : true;
        }
    }
}
