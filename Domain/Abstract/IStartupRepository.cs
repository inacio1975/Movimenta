using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IStartupRepository
    {
        void AddStartup(Startup startup);
        bool EliminStartup(int id);

        List<Startup> GetStartups();
        List<Startup> GetStartups(string selector);
        Startup GetStartup(int id);
    }
}
