using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_MVC.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Column("Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [Column("NomeCliente")]
        [Display(Name = "NomeCliente")]
        public string? NomeCliente { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [Column("TipoCliente")]
        [Display(Name = "TipoCliente")]
        public string? TipoCliente { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [Column("NomeContato")]
        [Display(Name = "NomeContato")]
        public string? NomeContato { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [Column("TelefoneContato")]
        [Display(Name = "TelefoneContato")]
        public string? TelefoneContato { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [Column("Cidade")]
        [Display(Name = "Cidade")]
        public string? Cidade { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [Column("Bairro")]
        [Display(Name = "Bairro")]
        public string? Bairro { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [Column("Logradouro")]
        [Display(Name = "Logradouro")]
        public string? Logradouro { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Column("DataCadastro")]
        [Display(Name = "DataCadastro")]
        public DateTime DataCadastro { get; set; }

    }
}
