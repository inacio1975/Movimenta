using System.Data.Entity;
using Domain.Concrete;
using Domain.Data;
using Microsoft.Owin;
using Owin;
using Web.Models;

[assembly: OwinStartupAttribute(typeof(Web.Startup))]
namespace Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //Garante que a base de dados se actualize sempre que há uma mudança no modelo
            //DropCreateDatabaseIfModelChanges<MovimentaContext> database = new DropCreateDatabaseIfModelChanges<MovimentaContext>();
            //database.InitializeDatabase(new MovimentaContext());

            //inicializa a base de dados com informações para testes
            DbInitializer.Initialize(new MovimentaContext());
            new ApplicationDbContext().Database.CreateIfNotExists();
        }
    }
}
