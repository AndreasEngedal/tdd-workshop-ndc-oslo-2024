namespace ClientRiskEvaluator.Infrastructure;

public class PostgresClientRiskEvaluationStore : IClientRiskEvaluationStore
{
    private readonly RiskScoreDbContext _dbContext;

    public PostgresClientRiskEvaluationStore(RiskScoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(RiskEvaluation riskEvaluation)
    {
        await _dbContext.AddAsync(riskEvaluation);
        await _dbContext.SaveChangesAsync();
    }
}