using System.ComponentModel.DataAnnotations;

namespace University.Service;

public class DiplomaTemplate
{
    [Key]
    public int Id { get; set; }
    public string SchemaUri { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
