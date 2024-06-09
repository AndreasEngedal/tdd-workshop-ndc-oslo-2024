namespace ClientRiskEvaluator;

public interface IBlockList
{
    Task<bool> IsBlocked(string email);
}