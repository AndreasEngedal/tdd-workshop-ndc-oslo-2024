namespace ClientRiskEvaluator.Tests.TestDoubles;

internal class NotInBlockListStub : IBlockList
{
    public Task<bool> IsBlocked(string email)
    {
        return Task.FromResult(false);
    }
}