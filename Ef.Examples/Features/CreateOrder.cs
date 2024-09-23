using Ef.Examples.Infrastructure;
using FastEndpoints;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ef.Examples.Features;

public class CreateOrder(SalesContext SalesContext) : Endpoint<CreateOrderRequest>
{
    public override void Configure()
    {
        Post("/order");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = new Domain.Order
        {
            Code = request.Code,
            CustomerName = request.CustomerName,
            Date = DateTime.Now
        };

        SalesContext.Orders.Add(order); 

        await SalesContext.SaveChangesAsync(cancellationToken);

        await SendOkAsync(order.Id, cancellationToken);
    }
}

public record CreateOrderRequest(string Code, string CustomerName);
