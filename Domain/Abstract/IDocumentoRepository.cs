using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IDocumentoRepository
    {
        void AddDocumento(Documento Documento);
        bool EliminDocumento(int id);

        List<Documento> GetDocumentos();
        List<Documento> GetDocumentos(string selector);
        Documento GetDocumentos(int id);
    }
}
