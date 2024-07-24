using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Genial.Framework.Data
{
    public abstract class BaseReadonlyTrackableEntity : IReadOnlyTrackableEntity
    {
        public virtual DateTime? CreatedAtDate { get; set; }
    }
}
