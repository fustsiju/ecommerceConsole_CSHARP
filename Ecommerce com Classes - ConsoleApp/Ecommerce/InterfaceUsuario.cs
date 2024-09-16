using MySql.Data.MySqlClient;

class UserInterface
{
    public void menuPrincipal()
    {
        Console.WriteLine("------------MENU------------");
        Console.WriteLine("1 - Login");
        Console.WriteLine("2 - Cadastro");
        Console.WriteLine("3 - Sair");
        Console.Write("Opcao: ");
    }

    public void menuLogin(out string username, out string password)
    {
        Console.Clear();
        Console.WriteLine("-------TELA DE LOGIN-------");
        Console.Write("Insira o usuário: ");
        username = Console.ReadLine();
        Console.Write("Insira a senha: ");
        password = Console.ReadLine();
    }

    public void menuAdmin()
    {
        
        Console.WriteLine("-----MENU ADMIN-----");
        Console.WriteLine("1 - Cadastrar Produtos");
        Console.WriteLine("2 - Ver Produtos");
        Console.WriteLine("3 - Atualizar Produtos");
        Console.WriteLine("4 - Deletar Produtos");
        Console.WriteLine("5 - Criar conta Admin");
        Console.WriteLine("0 - Fechar programa\n");
        Console.Write("Opcao: ");
    }
    public void menuUsuario()
    {
        
        Console.WriteLine("-----MENU USUARIO-----");
        Console.WriteLine("1 - Comprar");
        Console.WriteLine("2 - Carrinho");
        Console.WriteLine("0 - Fechar programa\n");
        Console.Write("Opção: ");
    }
    public void cadUsuario(out string cadUser, out string cadPwdUser)
    {
        Console.Clear();
        Console.WriteLine("-------TELA DE CADASTRO USUÁRIO-------");
        Console.WriteLine("Insira o nome do cadastro: ");
        cadUser = Console.ReadLine();
        Console.WriteLine("Insira a senha: ");
        cadPwdUser = Console.ReadLine();

    }
}
