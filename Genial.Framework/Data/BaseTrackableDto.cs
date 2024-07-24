using System;
using System.Collections.Generic;
using System.Text;

namespace Genial.Framework.Data
{
    public abstract class BaseTrackableDto : BaseReadOnlyTrackableDto, ITrackableDto
    {
        public DateTime? ModifiedAtDate { get; set; }
    }
}
