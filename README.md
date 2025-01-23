# InspireMe

InspireMe é uma API leve e simples desenvolvida em **C# com ASP.NET Core**, projetada para fornecer frases motivacionais e inspiradoras. Este projeto é ideal para integrar em aplicações que precisam de frases dinâmicas ou apenas adicionar um toque de inspiração.

## Funcionalidades
- **Listar frases**: Retorna todas as frases disponíveis.
- **Buscar frase por ID**: Retorna uma frase específica com base no ID fornecido.
- **Buscar frases por autor**: Retorna todas as frases de um autor específico.
- **Adicionar nova frase**: Permite adicionar frases personalizadas.
- **Atualizar frase**: Permite atualizar o texto ou autor de uma frase existente.
- **Excluir frase**: Remove uma frase específica do repositório.
- **Estrutura de testes**: Testes unitários implementados com **xUnit**.

## Endpoints da API

### **GET /api/quotes**
Retorna todas as frases disponíveis.

**Exemplo de Resposta:**
```json
[
  {
    "id": 1,
    "text": "O melhor jeito de prever o futuro é criá-lo.",
    "author": "Peter Drucker"
  },
  {
    "id": 2,
    "text": "A vida é o que acontece enquanto você está ocupado fazendo outros planos.",
    "author": "John Lennon"
  }
]
```

### **GET /api/quotes/{id}**
Retorna uma frase específica pelo ID.

**Exemplo de Requisição:**
```bash
GET /api/quotes/1
```

**Exemplo de Resposta:**
```json
{
  "id": 1,
  "text": "O melhor jeito de prever o futuro é criá-lo.",
  "author": "Peter Drucker"
}
```

**Resposta 404 (Não Encontrado):**
```json
{
  "message": "Frase não encontrada."
}
```

### **GET /api/quotes/author/{author}**
Retorna todas as frases de um autor específico.

**Exemplo de Requisição:**
```bash
GET /api/quotes/author/John Lennon
```

**Exemplo de Resposta:**
```json
[
  {
    "id": 2,
    "text": "A vida é o que acontece enquanto você está ocupado fazendo outros planos.",
    "author": "John Lennon"
  }
]
```

**Resposta 404 (Não Encontrado):**
```json
{
  "message": "Nenhuma frase encontrada para esse autor."
}
```

### **POST /api/quotes**
Adiciona uma nova frase ao repositório.

**Exemplo de Requisição:**
```json
{
  "text": "Nunca desista dos seus sonhos.",
  "author": "Desconhecido"
}
```

**Exemplo de Resposta:**
```json
{
  "id": 6,
  "text": "Nunca desista dos seus sonhos.",
  "author": "Desconhecido"
}
```

### **PUT /api/quotes/{id}**
Atualiza uma frase existente pelo ID.

**Exemplo de Requisição:**
```json
{
  "text": "Texto atualizado.",
  "author": "Autor Atualizado"
}
```

**Resposta 204 (No Content):**
A atualização foi realizada com sucesso.

**Resposta 404 (Não Encontrado):**
```json
{
  "message": "Frase não encontrada para atualização."
}
```

### **DELETE /api/quotes/{id}**
Exclui uma frase específica pelo ID.

**Exemplo de Requisição:**
```bash
DELETE /api/quotes/1
```

**Resposta 204 (No Content):**
A exclusão foi realizada com sucesso.

**Resposta 404 (Não Encontrado):**
```json
{
  "message": "Frase não encontrada para exclusão."
}
```

## Estrutura do Projeto

```plaintext
InspireMe/
├── src/
│   └── InspireMe.API/
│       ├── Controllers/
│       │   └── QuotesController.cs
│       ├── Models/
│       │   └── Quote.cs
│       ├── Services/
│       │   ├── IQuotesService.cs
│       │   └── QuotesService.cs
│       ├── Program.cs
│       └── appsettings.json
├── tests/
│   └── InspireMe.Tests/
│       ├── QuotesControllerTests.cs
│       └── QuotesServiceTests.cs
├── README.md
├── InspireMe.sln
└── .gitignore
```

## Como Rodar o Projeto

### **1. Clone o Repositório**
```bash
git clone https://github.com/O-Farias/inspireme.git
cd inspireme
```

### **2. Restaure as Dependências**
```bash
dotnet restore
```

### **3. Rode o Projeto**
```bash
dotnet run --project src/InspireMe.API
```
A API estará disponível em `http://localhost:5000` (ou outra porta configurada).

### **4. Execute os Testes**
Para verificar se tudo está funcionando corretamente, rode:
```bash
dotnet test
```

## Tecnologias Utilizadas
- **C# com ASP.NET Core**
- **xUnit** para testes unitários
- **Moq** para mockar dependências nos testes



Esse projeto foi desenvolvido como parte de um portfólio de aplicações backend. Sinta-se à vontade para contribuir ou sugerir melhorias!
