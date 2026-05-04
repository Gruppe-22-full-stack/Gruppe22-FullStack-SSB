using System.ComponentModel.DataAnnotations;

namespace StatistikkApp.Models;

public class StatistikkData
{
    public int Id { get; set; }

    [Required]
    public int Aar { get; set; }

    [Required]
    public int Verdi { get; set; }

    public int KommuneId { get; set; }
    public Kommune? Kommune { get; set; }

    public int StatistikkKategoriId { get; set; }
    public StatistikkKategori? StatistikkKategori { get; set; }
}