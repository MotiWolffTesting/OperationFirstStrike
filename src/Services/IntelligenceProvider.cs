using OperationFirstStrike.Core.Interfaces;
using OperationFirstStrike.Core.Models;

namespace OperationFirstStrike.Services
{
    public class IntelligenceProvider : IIntelligenceProvider
    {
        private readonly IntelligenceGenerator _generator;

        public IntelligenceProvider()
        {
            _generator = new IntelligenceGenerator();
        }

        public IntelligenceMessage Generate(Terrorist terrorist)
        {
            return _generator.Generate(terrorist);
        }
    }
}