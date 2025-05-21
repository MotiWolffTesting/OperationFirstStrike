namespace IDFOperationFirstStrike.Core.Interfaces
{
    public interface IStrikeExecutor
    {
        bool ExecuteStrike(IStrikeUnit unit, Terrorist target, IntelligenceMessage intel);
    }
}
