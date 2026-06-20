# 🚀 Projeto de RH - Sistema de Cadastro com Histórico 

Este projeto foi disponibilizado originalmente como um desafio técnico na trilha .NET da **DIO (Digital Innovation One)**. Fui a responsável por dar continuidade ao código base, programando as regras de negócio que faltavam e configurando a integração com o banco de dados na nuvem da Microsoft (Azure).

## 🎯 O que o sistema faz?
É uma Web API para cadastrar funcionários de uma empresa. O grande foco deste desafio é a **rastreabilidade e auditoria**. O sistema funciona como uma "linha do tempo": ele não guarda apenas o dado atual do funcionário, mas cria um histórico automático de tudo o que aconteceu com ele no banco de dados.

*   **Cadastrou um funcionário?** O sistema salva os dados dele e cria um histórico de `Inclusão`.
*   **Alterou o salário ou o departamento?** O sistema atualiza o cadastro principal e gera um log de `Atualização`, permitindo ver o histórico da mudança.
*   **Removeu do sistema?** O cadastro atual é deletado, mas um último histórico de `Remoção` é guardado para segurança da empresa.

## 🛠️ Minhas Contribuições no Desafio
*   **Implementação dos métodos:** Completei o código desenvolvendo as regras para os comandos de atualizar (`PUT`) e deletar (`DELETE`) funcionários funcionarem perfeitamente.
*   **Mecanismo de Auditoria:** Configurei o controlador para disparar e salvar os logs de histórico a cada ação realizada no sistema.
*   **Conexão com a Nuvem:** Conectei o sistema a um banco de dados real criado na **Azure (SQL Database)** através do arquivo de configurações.

## 📐 Estrutura e Premissas do Projeto

A classe principal, a classe Funcionario e a FuncionarioLog, deve ser a seguinte:

![Diagrama da classe Funcionario](Imagens/diagrama_classe.png)

A classe FuncionarioLog é filha da classe Funcionario, pois o log terá as mesmas informações da Funcionario.

## 📋 Métodos Esperados e Endpoints

### Interface de Testes (Swagger)
Abaixo, a interface visual gerada pelo Swagger onde os métodos do sistema podem ser testados:

![Métodos Swagger](Imagens/swagger.png)

### Endpoints da API

| Verbo  | Endpoint                | Parâmetro | Body               |
|--------|-------------------------|-----------|--------------------|
| GET    | /Funcionario/{id}       | id        | N/A                |
| PUT    | /Funcionario/{id}       | id        | Schema Funcionario |
| DELETE | /Funcionario/{id}       | id        | N/A                |
| POST   | /Funcionario            | N/A       | Schema Funcionario |

Esse é o schema (model) de Funcionario, utilizado para passar para os métodos que exigirem:

```json
{
  "nome": "Nome funcionario",
  "endereco": "Rua 1234",
  "ramal": "1234",
  "emailProfissional": "email@email.com",
  "departamento": "TI",
  "salario": 1000,
  "dataAdmissao": "2022-06-23T02:58:36.345Z"
}
```

## 🌐 Ambiente em Nuvem
Este é o diagrama do ambiente que deverá ser montado no Microsoft Azure, utilizando o App Service para a API, SQL Database para o banco relacional e Azure Table para armazenar os logs.

![Diagrama da classe Funcionario](Imagens/diagrama_api.png)

---
*Código base disponibilizado pela [DIO](https://dio.me). Implementação final, correções e melhorias desenvolvidas por Carol Angel.*
