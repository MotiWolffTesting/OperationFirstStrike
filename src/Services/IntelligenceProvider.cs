using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Services
{
    // Provides intelligence information about terrorists by generating intelligence messages
    public class IntelligenceProvider : IIntelligenceProvider
    {
        // Generator for creating intelligence messages
        private readonly IntelligenceGenerator _generator;

        // Initializes the provider with a new intelligence generator
        public IntelligenceProvider()
        {
            _generator = new IntelligenceGenerator();
        }

        // Generates an intelligence message for a specific terrorist
        // Returns a new intelligence message containing target information
        public IntelligenceMessage Generate(Terrorist terrorist)
        {
            return _generator.Generate(terrorist);
        }
    }
}