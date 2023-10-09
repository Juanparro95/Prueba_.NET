using Applications.Commands.Candidates;
using AutoMapper;
using Entities;
using MediatR;
using Models.MSSQL;
using Prueba_Panda_Pe.Data;

namespace Business.CandidateExperiences
{
    public class CreateCandidateExperience : IRequestHandler<CreateExperienceCommand, bool>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCandidateExperience(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
        {
            var candidateExperienceItem = _mapper.Map<CandidateExperienceSQL>(request.CandidateExperience);

            _dbContext.CandidateExperience.Add(candidateExperienceItem);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
