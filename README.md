# Genial CashFlow
Aplicação para conta corrente.

## Compilação e Execução
Todos os pré-requisitos para compilar e executar a aplicação estão nos containers, para executar basta rodar o comando abaixo na pasta raiz da solution:

```
docker-compose up
```
Também é possível utilizar o Visual Studio 2022, basta abrir o arquivo ```Genial.CashFlow.sln```.

## Banco de Dados
Este projeto utiliza FluentMigrator, o banco de dados será criado e preenchido com uma massa de dados na primeira execução.

## Acesso
Segue abaixo uma descrição de cada container:

- **genial.cashflow.api**: APIs.
  - **Endereço Swagger**: https://localhost:55901/swagger
  - **Collection Postman**: Utilize o arquivo ```Genial.CashFlow.postman_collection.json``` disponível na pasta raiz da solution para importar uma collection no Postman. Essa collection contém exemplos de chamadas das APIs.

- **genial.cashflow.logging**: Dashboard do Seq Log para visualização dos logs.
  - **Endereço**: http://localhost:55902

- **genial.cashflow.database**: Banco de Dados MS SQL Server 2022 Developer Edition.
  - **Server**: localhost:55903
  - **Database**: CashFlow
  - **User**: sa
  - **Password**: d93@1FfF#9756