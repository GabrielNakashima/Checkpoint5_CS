# Checkpoint 5 - Sistema de Gerenciamento de Produtos

> Aplicação Console desenvolvida em **C#** com **ADO.NET** e **SQLite** para gerenciamento completo de produtos (CRUD), aplicando padrões de arquitetura como **Repository Pattern**, configuração externa e logging de operações.

---

## Autor

* **Nome:** Gabriel Luni Nakashima
* **RM:** 558096
* **Turma:** 3ESPZ

---

## Funcionalidades

- [x] **Cadastrar Produto**: Inserção com validação e parâmetros SQL seguros against SQL Injection.
- [x] **Listar Produtos**: Exibição formatada de todos os registros do banco.
- [x] **Buscar por ID**: Consulta individual detalhada.
- [x] **Atualizar Produto**: Alteração de dados cadastrais existentes.
- [x] **Excluir Produto**: Remoção de registros via ID.
- [x] **Criação Automática de Tabela**: Executa o `script.sql` automaticamente na primeira inicialização para criar o banco `produtos.db`.
- [x] **Sistema de Log**: Registro de auditoria das operações e erros no arquivo `log.txt`.

---

## Padrões de Arquitetura e Boas Práticas

1. **Repository Pattern (`ProdutoRepository.cs`)**: Centraliza o acesso a dados e isola as regras do banco da interface de usuário (`Program.cs`).
2. **Consultas Parametrizadas**: Uso exclusivo de `SqliteParameter` (`AddWithValue`) para prevenir vulnerabilidades de SQL Injection.
3. **Gerenciamento de Recursos**: Utilização da instrução `using` para garantir o fechamento e descarte correto de conexões (`SqliteConnection`) e comandos (`SqliteCommand`).
4. **Configuração Externa (`appsettings.json`)**: String de conexão isolada do código-fonte utilizando `Microsoft.Extensions.Configuration`.
5. **Portabilidade**: Utilização de caminho relativo (`Data Source=produtos.db`), permitindo a execução imediata em qualquer ambiente/máquina sem dependência de caminhos absolutos.

---

## Estrutura do Repositório

```text
Checkpoint5/
│── Produto.cs                 # Classe de modelo do domínio
│── ProdutoRepository.cs       # Implementação do CRUD e Acesso a Dados (ADO.NET)
├── appsettings.json           # Configuração de conexão com o banco de dados
├── Program.cs                 # Menu interativo em Console (UI)
├── script.sql                 # Script DDL para criação da tabela
├── log.txt                    # Arquivo de log gerado automaticamente
├── .gitignore                 # Filtro de arquivos para versionamento Git
└── README.md                  # Documentação do projeto
```

## Modelo do Banco de Dados (DDL)

O arquivo `script.sql` contém a definição da estrutura utilizada pela aplicação:

```sql
CREATE TABLE IF NOT EXISTS Produtos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL,
    Preco REAL NOT NULL,
    Estoque INTEGER NOT NULL,
    Categoria TEXT NOT NULL
);
```

##  Como Executar o Projeto

### Pré-requisitos
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download) instalado.
* Visual Studio 2022 ou VS Code.

### Passos para Execução

1. **Clonar o repositório:**
   ```bash
   git clone [https://github.com/GabrielNakashima/Checkpoint5.git](https://github.com/GabrielNakashima/Checkpoint5.git)
   cd Checkpoint5
   ```

2. **Restaurar as dependências e executar:**
   ```bash
   dotnet run
   ```

3. **Ou via Visual Studio:**
   * Abra a solução (`.sln` ou `.csproj`).
   * Pressione **F5** ou clique no botão **Play**.

> **Nota:** *O banco de dados `produtos.db` e o arquivo `log.txt` serão criados automaticamente no mesmo diretório do executável na primeira inicialização.*

## Prints 

### Inserir novo produto:

<img width="555" height="300" alt="image" src="https://github.com/user-attachments/assets/35471235-95f8-4ef1-a7c6-a34dfdb46ab8" />

### Proteção contra SQL injection

<img width="555" height="300" alt="image" src="https://github.com/user-attachments/assets/a291d991-8d5a-4093-b15e-28847a10d69e" /> <br> <br>

<img width="550" height="55" alt="image" src="https://github.com/user-attachments/assets/bbad5868-9ebd-4c47-b0d7-3e693a3a34e2" />

### Listar produtos

<img width="555" height="300" alt="image" src="https://github.com/user-attachments/assets/e3d4bedc-5b04-401d-b97f-f837b19afec2" />

### Buscar produto por ID

<img width="555" height="300" alt="image" src="https://github.com/user-attachments/assets/1c8e90a4-47d9-4072-b011-4a1a0ebff191" />

### Atualizar produto

<img width="555" height="300" alt="image" src="https://github.com/user-attachments/assets/2a252b5b-c7e6-4549-ad20-8cd639028815" />

### Excluir produto

<img width="555" height="300" alt="image" src="https://github.com/user-attachments/assets/f1376e1c-ba91-490c-8554-093455b854bd" /> <br>

<img width="555" height="300" alt="image" src="https://github.com/user-attachments/assets/4b586f3d-523b-42eb-bbc7-06f90c0322d1" />

### Vídeo

https://youtu.be/LNjYOn-o1xo
