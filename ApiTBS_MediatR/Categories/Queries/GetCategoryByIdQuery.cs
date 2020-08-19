using ApiTBS_MediatR.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTBS_MediatR.Categories.Queries
{
    public class GetCategoryByIdQuery : BaseRequest, IRequest<Category>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(HardCode.Categories.Find(x => x.Id == request.Id));
        }
    }
}
