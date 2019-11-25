using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dto
{
    public class NewTodoItemDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool? IsComplete { get; set; }
    }
}