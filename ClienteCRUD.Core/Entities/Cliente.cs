using System.ComponentModel.DataAnnotations;

namespace ClienteCRUD.Core.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;
        
        [StringLength(20)]
        public string? Telefone { get; set; }
        
        [StringLength(200)]
        public string? Endereco { get; set; }
        
        [StringLength(100)]
        public string? Cidade { get; set; }
        
        [StringLength(2)]
        public string? Estado { get; set; }
        
        [StringLength(10)]
        public string? Cep { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Usuario { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Senha { get; set; } = string.Empty;
        
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; } = true;
        
        // Campos relacionados à equipe
        public int? EquipeId { get; set; }
        public virtual Equipe? Equipe { get; set; }
        public bool IsEquipeAdmin { get; set; } = false;
        
        // Campo especial para Land Tech
        public bool IsLandTechAdmin { get; set; } = false;
        
        // Campos de auditoria
        public string? CriadoPor { get; set; }
        public string? AtualizadoPor { get; set; }
    }
} 