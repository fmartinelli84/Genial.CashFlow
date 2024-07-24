using System;
using System.Collections.Generic;
using System.Text;

namespace Genial.Framework.Data
{
    public interface ITrackableDto : IReadOnlyTrackableDto
    {
        DateTime? ModifiedAtDate { get; set; }
    }
}
