# 🍔 TechBurger API

Bem-vindo ao **TechBurger API**, o sistema de backend para gerenciamento de pedidos de uma "Dark Kitchen" de hambúrgueres artesanais. Este projeto simula o fluxo real de uma lanchonete, desde a abertura do "carrinho" até a entrega, com cálculo automático de preços e travas de segurança por status.

## 🚀 Funcionalidades

* **Gestão de Pedidos:** Criação de comanda para clientes.
* **Adição de Itens:** Inserção de combos (Hambúrguer + Bebida) em pedidos abertos.
* **Cálculo Automático:** O valor total do pedido é calculado dinamicamente com base nos itens adicionados.
* **Máquina de Estados:** Controle rigoroso de status (`Open` → `Confirmed` → `Delivered`).
* **Catálogo Fixo:** Tabela de preços pré-definida no sistema.

## 📋 Regras de Negócio

1.  **Fluxo de Status:**
    * Todo pedido nasce como `Open`.
    * Itens só podem ser adicionados enquanto o status for `Open`.
    * Um pedido só pode ser mudado para `Confirmed` se tiver **pelo menos um item**.
2.  **Preços:** Os preços são buscados automaticamente de uma tabela interna. Se o cliente pedir um hambúrguer que não existe, o sistema rejeita.

### 💰 Tabela de Preços (Menu)

**Hambúrgueres:**
| Item | Preço |
| :--- | :--- |
| Cheeseburger | R$ 25.00 |
| X-Burger | R$ 20.00 |
| X-Egg | R$ 30.00 |
| X-Dog | R$ 35.00 |
| X-Full | R$ 40.00 |

**Bebidas:**
| Item | Preço |
| :--- | :--- |
| CocaCola | R$ 8.00 |
| Pepsi | R$ 8.50 |
| Fanta | R$ 7.50 |
| Sprite | R$ 7.50 |
| Water | R$ 5.00 |

## 🛠️ Tecnologias Utilizadas

* **C# .NET Core**: Web API robusta.
* **Dictionary Optimization**: Uso de dicionários para busca de preços em alta performance (O(1)).
* **Clean Code**: Separação clara entre Entidades, Modelos e Controladores.
* **Swagger/OpenAPI**: Documentação interativa dos endpoints.

## 🔌 Endpoints da API

### 1. Criar Pedido (Abrir Comanda)
**POST** `/api/orders`
* Cria um novo pedido vazio.
* **Body:** `{ "customerName": "João Silva" }`

### 2. Adicionar Item
**POST** `/api/orders/{id}/items`
* Adiciona um combo ao pedido.
* **Body:**
    ```json
    {
      "name": "X-Bacon",
      "drink": 1  // (Enum: 1=Coca, 2=Pepsi...)
    }
    ```
* *Nota: O sistema calcula o preço total (Burger + Drink) automaticamente.*

### 3. Consultar Pedidos
**GET** `/api/orders`
* Lista todos os pedidos, mostrando os itens detalhados e o `TotalPrice` somado.

### 4. Consultar Pedido Único
**GET** `/api/orders/{id}`
* Detalhes de um pedido específico.

### 5. Atualizar Status
**PATCH** `/api/orders/{id}/status`
* Avança o status do pedido.
* **Body:** `{ "status": 2 }` (1=Open, 2=Confirmed, 3=Delivered, 0=Canceled)
* *Trava:* Retorna erro 400 se tentar confirmar (`2`) um pedido sem itens.

### 6. Cancelar/Deletar
**DELETE** `/api/orders/{id}`
* Remove o pedido do sistema.

## 📦 Como Rodar o Projeto

1.  Clone este repositório ou baixe os arquivos.
2.  Abra o arquivo `.sln` no **Visual Studio**.
3.  Aperte `F5` ou clique no botão de "Play" (https).
4.  O navegador abrirá automaticamente no **Swagger UI**.
5.  Use a interface para testar os endpoints.

---
*Projeto desenvolvido para estudo de Estruturas de Dados e Lógica de Backend em C#.*