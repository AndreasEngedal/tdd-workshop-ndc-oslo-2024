using System.Configuration;
using System.Reflection;

namespace ParkSubscriptions.ApprovalTests;

public class SubscriptionFeatures
{
    private readonly SubscriptionsManager _manager;

    public SubscriptionFeatures()
    {
        _manager = new SubscriptionsManager();
#if NETCOREAPP
        // Fix: Issue https://github.com/dotnet/runtime/issues/22720
        var configFile = $"{Assembly.GetExecutingAssembly().Location}.config";
        var outputConfigFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
        File.Copy(configFile, outputConfigFile, true);
#endif
    }

    [Fact]
    public Task Gold_subscription()
    {
        const string licensePlate = "aa-bb-cc";
        _manager.AddSubscription(licensePlate,
            new DateTime(2023, 8, 1), "GOLD");

        var result = _manager.ApplyDiscount(licensePlate,
            new DateTime(2023, 8, 17), 10);

        return Verify(result);
    }

    [Fact]
    public Task Partner_subscription()
    {
        const string licensePlate = "aa-01-cc";
        _manager.AddSubscription(licensePlate,
            new DateTime(2023, 8, 1), "partner");

        var result = _manager.ApplyDiscount(licensePlate,
            new DateTime(2023, 8, 17), 10);

        return Verify(result);
    }

    [Fact]
    public Task Economic_subscription()
    {
        const string licensePlate = "aa-02-cc";
        _manager.AddSubscription(licensePlate,
            new DateTime(2023, 8, 1), "Economic");

        var result = _manager.ApplyDiscount(licensePlate,
            new DateTime(2023, 8, 17), 10);

        return Verify(result);
    }

    [Fact]
    public Task Partner_subscription_for_AA_BB_11()
    {
        const string licensePlate = "AA-BB-11";
        _manager.AddSubscription(licensePlate,
            new DateTime(2023, 8, 1), "partner");

        var result = _manager.ApplyDiscount(licensePlate,
            new DateTime(2023, 8, 17), 10);

        return Verify(result);
    }

    [Fact]
    public Task Weekends_subscription()
    {
        const string licensePlate = "aa-03-cc";
        _manager.AddSubscription(licensePlate,
            new DateTime(2023, 8, 1), "Weekends");

        var result = _manager.ApplyDiscount(licensePlate,
            new DateTime(2023, 8, 12), 10);

        return Verify(result);
    }

    [Fact]
    public Task Weekends_subscription_on_weekday()
    {
        const string licensePlate = "aa-03-cc";
        _manager.AddSubscription(licensePlate,
            new DateTime(2023, 8, 1), "Weekends");

        var result = _manager.ApplyDiscount(licensePlate,
            new DateTime(2023, 8, 17), 10);

        return Verify(result);
    }
}