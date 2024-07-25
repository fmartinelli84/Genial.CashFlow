using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Application.Dtos.Queries
{
    public class GetStatementQuery : AccountIdentificationParameterDto, IRequest<GetStatementQueryResult>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class GetStatementQueryResult : AccountIdentificationResultDto
    {
        public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
    }
}
