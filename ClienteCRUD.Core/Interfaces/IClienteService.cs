using ClienteCRUD.Core.Entities;

namespace ClienteCRUD.Core.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ObterTodosAsync();
        Task<IEnumerable<Cliente>> ObterTodosComPermissoesAsync(int funcionarioId);
        Task<Cliente?> ObterPorIdAsync(int id);
        Task<Cliente> CriarAsync(Cliente cliente);
        Task<Cliente> AtualizarAsync(int id, Cliente cliente);
        Task<Cliente> AtualizarComPermissoesAsync(int id, Cliente cliente, int funcionarioLogadoId);
        Task<bool> ExcluirAsync(int id);
        Task<bool> ExcluirComPermissoesAsync(int id, int funcionarioLogadoId);
        Task<bool> ValidarLoginAsync(string usuario, string senha);
        Task<Cliente> ReativarAsync(int id);
    }
} 