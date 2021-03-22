# Desafio de agendamento SMARAPD

# Configurando Ambiente

- Banco de dados utilizado: MYSQL

#### Configurando conexao com banco de dados

- Editar arquivo `appsettings.json`
- Alterar a linha `"SmarapdDesafioContext": "Server=SERVER;userid=USUARIO;password=SENHA;database=DATA_BASE"`

### Criando tabelas do banco

- Por usar o EntityFramework executar o comando 'Update-Database' no console de de Gerenciador de Pacotes do Visual Studio

### Executado com o IIS Express pelo Visual Studio 2019

# Executar o FrontEnd

- Usado `YARN` para rodar o frontend
- Entrar na pasta `frontend` do projeto
- Executar o Comando `yarn` para baixar a dependencias do node
- Executar o Comando `yarn start`

# ENDPOINTS

<hr/>

# Salas

### Salas Agendadas

`/salas/salasagendadas`

retorno

```json
[
  {
    "roomId": 0,
    "schedulings": [
      {
        "id": 0,
        "roomId": 0,
        "title": "string",
        "startTime": "2021-03-21T21:58:37.146Z",
        "endTime": "2021-03-21T21:58:37.146Z"
      }
    ]
  }
]
```

### Todas Salas

`/salas/listall`

retorno

```json
[
  {
    "roomId": 0,
    "schedulings": [
      {
        "id": 0,
        "roomId": 0,
        "title": "string",
        "startTime": "2021-03-21T21:58:37.146Z",
        "endTime": "2021-03-21T21:58:37.146Z"
      }
    ]
  }
]
```

### Obter Sala pelo id

`/salas/get?id=<ID>`

retorno

```json
[
  {
    "roomId": 0,
    "schedulings": [
      {
        "id": 0,
        "roomId": 0,
        "title": "string",
        "startTime": "2021-03-21T21:58:37.146Z",
        "endTime": "2021-03-21T21:58:37.146Z"
      }
    ]
  }
]
```

### Criar nova sala

`/salas/create`

retorno

```json
{
  "roomId": 1,
  "schedulings": []
}
```

### Apagar uma sala

`/salas/delete?id=<ID>`

- Metodo: DELETE

retorno

```json
{
  "type": "string",
  "message": "string"
}
```

# Agendamento

### Inserir

`/agendamento/insert`

- Metodo: POST

Enviar Body do tipo:

```json
{
  "id": 0,
  "roomId": 0,
  "title": "string",
  "startTime": "2021-03-21T22:01:52.014Z",
  "endTime": "2021-03-21T22:01:52.014Z"
}
```

retorno

```json
{
  "type": "string",
  "message": "string"
}
```

### Apagar um agendamento

`/agendamento/delete?id=<ID>`

- Metodo: DELETE

retorno

```json
{
  "type": "string",
  "message": "string"
}
```

### Atualizar

`/agendamento/update`

- Metodo: POST

Enviar Body do tipo:

```json
{
  "id": 0,
  "roomId": 0,
  "title": "string",
  "startTime": "2021-03-21T22:01:52.014Z",
  "endTime": "2021-03-21T22:01:52.014Z"
}
```

retorno

```json
{
  "type": "string",
  "message": "string"
}
```

### Obter pelo id

`/agendamento/get?id=<ID>&roomid=<ROOMID>`

- Metodo: POST

retorno

```json
{
  "id": 0,
  "roomId": 0,
  "title": "string",
  "startTime": "2021-03-21T22:01:52.014Z",
  "endTime": "2021-03-21T22:01:52.014Z"
}
```
