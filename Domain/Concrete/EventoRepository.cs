using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EventoRepository
    {
        public List<Evento> GetEventos()
        {
            List<Evento> eventos;
            using (var context = new MovimentaContext())
            {
                eventos = context.Eventos.ToList();
            }
            return eventos;
        }

        public bool Add(Evento element)
        {
            using (var context = new MovimentaContext())
            {
                if (element == null) return false;
                context.Eventos.Add(element);
                context.SaveChanges();
            }
            return true;
        }

        public bool Editar(Evento element)
        {
            using (var context = new MovimentaContext())
            {
                if (element == null) return false;
                context.Entry(element).State = EntityState.Modified;
                context.SaveChanges();
            }
            return true;
        }

        public bool Eliminar(int id)
        {
            using (var context = new MovimentaContext())
            {
                var element = context.Eventos.Find(id);
                if (element != null)
                {
                    context.Eventos.Remove(element);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Evento GetEvento(int id)
        {
            Evento evento;
            using (var context = new MovimentaContext())
            {
                evento = context.Eventos.Find(id);
            }
            return evento;
        }

        public bool Exists(int id)
        {
            bool flag;
            using (var context = new MovimentaContext())
            {
                flag = context.Eventos.Find(id) != null;
            }
            return flag;
        }
    }
}
