namespace OrderDiscounts.Tests;

public class Order
{
    public List<OrderItem> Items { get; set; } = new();
    public decimal Discount { get; set; }

    public decimal GetFinalPrice()
    {
        return 0;
    }
}