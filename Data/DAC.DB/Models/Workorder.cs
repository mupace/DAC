using System;
using System.Collections.Generic;

namespace DAC.DB.Models
{
    public partial class Workorder
    {
        public Workorder()
        {
            WorkOrderNotes = new HashSet<WorkOrderNote>();
        }

        public Guid Id { get; set; }
        public string Responsible { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<WorkOrderNote> WorkOrderNotes { get; set; }
    }
}
