using MyMySQLApp;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace MyMySQLApp
{
    class Program
    {
         
        static void Main(string[] args)
        {

            string connectionString = "Server=localhost;Database=ecommerce;User ID=root;Password=;";
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection successful!");
                    ArrayList carrinho = new ArrayList();
                    var totalCarrinho = 0;
                    while (true)
                    {
                        {
                            Console.WriteLine("------------MENU------------");
                            Console.WriteLine("1 - Login");
                            Console.WriteLine("2 - Cadastro");
                            Console.WriteLine("3 - Sair");
                            int respUsuario = int.Parse(Console.ReadLine());
                            if (respUsuario == 1)
                            {
                                Console.Clear();
                                Console.WriteLine("-------TELA DE LOGIN-------");
                                Console.WriteLine("Insira o usuário: ");
                                String loginUser = Console.ReadLine();
                                Console.WriteLine("Insira a senha: ");
                                String pwdUser = Console.ReadLine();



                                string queryLogin = "SELECT userpwd_hash FROM users WHERE username = @username";
                                MySqlCommand cmd = new MySqlCommand(queryLogin, connection);
                                cmd.Parameters.AddWithValue("@username", loginUser);

                                var result = cmd.ExecuteScalar();

                                if (result != null)
                                {
                                    string storedPasswordHash = result.ToString();


                                    while (storedPasswordHash == pwdUser)
                                    {
                                        string queryTypeLogin = "SELECT type FROM users WHERE username = @username";
                                        MySqlCommand cmdTypeUser = new MySqlCommand(queryTypeLogin, connection);
                                        cmdTypeUser.Parameters.AddWithValue("@username", loginUser);
                                        bool type = (bool) cmdTypeUser.ExecuteScalar();
                                        if (type == true)
                                        {
                                           
                                            Console.WriteLine("-----MENU ADMIN-----");
                                            Console.WriteLine("1 - Cadastrar Produtos");
                                            Console.WriteLine("2 - Ver Produtos");
                                            Console.WriteLine("3 - Atualizar Produtos");
                                            Console.WriteLine("4 - Deletar Produtos");
                                            Console.WriteLine("5 - Criar conta Admin");
                                            Console.WriteLine("0 - Fechar programa\n");

                                            int loggedUser = int.Parse(Console.ReadLine());

                                            if (loggedUser == 1)
                                            {
                                                Console.WriteLine("Insira o nome do produto a dar entrada: ");
                                                String nomeProd = Console.ReadLine();
                                                Console.WriteLine("Insira o valor do produto: ");
                                                float valProd = float.Parse(Console.ReadLine());
                                                Console.WriteLine("Insira a quantidade: ");
                                                int quantProd = int.Parse(Console.ReadLine());

                                                String queryCad = "INSERT INTO prod (nome, valor, quant) VALUES (@nome, @valor, @quant)";
                                                MySqlCommand cmdCadProd = new MySqlCommand(@queryCad, connection);
                                                cmdCadProd.Parameters.AddWithValue("@nome", nomeProd);
                                                cmdCadProd.Parameters.AddWithValue("@valor", valProd);
                                                cmdCadProd.Parameters.AddWithValue("@quant", quantProd);
                                                cmdCadProd.ExecuteNonQuery();

                                                Console.Clear();

                                            }
                                            else if (loggedUser == 2)
                                            {

                                                String queryVisCad = "SELECT * FROM prod";

                                                using (MySqlCommand command = new MySqlCommand(queryVisCad, connection))
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
                                            else if (loggedUser == 3)
                                            {
                                                String queryVisCad = "SELECT * FROM prod;";

                                                using (MySqlCommand command = new MySqlCommand(queryVisCad, connection))
                                                {
                                                    Console.WriteLine("Escolha o ID do produto que vai alterar: ");

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
                                                    String queryAltNomeProd = "UPDATE prod SET nome = @nomeAlt WHERE id = @altProd";
                                                    MySqlCommand cmdAltNome = new MySqlCommand(queryAltNomeProd, connection);
                                                    cmdAltNome.Parameters.AddWithValue("@nomeAlt", novoNomeProd);
                                                    cmdAltNome.Parameters.AddWithValue("@altProd", altProd);

                                                    cmdAltNome.ExecuteNonQuery();

                                                    Console.WriteLine("Produto atualizado!");
                                                }
                                                else if (respUser == 2)
                                                {
                                                    Console.WriteLine("Qual o novo valor? ");
                                                    String novoValProd = Console.ReadLine();
                                                    String queryAltValProd = "UPDATE prod SET valor = @valAlt WHERE id = @altProd";
                                                    MySqlCommand cmdAltNome = new MySqlCommand(queryAltValProd, connection);
                                                    cmdAltNome.Parameters.AddWithValue("@valAlt", novoValProd);
                                                    cmdAltNome.Parameters.AddWithValue("@altProd", altProd);

                                                    cmdAltNome.ExecuteNonQuery();

                                                    Console.WriteLine("Produto atualizado!");
                                                }
                                                else if (respUser == 3)
                                                {
                                                    Console.WriteLine("Qual a nova quantidade? ");
                                                    String novaQuantProd = Console.ReadLine();
                                                    String queryAltQuantProd = "UPDATE prod SET quant = @quantAlt WHERE id = @altProd";
                                                    MySqlCommand cmdAltQuant = new MySqlCommand(queryAltQuantProd, connection);
                                                    cmdAltQuant.Parameters.AddWithValue("@quantAlt", novaQuantProd);
                                                    cmdAltQuant.Parameters.AddWithValue("@altProd", altProd);

                                                    cmdAltQuant.ExecuteNonQuery();

                                                    Console.WriteLine("Produto atualizado!");
                                                }
                                                else if (respUser == 0)
                                                {
                                                    continue;

                                                }
                                            }
                                            else if (loggedUser == 4)
                                            {
                                                String queryVisCad = "SELECT * FROM prod;";

                                                using (MySqlCommand command = new MySqlCommand(queryVisCad, connection))
                                                {


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

                                                Console.WriteLine("Qual o id do produto a ser excluido? ");
                                                int idDelProd = int.Parse(Console.ReadLine());

                                                String queryDelProd = "DELETE FROM prod WHERE id = @idDel";
                                                MySqlCommand cmdDelProd = new MySqlCommand(queryDelProd, connection);
                                                cmdDelProd.Parameters.AddWithValue("@idDel", idDelProd);
                                                cmdDelProd.ExecuteNonQuery();


                                                Console.WriteLine("Produto excluído com sucesso!");


                                            }else if (loggedUser == 5){
                                                Console.WriteLine("Nome novo admin: ");
                                                String newAdmin = Console.ReadLine();
                                                Console.WriteLine("Senha novo admin: ");
                                                String newPwdAdmin = Console.ReadLine();

                                                String queryAddAdmin = "INSERT INTO users (username, userpwd_hash, type) VALUES (@newAdmin, @newPwdAdmin, 1)";
                                                MySqlCommand cmdAddAdmin = new MySqlCommand(queryAddAdmin,  connection);
                                                cmdAddAdmin.Parameters.AddWithValue("@newAdmin", newAdmin);
                                                cmdAddAdmin.Parameters.AddWithValue("newPwdAdmin", newPwdAdmin);
                                                cmdAddAdmin.ExecuteNonQuery();
                                                Console.Clear();
                                                Console.WriteLine("Novo admin cadastrado!\n");
                                                continue;
                                            }
                                            else if (loggedUser == 0)
                                            {
                                                Environment.Exit(0);
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.WriteLine("ID não cadastrado!");
                                            }
                                        }
                                        else
                                        {

                                            Console.WriteLine(type);
                                            Console.WriteLine("-----MENU USUARIO-----");
                                            Console.WriteLine("1 - Comprar");
                                            Console.WriteLine("2 - Carrinho");
                                            Console.WriteLine("0 - Fechar programa\n");

                                            Console.Write("Opção: ");
                                            int resp = int.Parse(Console.ReadLine());

                                            if(resp == 1)
                                            {
                                                String itensDispo = "SELECT * FROM prod";
                                                MySqlCommand cmdItensDisp = new MySqlCommand(itensDispo, connection);
                                                cmdItensDisp.ExecuteNonQuery() ;

                                                using (MySqlDataReader readItens = cmdItensDisp.ExecuteReader())
                                                {
                                                    while (readItens.Read())
                                                    {
                                                        Console.Write("ID: " + readItens["id"].ToString() + " - ");
                                                        Console.Write("Nome: " + readItens["nome"].ToString() + " ");
                                                        Console.WriteLine("Valor: R$ " + readItens["valor"].ToString());


                                                    }


                                                    readItens.Close();
                                                   

                                                }
                                                Console.WriteLine("\n");

                                                Console.WriteLine("ID do produto para compra: ");
                                                int compraId = int.Parse(Console.ReadLine()) ;
                                                Console.WriteLine("Quantidade: ");
                                                int compraQuant = int.Parse(Console.ReadLine() );


                                                String totalCompraQuery = "SELECT SUM(valor * @compraQuant) FROM prod WHERE id = @compraId";
                                                String nomeItemQuery = "SELECT nome FROM prod WHERE id = @compraId";
                                                MySqlCommand cmdTotalCompraQuery = new MySqlCommand(@totalCompraQuery, connection);
                                                MySqlCommand cmdNomeQuery = new MySqlCommand(nomeItemQuery, connection);
                                                cmdNomeQuery.Parameters.AddWithValue("@compraId", compraId);
                                                cmdTotalCompraQuery.Parameters.AddWithValue("@compraId", compraId);
                                                cmdTotalCompraQuery.Parameters.AddWithValue("@compraQuant", compraQuant);

                                                var nome = cmdNomeQuery.ExecuteScalar();
                                                double totalCompra = (double) cmdTotalCompraQuery.ExecuteScalar();

                                                

                                                string compra = $"Produto: {nome} - Quantidade: {compraQuant} - Total: R${totalCompra}";



                                                totalCarrinho += (int)totalCompra;


                                                
                                                carrinho.Add(compra);
                                                
                                                Console.WriteLine("Produtos enviados ao carrinho!");


                                            } if(resp == 2)
                                            {
                                                Console.WriteLine("Produtos no carrinho");

                                                for( int i = 0; i < carrinho.Count; i++ )
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
                                                else
                                                {
                                                    continue;
                                                }
                                            }
                                            else if( resp == 0)
                                            { 
                                                Environment.Exit(0);
                                            }

                                            continue;

                                            
                                        }
                                        
                                        
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Usuário não encontrado.");
                                }

                            }

                            else if (respUsuario == 2)

                            {
                                Console.Clear();
                                Console.WriteLine("-------TELA DE CADASTRO USUÁRIO-------");
                                Console.WriteLine("Insira o nome do cadastro: ");
                                String cadUser = Console.ReadLine();
                                Console.WriteLine("Insira a senha: ");
                                String cadPwdUser = Console.ReadLine();

                                string queryCad = "INSERT INTO users (username, userpwd_hash, type) VALUES (@username, @password, 0);";
                                MySqlCommand cmd = new MySqlCommand(@queryCad, connection);
                                cmd.Parameters.AddWithValue("@username", cadUser);
                                cmd.Parameters.AddWithValue("@password", cadPwdUser);

                                cmd.ExecuteNonQuery();

                            }
                            else
                            {
                                Environment.Exit(0);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
