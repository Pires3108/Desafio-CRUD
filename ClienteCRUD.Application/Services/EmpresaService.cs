using ClienteCRUD.Core.Entities;
using ClienteCRUD.Core.Interfaces;

namespace ClienteCRUD.Application.Services
{
    public class EquipeService : IEquipeService
    {
        private readonly IEquipeRepository _equipeRepository;

        public EquipeService(IEquipeRepository equipeRepository)
        {
            _equipeRepository = equipeRepository;
        }

        public async Task<IEnumerable<Equipe>> ObterTodasAsync()
        {
            return await _equipeRepository.ObterTodasAsync();
        }

        public async Task<Equipe?> ObterPorIdAsync(int id)
        {
            return await _equipeRepository.ObterPorIdAsync(id);
        }

        public async Task<Equipe> CriarAsync(Equipe equipe)
        {
            // Validar se nome já existe
            if (await _equipeRepository.NomeExisteAsync(equipe.Nome))
                throw new InvalidOperationException("Equipe com este nome já existe");

            return await _equipeRepository.AdicionarAsync(equipe);
        }

        public async Task<Equipe> AtualizarAsync(int id, Equipe equipe)
        {
            var equipeExistente = await _equipeRepository.ObterPorIdAsync(id);
            if (equipeExistente == null)
                throw new InvalidOperationException("Equipe não encontrada");

            // Verificar se nome foi alterado e se já existe
            if (equipe.Nome != equipeExistente.Nome && 
                await _equipeRepository.NomeExisteAsync(equipe.Nome))
                throw new InvalidOperationException("Equipe com este nome já existe");

            // Atualizar propriedades
            equipeExistente.Nome = equipe.Nome;

            return await _equipeRepository.AtualizarAsync(equipeExistente);
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            return await _equipeRepository.ExcluirAsync(id);
        }
    }
} 