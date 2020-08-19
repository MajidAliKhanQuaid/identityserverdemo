using ApiTBS_MediatR.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTBS_MediatR.Categories.Queries
{
    public class GetAllCategoriesQuery : BaseRequest, IRequest<IEnumerable<Category>>
    {
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
    {
        public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(HardCode.Categories);
        }
    }
}
