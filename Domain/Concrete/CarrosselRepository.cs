using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class CarrosselRepository : ICarrosselRepository
    {
        public void AddCarrossel(Carrossel carrossel)
        {
            using (var context = new MovimentaContext())
            {
                context.Carrossels.Add(carrossel);
                context.SaveChanges();
            }
        }

        public bool EliminaCarrossel(int id)
        {
            throw new NotImplementedException();
        }

        public List<Carrossel> GetCarrossels()
        {
            using (var context = new MovimentaContext())
            {
                return context.Carrossels.ToList();
            }
        }
    }
}
