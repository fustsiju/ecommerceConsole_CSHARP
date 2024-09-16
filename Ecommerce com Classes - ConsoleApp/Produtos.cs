class SistemaProd
{
    private DatabaseManager dbManager;

    public SistemaProd(DatabaseManager databaseManager)
    {
        dbManager = databaseManager;
    }
    public void registerProd(out string nomeProd,out float valProd,out int quantProd)
    {
        Console.WriteLine("Insira o nome do produto a dar entrada: ");
        nomeProd = Console.ReadLine();
        Console.WriteLine("Insira o valor do produto: ");
        valProd = float.Parse(Console.ReadLine());
        Console.WriteLine("Insira a quantidade: ");
        quantProd = int.Parse(Console.ReadLine());

        dbManager.addProd(nomeProd, valProd, quantProd);
    }


}
