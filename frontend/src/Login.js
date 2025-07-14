import React, { useState } from 'react';
import api from './api';

const Login = ({ onLogin, onShowRegister, error }) => {
  const [form, setForm] = useState({ login: '', senha: '' });

  const handleChange = e => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async e => {
    e.preventDefault();
    // Chama o onLogin, que já usa api.post no App.js
    onLogin(form);
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
      <div className="login-container" style={{ maxWidth: 600, padding: 64 }}>
        <h2 style={{ fontSize: 2.2 + 'rem', marginBottom: 32 }}>Bem Vindo!</h2>
        <form onSubmit={handleSubmit}>
          <input
            type="text"
            name="login"
            placeholder="E-mail ou Usuário"
            value={form.login}
            onChange={handleChange}
            required
            className="login-input"
            style={{ fontSize: 20, height: 48 }}
          />
          <input
            type="password"
            name="senha"
            placeholder="Senha"
            value={form.senha}
            onChange={handleChange}
            required
            className="login-input"
            style={{ fontSize: 20, height: 48 }}
          />
          <button type="submit" className="login-btn">Entrar</button>
          {error && <div className="login-error">{error}</div>}
        </form>
        <div className="footer-text">
          <p>Não tem conta? <button className="footer-link" onClick={onShowRegister}>Cadastrar</button></p>
        </div>
      </div>
    </div>
  );
};

export default Login; 