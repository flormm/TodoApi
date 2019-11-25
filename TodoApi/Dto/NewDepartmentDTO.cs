using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Dto
{
    public class NewDepartmentDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
