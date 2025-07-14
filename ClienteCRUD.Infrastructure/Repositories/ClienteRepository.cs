using ClienteCRUD.Core.Entities;
using ClienteCRUD.Core.Interfaces;
using ClienteCRUD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClienteCRUD.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _context.Funcionarios
                .Where(c => c.Ativo)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObterPorEquipeAsync(int equipeId)
        {
            return await _context.Funcionarios
                .Where(c => c.Ativo && c.EquipeId == equipeId)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Cliente?> ObterPorIdAsync(int id)
        {
            return await _context.Funcionarios
                .FirstOrDefaultAsync(c => c.Id == id && c.Ativo);
        }

        public async Task<Cliente?> ObterPorEmailAsync(string email)
        {
            return await _context.Funcionarios
                .FirstOrDefaultAsync(c => c.Email == email && c.Ativo);
        }

        public async Task<Cliente?> ObterPorUsuarioAsync(string usuario)
        {
            return await _context.Funcionarios
                .FirstOrDefaultAsync(c => c.Usuario == usuario && c.Ativo);
        }

        public async Task<Cliente> AdicionarAsync(Cliente cliente)
        {
            cliente.DataCadastro = DateTime.UtcNow;
            cliente.Ativo = true;
            
            _context.Funcionarios.Add(cliente);
            await _context.SaveChangesAsync();
            
            return cliente;
        }

        public async Task<Cliente> AtualizarAsync(Cliente cliente)
        {
            cliente.DataAtualizacao = DateTime.UtcNow;
            
            _context.Funcionarios.Update(cliente);
            await _context.SaveChangesAsync();
            
            return cliente;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var cliente = await ObterPorIdAsync(id);
            if (cliente == null)
                return false;

            cliente.Ativo = false;
            cliente.DataAtualizacao = DateTime.Now;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Funcionarios
                .AnyAsync(c => c.Id == id && c.Ativo);
        }

        public async Task<bool> EmailExisteAsync(string email)
        {
            return await _context.Funcionarios
                .AnyAsync(c => c.Email == email && c.Ativo);
        }

        public async Task<bool> UsuarioExisteAsync(string usuario)
        {
            return await _context.Funcionarios
                .AnyAsync(c => c.Usuario == usuario && c.Ativo);
        }

        public async Task<Cliente?> ObterInativoPorIdAsync(int id)
        {
            return await _context.Funcionarios.FirstOrDefaultAsync(c => c.Id == id && !c.Ativo);
        }

        public async Task<Cliente> ReativarAsync(int id)
        {
            var cliente = await ObterInativoPorIdAsync(id);
            if (cliente == null)
                throw new InvalidOperationException("Funcionário inativo não encontrado");
            cliente.Ativo = true;
            cliente.DataAtualizacao = DateTime.UtcNow;
            _context.Funcionarios.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
    }
} 