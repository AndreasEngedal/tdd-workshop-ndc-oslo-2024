namespace ClientRiskEvaluator.RiskCalculators;

internal class BlockListRiskCalculator : RiskCalculator
{
    private readonly IBlockList _blockList;

    public BlockListRiskCalculator(IBlockList blockList)
    {
        _blockList = blockList;
    }

    public override async ValueTask<int> CalculateAsync(Client client)
    {
        var isBlocked = await _blockList.IsBlocked(client.Email);

        if (isBlocked) return RiskScores.BlockListed;

        return await base.CalculateAsync(client);
    }
}