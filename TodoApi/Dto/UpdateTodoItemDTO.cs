using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dto
{
    public class UpdateTodoItemDTO
    {
        [Required]
        public string Field {get; set;}
        [Required]
        public string Value {get; set;}

    }
}