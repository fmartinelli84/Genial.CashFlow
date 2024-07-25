using Genial.Framework.Data;
using System.ComponentModel.DataAnnotations;

namespace Genial.CashFlow.Application.Dtos
{
    public class CustomerDto : BaseTrackableDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Document { get; set; } = null!;
    }
}