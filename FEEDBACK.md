# Feedback do Instrutor

#### 11/10/24 - Revisão Inicial - Eduardo Pires

## Pontos Positivos:

- Boa separação de responsabilidades.
- Uso adequado de ferramentas como AutoMapper e FluentValidation.
- Controle eficiente de usuários com autorização e registro de permissões por Roles.
- Demonstrou sólido conhecimento em Identity e JWT.
- Mostrou entendimento do ecossistema de desenvolvimento com EF, ASP.NET, ferramentas e padrões.

## Pontos Negativos:

- Utilizou muitas camadas para um projeto de baixa complexidade.
- Camadas como DTO poderiam estar dentro do Domain, por exemplo.
- Implementou complexidade adicional ao criar uma camada de domínio, mas fez uso de entidades anêmicas, delegando toda a responsabilidade de negócio aos serviços.

## Sugestões:

- Para um projeto dessa complexidade, seria mais viável criar apenas uma camada de aplicação para compartilhar o controle das chamadas entre MVC e API, dispensando as demais camadas.
- Reserve a implementação de estruturas mais complexas para projetos críticos que ainda virão; o Módulo 1 não abrange tudo isso.

## Problemas:

- Não consegui executar a aplicação de imediato na máquina. É necessário que o Seed esteja configurado corretamente, com uma connection string apontando para o SQLite.

  **P.S.** As migrations precisam ser geradas com uma conexão apontando para o SQLite; caso contrário, a aplicação não roda.
