using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Application.Dtos.Queries
{
    public class GetBalanceQuery : AccountIdentificationParameterDto, IRequest<GetBalanceQueryResult>
    {
    }

    public class GetBalanceQueryResult : AccountIdentificationResultDto
    {
        public decimal BalanceValue { get; set; }
    }
}
