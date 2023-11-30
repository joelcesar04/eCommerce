using eCommerce.API.Database;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly eCommerceContext _db;
        public UsuarioRepository(eCommerceContext db)
        {
            _db = db;
        }
        public List<Usuario> Get()
        {
            return _db.Usuarios
                .Include(a => a.Contato)
                .OrderBy(x => x.Id)
                .ToList();
        }

        public Usuario Get(int id)
        {
            // EF Core = 1; Dapper = +20; ADO.NET = +40
            return _db.Usuarios
                .Include(a => a.Contato)
                .Include(a => a.EnderecosEntrega)
                .Include(a => a.Departamentos)
                .FirstOrDefault(a => a.Id == id)!;
        }

        public void Add(Usuario usuario)
        {
            CriarVinculoDoUsuarioComDepartamento(usuario);
            
            _db.Usuarios.Add(usuario);
            _db.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            // TODO - Excluir os vínculos do Usuario com o Departamento
            ExcluirVinculoDoUsuarioComDepartamento(usuario);

            // TODO - Criar vínculos e Criar departamento se necessário
            CriarVinculoDoUsuarioComDepartamento(usuario);

            _db.Usuarios.Update(usuario);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Usuarios.Remove(Get(id));
            _db.SaveChanges();
        }

        private void CriarVinculoDoUsuarioComDepartamento(Usuario usuario)
        {
            if (usuario.Departamentos != null)
            {
                var departamentos = usuario.Departamentos;
                usuario.Departamentos = new List<Departamento>();

                foreach (var departamento in departamentos)
                {
                    if (departamento.Id > 0)
                    {
                        // Ref. Registro do Banco de Dados
                        usuario.Departamentos.Add(_db.Departamentos.Find(departamento.Id)!);
                    }
                    else
                    {
                        // Ref. Objeto novo, que não existe no banco de dados (Novo registro de Departamento)
                        usuario.Departamentos.Add(departamento);
                    }
                }
            }
        }

        private void ExcluirVinculoDoUsuarioComDepartamento(Usuario usuario)
        {
            var usuarioDoBanco = _db.Usuarios
                                        .Include(a => a.Departamentos)
                                        .FirstOrDefault(a => a.Id == usuario.Id);

            foreach (var departamento in usuarioDoBanco!.Departamentos!)
            {
                usuarioDoBanco.Departamentos.Remove(departamento);
            }

            _db.SaveChanges();
            _db.ChangeTracker.Clear();
        }
    }
}
