using Genial.CashFlow.Application.Dtos.Queries;
using Genial.Framework.Data;
using Genial.Framework.Exceptions;
using MediatR;

namespace Genial.CashFlow.Application.Queries
{
    public class GetBalanceHandler : IRequestHandler<GetBalanceQuery, GetBalanceQueryResult>
    {
        public GetBalanceHandler()
        {
        }

        public async Task<GetBalanceQueryResult> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
