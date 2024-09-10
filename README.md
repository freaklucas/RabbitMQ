## Run

```
 rabbitmq:3-management
```

```
 docker pull rabbitmq:3-management
```
```
docker run --rm  -it -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```
```
User: guest
```

para ```/api/Products```
```
{
  "name": "Produto 1",
  "description": "Descrição produto 1",
  "stock": 5
}
```