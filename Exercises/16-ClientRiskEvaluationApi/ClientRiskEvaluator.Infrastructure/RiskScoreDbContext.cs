using Microsoft.EntityFrameworkCore;

namespace ClientRiskEvaluator.Infrastructure;

public class RiskScoreDbContext: DbContext
{
    public RiskScoreDbContext(DbContextOptions<RiskScoreDbContext> options)
        : base(options)
    {
    }

    public DbSet<RiskEvaluation> Evaluations { get; set; }
}