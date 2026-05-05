using System.ComponentModel.DataAnnotations;

namespace StatistikkApp.Models
{
    public class StatistikkData
    {
        public int Id { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int Aar { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Verdi { get; set; }

        [Required]
        public int KommuneId { get; set; }

        public Kommune? Kommune { get; set; }

        [Required]
        public int StatistikkKategoriId { get; set; }

        public StatistikkKategori? StatistikkKategori { get; set; }
    }
}