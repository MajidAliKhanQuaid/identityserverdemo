using ApiTBS_MediatR.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTBS_MediatR.Products.Queries
{
    public class GetProductByIdQuery : BaseRequest, IRequest<Product>
    {
        public int Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        public Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult<Product>(HardCode.Products.Find(x => x.Id == request.Id));
        }
    }

}
