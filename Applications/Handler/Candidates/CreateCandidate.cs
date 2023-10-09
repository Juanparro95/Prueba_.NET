namespace Applications.Handler.Candidates
{
    using Entities;
    using MediatR;
    using global::Models.MSSQL;
    using PandaPeUtilidades.Exceptions;
    using General.Interfaces;
    using Applications.Commands.Candidates;

    /// <summary>
    /// Handles the creation of a new candidate.
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 08/ Octubre / 2023
    /// </remarks>
    public class CreateCandidate : IRequestHandler<CreateCandidateCommand, Candidate>
    {
        private readonly ICandidateDAL _candidateDAL;

        public CreateCandidate(ICandidateDAL candidateDAL)
        {
            _candidateDAL = candidateDAL;
        }

        /// <summary>
        /// Handles the creation of a new candidate based on the provided command.
        /// </summary>
        /// <param name="request">The command containing the candidate's information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created candidate if successful; otherwise, throws an exception.</returns>
        public async Task<Candidate> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidateItem = new CandidateSQL
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Birthday = request.Birthday,
                InsertDate = DateTime.Now,
                ModifyDate = DateTime.Now,
            };

            bool addNewCandidate = await _candidateDAL.AddCandidateAsync(candidateItem);

            return (!addNewCandidate) ? throw new PandaPeUtilidadesException("Hubo un error al registrar un nuevo candidato, por favor intentalo de nuevo.") : 
                new Candidate
                    {
                        IdCandidate = candidateItem.IdCandidate,
                        Name = candidateItem.Name,
                        Surname = candidateItem.Surname,
                        Email = candidateItem.Email,    
                        Birthday = candidateItem.Birthday,  
                        InsertDate = candidateItem.InsertDate,
                        ModifyDate = candidateItem.ModifyDate, 
                    };
        }
    }
}
