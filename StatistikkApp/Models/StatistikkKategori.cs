using System.ComponentModel.DataAnnotations;

namespace StatistikkApp.Models;

public class StatistikkKategori
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Navn { get; set; } = string.Empty;

    public List<StatistikkData> StatistikkData { get; set; } = new();
}