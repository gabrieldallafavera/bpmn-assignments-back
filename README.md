# Executar o projeto como docker

## Executar o comando para criar a build do projeto (imagem no docker)

```bash
$ docker build --rm -t example-dev/bpmn-assignments:latest .
```

-rm (para remover imagens)

-t (definir tag)

. (indica a pasta onde tem o Dockerfile)

## Executar container

```bash
$ docker run --rm -d -p 8080:80 --name myapp example-dev/bpmn-assignments
```

-p (mapear portas)

-e (Environments)