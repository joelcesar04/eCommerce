using eCommerce.Console.Database;

var db = new eCommerceContext();

foreach (var user in db.Usuarios)
{
    Console.WriteLine(user.Nome);
}

Console.WriteLine("Hello, World!");
