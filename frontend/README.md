# Cliente CRUD Frontend - Land Tech

Este projeto é o frontend do sistema de gerenciamento de equipes e funcionários da Land Tech.

## Principais Funcionalidades
- Login e cadastro com painel moderno e responsivo
- Painéis de login e cadastro ampliados para melhor experiência visual
- Listagem de equipes e funcionários com permissões (Land Tech Admin, Equipe Admin, Funcionário)
- Edição inline dos dados do funcionário
- Integração total com backend em C# (.NET)
- Consumo de endpoints protegidos por permissão
- Visual inspirado no site da Land Tech

## Tecnologias Utilizadas
- React (frontend)
- Axios (requisições HTTP)
- CSS customizado
- Integração com API C# (.NET)

## Instalação e Execução

1. **Clone o repositório:**
   ```bash
   git clone <repo-url>
   cd cliente-crud-frontend
   ```
2. **Instale as dependências:**
   ```bash
   npm install
   ```
3. **Inicie o frontend:**
   ```bash
   npm start
   ```
   O app estará disponível em `http://localhost:3000`.

4. **Certifique-se de que o backend C# (.NET) está rodando em `http://localhost:5000`**

## Permissões e Endpoints
- **Listagem de funcionários:**
  - `GET /api/clientes/com-permissoes/{funcionarioId}`
- **Atualização de funcionário:**
  - `PUT /api/clientes/{id}/com-permissoes?funcionarioLogadoId={funcionarioId}`
- **Exclusão de funcionário:**
  - `DELETE /api/clientes/{id}/com-permissoes?funcionarioLogadoId={funcionarioId}`
- O frontend sempre passa o ID do funcionário logado para garantir as regras de permissão.

## Visual
- Painéis centrais grandes e modernos para login e cadastro
- Logo Land Tech destacada
- Cards de equipe e funcionários com edição inline
- Responsivo e com visual inspirado no site da Land Tech

## Observações
- Para alterar a logo, substitua o arquivo `public/landtech-logo.png`.
- O backend deve estar rodando e acessível na porta 5000.

---

Desenvolvido para Land Tech 🚀
