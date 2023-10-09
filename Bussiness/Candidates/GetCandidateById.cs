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
    public class GetCandidateById : IRequestHandler<GetCandidateByIdQuery, Candidate>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCandidateById(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Candidate> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _dbContext.Candidates.FindAsync(new object[] {request.Id}, cancellationToken);

            if (candidate == null)
            {
                return null;
            }

            return new Candidate
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
