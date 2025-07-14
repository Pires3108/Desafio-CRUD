using ClienteCRUD.Core.Entities;
using ClienteCRUD.Core.Interfaces;
using ClienteCRUD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClienteCRUD.Infrastructure.Repositories
{
    public class EquipeRepository : IEquipeRepository
    {
        private readonly ApplicationDbContext _context;

        public EquipeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Equipe>> ObterTodasAsync()
        {
            return await _context.Equipes.ToListAsync();
        }

        public async Task<Equipe?> ObterPorIdAsync(int id)
        {
            return await _context.Equipes.FindAsync(id);
        }

        public async Task<Equipe?> ObterPorNomeAsync(string nome)
        {
            return await _context.Equipes.FirstOrDefaultAsync(e => e.Nome == nome);
        }

        public async Task<Equipe> AdicionarAsync(Equipe equipe)
        {
            _context.Equipes.Add(equipe);
            await _context.SaveChangesAsync();
            return equipe;
        }

        public async Task<Equipe> AtualizarAsync(Equipe equipe)
        {
            _context.Equipes.Update(equipe);
            await _context.SaveChangesAsync();
            return equipe;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var equipe = await _context.Equipes.FindAsync(id);
            if (equipe == null)
                return false;

            _context.Equipes.Remove(equipe);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NomeExisteAsync(string nome)
        {
            return await _context.Equipes.AnyAsync(e => e.Nome == nome);
        }
    }
} 