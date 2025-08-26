# 🚀 Configuração de Deploy Automático para Azure

## 📋 Pré-requisitos

1. **GitHub Account** (gratuito)
2. **Azure Account** (já tem)
3. **Código no GitHub** (vamos configurar)

## 🔧 Passo a Passo

### 1. Criar Repositório no GitHub

```bash
# No seu projeto local
git init
git add .
git commit -m "Primeiro commit - Sistema MidCar"

# Criar repositório no GitHub.com
# Depois conectar:
git remote add origin https://github.com/SEU_USUARIO/midcar-sistema.git
git branch -M main
git push -u origin main
```

### 2. Obter Publish Profile do Azure

1. **Acesse:** https://portal.azure.com
2. **Vá em:** App Services → midcar-engenharia
3. **Clique em:** "Get publish profile"
4. **Baixe** o arquivo `.publishsettings`

### 3. Configurar GitHub Secrets

1. **Acesse:** https://github.com/SEU_USUARIO/midcar-sistema/settings/secrets/actions
2. **Clique em:** "New repository secret"
3. **Nome:** `AZURE_WEBAPP_PUBLISH_PROFILE`
4. **Valor:** Cole todo o conteúdo do arquivo `.publishsettings`

### 4. Testar Deploy

```bash
# Fazer uma mudança no código
# Depois:
git add .
git commit -m "Teste deploy automático"
git push origin main
```

## 🎯 Como Funciona

### **Fluxo Automático:**

1. **Você faz mudanças** no código local
2. **Faz commit e push** para GitHub
3. **GitHub Actions detecta** a mudança
4. **Compila e testa** automaticamente
5. **Deploy para Azure** automaticamente
6. **Sua aplicação atualiza** em produção

### **Comandos Diários:**

```bash
# Desenvolver localmente
dotnet run  # localhost:5000

# Quando quiser enviar para produção:
git add .
git commit -m "Nova funcionalidade"
git push origin main
# ⚡ Deploy automático acontece!
```

## 🔍 Monitoramento

- **GitHub Actions:** https://github.com/SEU_USUARIO/midcar-sistema/actions
- **Azure App Service:** https://portal.azure.com
- **Aplicação:** https://midcar-engenharia-dnetdnd6axdzbsat.brazilsouth-01.azurewebsites.net

## 🛠️ Configurações Importantes

### **Branch Principal:**
- Use `main` ou `master` como branch principal
- Deploy só acontece nesta branch

### **Ambientes:**
- **Local:** Desenvolvimento e testes
- **Azure:** Produção (usuários reais)

### **Banco de Dados:**
- **Local:** LocalDB (desenvolvimento)
- **Azure:** Azure SQL Database (produção)

## ✅ Benefícios

- ✅ **Deploy automático** - sem trabalho manual
- ✅ **Testes automáticos** - qualidade garantida
- ✅ **Rollback fácil** - volta versão anterior
- ✅ **Histórico completo** - todas as mudanças
- ✅ **Colaboração** - equipe pode trabalhar junto

## 🚨 Importante

- **Sempre teste localmente** antes do push
- **Commit frequente** - não acumule mudanças
- **Mensagens claras** - descreva o que mudou
- **Backup do banco** - antes de mudanças grandes

---

**🎉 Pronto! Agora seu sistema tem deploy automático!**
