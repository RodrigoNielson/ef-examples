using System;

namespace Ef.Examples.Domain;

public class OrderItem
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Product Product { get; set; }
}