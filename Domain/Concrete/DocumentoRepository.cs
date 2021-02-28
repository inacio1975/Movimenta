using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class DocumentoRepository : IDocumentoRepository
    {
        public void AddDocumento(Documento documento)
        {
            throw new NotImplementedException();
        }

        public bool EliminDocumento(int id)
        {
            throw new NotImplementedException();
        }

        public List<Documento> GetDocumentos()
        {
            throw new NotImplementedException();
        }

        public List<Documento> GetDocumentos(string selector)
        {
            throw new NotImplementedException();
        }

        public Documento GetDocumentos(int id)
        {
            throw new NotImplementedException();
        }
    }
}
