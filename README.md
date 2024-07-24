# Genial CashFlow

Aplicação para conta corrente.

## Compilação e Execução

Todos os pré-requisitos para compilar e executar a aplicação estão nos containers, para executar basta rodar o comando abaixo na pasta raiz da solution:

```
docker-compose up
```
Também é possível utilizar o Visual Studio 2022, basta abrir o arquivo ```Genial.CashFlow.sln```.

## Acesso

Segue abaixo uma descrição de cada container:

- **genial.cashflow.api**: Swagger das APIs.
  - **Endereço**: https://localhost:55901/swagger

- **genial.cashflow.logging**: Dashboard do Seq Log para visualização dos logs.
  - **Endereço**: http://localhost:55902

- **genial.cashflow.database**: Banco de Dados MS SQL Server 2022 Developer Edition.
  - **Server**: localhost:55903
  - **Database**: CashFlow
  - **User**: sa
  - **Password**: d93@1FfF#9756