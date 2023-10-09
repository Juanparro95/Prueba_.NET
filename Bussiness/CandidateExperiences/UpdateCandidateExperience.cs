using Applications.Commands.Candidates;
using AutoMapper;
using MediatR;
using Models.MSSQL;
using Prueba_Panda_Pe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CandidateExperiences
{
    public class UpdateCandidateExperience : IRequestHandler<UpdateExperienceCommand, bool>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateCandidateExperience(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
        {
            var candidateExperienceItem = _mapper.Map<CandidateExperienceSQL>(request.CandidateExperience);

            _dbContext.CandidateExperience.Update(candidateExperienceItem);
            return (await _dbContext.SaveChangesAsync(cancellationToken) > 0);
        }    
    }
}
