using System;
using System.Collections.Generic;
using System.Text;

namespace Genial.Framework.Data
{
    public interface ITrackableEntity : IReadOnlyTrackableEntity
    {
        DateTime? ModifiedAtDate { get; set; }
    }
}
