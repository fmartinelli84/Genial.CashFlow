using Genial.Framework.Data;

namespace Genial.CashFlow.Application.Dtos
{
    public class TransactionDto : BaseReadOnlyTrackableDto
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public TransactionType Type { get; set; }
        public string Description { get; set; } = null!;
        public decimal Value { get; set; }
    }
}