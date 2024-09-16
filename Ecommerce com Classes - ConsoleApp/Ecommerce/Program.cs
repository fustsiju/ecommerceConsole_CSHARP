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

            DatabaseManager dbconexao = new DatabaseManager();
            UserInterface iu = new UserInterface();
            SistemaLogin login = new SistemaLogin(dbconexao);
            SistemaProd produtos = new SistemaProd(dbconexao);

            while (true)
            {
                {
                    iu.menuPrincipal();
                    int respUsuario = int.Parse(Console.ReadLine());
                    if (respUsuario == 1)
                    {
                        String username, password;
                        iu.menuLogin(out username, out password);

                        while (true)
                        {
                            if (login.loginUser(username, password) && dbconexao.GetUserType(username) == 1)
                            {

                                iu.menuAdmin();

                                int loggedUser = int.Parse(Console.ReadLine());

                                if (loggedUser == 1)
                                {
                                    Console.Clear();
                                    string nomeProd;
                                    float valProd;
                                    int quantProd;
                                    produtos.registerProd(out nomeProd, out valProd, out quantProd);

                                    Console.Clear();
                                    continue;

                                }
                                else if (loggedUser == 2)
                                {
                                    
                                    dbconexao.showProd();
                                    continue;
                                }
                                else if (loggedUser == 3)
                                {
                                    
                                    dbconexao.alterProd();
                                    continue;
                                }
                                else if (loggedUser == 4)
                                {
                                    
                                    dbconexao.delProd();
                                    continue;
                                }
                                else if (loggedUser == 5)
                                {
                                    
                                    dbconexao.addAdmin();
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

                                iu.menuUsuario();
                                int resp = int.Parse(Console.ReadLine());
                                if (resp == 1)
                                {
                                    Console.Clear();
                                    dbconexao.compraProd();
                                }
                                if (resp == 2)
                                {
                                    Console.Clear();
                                    dbconexao.verProd();
                                    continue;
                                }
                                else if (resp == 0)
                                {
                                    Environment.Exit(0);
                                }

                                continue;

                            }
                        }
                    }




                    else if (respUsuario == 2)

                    {
                        string username, password;
                        iu.cadUsuario(out username, out password);
                        dbconexao.addUser(username, password);
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}

