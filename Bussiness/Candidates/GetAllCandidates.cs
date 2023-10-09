using Applications.Queries.Candidates;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prueba_Panda_Pe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Candidates
{
    public class GetAllCandidates : IRequestHandler<GetAllCandidatesQuery, IEnumerable<Candidate>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAllCandidates(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Candidate>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _dbContext.Candidates.ToListAsync(cancellationToken);

            return candidates.Select(candidate => new Candidate
            {
                IdCandidate = candidate.IdCandidate,
                Name = candidate.Name,
                Surname = candidate.Surname,
                Birthday = candidate.Birthday,
                Email = candidate.Email,
                InsertDate = candidate.InsertDate
            });
        }
    }
}
