using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MembroStartup
    {
        [Key]
        public int Id { get; set; }

        public int MembroId { get; set; }
        public virtual Membro Membro { get; set; }

        public int StartupId { get; set; }
        public virtual Startup Startup { get; set; }
    }
}
