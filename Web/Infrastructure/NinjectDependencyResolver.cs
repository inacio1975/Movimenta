using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Ninject;

namespace Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IBancoRepository>().To<BancoRepository>();
            kernel.Bind<IComentarioRepository>().To<ComentarioRepository>();
            kernel.Bind<IContactoRepository>().To<ContactoRepository>();
            kernel.Bind<IContactoStRepository>().To<ContactoStRepository>();
            kernel.Bind<IContactoMbRepository>().To<ContactoMbRepository>();
            kernel.Bind<IDocumentoRepository>().To<DocumentoRepository>();
            kernel.Bind<IEnderecoRepository>().To<EnderecoRepository>();
            kernel.Bind<IEnderecoMbRepository>().To<EnderecoMbRepository>();
            kernel.Bind<IEnderecoStRepository>().To<EnderecoStRepository>();
            kernel.Bind<IMembroRepository>().To<MembroRepository>();
            kernel.Bind<INovidadeRepository>().To<NovidadeRepository>();
            kernel.Bind<IProjectoRepository>().To<ProjectoRepository>();
            kernel.Bind<IStartupRepository>().To<StartupRepository>();
            kernel.Bind<IMovimentadorRepository>().To<MovimentadorRepository>();
            kernel.Bind<IUsuarioRepository>().To<UsuarioRepository>();
        }
    }
}