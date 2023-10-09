namespace Applications.Handler.Candidates
{
    using Entities;
    using MediatR;
    using General.Interfaces;
    using global::Models.MSSQL;
    using PandaPeUtilidades.Exceptions;
    using Applications.Commands.Candidates;

    /// <summary>
    /// Handles the update of a candidate's information.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 08/ Octubre / 2023
    /// </remarks>
    public class UpdateCandidate : IRequestHandler<UpdateCandidateCommand, Candidate>
    {
        private readonly ICandidateDAL _candidateDAL;

        public UpdateCandidate(ICandidateDAL candidateDAL)
        {
            _candidateDAL = candidateDAL;
        }

        /// <summary>
        /// Handles the update of a candidate's information based on the provided command.
        /// </summary>
        /// <param name="request">The command to update a candidate's information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated candidate's information, or throws an exception if the update fails.</returns>
        public async Task<Candidate> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {

            var candidateItem = new CandidateSQL
            {
                IdCandidate = request.Id,
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Birthday = request.Birthday,
                InsertDate = DateTime.Now,
                ModifyDate = DateTime.Now,
            };

            bool addNewCandidate = await _candidateDAL.UpdateCandidateAsync(candidateItem);

            return (!addNewCandidate) ? throw new PandaPeUtilidadesException($"Hubo un error al actualizar al candidato {request.Name}, por favor intentalo de nuevo.") :
                new Candidate
                    {
                        IdCandidate = candidateItem.IdCandidate,
                        Name = candidateItem.Name,
                        Surname = candidateItem.Surname,
                        Email = candidateItem.Email,
                        Birthday = candidateItem.Birthday,
                        ModifyDate = candidateItem.ModifyDate,
                    };
        }
    }
}
