using Applications.Commands.Candidates;
using Entities;
using MediatR;
using Prueba_Panda_Pe.Data;

namespace Business.Candidates
{
    public class UpdateCandidate : IRequestHandler<UpdateCandidateCommand, Candidate>
    {
        private readonly ApplicationDbContext _dbContext;

        public UpdateCandidate(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Candidate> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidateSearch = await _dbContext.Candidates.FindAsync(new object[] { request.Id }, cancellationToken);

            if (candidateSearch == null) {
                return null;
            }

            candidateSearch.Name = request.Name;
            candidateSearch.Surname = request.Surname;
            candidateSearch.Email = request.Email;
            candidateSearch.Birthday = request.Birthday;
            candidateSearch.ModifyDate = DateTime.Now;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Candidate
            {
                IdCandidate = candidateSearch.IdCandidate,
                Name = candidateSearch.Name,
                Surname = candidateSearch.Surname,
                Email = candidateSearch.Email,
                Birthday = candidateSearch.Birthday,
                ModifyDate = candidateSearch.ModifyDate,
            };
        }
    }
}
