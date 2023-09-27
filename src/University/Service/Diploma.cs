using System.ComponentModel.DataAnnotations;

namespace University.Service;

public class Diploma
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string DiplomaTitle { get; set; } = string.Empty;
    public string DiplomaSpecialisation { get; set; } = string.Empty;
    public string DiplomaIssuedDate { get; set; } = string.Empty;
}
