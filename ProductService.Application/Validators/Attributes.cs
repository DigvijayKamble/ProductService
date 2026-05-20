// C#
using System.ComponentModel.DataAnnotations;

public class Attributes
{
    [Key]
    public Guid AttributeID { get; set; }
    public string AttributeName { get; set; } = string.Empty;
}