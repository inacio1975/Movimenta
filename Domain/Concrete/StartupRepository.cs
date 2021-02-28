using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class StartupRepository : IStartupRepository
    {
        public void AddStartup(Startup startup)
        {
            throw new NotImplementedException();
        }

        public bool EliminStartup(int id)
        {
            throw new NotImplementedException();
        }

        public List<Startup> GetStartups()
        {
            throw new NotImplementedException();
        }

        public List<Startup> GetStartups(string selector)
        {
            throw new NotImplementedException();
        }

        public Startup GetStartup(int id)
        {
            using (var context = new MovimentaContext())
            {
                return (from startup in context.Startups
                        where startup.StartupId == id
                        select startup).SingleOrDefault();
            }
        }

        public Startup GetStartupMember(int id)
        {
            using (var context = new MovimentaContext())
            {
                return (from startup in context.Startups
                        where startup.StartupId == id
                        select startup).SingleOrDefault();
            }
        }
    }
}
