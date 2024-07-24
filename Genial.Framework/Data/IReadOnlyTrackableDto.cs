using System;
using System.Collections.Generic;
using System.Text;

namespace Genial.Framework.Data
{
    public interface IReadOnlyTrackableDto
    {
        DateTime? CreatedAtDate { get; set; }
    }
}
