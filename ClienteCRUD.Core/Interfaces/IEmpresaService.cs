using ClienteCRUD.Core.Entities;

namespace ClienteCRUD.Core.Interfaces
{
    public interface IEquipeService
    {
        Task<IEnumerable<Equipe>> ObterTodasAsync();
        Task<Equipe?> ObterPorIdAsync(int id);
        Task<Equipe> CriarAsync(Equipe equipe);
        Task<Equipe> AtualizarAsync(int id, Equipe equipe);
        Task<bool> ExcluirAsync(int id);
    }
} 