using Applications.Commands.Candidates;
using Entities;
using MediatR;
using Prueba_Panda_Pe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Candidates
{
    public class DeleteCandidate : IRequestHandler<DeleteCandidateCommand, bool>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteCandidate(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidateSearch = await _dbContext.Candidates.FindAsync(new object[] { request.Id }, cancellationToken);

            if (candidateSearch == null)
            {
                return false;
            }

            _dbContext.Candidates.Remove(candidateSearch);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
