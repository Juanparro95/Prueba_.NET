using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public static class CandidateModule
    {
        public static IServiceCollection AddCandidateModule(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(CandidateModule));
            return serviceCollection;
        }
    }
}