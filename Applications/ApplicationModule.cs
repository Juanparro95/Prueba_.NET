namespace Applications
{
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// This is an application module that provides an extension method for IServiceCollection.
    /// Modules are a way to organize and configure services in an application.
    /// </summary>
    public static class ApplicationModule
    {
        /// <summary>
        /// Adds candidate-related modules to the IServiceCollection.
        /// </summary>
        /// <param name="serviceCollection">The IServiceCollection to which modules will be added.</param>
        /// <returns>The updated IServiceCollection with candidate-related modules added.</returns>
        public static IServiceCollection AddCandidateModules(this IServiceCollection serviceCollection)
        {
            // Adds MediatR to the service collection. MediatR is a library that helps implement design patterns
            // like CQRS (Command Query Responsibility Segregation) and Mediator Pattern.
            // typeof(ApplicationModule) is used to scan assemblies and find command handlers and request handlers
            // associated with it.
            serviceCollection.AddMediatR(typeof(ApplicationModule));

            // Returns the service collection updated with candidate-related modules added.
            return serviceCollection;
        }
    }
}
