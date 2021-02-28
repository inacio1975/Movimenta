using System;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;
using Domain.Entities;

namespace Domain.Concrete
{
    public enum EstadoConta
    {
        Sucesso,
        Falha,
        Bloquada,
        ReuqerVerificacao
    }


    public static class ManejaConta
    {
        public static EstadoConta Entrar(string email, string password)
        {
            var usuarios = new UsuarioRepository();

            Usuario usuario = usuarios.GeUsuarioByEmail(email);
            if (usuario != null)
            {
                //try
                //{
                    
                    if (VerificaHasedPassword(usuario.PasswordHash, password))
                    {
                        return EstadoConta.Sucesso;
                    }
                    
                //}
                //catch (FormatException f)
                //{

                //}
            }
            return EstadoConta.Falha;
        }

        public static EstadoConta Registrar(string email, string password)
        {
            try
            {
                var usuarios = new UsuarioRepository();
                usuarios.AddUsuario(new Usuario
                {
                    UsuarioId = Guid.NewGuid(),
                    Email = email,
                    EmailConfirmado = false,
                    TelefoneConfirmado = false,
                    PasswordHash = HashPassword(password),
                    DataRegistro = DateTime.Now,
                    AcessoFailedCount = 0
                });
                return EstadoConta.Sucesso;
            }
            catch (Exception)
            {
                return EstadoConta.Falha;
                throw;
            }
        }

        private static string HashPassword(string password)
        {
            byte[] salt, buffer2;
            if (password == null)
            {
                throw new ArgumentException("password");
            }
            using (var bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            var dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        private static bool VerificaHasedPassword(string hashedpassword, string password)
        {
            byte[] buffer4;
            if (hashedpassword == null)
                return false;
            if (password == null)
                throw new ArgumentException("password");
            Debug.WriteLine(hashedpassword);
            var src = Convert.FromBase64String(hashedpassword);
            if ((src.Length != 0x31) || (src[0] != 0))
                return false;
            var dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            var buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (var bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
            //return buffer3.Equals(buffer4);
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if(a==null || b == null || a.Length != b.Length)
                return false;
            var areSame = true;
            for (int i = 0; i < a.Length; i++)
                areSame &= (a[i] == b[i]);
            return areSame;
        }
    }
}