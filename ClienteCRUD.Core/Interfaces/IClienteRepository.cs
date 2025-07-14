using ClienteCRUD.Core.Entities;

namespace ClienteCRUD.Core.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObterTodosAsync();
        Task<IEnumerable<Cliente>> ObterPorEquipeAsync(int equipeId);
        Task<Cliente?> ObterPorIdAsync(int id);
        Task<Cliente?> ObterPorEmailAsync(string email);
        Task<Cliente?> ObterPorUsuarioAsync(string usuario);
        Task<Cliente?> ObterInativoPorIdAsync(int id);
        Task<Cliente> AdicionarAsync(Cliente cliente);
        Task<Cliente> AtualizarAsync(Cliente cliente);
        Task<bool> ExcluirAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<bool> EmailExisteAsync(string email);
        Task<bool> UsuarioExisteAsync(string usuario);
        Task<Cliente> ReativarAsync(int id);
    }
} 