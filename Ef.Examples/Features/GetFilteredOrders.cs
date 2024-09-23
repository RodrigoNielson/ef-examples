using FastEndpoints;
using System.Threading.Tasks;
using System.Threading;
using Ef.Examples.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Ef.Examples.Features;

public class GetFilteredOrders(SalesContext SalesContext) : Endpoint<GetFilteredOrdersRequest, IEnumerable<GetFilteredOrdersResult>>
{
    public override void Configure()
    {
        Get("/orders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetFilteredOrdersRequest request, CancellationToken cancellationToken)
    {
        var orders = SalesContext.Orders
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Code))
            orders = orders.Where(c => c.Code == request.Code);

        if (!string.IsNullOrWhiteSpace(request.CustomerName))
            orders = orders.Where(c => c.Code == request.CustomerName);

        var result = orders.Select(c => new GetFilteredOrdersResult(c.Code, c.CustomerName, c.Date));

        await SendOkAsync(result, cancellationToken);
    }
}

public class GetFilteredOrdersRequest
{
    [QueryParam]
    public string Code { get; set; }
    [QueryParam]
    public string CustomerName { get; set; }
}

public record GetFilteredOrdersResult(string Code, string CustomerName, DateTime Date);