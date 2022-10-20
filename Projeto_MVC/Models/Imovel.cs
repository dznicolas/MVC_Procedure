using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Projeto_MVC.Models
{
    [Table("Imovel")]
    public class Imovel
    {
        [Column("ImovelId")]
        [Display(Name = "ImovelId")]
        public int ImovelId { get; set; }

        [Column("ClienteId")]
        [Display(Name = "ClienteId")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [Column("Finalidade")]
        [Display(Name = "Finalidade")]
        public string? Finalidade { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [Column("Valor")]
        [Display(Name = "Valor")]
        public decimal? Valor { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column("DataCadastro")]
        [Display(Name = "DataCadastro")]
        public DateTime? DataCadastro { get; set; }

        public string DataFormat
        {
            get
            {
                return string.Format("{0:dd/MM/yyyy}", DataCadastro);
            }
        }

    }
}
