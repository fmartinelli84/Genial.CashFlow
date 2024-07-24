using System;
using System.Collections.Generic;
using System.Text;

namespace Genial.Framework.Data
{
    public interface IReadOnlyTrackableEntity
    {
        DateTime? CreatedAtDate { get; set; }
    }
}
