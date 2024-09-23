using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Ef.Examples.Infrastructure;
using System.Linq;

namespace Ef.Examples.Features;

public class GetProductReport(SalesContext SalesContext) : Endpoint<GetProductReportRequest, IEnumerable<GetProductReportResult>>
{
    public override void Configure()
    {
        Get("/{productId}/report");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductReportRequest request, CancellationToken cancellationToken)
    {
        var product = SalesContext.Products
            .AsNoTracking()
            .Where(c => c.Id == request.ProductId);

        var orderItems = SalesContext.OrderItems
            .AsNoTracking();

        var result = product.Join(orderItems,
            c => c.Id,
            c => c.Product.Id,
            (product, orderItem) => new GetProductReportResult(
                product.Name,
                orderItem.Quantity
            ));
        
        
        await SendOkAsync(result, cancellationToken);
    }
}

public record GetProductReportRequest(Guid ProductId);

public record GetProductReportResult(string ProductName, decimal Quantity);