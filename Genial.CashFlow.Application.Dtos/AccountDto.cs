using Genial.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Application.Dtos
{
    public class AccountDto : BaseTrackableDto
    {
        public long Id { get; set; }

        public string AgencyNumber { get; set; } = null!;

        public string Number { get; set; } = null!;
    }
}
