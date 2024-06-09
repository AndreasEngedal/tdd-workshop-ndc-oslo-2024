namespace OrderDiscounts.Tests;

public class OrderDiscountsScenarios
{
    [Fact]
    public void Should_calculate_total_price_with_no_discount()
    {
        var order = new Order();
        order.Items.Add(new OrderItem { Price = 100, Quantity = 2 });
        order.Items.Add(new OrderItem { Price = 50, Quantity = 1 });

        var finalPrice = order.GetFinalPrice();

        Assert.Equal(250, finalPrice);
    }
    [Fact]
    public void Should_calculate_total_price_with_discount()
    {
        var order = new Order();
        order.Items.Add(new OrderItem { Price = 100, Quantity = 2 });
        order.Items.Add(new OrderItem { Price = 50, Quantity = 1 });
        order.Discount = 0.1m;

        var finalPrice = order.GetFinalPrice();

        Assert.Equal(225, finalPrice);
    }
}