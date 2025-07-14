using System.ComponentModel.DataAnnotations;

namespace ClienteCRUD.Core.Entities
{
    public class Equipe
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;
        
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        
        // Navegação para funcionários
        public virtual ICollection<Cliente> Funcionarios { get; set; } = new List<Cliente>();
    }
} 