using System.Data.Entity;
using Domain.Entities;

namespace Domain.Concrete
{
    public class CategoriaRepository
    {
        public bool Eliminar(int id)
        {
            using (var context = new MovimentaContext())
            {
                var categoria = context.Categorias.Find(id);
                if (categoria != null)
                {
                    context.Categorias.Remove(categoria);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool Editar(Categoria element)
        {
            var flag = false;
            using (var context = new MovimentaContext())
            {
                context.Entry(element).State = EntityState.Modified;
                context.SaveChanges();
                flag = true;
            }
            return flag;
        }

        public bool Adicionar(Categoria element)
        {
            using (var context = new MovimentaContext())
            {
                context.Categorias.Add(element);
                context.SaveChanges();
            }
            return true;
        }
    }
}