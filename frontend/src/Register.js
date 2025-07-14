import React, { useState, useEffect } from 'react';
import api from './api';

const Register = ({ onRegister, onShowLogin, error }) => {
  const [form, setForm] = useState({
    nome: '',
    email: '',
    usuario: '',
    senha: '',
    equipeId: '',
    novaEquipe: ''
  });
  const [equipes, setEquipes] = useState([]);
  const [criandoEquipe, setCriandoEquipe] = useState(false);

  useEffect(() => {
    // Buscar equipes existentes na API
    api.get('/api/equipes')
      .then(res => setEquipes(res.data))
      .catch(() => setEquipes([]));
  }, []);

  const handleChange = e => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleEquipeChange = e => {
    if (e.target.value === '__nova__') {
      setCriandoEquipe(true);
      setForm({ ...form, equipeId: '', novaEquipe: '' });
    } else {
      setCriandoEquipe(false);
      setForm({ ...form, equipeId: e.target.value, novaEquipe: '' });
    }
  };

  const handleSubmit = e => {
    e.preventDefault();
    // Se criando nova equipe, enviar novaEquipe; senão, enviar equipeId
    const payload = {
      nome: form.nome,
      email: form.email,
      usuario: form.usuario,
      senha: form.senha,
      equipeId: criandoEquipe ? undefined : form.equipeId,
      novaEquipe: criandoEquipe ? form.novaEquipe : undefined
    };
    onRegister(payload);
  };

  return (
    <div className="login-page">
      <div className="header">
        <img 
          src="/landtech-logo.png" 
          alt="Land Tech"
          className="logo"
        />
      </div>
      <div className="cadastro-container" style={{ maxWidth: 600, padding: 64 }}>
        <h2 style={{ fontSize: 2.2 + 'rem', marginBottom: 32 }}>Cadastro de Funcionário</h2>
        <form onSubmit={handleSubmit}>
          <input name="nome" type="text" placeholder="Nome completo" value={form.nome} onChange={handleChange} required className="login-input" style={{ fontSize: 20, height: 48 }} />
          <input name="email" type="email" placeholder="Email" value={form.email} onChange={handleChange} required className="login-input" style={{ fontSize: 20, height: 48 }} />
          <input name="usuario" type="text" placeholder="Usuário" value={form.usuario} onChange={handleChange} required className="login-input" style={{ fontSize: 20, height: 48 }} />
          <input name="senha" type="password" placeholder="Senha" value={form.senha} onChange={handleChange} required className="login-input" style={{ fontSize: 20, height: 48 }} />

          <label style={{textAlign: 'left', color: '#6A0DAD', fontWeight: 600, margin: '8px 0 4px 2px', display: 'block'}}>Equipe</label>
          <select
            className="login-input"
            value={criandoEquipe ? '__nova__' : form.equipeId}
            onChange={handleEquipeChange}
            required={!criandoEquipe}
          >
            <option value="">Selecione uma equipe</option>
            {equipes.map(eq => (
              <option key={eq.id} value={eq.id}>{eq.nome}</option>
            ))}
            <option value="__nova__">Criar nova equipe</option>
          </select>
          {criandoEquipe && (
            <input
              name="novaEquipe"
              type="text"
              placeholder="Nome da nova equipe"
              value={form.novaEquipe}
              onChange={handleChange}
              required
              className="login-input"
            />
          )}

          <button type="submit" className="login-btn">Cadastrar</button>
          {error && <div className="login-error">{error}</div>}
        </form>
        <div className="footer-text">
          <p>Já tem conta? <button className="footer-link" onClick={onShowLogin}>Entrar</button></p>
        </div>
      </div>
    </div>
  );
};

export default Register; 