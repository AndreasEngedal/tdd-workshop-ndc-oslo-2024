namespace ClientRiskEvaluator.Tests.TestDoubles;

public class InMemoryBlockList : HashSet<string>, IBlockList
{
    public Task<bool> IsBlocked(string email)
    {
        return Task.FromResult(Contains(email));
    }
}