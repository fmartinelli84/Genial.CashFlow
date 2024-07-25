using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Application.Dtos
{
    public class AccountIdentificationResultDto
    {
        public CustomerDto Customer { get; set; } = null!;
        public AccountDto Account { get; set; } = null!;
    }
}
