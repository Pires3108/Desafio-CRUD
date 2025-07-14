# 📋 TODO List - Sistema de Gestão de Funcionários

## ✅ **CONCLUÍDO**

### Backend - Funcionalidades Principais
- [x] **Migração de Empresa para Equipe**
  - Renomeação completa de entidades, DTOs, controllers
  - Atualização de todos os repositórios e serviços
  - Migração do banco de dados aplicada

- [x] **Sistema de Equipes**
  - CRUD completo de equipes
  - Endpoints: GET, POST, PUT, DELETE /api/equipes
  - Validação de nomes únicos

- [x] **Sistema de Funcionários**
  - CRUD completo de funcionários
  - Vínculo obrigatório com equipe
  - Criação de nova equipe durante cadastro
  - Criptografia de senhas com BCrypt

- [x] **Funcionalidade Land Tech**
  - Detecção automática da equipe "Land Tech" (qualquer formatação)
  - Campo `IsLandTechAdmin` para privilégios especiais
  - Acesso total para funcionários da Land Tech

- [x] **Sistema de Permissões**
  - **Land Tech Admin:** Vê todos os funcionários de todas as equipes
  - **Equipe Admin:** Vê todos os funcionários da sua equipe
  - **Funcionário Normal:** Vê apenas seus próprios dados
  - Endpoint: GET /api/clientes/com-permissoes/{funcionarioId}

- [x] **Sistema de Login**
  - Endpoint: POST /api/clientes/login
  - Validação de usuário e senha
  - Retorno dos dados do funcionário logado

- [x] **Estrutura de Dados**
  - Entidade Equipe com relacionamento 1:N
  - Entidade Cliente (Funcionário) com campos de permissão
  - DTOs atualizados com novos campos
  - AutoMapper configurado

## 🔄 **EM ANDAMENTO**

### Frontend - Atualizações Necessárias
- [ ] **Atualização de Terminologia**
  - Renomear "Empresa" para "Equipe" em todos os textos
  - Renomear "Cliente" para "Funcionário" em todos os textos
  - Atualizar labels, títulos, mensagens

- [ ] **Atualização de Formulários**
  - Mudar "empresaId" para "equipeId"
  - Mudar "novaEmpresa" para "novaEquipe"
  - Atualizar validações e campos

- [ ] **Implementação do Sistema de Login**
  - Criar tela de login
  - Integrar com POST /api/clientes/login
  - Armazenar dados do funcionário logado

- [ ] **Sistema de Permissões no Frontend**
  - Implementar lógica de permissões
  - Mostrar apenas dados permitidos
  - Indicadores visuais para Land Tech

- [ ] **CRUD de Equipes**
  - Criar interface para gerenciar equipes
  - Integrar com endpoints /api/equipes
  - Validações e feedback visual

## 📋 **PRÓXIMOS PASSOS**

### Frontend - Prioridades
1. **Implementar Login** (ALTA PRIORIDADE)
   - Tela de login funcional
   - Armazenamento de sessão
   - Redirecionamento baseado em permissões

2. **Atualizar Terminologia** (ALTA PRIORIDADE)
   - Renomear todos os textos
   - Atualizar formulários
   - Manter consistência visual

3. **Implementar Sistema de Permissões** (MÉDIA PRIORIDADE)
   - Lógica de exibição baseada em permissões
   - Indicadores visuais para Land Tech
   - Controle de acesso a funcionalidades

4. **CRUD de Equipes** (MÉDIA PRIORIDADE)
   - Interface para gerenciar equipes
   - Validações e feedback
   - Integração completa

### Backend - Melhorias Futuras
- [ ] **Autenticação JWT**
  - Implementar tokens JWT
  - Middleware de autenticação
  - Refresh tokens

- [ ] **Auditoria**
  - Log de ações dos usuários
  - Histórico de alterações
  - Rastreabilidade

- [ ] **Validações Avançadas**
  - Validação de força de senha
  - Validação de formato de email
  - Validação de CPF/CNPJ

- [ ] **Relatórios**
  - Relatórios de funcionários por equipe
  - Estatísticas de uso
  - Exportação de dados

## 🎯 **OBJETIVOS ATUAIS**

### Curto Prazo (1-2 semanas)
- [ ] Frontend completamente atualizado com nova terminologia
- [ ] Sistema de login funcional
- [ ] Sistema de permissões implementado
- [ ] CRUD de equipes no frontend

### Médio Prazo (1 mês)
- [ ] Autenticação JWT
- [ ] Melhorias de UX/UI
- [ ] Testes automatizados
- [ ] Documentação completa

### Longo Prazo (2-3 meses)
- [ ] Relatórios avançados
- [ ] Sistema de auditoria
- [ ] Múltiplos idiomas
- [ ] Aplicação mobile

---

**Status Atual:** Backend 100% funcional ✅ | Frontend em atualização 🔄
**Próxima Milestone:** Sistema de login e permissões no frontend 