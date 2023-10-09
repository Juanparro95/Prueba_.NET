using Applications.Commands.Candidates;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prueba_Panda_Pe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CandidateExperiences
{
    public class DeleteCandidateExperience : IRequestHandler<DeleteExperienceCommand, bool>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteCandidateExperience(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
        {
            var candidateSearch = await _dbContext.CandidateExperience.FindAsync(new object[] { request.Id }, cancellationToken);

            if (candidateSearch == null)
            {
                return false;
            }

            _dbContext.CandidateExperience.Remove(candidateSearch);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
