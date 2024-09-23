using FastEndpoints;
using System.Threading.Tasks;
using System.Threading;
using System;
using Ef.Examples.Infrastructure;
using System.Linq;
using System.Net;

namespace Ef.Examples.Features;

public class AddOrderItem(SalesContext SalesContext) : Endpoint<AddOrderItemRequest>
{
    public override void Configure()
    {
        Post("/order/item");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddOrderItemRequest request, CancellationToken cancellationToken)
    {
        var order = SalesContext.Orders.FirstOrDefault(c => c.Id ==  request.OrderId);  

        if (order == null)
        {
            await SendAsync("Order doesn't exist", (int)HttpStatusCode.BadRequest, cancellationToken);
            return;
        }

        var product = SalesContext.Products.FirstOrDefault(c => c.Id == request.ProductId);

        if (product == null)
        {
            await SendAsync("Order doesn't exist", (int)HttpStatusCode.BadRequest, cancellationToken);
            return;
        }

        order.AddItem(request.quantity, product);

        await SalesContext.SaveChangesAsync(cancellationToken);

        await SendOkAsync(order.Id, cancellationToken);
    }
}

public record AddOrderItemRequest(Guid OrderId, int quantity, Guid ProductId);
