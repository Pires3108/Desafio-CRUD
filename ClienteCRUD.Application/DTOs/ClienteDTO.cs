using System.ComponentModel.DataAnnotations;

namespace ClienteCRUD.Application.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string? Endereco { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Cep { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; }
        public int? EquipeId { get; set; }
        public string? EquipeNome { get; set; }
        public bool IsEquipeAdmin { get; set; }
        public bool IsLandTechAdmin { get; set; }
    }

    public class CriarClienteDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(150, ErrorMessage = "Email deve ter no máximo 150 caracteres")]
        public string Email { get; set; } = string.Empty;

        public string? Telefone { get; set; }
        [StringLength(200, ErrorMessage = "Endereço deve ter no máximo 200 caracteres")]
        public string? Endereco { get; set; }
        [StringLength(100, ErrorMessage = "Cidade deve ter no máximo 100 caracteres")]
        public string? Cidade { get; set; }
        [StringLength(2, ErrorMessage = "Estado deve ter no máximo 2 caracteres")]
        public string? Estado { get; set; }
        [StringLength(10, ErrorMessage = "CEP deve ter no máximo 10 caracteres")]
        public string? Cep { get; set; }

        [Required(ErrorMessage = "Usuário é obrigatório")]
        [StringLength(50, ErrorMessage = "Usuário deve ter no máximo 50 caracteres")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
        public string Senha { get; set; } = string.Empty;
        public int? EquipeId { get; set; }
        public string? NovaEquipe { get; set; }
    }

    public class AtualizarClienteDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(150, ErrorMessage = "Email deve ter no máximo 150 caracteres")]
        public string Email { get; set; } = string.Empty;

        public string? Telefone { get; set; }
        [StringLength(200, ErrorMessage = "Endereço deve ter no máximo 200 caracteres")]
        public string? Endereco { get; set; }
        [StringLength(100, ErrorMessage = "Cidade deve ter no máximo 100 caracteres")]
        public string? Cidade { get; set; }
        [StringLength(2, ErrorMessage = "Estado deve ter no máximo 2 caracteres")]
        public string? Estado { get; set; }
        [StringLength(10, ErrorMessage = "CEP deve ter no máximo 10 caracteres")]
        public string? Cep { get; set; }

        [Required(ErrorMessage = "Usuário é obrigatório")]
        [StringLength(50, ErrorMessage = "Usuário deve ter no máximo 50 caracteres")]
        public string Usuario { get; set; } = string.Empty;

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
        public string? Senha { get; set; }
    }

    public class LoginDTO
    {
        [Required(ErrorMessage = "Usuário é obrigatório")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Senha { get; set; } = string.Empty;
    }
} 