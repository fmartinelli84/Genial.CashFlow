using Genial.CashFlow.Application.Dtos.Queries;
using Genial.Framework.Data;
using Genial.Framework.Exceptions;
using MediatR;

namespace Genial.CashFlow.Application.Queries
{
    public class GetStatementHandler : IRequestHandler<GetStatementQuery, GetStatementQueryResult>
    {
        public GetStatementHandler()
        {
        }

        public async Task<GetStatementQueryResult> Handle(GetStatementQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
