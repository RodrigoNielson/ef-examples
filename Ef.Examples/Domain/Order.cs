using System;
using System.Collections.Generic;

namespace Ef.Examples.Domain;

public class Order
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string CustomerName { get; set; }
    public DateTime Date { get; set; }
    public List<OrderItem> Items { get; set; }

    public void AddItem(int quantity, Product product)
    {
        Items ??= [];

        Items.Add(new OrderItem
        {
            Quantity = quantity,
            Product = product
        });
    }
}
