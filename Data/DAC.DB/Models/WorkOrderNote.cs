using System;
using System.Collections.Generic;

namespace DAC.DB.Models
{
    public partial class WorkOrderNote
    {
        public Guid Id { get; set; }
        public Guid WorkOrderId { get; set; }
        public string Note { get; set; } = null!;
        public DateTime CreateDate { get; set; }

        public virtual Workorder WorkOrder { get; set; } = null!;
    }
}
