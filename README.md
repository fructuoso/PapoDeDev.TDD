# PapoDeDev.TDD

## Cobertura de Testes

Passo a passo sobre como executar os testes unitários (e calcular o code coverage) localmente antes de realizar o commit.

### Pré-Requisitos

Para gerar o relatório é necessário instalar o **dotnet-reportgenerator-globaltool**

```script
dotnet tool install --global dotnet-reportgenerator-globaltool
````

### Execução

Executar o **.bat** para realizar a execução dos testes automatizados com a extração do relatório de cobertura na sequência.

```script
cd .\tests\
test-coverage.bat
```

**Obs.:** O bat só funciona se você estiver rodando a partir da pasta **tests**
