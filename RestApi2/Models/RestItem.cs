using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApi2.Models
{
    public class RestItem
    {
        [Key]   //assume identity
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Nazwa_produktu { get; set; }

        [Required]
        [Column(TypeName = "tinyint")]
        public byte Ilosc { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool Zakupiony { get; set; }
    }
}

