using Applications.Commands.Candidates;
using Entities;
using MediatR;
using Models.MSSQL;
using Prueba_Panda_Pe.Data;

namespace Business.Candidates
{
    public class CreateCandidate : IRequestHandler<CreateCandidateCommand, Candidate>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateCandidate(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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

            _dbContext.Candidates.Add(candidateItem);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Candidate
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
