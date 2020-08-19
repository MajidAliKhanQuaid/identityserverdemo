using ApiTBS_MediatR.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTBS_MediatR.Products.Queries
{
    public class GetAllProductsQuery : BaseRequest, IRequest<IEnumerable<Product>> { }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        public Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult<IEnumerable<Product>>(HardCode.Products);
        }
    }

}
