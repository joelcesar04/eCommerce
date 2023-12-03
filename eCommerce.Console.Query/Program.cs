using eCommerce.API.Database;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

/*
 * EF Core > Support LINQ > SQL - EFCore > SGDB
 * To, Find, First, Single, Last, Count, Max, Min... Executa o Código SQL -> App -> C# (Memory)
 * 
 * db.Usuario.{Methods LINQ > EF > SQL > SGDB}.ToList().{Methods > LINQ > C# > Processador + Memory};
 */

var db = new eCommerceContext();
var usuarios = db.Usuarios.ToList();

Console.WriteLine("LISTA DE USUÁRIOS");
foreach (var usuario in usuarios)
{
    Console.WriteLine($" - {usuario.Nome}");
}

Console.WriteLine("BUSCAR 1 USUÁRIO");
//var usuario01 = db.Usuarios.Find(1);
//var usuario01 = db.Usuarios.Where(a => a.Email == "joelcesar0@gmail.com").First();
//var usuario01 = db.Usuarios.Where(a => a.Email == "filipe@gmail.com").FirstOrDefault();
//var usuario01 = db.Usuarios.OrderBy(a => a.Id).Last();
//var usuario01 = db.Usuarios.OrderBy(a => a.Id).Where(a => a.Email == "filipe@gmail.com").LastOrDefault();
//var usuario01 = db.Usuarios.FirstOrDefault(a => a.Id == 1);
var usuario01 = db.Usuarios.Single(a => a.Email == "filipe@gmail.com");
//var usuario01 = db.Usuarios.SingleOrDefault(a => a.Nome.Contains("a"));

if (usuario01 == null)
    Console.WriteLine("Usuário não encontrado!");
else
    Console.WriteLine($" CÓD: {usuario01!.Id} - NOME: {usuario01!.Nome}");

var count = db.Usuarios.Where(a => a.Nome.Contains("a")).Count();
Console.WriteLine($"NÚMERO DE USUÁRIOS QUE CONTÉM A LETRA 'A' EM SEU NOME: {count}");

var min = db.Usuarios.Min(a => a.Id);
Console.WriteLine($"VALOR MIN(ID): {min}");

var max = db.Usuarios.Max(a => a.Id);
Console.WriteLine($"VALOR MAX(ID): {max}");

// WHERE

Console.WriteLine("LISTA DE USUÁRIOS (WHERE)");
//var usuariosList = db.Usuarios.Where(a => a.Nome == "José Ribeiro").ToList();
//var usuariosList = db.Usuarios.Where(a => EF.Functions.Like(a.Nome, "%Ribeiro%")).ToList(); // LIKE
//var usuariosList = db.Usuarios.Where(a => a.Nome.StartsWith("J")).ToList();
//var usuariosList = db.Usuarios.Where(a => a.Nome.EndsWith("Ribeiro")).ToList();
var usuariosList = db.Usuarios.Where(a => a.Nome.Contains("a") || a.Nome.Contains("j")).ToList();
foreach (var usuario in usuariosList)
{
    Console.WriteLine($" - {usuario.Nome}");
}

// OrderBy, OrderByDescending, ThenBy, ThenByDescending

Console.WriteLine("LISTA DE USUÁRIOS (ORDER)");
//var usuariosListOrder = db.Usuarios.OrderBy(a => a.Nome).ToList();
//var usuariosListOrder = db.Usuarios.OrderByDescending(a => a.Nome).ToList();
//var usuariosListOrder = db.Usuarios.OrderBy(a => a.Sexo).ThenBy(a => a.Nome).ToList();
var usuariosListOrder = db.Usuarios.OrderBy(a => a.Sexo).ThenByDescending(a => a.Nome).ToList();
foreach (var usuario in usuariosListOrder)
{
    Console.WriteLine($" - {usuario.Nome}");
}

/*
 * EagerLoad (Carregamento Adiantado)
 * Include, ThenInclude e AutoInclude
 * Include (Nível 1)
 * ThenInclude (Nível 2)
 */

Console.WriteLine("LISTA DE USUÁRIOS (INCLUDE)");
var usuariosListInclude = db.Usuarios
    .Include(a => a.Contato)
    .Include(a => a.EnderecosEntrega!.Where(a => a.Estado == "SP"))
    .Include(a => a.Departamentos)
    .ToList();
foreach (var usuario in usuariosListInclude)
{
    Console.WriteLine($" - {usuario.Nome} / TEL: {usuario.Contato!.Telefone} / QNT. END.: {usuario.EnderecosEntrega!.Count} / QNT. DEP.: {usuario.Departamentos!.Count}");

    foreach (var endereco in usuario.EnderecosEntrega)
    {
        Console.WriteLine($"  -- {endereco.NomeEndereco}: {endereco.CEP} - {endereco.Estado} - {endereco.Bairro} - {endereco.Endereco}");
    }
}

