using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Section
    {
        public Ulid Id { get; set; } = Ulid.NewUlid();
        public string Name { get; set; }

        public Ulid SchoolId { get; set; }
        public School School { get; set; }
    }
}
