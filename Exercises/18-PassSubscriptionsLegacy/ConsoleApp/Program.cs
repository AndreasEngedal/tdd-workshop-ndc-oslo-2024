using ParkSubscriptions;

var manager = new SubscriptionsManager();

manager.AddSubscription("aa-bb-11", new DateTime(2023, 8, 1), "partner");

var res = manager.ApplyDiscount("aa-bb-11", new DateTime(2023, 8, 17), 10);

Console.WriteLine($"Price after discount: {res}");