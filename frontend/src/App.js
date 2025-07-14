import React, { useState, useEffect } from 'react';
import api from './api';
import './App.css';
import Login from './Login';
import Register from './Register';

function App() {
  const [auth, setAuth] = useState({ isLogged: false, funcionario: null });
  const [screen, setScreen] = useState('login');
  const [loginError, setLoginError] = useState('');
  const [registerError, setRegisterError] = useState('');
  const [registerSuccess, setRegisterSuccess] = useState('');
  const [funcionarios, setFuncionarios] = useState([]);
  const [equipes, setEquipes] = useState([]);
  const [loading, setLoading] = useState(false);
  const [showEquipeForm, setShowEquipeForm] = useState(false);
  const [equipeForm, setEquipeForm] = useState({ id: '', nome: '' });
  const [editingEquipeId, setEditingEquipeId] = useState(null);
  const [selectedEquipeId, setSelectedEquipeId] = useState(null);
  // --- Estado para edição de funcionário ---
  const [editingFuncionario, setEditingFuncionario] = useState(null);
  const [showEditModal, setShowEditModal] = useState(false);
  // Novo estado para distinguir modo de visualização do modal
  const [modalModoVisualizacao, setModalModoVisualizacao] = useState(false);

  // --- Autenticação ---
  const handleLogin = async ({ login, senha }) => {
    setLoginError('');
    setLoading(true);
    try {
      const res = await api.post(`/api/clientes/login`, { usuario: login, senha });
      setAuth({ isLogged: true, funcionario: res.data });
      setScreen('crud');
    } catch (err) {
      setLoginError('Usuário ou senha inválidos');
    } finally {
      setLoading(false);
    }
  };

  // --- Cadastro de Funcionário ---
  const handleRegister = async (payload) => {
    setRegisterError('');
    setRegisterSuccess('');
    setLoading(true);
    try {
      await api.post('/api/clientes', payload);
      setRegisterSuccess('Cadastro realizado com sucesso!');
      setTimeout(() => {
        setScreen('login');
        setRegisterSuccess('');
      }, 2000);
    } catch (err) {
      if (err.response && err.response.data && err.response.data.message) {
        setRegisterError(err.response.data.message);
      } else {
        setRegisterError('Erro ao cadastrar.');
      }
    } finally {
      setLoading(false);
    }
  };

  // --- Listagem com permissões ---
  const fetchFuncionarios = async () => {
    if (!auth.funcionario) return;
    setLoading(true);
    try {
      console.log('Usuário logado:', auth.funcionario);
      const response = await api.get(`/api/clientes/com-permissoes/${auth.funcionario.id}`);
      setFuncionarios(response.data);
      console.log('Funcionarios recebidos do backend:', response.data);
    } catch (error) {
      if (error.response && error.response.status === 403) {
        alert('Você não tem permissão para visualizar esses funcionários.');
      } else {
        alert('Erro ao carregar funcionários');
      }
    } finally {
      setLoading(false);
    }
  };

  const fetchEquipes = async () => {
    setLoading(true);
    try {
      const response = await api.get(`/api/equipes`);
      setEquipes(response.data);
    } catch (error) {
      setEquipes([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (auth.isLogged && screen === 'crud') {
      fetchFuncionarios();
      fetchEquipes();
    }
    // eslint-disable-next-line
  }, [auth, screen]);

  // --- CRUD de Equipes ---
  const handleEquipeFormChange = e => {
    setEquipeForm({ ...equipeForm, [e.target.name]: e.target.value });
  };

  const handleEquipeSubmit = async e => {
    e.preventDefault();
    setLoading(true);
    try {
      if (editingEquipeId) {
        await api.put(`/api/equipes/${editingEquipeId}`, { nome: equipeForm.nome });
      } else {
        await api.post(`/api/equipes`, { nome: equipeForm.nome });
      }
      setShowEquipeForm(false);
      setEquipeForm({ id: '', nome: '' });
      setEditingEquipeId(null);
      fetchEquipes();
    } catch {
      alert('Erro ao salvar equipe');
    } finally {
      setLoading(false);
    }
  };

  const handleEditEquipe = equipe => {
    setEquipeForm({ id: equipe.id, nome: equipe.nome });
    setEditingEquipeId(equipe.id);
    setShowEquipeForm(true);
  };

  const handleDeleteEquipe = async equipeId => {
    if (!window.confirm('Tem certeza que deseja excluir esta equipe?')) return;
    setLoading(true);
    try {
      await api.delete(`/api/equipes/${equipeId}`);
      fetchEquipes();
      if (selectedEquipeId === equipeId) setSelectedEquipeId(null);
    } catch {
      alert('Erro ao excluir equipe');
    } finally {
      setLoading(false);
    }
  };

  // --- Permissões ---
  const isLandTech = auth.funcionario?.equipeNome?.toLowerCase().replace(/[^a-z]/g, '') === 'landtech';
  const isLandTechAdmin = !!auth.funcionario?.isLandTechAdmin;
  const isEquipeAdmin = !!auth.funcionario?.isEquipeAdmin;

  // Permissões para edição de funcionário
  const podeEditarFuncionario = (func) => {
    // Land Tech Admin pode editar todos
    if (isLandTechAdmin) return true;
    
    // Equipe Admin pode editar funcionários da própria equipe
    if (isEquipeAdmin && func.equipeId === auth.funcionario.equipeId) return true;
    
    // Funcionário comum pode editar apenas a si mesmo
    if (!isEquipeAdmin && !isLandTechAdmin && func.id === auth.funcionario.id) return true;
    
    return false;
  };

  // Permissões para visualização de funcionário
  const podeVerFuncionario = (func) => {
    // Land Tech Admin pode ver todos
    if (isLandTechAdmin) return true;
    
    // Equipe Admin pode ver funcionários da própria equipe
    if (isEquipeAdmin && func.equipeId === auth.funcionario.equipeId) return true;
    
    // Funcionário comum pode ver funcionários da própria equipe
    if (!isEquipeAdmin && !isLandTechAdmin && func.equipeId === auth.funcionario.equipeId) return true;
    
    return false;
  };

  // --- Listagem de Funcionários por Equipe ---
  const funcionariosPorEquipe = equipeId => funcionarios.filter(f => f.equipeId === equipeId);

  // --- Atualização de Funcionário (exemplo de uso)
  const handleUpdateFuncionario = async (id, dados) => {
    setLoading(true);
    try {
      await api.put(`/api/clientes/${id}/com-permissoes?funcionarioLogadoId=${auth.funcionario.id}`, dados);
      fetchFuncionarios();
    } catch (error) {
      if (error.response && error.response.status === 403) {
        alert('Você não tem permissão para atualizar este funcionário.');
      } else {
        alert('Erro ao atualizar funcionário');
      }
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteFuncionario = async (id) => {
    if (!window.confirm('Tem certeza que deseja excluir este funcionário?')) return;
    setLoading(true);
    try {
      await api.delete(`/api/clientes/${id}/com-permissoes?funcionarioLogadoId=${auth.funcionario.id}`);
      fetchFuncionarios();
    } catch (error) {
      if (error.response && error.response.status === 403) {
        alert('Você não tem permissão para excluir este funcionário.');
      } else {
        alert('Erro ao excluir funcionário');
      }
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return <div className="loading">Carregando...</div>;
  }

  if (screen === 'login') {
    return <Login onLogin={handleLogin} onShowRegister={() => setScreen('register')} error={loginError} />;
  }

  if (screen === 'register') {
    return <Register onRegister={handleRegister} onShowLogin={() => setScreen('login')} error={registerError} success={registerSuccess} />;
  }

  if (!auth.isLogged || screen !== 'crud') return null;

  // Ordenar equipes: Land Tech sempre no topo
  const equipesOrdenadas = (isLandTechAdmin ? equipes : equipes.filter(eq => eq.id === auth.funcionario.equipeId)).slice();
  equipesOrdenadas.sort((a, b) => {
    const nomeA = (a.nome || '').replace(/\s+/g, '').toLowerCase();
    const nomeB = (b.nome || '').replace(/\s+/g, '').toLowerCase();
    if (nomeA === 'landtech') return -1;
    if (nomeB === 'landtech') return 1;
    return nomeA.localeCompare(nomeB);
  });

  return (
    <div style={{ minHeight: '100vh', background: 'linear-gradient(to bottom right, #6A0DAD, #9B30FF)' }}>
      <header className="top-bar" style={{ background: 'transparent', padding: '20px 40px', borderRadius: '0 0 20px 20px', display: 'flex', alignItems: 'center', justifyContent: 'space-between', boxShadow: '0 2px 10px rgba(0,0,0,0.1)' }}>
        <div className="logo-bar" style={{ display: 'flex', alignItems: 'center', gap: 16 }}>
          <img src="/landtech-logo.png" alt="Logo Land Tech" style={{ height: 100 }} />
          <h1 style={{ fontSize: '1.5rem', color: '#fff' }}>Sistema de Gerenciamento de Funcionários</h1>
        </div>
        <div className="buttons" style={{ display: 'flex', gap: 10 }}>
          <button className="btn-sair" style={{ padding: '10px 20px', border: 'none', borderRadius: 8, fontWeight: 600, cursor: 'pointer', background: '#ccc' }} onClick={() => { setAuth({ isLogged: false, funcionario: null }); setScreen('login'); }}>Sair</button>
        </div>
      </header>
      <main style={{ padding: 40 }}>
        <div className="container" style={{ backgroundColor: '#fff', borderRadius: 20, padding: 30, maxWidth: 1100, margin: '0 auto' }}>
          <div className="section-header" style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 20 }}>
            <h2 style={{ color: '#6A0DAD', margin: 0 }}>Equipes</h2>
            <div>
              <button className="btn btn-purple" style={{ backgroundColor: '#6A0DAD', color: '#fff', padding: '8px 16px', border: 'none', borderRadius: 8, fontWeight: 600, marginRight: 10, cursor: 'pointer' }} onClick={() => { setShowEquipeForm(true); setEquipeForm({ id: '', nome: '' }); setEditingEquipeId(null); }}>Nova Equipe</button>
              <button className="btn btn-red" style={{ backgroundColor: '#f44336', color: '#fff', padding: '8px 16px', border: 'none', borderRadius: 8, fontWeight: 600, cursor: 'pointer' }} disabled>Excluir</button>
            </div>
          </div>
          {equipesOrdenadas.map(equipe => (
            <div className="equipe-card" key={equipe.id} style={{ backgroundColor: '#f9f9f9', borderRadius: 16, padding: 20, marginTop: 20 }}>
              <div className="equipe-header" style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <h3>{equipe.nome}</h3>
              </div>
              {funcionariosPorEquipe(equipe.id).map(func => (
                podeVerFuncionario(func) && (
                  editingFuncionario && editingFuncionario.id === func.id && showEditModal ? (
                    modalModoVisualizacao || !podeEditarFuncionario(func) ? (
                      // Modal somente leitura
                      <div className="funcionario" key={func.id} style={{ display: 'flex', flexDirection: 'column', alignItems: 'stretch', marginTop: 15, padding: 16, borderRadius: 12, backgroundColor: '#fff', boxShadow: '0 2px 5px rgba(0,0,0,0.05)' }}>
                        <div style={{ display: 'flex', flexDirection: 'column', gap: 10 }}>
                          <input name="nome" value={editingFuncionario.nome || ''} className="login-input" disabled />
                          <input name="email" value={editingFuncionario.email || ''} className="login-input" disabled />
                          <input name="telefone" value={editingFuncionario.telefone || ''} className="login-input" disabled />
                          <input name="endereco" value={editingFuncionario.endereco || ''} className="login-input" disabled />
                          <input name="cidade" value={editingFuncionario.cidade || ''} className="login-input" disabled />
                          <input name="estado" value={editingFuncionario.estado || ''} className="login-input" disabled />
                          <input name="cep" value={editingFuncionario.cep || ''} className="login-input" disabled />
                          <input name="usuario" value={editingFuncionario.usuario || ''} className="login-input" disabled />
                        </div>
                        <div style={{ display: 'flex', gap: 10, justifyContent: 'flex-end', marginTop: 16 }}>
                          <button type="button" className="btn-secondary" onClick={() => { setShowEditModal(false); setEditingFuncionario(null); setModalModoVisualizacao(false); }}>Fechar</button>
                        </div>
                      </div>
                    ) : (
                      // Modal de edição com formulário editável
                      <div className="funcionario" key={func.id} style={{ display: 'flex', flexDirection: 'column', alignItems: 'stretch', marginTop: 15, padding: 16, borderRadius: 12, backgroundColor: '#fff', boxShadow: '0 2px 5px rgba(0,0,0,0.05)' }}>
                        <form style={{ width: '100%' }} onSubmit={async e => {
                          e.preventDefault();
                          await handleUpdateFuncionario(editingFuncionario.id, editingFuncionario);
                          setShowEditModal(false);
                          setEditingFuncionario(null);
                        }}>
                          <div style={{ display: 'flex', flexDirection: 'column', gap: 10 }}>
                            <input name="nome" placeholder="Nome" value={editingFuncionario.nome === 'string' ? '' : (editingFuncionario.nome || '')} onChange={e => setEditingFuncionario({ ...editingFuncionario, nome: e.target.value })} className="login-input" required />
                            <input name="email" placeholder="Email" value={editingFuncionario.email === 'string' ? '' : (editingFuncionario.email || '')} onChange={e => setEditingFuncionario({ ...editingFuncionario, email: e.target.value })} className="login-input" required />
                            <input name="telefone" placeholder="Telefone" value={editingFuncionario.telefone === 'string' ? '' : (editingFuncionario.telefone || '')} onChange={e => setEditingFuncionario({ ...editingFuncionario, telefone: e.target.value })} className="login-input" />
                            <input name="endereco" placeholder="Endereço" value={editingFuncionario.endereco === 'string' ? '' : (editingFuncionario.endereco || '')} onChange={e => setEditingFuncionario({ ...editingFuncionario, endereco: e.target.value })} className="login-input" />
                            <input name="cidade" placeholder="Cidade" value={editingFuncionario.cidade === 'string' ? '' : (editingFuncionario.cidade || '')} onChange={e => setEditingFuncionario({ ...editingFuncionario, cidade: e.target.value })} className="login-input" />
                            <input name="estado" placeholder="Estado (ex: SP)" maxLength={2} value={['string','st'].includes(editingFuncionario.estado) ? '' : (editingFuncionario.estado || '')} onChange={e => setEditingFuncionario({ ...editingFuncionario, estado: e.target.value.toUpperCase() })} className="login-input" />
                            <input name="cep" placeholder="CEP" value={editingFuncionario.cep === 'string' ? '' : (editingFuncionario.cep || '')} onChange={e => setEditingFuncionario({ ...editingFuncionario, cep: e.target.value })} className="login-input" />
                            <input name="usuario" placeholder="Usuário" value={editingFuncionario.usuario === 'string' ? '' : (editingFuncionario.usuario || '')} onChange={e => setEditingFuncionario({ ...editingFuncionario, usuario: e.target.value })} className="login-input" required />
                            <input name="senha" type="password" placeholder="Nova senha (opcional)" value={''} onChange={e => setEditingFuncionario({ ...editingFuncionario, senha: e.target.value })} className="login-input" />
                          </div>
                          <div style={{ display: 'flex', gap: 10, justifyContent: 'flex-end', marginTop: 16 }}>
                            <button type="submit" className="btn-success">Salvar</button>
                            <button type="button" className="btn-secondary" onClick={() => { setShowEditModal(false); setEditingFuncionario(null); setModalModoVisualizacao(false); }}>Cancelar</button>
                          </div>
                        </form>
                      </div>
                    )
                  ) : (
                    <div className="funcionario" key={func.id} style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginTop: 15, padding: 16, borderRadius: 12, backgroundColor: '#fff', boxShadow: '0 2px 5px rgba(0,0,0,0.05)' }}>
                      <div className="func-info" style={{ display: 'flex', flexDirection: 'column', gap: 4 }}>
                        <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
                          <strong>{func.nome}</strong>
                          {func.isLandTechAdmin && (
                            <span className="tag admin" style={{ backgroundColor: '#e0f0ff', color: '#0066cc', padding: '4px 10px', borderRadius: 20, fontSize: '0.8rem', fontWeight: 600 }}>Land Tech Admin</span>
                          )}
                          {func.isEquipeAdmin && !func.isLandTechAdmin && (
                            <span className="tag admin" style={{ backgroundColor: '#e0f0ff', color: '#2979ff', padding: '4px 10px', borderRadius: 20, fontSize: '0.8rem', fontWeight: 600 }}>Equipe Admin</span>
                          )}
                        </div>
                        {func.email && func.email !== 'string' && <p><strong>Email:</strong> {func.email}</p>}
                        {func.telefone && func.telefone !== 'string' && <p><strong>Telefone:</strong> {func.telefone}</p>}
                        {func.endereco && func.endereco !== 'string' && <p><strong>Endereço:</strong> {func.endereco}</p>}
                        {func.cidade && func.cidade !== 'string' && <p><strong>Cidade:</strong> {func.cidade}</p>}
                        {func.estado && func.estado !== 'string' && func.estado !== 'st' && <p><strong>Estado:</strong> {func.estado}</p>}
                        {func.cep && func.cep !== 'string' && <p><strong>CEP:</strong> {func.cep}</p>}
                        {func.usuario && func.usuario !== 'string' && <p><strong>Usuário:</strong> {func.usuario}</p>}
                        <p><strong>Equipe:</strong> {func.equipeNome || equipe.nome}</p>
                      </div>
                      <div className="status-tags" style={{ display: 'flex', gap: 10, alignItems: 'center' }}>
                        <span className="tag ativo" style={{ backgroundColor: '#d4edda', color: '#155724', padding: '4px 10px', borderRadius: 20, fontSize: '0.8rem', fontWeight: 600 }}>{func.ativo ? 'Ativo' : 'Inativo'}</span>
                        {podeEditarFuncionario(func) ? (
                          <>
                            <button className="btn btn-pink" style={{ backgroundColor: '#f06292', color: '#fff', padding: '8px 16px', border: 'none', borderRadius: 8, fontWeight: 600, cursor: 'pointer' }} onClick={() => { setEditingFuncionario(func); setShowEditModal(true); setModalModoVisualizacao(false); }}>Editar</button>
                            <button className="btn btn-red" style={{ backgroundColor: '#f44336', color: '#fff', padding: '8px 16px', border: 'none', borderRadius: 8, fontWeight: 600, cursor: 'pointer' }} onClick={() => handleDeleteFuncionario(func.id)}>Excluir</button>
                          </>
                        ) : (
                          <>
                            <button className="btn btn-info" style={{ backgroundColor: '#64b5f6', color: '#fff', padding: '8px 16px', border: 'none', borderRadius: 8, fontWeight: 600, cursor: 'pointer' }} onClick={() => { setEditingFuncionario(func); setShowEditModal(true); setModalModoVisualizacao(true); }}>Visualizar</button>
                          </>
                        )}
                      </div>
                    </div>
                  )
                )
              ))}
            </div>
          ))}
          {/* Modal de criar/editar equipe (mantido, mas sem visual customizado) */}
          {showEquipeForm && (
            <div className="modal-bg">
              <div className="modal">
                <h3>{editingEquipeId ? 'Editar Equipe' : 'Nova Equipe'}</h3>
                <form onSubmit={handleEquipeSubmit}>
                  <input
                    name="nome"
                    placeholder="Nome da equipe"
                    value={equipeForm.nome}
                    onChange={handleEquipeFormChange}
                    required
                    className="login-input"
                    style={{ marginBottom: 16 }}
                  />
                  <div style={{ display: 'flex', gap: 10, justifyContent: 'flex-end' }}>
                    <button type="submit" className="btn-success">Salvar</button>
                    <button type="button" className="btn-secondary" onClick={() => { setShowEquipeForm(false); setEditingEquipeId(null); }}>Cancelar</button>
                  </div>
                </form>
              </div>
            </div>
          )}
        </div>
      </main>
    </div>
  );
}

export default App;
