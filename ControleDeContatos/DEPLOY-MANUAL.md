# 🚀 DEPLOY MANUAL - MID'CAR AZURE

## 📋 PASSO A PASSO

### 1. Abrir Terminal/PowerShell
- Pressione `Win + R`
- Digite: `powershell`
- Pressione Enter

### 2. Navegar para o Projeto
```powershell
cd "C:\Users\claud\source\repos\ControleDeContatos\ControleDeContatos"
```

### 3. Verificar Status
```powershell
git status
```

### 4. Adicionar Alterações
```powershell
git add .
```

### 5. Fazer Commit
```powershell
git commit -m "Corrigir string de conexão para Azure - admin/123456"
```

### 6. Fazer Push
```powershell
git push origin master
```

## ✅ RESULTADO ESPERADO

Após executar os comandos, você deve ver:
- ✅ "Changes committed"
- ✅ "Pushed to origin/master"

## ⏳ AGUARDAR DEPLOY

1. **Aguarde 2-3 minutos**
2. **O Azure vai detectar as mudanças automaticamente**
3. **Deploy automático acontecerá**

## 🧪 TESTAR SITE

1. **Acesse:** https://midcar-engenharia-dnetdnd6axdzbsat.brazilsouth-01.azurewebsites.net
2. **Login:** admin
3. **Senha:** 123456

## 🆘 SE DER ERRO

### Erro de Git:
- Verifique se está no diretório correto
- Verifique se tem conexão com internet

### Erro de Push:
- Pode ser necessário fazer login no GitHub
- Use: `git config --global user.name "Seu Nome"`
- Use: `git config --global user.email "seu@email.com"`

## 📞 AJUDA

Se não conseguir, me avise qual erro aparece!
