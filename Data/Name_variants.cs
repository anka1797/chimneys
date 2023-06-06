using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chimneys.Data
{
    public class Name_variants
    {
        [Key]
        public int Id_var { get; set; }
        public string? Name_var { get; set; }

    }
}
