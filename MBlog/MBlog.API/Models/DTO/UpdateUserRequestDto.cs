using System.ComponentModel.DataAnnotations;

namespace MBlog.API.Models.DTO;

public class UpdateUserRequestDto
{
    [Required]
    [MinLength(2, ErrorMessage = "FirstName has to be a minimum of 2 characters!")]
    public string FirstName { get; set; }
    [Required]
    [MinLength(3, ErrorMessage = "LastName has to be a minimum of 3 characters!")]
    public string LastName { get; set; }
}
