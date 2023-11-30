using eCommerce.Office;
using Microsoft.EntityFrameworkCore;

var db = new eCommerceOfficeContext();

#region Many-To-Many for EF Core <= 3.1 
// Setor > ColaboradoresSetores > Colaborador
/*
var resultado = db.Setores!.Include(a => a.ColaboradoresSetores!).ThenInclude(a => a.Colaborador);

foreach (var setor in resultado)
{
    Console.WriteLine(setor.Nome);
    foreach (var colabSetor in setor.ColaboradoresSetores!)
    {
        Console.WriteLine($" - {colabSetor.Colaborador.Nome}");
    }
}
*/
#endregion

#region Many-To-Many for EF Core 5.0+
//var resultado2 = db.Colaboradores!.Include(a => a.Turmas);
//foreach (var colab in resultado2)
//{
//    Console.WriteLine(colab.Nome);
//    foreach (var turma in colab.Turmas!)
//    {
//        Console.WriteLine($" - {turma.Nome}");
//    }
//}
#endregion

#region Many-To-Many + Payload for EF Core 5.0+
var colabVeiculo = db.Colaboradores!.Include(a => a.ColaboradoresVeiculos!).ThenInclude(a => a.Veiculo);

foreach (var colab in colabVeiculo)
{
    Console.WriteLine($"{colab.Nome}");
    foreach (var vinculo in colab.ColaboradoresVeiculos!)
    {
        Console.WriteLine($" - {vinculo.Veiculo.Nome} ({vinculo.Veiculo.Placa}) : {vinculo.DataDeVinculo}");
    }
}
#endregion