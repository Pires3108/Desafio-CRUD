using ClienteCRUD.Core.Entities;

namespace ClienteCRUD.Core.Interfaces
{
    public interface IEquipeRepository
    {
        Task<IEnumerable<Equipe>> ObterTodasAsync();
        Task<Equipe?> ObterPorIdAsync(int id);
        Task<Equipe?> ObterPorNomeAsync(string nome);
        Task<Equipe> AdicionarAsync(Equipe equipe);
        Task<Equipe> AtualizarAsync(Equipe equipe);
        Task<bool> ExcluirAsync(int id);
        Task<bool> NomeExisteAsync(string nome);
    }
} 