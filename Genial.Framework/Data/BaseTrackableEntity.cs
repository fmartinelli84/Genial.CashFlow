using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Genial.Framework.Data
{
    public abstract class BaseTrackableEntity : BaseReadonlyTrackableEntity, ITrackableEntity
    {
        public virtual DateTime? ModifiedAtDate { get; set; }
    }
}
