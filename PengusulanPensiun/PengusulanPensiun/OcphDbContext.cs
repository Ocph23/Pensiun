using DAL.DContext;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using PengusulanPensiun.Models;

namespace DAL
{
    public class OcphDbContext : IDbContext, IDisposable
    {
        private string ConnectionString;
        private IDbConnection _Connection;

        public OcphDbContext()
        {

            this.ConnectionString = "Server=localhost; Database=dbpensiun;UserId=root;password=;CharSet=utf8;Persist Security Info=True";
        }

		public IRepository<user> Users { get { return new Repository<user>(this); } }
        public IRepository<periode> Periodes { get { return new Repository<periode>(this); } }
        public IRepository<instansi> Instansis { get { return new Repository<instansi>(this); } }
        public IRepository<pegawai> Pegawais{ get { return new Repository<pegawai>(this); } }
        public IRepository<kelengkapanberkas> Kelengkapans{ get { return new Repository<kelengkapanberkas>(this); } }

        public IDbConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new MySqlDbContext(this.ConnectionString);
                    return _Connection;
                }
                else
                {
                    return _Connection;
                }
            }
        }

        public void Dispose()
        {
            if (_Connection != null)
            {
                if (this.Connection.State != ConnectionState.Closed)
                {
                    this.Connection.Close();
                }
            }
        }
    }
}
