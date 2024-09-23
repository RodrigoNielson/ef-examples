using FastEndpoints;
using System.Threading.Tasks;
using System.Threading;
using System;
using Ef.Examples.Infrastructure;

namespace Ef.Examples.Features;

public class CreateProduct(SalesContext SalesContext) : Endpoint<CreateProductRequest>
{
    public override void Configure()
    {
        Post("/product");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Domain.Product
        {
            Description = request.Description,
            Name = request.Name,
            Price = request.Price
        };

        SalesContext.Products.Add(product);

        await SalesContext.SaveChangesAsync(cancellationToken);

        await SendOkAsync(product.Id, cancellationToken);
    }
}

public record CreateProductRequest(string Description, string Name, decimal Price);
