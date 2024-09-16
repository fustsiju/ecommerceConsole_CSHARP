using MySql.Data.MySqlClient;
using System.Collections;

class DatabaseManager
{
    private string connectionString = "Server=localhost;Database=ecommerce;User ID=root;Password=201023;";
    float totalCarrinho = 0;
    ArrayList carrinho = new ArrayList();
    UserInterface iu = new UserInterface();
    public MySqlConnection Connect()
    {
        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();
        return connection;
    }

    public string GetPasswordHash(string username)
    {
        using (var connection = Connect())
        {
            string queryLogin = "SELECT userpwd_hash FROM users WHERE username = @username";
            MySqlCommand cmd = new MySqlCommand(queryLogin, connection);
            cmd.Parameters.AddWithValue("@username", username);

            return (string)cmd.ExecuteScalar();
        }
    }

    public int GetUserType(string username)
    {
        using (var connection = Connect())
        {
            string query = "SELECT type FROM users WHERE username = @username";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@username", username);

            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
        }
    }
        public void addUser(string username, string password)
    {
        using (var connection = Connect())
        {
            string query = "INSERT INTO users (username, userpwd_hash, type) VALUES (@username, @password, 0);";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.ExecuteNonQuery();
        }
    }

    public void addProd(string nomeProd, float valorProd, int quantProd)
    {
        using (var connection = Connect())
        {
            string query = "INSERT INTO prod (nome, valor, quant) VALUES (@nome, @valor,@quant);";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@nome", nomeProd);
            cmd.Parameters.AddWithValue("@valor", valorProd);
            cmd.Parameters.AddWithValue("@quant", quantProd);
            cmd.ExecuteNonQuery();
        }
    }
    public void showProd()
    {
        using (var connection = Connect())
        {
            string query = "SELECT * FROM prod;";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                Console.WriteLine("Produtos Cadastrados: ");

                using (MySqlDataReader prods = command.ExecuteReader())
                {
                    while (prods.Read())
                    {
                        Console.Write("ID: " + prods["id"].ToString() + " - ");
                        Console.Write("Nome: " + prods["nome"].ToString() + " ");
                        Console.WriteLine("Valor: R$ " + prods["valor"].ToString());

                    }
                    prods.Close();
                    Console.WriteLine("\n");

                }

            }

        }
    }
    public void alterProd()
    {

        using (var connection = Connect())
        {

            string query = "SELECT * FROM prod;";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                Console.WriteLine("Produtos Cadastrados: ");

                using (MySqlDataReader prods = command.ExecuteReader())
                {
                    while (prods.Read())
                    {
                        Console.Write("ID: " + prods["id"].ToString() + " - ");
                        Console.Write("Nome: " + prods["nome"].ToString() + " ");
                        Console.WriteLine("Valor: R$ " + prods["valor"].ToString());

                    }
                    prods.Close();
                    Console.WriteLine("\n");
                }
            }
        }

        Console.Write("ID: ");
        int altProd = int.Parse(Console.ReadLine());

        Console.WriteLine("Produto Selecionado!\n");

        Console.WriteLine("O que deseja alterar? ");
        Console.WriteLine("1 - Nome");
        Console.WriteLine("2 - Valor");
        Console.WriteLine("3 - Quantidade");
        Console.WriteLine("0 - Voltar");

        int respUser = int.Parse(Console.ReadLine());

        if (respUser == 1)
        {
            Console.WriteLine("Qual o novo nome? ");
            String novoNomeProd = Console.ReadLine();
            using (var connection = Connect())
            {
                String queryAltNomeProd = "UPDATE prod SET nome = @nomeAlt WHERE id = @altProd";
                MySqlCommand cmdAltNome = new MySqlCommand(queryAltNomeProd, connection);
                cmdAltNome.Parameters.AddWithValue("@nomeAlt", novoNomeProd);
                cmdAltNome.Parameters.AddWithValue("@altProd", altProd);

                cmdAltNome.ExecuteNonQuery();
            }
            Console.WriteLine("Produto atualizado!");
        }
        else if (respUser == 2)
        {
            Console.WriteLine("Qual o novo valor? ");
            String novoValProd = Console.ReadLine();
            String queryAltValProd = "UPDATE prod SET valor = @valAlt WHERE id = @altProd";
            using (var connection = Connect())
            {
                MySqlCommand cmdAltNome = new MySqlCommand(queryAltValProd, connection);
                cmdAltNome.Parameters.AddWithValue("@valAlt", novoValProd);
                cmdAltNome.Parameters.AddWithValue("@altProd", altProd);

                cmdAltNome.ExecuteNonQuery();
            }
            Console.WriteLine("Produto atualizado!");
        }
        else if (respUser == 3)
        {
            Console.WriteLine("Qual a nova quantidade? ");
            String novaQuantProd = Console.ReadLine();
            String queryAltQuantProd = "UPDATE prod SET quant = @quantAlt WHERE id = @altProd";
            using (var connection = Connect())
            {
                MySqlCommand cmdAltQuant = new MySqlCommand(queryAltQuantProd, connection);
                cmdAltQuant.Parameters.AddWithValue("@quantAlt", novaQuantProd);
                cmdAltQuant.Parameters.AddWithValue("@altProd", altProd);

                cmdAltQuant.ExecuteNonQuery();
            }
            Console.WriteLine("Produto atualizado!");
        }
        else if (respUser == 0)
        {
            return;

        }

    }

    public void delProd()
    {
        showProd();

        Console.WriteLine("Qual o id do produto a ser excluido? ");
        int idDelProd = int.Parse(Console.ReadLine());
        using (var connection = Connect())
        {
            String queryDelProd = "DELETE FROM prod WHERE id = @idDel";
            MySqlCommand cmdDelProd = new MySqlCommand(queryDelProd, connection);
            cmdDelProd.Parameters.AddWithValue("@idDel", idDelProd);
            cmdDelProd.ExecuteNonQuery();

            Console.WriteLine("Produto excluído com sucesso!");
        }
    }


    public void addAdmin()
    {
        Console.WriteLine("Nome novo admin: ");
        String newAdmin = Console.ReadLine();
        Console.WriteLine("Senha novo admin: ");
        String newPwdAdmin = Console.ReadLine();

        String queryAddAdmin = "INSERT INTO users (username, userpwd_hash, type) VALUES (@newAdmin, @newPwdAdmin, 1)";
        using (var connection = Connect())
        {
            MySqlCommand cmdAddAdmin = new MySqlCommand(queryAddAdmin, connection);
            cmdAddAdmin.Parameters.AddWithValue("@newAdmin", newAdmin);
            cmdAddAdmin.Parameters.AddWithValue("newPwdAdmin", newPwdAdmin);
            cmdAddAdmin.ExecuteNonQuery();
            Console.Clear();
            Console.WriteLine("Novo admin cadastrado!\n");
            return;
        }
    }

    public void compraProd()
    {
        using (var connection = Connect())
        {
            showProd();

            Console.WriteLine("ID do produto para compra: ");
            int compraId = int.Parse(Console.ReadLine());
            Console.WriteLine("Quantidade: ");
            int compraQuant = int.Parse(Console.ReadLine());

            String totalCompraQuery = "SELECT SUM(valor * @compraQuant) FROM prod WHERE id = @compraId";
            String nomeItemQuery = "SELECT nome FROM prod WHERE id = @compraId";
            MySqlCommand cmdTotalCompraQuery = new MySqlCommand(@totalCompraQuery, connection);
            MySqlCommand cmdNomeQuery = new MySqlCommand(nomeItemQuery, connection);
            cmdNomeQuery.Parameters.AddWithValue("@compraId", compraId);
            cmdTotalCompraQuery.Parameters.AddWithValue("@compraId", compraId);
            cmdTotalCompraQuery.Parameters.AddWithValue("@compraQuant", compraQuant);

            var nome = cmdNomeQuery.ExecuteScalar();
            double totalCompra = (double)cmdTotalCompraQuery.ExecuteScalar();

            string compra = $"Produto: {nome} - Quantidade: {compraQuant} - Total: R${totalCompra}";

            totalCarrinho += (int)totalCompra;

            carrinho.Add(compra);

            Console.WriteLine("Produtos enviados ao carrinho!");

        }
    }
    public void verProd()
    {
        Console.WriteLine("Produtos no carrinho");

        for (int i = 0; i < carrinho.Count; i++)
        {
            Console.WriteLine(carrinho[i]);
        }

        Console.WriteLine($"Total carrinho: {totalCarrinho}");

        Console.WriteLine("\n1 - Finalizar a compra");
        Console.WriteLine("0 - Voltar");

        int respCarrinho = int.Parse(Console.ReadLine());

        if (respCarrinho == 1)
        {
            Console.WriteLine("Escolha a forma de pagamento: ");
            using (var connection = Connect())
            {
                String pagDispo = "SELECT * FROM pagamento";
                MySqlCommand cmdItensDisp = new MySqlCommand(pagDispo, connection);
                cmdItensDisp.ExecuteNonQuery();

                using (MySqlDataReader readPag = cmdItensDisp.ExecuteReader())
                {
                    while (readPag.Read())
                    {
                        Console.Write("ID: " + readPag["id"].ToString() + " - ");
                        Console.WriteLine("Pagamento: " + readPag["pag"].ToString() + " ");

                    }

                    readPag.Close();

                }
                Console.WriteLine("\n");
                Console.Write("Opção: ");
                int pagEsc = int.Parse(Console.ReadLine());

                Console.WriteLine("Pagamento Efetuado com Sucesso!!!");
                totalCarrinho = 0;
                carrinho.Clear();
            }
        }
        else
        {
            return;
        }
    }
}

