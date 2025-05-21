namespace OperationFirstStrike.Core.Interfaces
{
    public interface IIntelligenceProvider
    {
        IntelligenceMessage Generate(Terrorist terrorist);
    }
}