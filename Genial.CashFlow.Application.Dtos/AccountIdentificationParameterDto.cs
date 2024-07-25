using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Application.Dtos
{
    public class AccountIdentificationParameterDto
    {
        public string CustomerDocument { get; set; } = null!;

        public string AgencyNumber { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
    }
}
