using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class DepartmentZone
    {
        public long Id { get; set; }
        
        [Required]
         public string Name { get; set; }
        

        
    }
}