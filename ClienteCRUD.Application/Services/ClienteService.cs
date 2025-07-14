using AutoMapper;
using BCrypt.Net;
using ClienteCRUD.Application.DTOs;
using ClienteCRUD.Core.Entities;
using ClienteCRUD.Core.Interfaces;

namespace ClienteCRUD.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IEquipeService _equipeService;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IEquipeService equipeService, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _equipeService = equipeService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _clienteRepository.ObterTodosAsync();
        }

        public async Task<IEnumerable<Cliente>> ObterTodosComPermissoesAsync(int funcionarioId)
        {
            var funcionario = await _clienteRepository.ObterPorIdAsync(funcionarioId);
            if (funcionario == null)
                throw new InvalidOperationException("Funcionário não encontrado");

            // Se é Land Tech Admin, pode ver todos os funcionários
            if (funcionario.IsLandTechAdmin)
            {
                return await _clienteRepository.ObterTodosAsync();
            }

            // Se é admin da equipe (criador da equipe), pode ver apenas sua equipe
            if (funcionario.IsEquipeAdmin && funcionario.EquipeId.HasValue)
            {
                return await _clienteRepository.ObterPorEquipeAsync(funcionario.EquipeId.Value);
            }

            // Funcionário comum vê todos os membros da própria equipe
            if (!funcionario.IsEquipeAdmin && !funcionario.IsLandTechAdmin && funcionario.EquipeId.HasValue)
            {
                return await _clienteRepository.ObterPorEquipeAsync(funcionario.EquipeId.Value);
            }

            // Caso não tenha equipe, retorna apenas a si mesmo
            return new List<Cliente> { funcionario };
        }

        public async Task<Cliente?> ObterPorIdAsync(int id)
        {
            return await _clienteRepository.ObterPorIdAsync(id);
        }

        public async Task<Cliente> CriarAsync(Cliente cliente)
        {
            // Validar se email já existe
            if (await _clienteRepository.EmailExisteAsync(cliente.Email))
                throw new InvalidOperationException("Email já cadastrado");

            // Validar se usuário já existe
            if (await _clienteRepository.UsuarioExisteAsync(cliente.Usuario))
                throw new InvalidOperationException("Usuário já cadastrado");

            // Criptografar senha
            cliente.Senha = BCrypt.Net.BCrypt.HashPassword(cliente.Senha);

            return await _clienteRepository.AdicionarAsync(cliente);
        }

        public async Task<Cliente> CriarAsync(CriarClienteDTO dto)
        {
            // Validar se email já existe
            if (await _clienteRepository.EmailExisteAsync(dto.Email))
                throw new InvalidOperationException("Email já cadastrado");

            // Validar se usuário já existe
            if (await _clienteRepository.UsuarioExisteAsync(dto.Usuario))
                throw new InvalidOperationException("Usuário já cadastrado");

            // Mapear DTO para entidade
            var cliente = _mapper.Map<Cliente>(dto);

            // Processar equipe
            Equipe? equipe = null;
            if (!string.IsNullOrEmpty(dto.NovaEquipe))
            {
                // Criar nova equipe
                equipe = new Equipe { Nome = dto.NovaEquipe };
                equipe = await _equipeService.CriarAsync(equipe);
                cliente.IsEquipeAdmin = true; // Quem cria a equipe é admin
                
                // Verificar se é Land Tech (independente da formatação)
                if (IsLandTech(equipe.Nome))
                {
                    cliente.IsLandTechAdmin = true; // Criador da Land Tech tem acesso total
                }
            }
            else if (dto.EquipeId.HasValue)
            {
                // Usar equipe existente
                equipe = await _equipeService.ObterPorIdAsync(dto.EquipeId.Value);
                if (equipe == null)
                    throw new InvalidOperationException("Equipe não encontrada");
                cliente.IsEquipeAdmin = false; // Funcionário normal
                
                // Verificar se é Land Tech
                if (IsLandTech(equipe.Nome))
                {
                    cliente.IsLandTechAdmin = true; // Funcionário da Land Tech tem acesso total
                }
            }
            else
            {
                throw new InvalidOperationException("É necessário informar uma equipe existente ou criar uma nova");
            }

            cliente.EquipeId = equipe.Id;
            cliente.Equipe = equipe;

            // Criptografar senha
            cliente.Senha = BCrypt.Net.BCrypt.HashPassword(cliente.Senha);

            return await _clienteRepository.AdicionarAsync(cliente);
        }

        public async Task<Cliente> AtualizarAsync(int id, Cliente cliente)
        {
            var clienteExistente = await _clienteRepository.ObterPorIdAsync(id);
            if (clienteExistente == null)
                throw new InvalidOperationException("Funcionário não encontrado");

            // Verificar permissões de edição
            await VerificarPermissaoEdicaoAsync(clienteExistente, cliente);

            // Verificar se email foi alterado e se já existe
            if (cliente.Email != clienteExistente.Email && 
                await _clienteRepository.EmailExisteAsync(cliente.Email))
                throw new InvalidOperationException("Email já cadastrado");

            // Verificar se usuário foi alterado e se já existe
            if (cliente.Usuario != clienteExistente.Usuario && 
                await _clienteRepository.UsuarioExisteAsync(cliente.Usuario))
                throw new InvalidOperationException("Usuário já cadastrado");

            // Atualizar propriedades
            clienteExistente.Nome = cliente.Nome;
            clienteExistente.Email = cliente.Email;
            clienteExistente.Telefone = cliente.Telefone;
            clienteExistente.Endereco = cliente.Endereco;
            clienteExistente.Cidade = cliente.Cidade;
            clienteExistente.Estado = cliente.Estado;
            clienteExistente.Cep = cliente.Cep;
            clienteExistente.Usuario = cliente.Usuario;

            // Se senha foi fornecida, criptografar e atualizar
            if (!string.IsNullOrEmpty(cliente.Senha))
            {
                clienteExistente.Senha = BCrypt.Net.BCrypt.HashPassword(cliente.Senha);
            }

            return await _clienteRepository.AtualizarAsync(clienteExistente);
        }

        public async Task<Cliente> AtualizarComPermissoesAsync(int id, Cliente cliente, int funcionarioLogadoId)
        {
            var funcionarioLogado = await _clienteRepository.ObterPorIdAsync(funcionarioLogadoId);
            if (funcionarioLogado == null)
                throw new InvalidOperationException("Funcionário logado não encontrado");

            var clienteExistente = await _clienteRepository.ObterPorIdAsync(id);
            if (clienteExistente == null)
                throw new InvalidOperationException("Funcionário não encontrado");

            // Verificar permissões de edição
            await VerificarPermissaoEdicaoAsync(funcionarioLogado, clienteExistente);

            // Verificar se email foi alterado e se já existe
            if (cliente.Email != clienteExistente.Email && 
                await _clienteRepository.EmailExisteAsync(cliente.Email))
                throw new InvalidOperationException("Email já cadastrado");

            // Verificar se usuário foi alterado e se já existe
            if (cliente.Usuario != clienteExistente.Usuario && 
                await _clienteRepository.UsuarioExisteAsync(cliente.Usuario))
                throw new InvalidOperationException("Usuário já cadastrado");

            // Atualizar propriedades
            clienteExistente.Nome = cliente.Nome;
            clienteExistente.Email = cliente.Email;
            clienteExistente.Telefone = cliente.Telefone;
            clienteExistente.Endereco = cliente.Endereco;
            clienteExistente.Cidade = cliente.Cidade;
            clienteExistente.Estado = cliente.Estado;
            clienteExistente.Cep = cliente.Cep;
            clienteExistente.Usuario = cliente.Usuario;

            // Se senha foi fornecida, criptografar e atualizar
            if (!string.IsNullOrEmpty(cliente.Senha))
            {
                clienteExistente.Senha = BCrypt.Net.BCrypt.HashPassword(cliente.Senha);
            }

            return await _clienteRepository.AtualizarAsync(clienteExistente);
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            return await _clienteRepository.ExcluirAsync(id);
        }

        public async Task<bool> ExcluirComPermissoesAsync(int id, int funcionarioLogadoId)
        {
            var funcionarioLogado = await _clienteRepository.ObterPorIdAsync(funcionarioLogadoId);
            if (funcionarioLogado == null)
                throw new InvalidOperationException("Funcionário logado não encontrado");

            var clienteParaExcluir = await _clienteRepository.ObterPorIdAsync(id);
            if (clienteParaExcluir == null)
                throw new InvalidOperationException("Funcionário não encontrado");

            // Verificar permissões de exclusão
            await VerificarPermissaoEdicaoAsync(funcionarioLogado, clienteParaExcluir);

            return await _clienteRepository.ExcluirAsync(id);
        }

        public async Task<bool> ValidarLoginAsync(string usuario, string senha)
        {
            var cliente = await _clienteRepository.ObterPorUsuarioAsync(usuario);
            if (cliente == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(senha, cliente.Senha);
        }

        public async Task<Cliente> ReativarAsync(int id)
        {
            return await _clienteRepository.ReativarAsync(id);
        }

        // Método auxiliar para verificar se é Land Tech (independente da formatação)
        private bool IsLandTech(string nomeEquipe)
        {
            if (string.IsNullOrEmpty(nomeEquipe))
                return false;

            var nomeNormalizado = nomeEquipe.Trim().ToLowerInvariant();
            return nomeNormalizado == "land tech" || 
                   nomeNormalizado == "landtech" || 
                   nomeNormalizado == "land_tech" ||
                   nomeNormalizado == "land-tech";
        }

        private async Task VerificarPermissaoEdicaoAsync(Cliente funcionarioLogado, Cliente clienteParaEditar)
        {
            // Land Tech Admin pode editar qualquer funcionário
            if (funcionarioLogado.IsLandTechAdmin)
                return;

            // Admin da equipe pode editar apenas funcionários da sua equipe
            if (funcionarioLogado.IsEquipeAdmin && funcionarioLogado.EquipeId.HasValue)
            {
                if (clienteParaEditar.EquipeId == funcionarioLogado.EquipeId)
                    return;
                throw new InvalidOperationException("Você só pode editar funcionários da sua equipe");
            }

            // Funcionário normal só pode editar seus próprios dados
            if (funcionarioLogado.Id == clienteParaEditar.Id)
                return;

            throw new InvalidOperationException("Você só pode editar seus próprios dados");
        }
    }
} 