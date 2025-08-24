# 📋 Perfil de Publicação - Azure

## 🔧 Configurações do Projeto

### 1. Target Framework
```xml
<TargetFramework>net5.0</TargetFramework>
```

### 2. Dependências Necessárias
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="5.0.17" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="5.0.17" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.Cookies" Version="2.2.0" />
```

### 3. Configurações de Build
```xml
<PropertyGroup>
  <AssemblyName>MidCarEngenharia</AssemblyName>
  <RootNamespace>MidCarEngenharia</RootNamespace>
  <CopyRefAssembliesToPublishDirectory>false</CopyRefAssembliesToPublishDirectory>
</PropertyGroup>
```

## 🌐 Configurações do Azure

### 1. App Service Settings
```
WEBSITE_RUN_FROM_PACKAGE=1
DOTNET_ENVIRONMENT=Production
ASPNETCORE_ENVIRONMENT=Production
```

### 2. Connection String
```
ConnectionStrings__DataBase=Server=midcar-server.database.windows.net;Database=midcar-database;User Id=midcar_admin;Password=[SUA_SENHA];TrustServerCertificate=true;MultipleActiveResultSets=true
```

### 3. Authentication Settings
```
Authentication__DefaultAdminUser=admin
Authentication__DefaultAdminPassword=[SENHA_ADMIN]
```

## 📦 Deploy Configuration

### 1. Build Commands
```bash
dotnet restore
dotnet build --configuration Release
dotnet publish --configuration Release --output ./publish
```

### 2. Publish Output
```
bin/Release/net5.0/publish/
```

### 3. Web.config (se necessário)
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <location path="." inheritInChildApplications="false">
    <system.webServer>
      <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
      </handlers>
      <aspNetCore processPath="dotnet" arguments=".\MidCarEngenharia.dll" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" hostingModel="inprocess" />
    </system.webServer>
  </location>
</configuration>
```

## 🔄 CI/CD Pipeline

### 1. GitHub Actions (opcional)
```yaml
name: Deploy to Azure
on:
  push:
    branches: [ master ]

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v2
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 5.0.x
        
    - name: Build
      run: dotnet build --configuration Release
      
    - name: Deploy to Azure
      uses: azure/webapps-deploy@v2
      with:
        app-name: 'midcar-engenharia'
        publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
        package: .
```

### 2. Azure DevOps (opcional)
```yaml
trigger:
- master

pool:
  vmImage: 'ubuntu-latest'

variables:
  solution: '**/*.sln'
  buildPlatform: 'Any CPU'
  buildConfiguration: 'Release'

steps:
- task: DotNetCoreCLI@2
  inputs:
    command: 'restore'
    projects: '**/*.csproj'
    
- task: DotNetCoreCLI@2
  inputs:
    command: 'build'
    projects: '**/*.csproj'
    arguments: '--configuration $(buildConfiguration)'
    
- task: DotNetCoreCLI@2
  inputs:
    command: 'publish'
    publishWebProjects: true
    arguments: '--configuration $(buildConfiguration) --output $(Build.ArtifactStagingDirectory)'
    zipAfterPublish: true
    
- task: AzureWebApp@1
  inputs:
    azureSubscription: 'Your-Azure-Subscription'
    appName: 'midcar-engenharia'
    package: '$(Build.ArtifactStagingDirectory)/**/*.zip'
```

## 🚀 Deploy Manual

### 1. Via Visual Studio
1. Clique com botão direito no projeto
2. Selecione "Publicar"
3. Escolha "Azure"
4. Selecione "App Service"
5. Configure as opções

### 2. Via Azure CLI
```bash
# Login no Azure
az login

# Criar App Service (se não existir)
az webapp create --resource-group midcar-rg --plan midcar-plan --name midcar-engenharia --runtime "DOTNET|5.0"

# Deploy
az webapp deployment source config --name midcar-engenharia --resource-group midcar-rg --repo-url https://github.com/Claudioofc/MIDCARENGENHARIA.git --branch master --manual-integration
```

### 3. Via Kudu (Console)
```bash
# Acesse: https://midcar-engenharia.scm.azurewebsites.net
# Vá em "Console" > "CMD"
git clone https://github.com/Claudioofc/MIDCARENGENHARIA.git
cd MIDCARENGENHARIA
dotnet restore
dotnet build --configuration Release
dotnet publish --configuration Release --output ./publish
```

## 🔍 Troubleshooting

### 1. Erros Comuns
- **500 Internal Server Error**: Verificar logs em Kudu
- **Connection String**: Verificar configurações do App Service
- **Migrations**: Executar `dotnet ef database update`

### 2. Logs
- **Application Logs**: Kudu > Log stream
- **Web Server Logs**: Kudu > Logs
- **Failed Request Logs**: Kudu > Logs

### 3. Performance
- **Cold Start**: Primeira requisição pode ser lenta
- **Memory**: Monitorar uso de memória
- **CPU**: Verificar limites do plano F1

## 📊 Monitoramento

### 1. Application Insights
```xml
<PackageReference Include="Microsoft.ApplicationInsights.AspNetCore" Version="2.21.0" />
```

### 2. Métricas Importantes
- **Response Time**: < 2 segundos
- **CPU Usage**: < 80%
- **Memory Usage**: < 1 GB
- **Requests/sec**: Monitorar picos

### 3. Alertas
- **CPU > 80%** por 5 minutos
- **Memory > 90%** por 5 minutos
- **Response Time > 5s** por 1 minuto
- **HTTP 500 > 10** por 1 minuto