Console.WriteLine("LISTA DE USUÁRIOS (THENINCLUDE)");
var contatos = db.Contatos
    .Include(a => a.Usuario)
    .ThenInclude(u => u!.EnderecosEntrega)
    .Include(a => a.Usuario)
    .ThenInclude(a => a!.Departamentos)
    .ToList();
foreach (var contato in contatos)
{
    Console.WriteLine($" - {contato.Telefone} -> {contato.Usuario!.Nome}");
    
    Console.WriteLine("  CID:");
    foreach (var endereco in contato.Usuario.EnderecosEntrega!)
    {
        Console.WriteLine($"   -- {endereco.Cidade}");
    }

    Console.WriteLine("  DEP: ");
    foreach (var departamento in contato.Usuario.Departamentos!)
    {
        Console.WriteLine($"   -- {departamento.Nome}");
    }
}


Console.WriteLine("LISTA DE USUÁRIOS (AUTOINCLUDE)");
var usuariosAutoInclude = db.Usuarios.IgnoreAutoIncludes().ToList();
foreach (var usuario in usuariosAutoInclude)
{
    Console.WriteLine($"NOME: {usuario.Nome} - TEL: {usuario.Contato?.Telefone}");
}

/*
 * EXPLICIT LOAD - Carregamento Explícito
 */

db.ChangeTracker.Clear();

Console.WriteLine("CARREGAMENTO EXPLÍCITO:");
var usuarioExplicitLoad = db.Usuarios.IgnoreAutoIncludes().FirstOrDefault(a => a.Id == 1);
Console.WriteLine($"NOME: {usuarioExplicitLoad!.Nome} - TEL: {usuarioExplicitLoad!.Contato?.Telefone} - END: {usuarioExplicitLoad.EnderecosEntrega?.Count}");

db.Entry(usuarioExplicitLoad).Reference(a => a.Contato).Load();
db.Entry(usuarioExplicitLoad).Collection(a => a.EnderecosEntrega!).Load();
Console.WriteLine($"NOME: {usuarioExplicitLoad!.Nome} - TEL: {usuarioExplicitLoad!.Contato!.Telefone} - END: {usuarioExplicitLoad.EnderecosEntrega?.Count}");

var enderecos = db.Entry(usuarioExplicitLoad).Collection(a => a.EnderecosEntrega!).Query().Where(a => a.Estado == "SP").ToList();

foreach (var endereco in enderecos)
{
    Console.WriteLine($" -- {endereco.NomeEndereco}: {endereco.Estado} {endereco.Bairro} {endereco.Endereco} - {endereco.Usuario!.Nome}");
}

/*
 * LAZY LOADING - Carregamento Preguiçoso
 * - Proxies
 * - S/ Proxies
 * -- ILazyLoader (EF Core)
 * -- Delegate (Modelo -> Acoplamento com ILazyLoader)
 */

Console.WriteLine("CARREGAMENTO PREGUIÇOSO:");
db.ChangeTracker.Clear();

var usuarioLazyLoad = db.Usuarios.Find(1);
Console.WriteLine($" - NOME: {usuarioLazyLoad!.Nome} - END: {usuarioLazyLoad.EnderecosEntrega?.Count}");

/*
 * SPLITQUERY - Query Dividida
 */

Console.WriteLine("QUERY DIVIDIDA:");
var usuarioSPlitQuery = db.Usuarios.AsSplitQuery().Include(a => a.EnderecosEntrega).FirstOrDefault(a => a.Id == 1);
Console.WriteLine($" - NOME: {usuarioSPlitQuery!.Nome} - END: {usuarioSPlitQuery.EnderecosEntrega?.Count}");

/*
 * TAKE - SKIP [Páginação]
 * TAKE - TAKE(2) - Obter uma quantidade definida de registros.
 * SKIP - SKIP(2) - Pular uma quantidade definida de registros.
 */

Console.WriteLine("TAKE E SKIP:");
var usuariosSkipTake = db.Usuarios.Skip(1).Take(2).ToList();
foreach (var usuario in usuariosSkipTake)
{
    Console.WriteLine($" -- {usuario.Nome}");
}

/*
 * SELECT
 */

Console.WriteLine("SELECT:");
var usuariosSelect = db.Usuarios
    .Where(a => a.Id > 2)
    .Select(a => new { a.Id, a.Nome, a.NomeMae })
    .ToList();

foreach (var usuario in usuariosSelect)
{
    Console.WriteLine($" - COD: {usuario.Id} - NOME: {usuario.Nome} - MÃE: {usuario.NomeMae}");
}
