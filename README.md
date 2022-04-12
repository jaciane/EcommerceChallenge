<b>Code Challenge</b>
<p>Para esse POC buscou-se utilizar simplicidade e boas práticas de desenvolvimento de software (SOLID) em uma solução baseada em arquitetura de microserviço.</p>
<p>O ambiente de hospedagem é composto por contêineres implantados em um único host do Docker.</p>
<p>A solução é composta por 3 projetos:

  <ul>
  <li>Basket.api: Faz interface com clientes. Possui validações e regras de negócio. Comunica-se com os serviços de discount e catalog.</li>
  <li>Catalog.grpc: Faz comunicação com banco de dados (MongoDB) para acesso aos dados (lista de produtos)</li>
  <li>Test: Contém alguns testes unitários</li>
  </ul>
  
  <p>OBS: Em outro contexto teríamos uma API gateway como ponto de entrada para os microsserviços</p>

<b>Comando docker-compose para iniciar a aplicação:</b></br>
 docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d catalogdb mongo_seed  discount.grpc catalog.grpc basket.api
 
<b>URL de acesso:</b></br>
 http://localhost:8081/swagger/index.html


Exemplo de requisição:</br>
<pre>
  <code>
{
    "products": [
        {
            "id": 1,
            "quantity": 1 // Quantidade a ser comprada do produto
        }
    ]
}
    </pre>
  </code>          
