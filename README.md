# Executar o projeto como docker

## Configuração e execução do projeto Docker

### Executar o comando para criar a build do projeto (imagem no docker)

```bash
$ docker build --rm -t example-dev/bpmn-assignments:latest .
```

-rm (para remover imagens)

-t (definir tag)

. (indica a pasta onde tem o Dockerfile)

### Executar container

```bash
$ docker run --rm -d -p 8080:80 --name myapp example-dev/bpmn-assignments
```

-p (mapear portas)

-e (Environments)

## Execução do projeto e MSSQL com docker composer

### Executar docker composer

```bash
$ docker composer up -d
```

### Para execução do docker composer

```bash
$ docker composer down
```