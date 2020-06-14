using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Dto
{
    public class JobDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CreatedAt { get; set; }

        public string ExpiresAt { get; set; }
    }
}
