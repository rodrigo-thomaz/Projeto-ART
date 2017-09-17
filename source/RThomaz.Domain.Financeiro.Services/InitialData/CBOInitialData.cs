using RThomaz.Infra.Data.Persistence.Contexts;
using RThomaz.Domain.Financeiro.Entities;
using System;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RThomaz.Domain.Financeiro.Services.InitialData
{
    public class CBOInitialData : IDisposable
    {
        #region private fields

        private readonly RThomazDbContext _context;
        private readonly Encoding _encoding;
        private readonly string _cboDirectoryName;

        #endregion

        #region constructors

        public CBOInitialData()
        {
            _context = new RThomazDbContext();

            var cultureInfo = new CultureInfo("pt-BR");
            _encoding = Encoding.GetEncoding(cultureInfo.TextInfo.ANSICodePage);

            string assemblyLocalPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var assemblyDirectoryName = Path.GetDirectoryName(assemblyLocalPath);

            _cboDirectoryName = Path.Combine(assemblyDirectoryName, "InitialData", "Files", "CBO");
        }

        #endregion

        #region public voids

        public async Task Seed()
        {
            var dbContextTransaction = _context.Database.BeginTransaction();

            try
            {
                SeedGrandeGrupo();
                SeedSubGrupoPrincipal();
                SeedSubGrupo();
                SeedFamilia();
                SeedOcupacao();
                SeedSinonimo();

                await _context.SaveChangesAsync();

                dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Dispose();
                }
            }
        }

        #endregion

        #region private voids

        private void SeedGrandeGrupo()
        {
            var fileName = Path.Combine(_cboDirectoryName, "CBO2002 - Grande Grupo.txt");

            var streamReader = new StreamReader(fileName, _encoding, true, 512);

            //Descartando coluna Título e Separador
            streamReader.ReadLine();
            streamReader.ReadLine();

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();

                var codigo = line.Substring(0, 6).Trim();
                var cboGrandeGrupoId = Convert.ToInt16(codigo);
                var titulo = line.Substring(7, line.Length - 7).Trim();

                _context.CBOGrandeGrupo.AddOrUpdate(x => x.CBOGrandeGrupoId,
                    new CBOGrandeGrupo
                    {
                        CBOGrandeGrupoId = cboGrandeGrupoId,
                        Codigo = codigo,
                        Titulo = titulo
                    }
                );
            }
        }

        private void SeedSubGrupoPrincipal()
        {
            var fileName = Path.Combine(_cboDirectoryName, "CBO2002 - SubGrupo Principal.txt");

            var streamReader = new StreamReader(fileName, _encoding, true, 512);

            //Descartando coluna Título e Separador
            streamReader.ReadLine();
            streamReader.ReadLine();

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();

                var codigo = line.Substring(0, 6).Trim();
                var cboSubGrupoPrincipalId = Convert.ToInt16(codigo);
                var titulo = line.Substring(7, line.Length - 7).Trim();
                var cboGrandeGrupoId = Convert.ToInt16(codigo.Substring(0, 1));

                _context.CBOSubGrupoPrincipal.AddOrUpdate(x => x.CBOSubGrupoPrincipalId,
                    new CBOSubGrupoPrincipal
                    {
                        CBOSubGrupoPrincipalId = cboSubGrupoPrincipalId,
                        CBOGrandeGrupoId = cboGrandeGrupoId,
                        Codigo = codigo,
                        Titulo = titulo
                    }
                );
            }
        }

        private void SeedSubGrupo()
        {
            var fileName = Path.Combine(_cboDirectoryName, "CBO2002 - SubGrupo.txt");

            var streamReader = new StreamReader(fileName, _encoding, true, 512);

            //Descartando coluna Título e Separador
            streamReader.ReadLine();
            streamReader.ReadLine();

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();

                var codigo = line.Substring(0, 6).Trim();
                var cboSubGrupoId = Convert.ToInt16(codigo);
                var titulo = line.Substring(7, line.Length - 7).Trim();
                var cboSubGrupoPrincipalId = Convert.ToInt16(codigo.Substring(0, 2));

                _context.CBOSubGrupo.AddOrUpdate(x => x.CBOSubGrupoId,
                    new CBOSubGrupo
                    {
                        CBOSubGrupoId = cboSubGrupoId,
                        CBOSubGrupoPrincipalId = cboSubGrupoPrincipalId,
                        Codigo = codigo,
                        Titulo = titulo
                    }
                );
            }
        }

        private void SeedFamilia()
        {
            var fileName = Path.Combine(_cboDirectoryName, "CBO2002 - Familia.txt");

            var streamReader = new StreamReader(fileName, _encoding, true, 512);

            //Descartando coluna Título e Separador
            streamReader.ReadLine();
            streamReader.ReadLine();

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();

                var codigo = line.Substring(0, 6).Trim();
                var cboFamiliaId = Convert.ToInt16(codigo);
                var titulo = line.Substring(7, line.Length - 7).Trim();
                var cboSubGrupoId = Convert.ToInt16(codigo.Substring(0, 3));

                _context.CBOFamilia.AddOrUpdate(x => x.CBOFamiliaId,
                    new CBOFamilia
                    {
                        CBOFamiliaId = cboFamiliaId,
                        CBOSubGrupoId = cboSubGrupoId,
                        Codigo = codigo,
                        Titulo = titulo
                    }
                );
            }
        }

        private void SeedOcupacao()
        {
            var fileName = Path.Combine(_cboDirectoryName, "CBO2002 - Ocupacao.txt");

            var streamReader = new StreamReader(fileName, _encoding, true, 512);

            //Descartando coluna Título e Separador
            streamReader.ReadLine();
            streamReader.ReadLine();

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();

                var codigo = line.Substring(0, 6).Trim();
                var cboOcupacaoId = Convert.ToInt32(codigo);
                var titulo = line.Substring(7, line.Length - 7).Trim();
                var cboFamiliaId = Convert.ToInt16(codigo.Substring(0, 4));

                _context.CBOOcupacao.AddOrUpdate(x => x.CBOOcupacaoId,
                    new CBOOcupacao
                    {
                        CBOOcupacaoId = cboOcupacaoId,
                        CBOFamiliaId = cboFamiliaId,
                        Codigo = codigo,
                        Titulo = titulo
                    }
                );
            }
        }

        private void SeedSinonimo()
        {
            var fileName = Path.Combine(_cboDirectoryName, "CBO2002 - Sinonimo.txt");

            var streamReader = new StreamReader(fileName, _encoding, true, 512);

            //Descartando coluna Título e Separador
            streamReader.ReadLine();
            streamReader.ReadLine();

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();

                var codigo = line.Substring(0, 8).Trim();
                var titulo = line.Substring(9, line.Length - 9).Trim();
                var cboOcupacaoId = Convert.ToInt32(codigo);

                _context.CBOSinonimo.AddOrUpdate(x => new
                {
                    x.CBOOcupacaoId,
                    x.Titulo,
                },
                    new CBOSinonimo
                    {
                        CBOOcupacaoId = cboOcupacaoId,
                        Titulo = titulo,
                    }
                );
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        } 
        #endregion
    }
}
