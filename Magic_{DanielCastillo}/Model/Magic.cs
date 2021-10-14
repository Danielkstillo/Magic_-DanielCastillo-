using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Magic__DanielCastillo_.Model
{
    public class Magic
    {
        [Key]
        public String FuturoId { get; set; }

        public String Vision { get; set; }

        [Url]
        public String Imagen { get; set; }

    }
}
