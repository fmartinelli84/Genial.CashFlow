using Genial.Framework.Data;

namespace Genial.CashFlow.Application.Dtos
{
    public class BalanceDto : BaseTrackableDto
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Value { get; set; }
    }
}