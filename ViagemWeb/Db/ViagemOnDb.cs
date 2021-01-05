using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ViagemWeb.Models;

namespace ViagemWeb.Db
{
    public class ViagemOnDb: DbContext
    {
        private const string conexao = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                  AttachDbFilename=
                                   C:\Users\Samsung\Desktop\Minhas Atividades\AprendendoViagem\ViagemWeb\ViagemWeb\App_Data\ViagemOnlineDb.mdf;  
                                  Integrated Security=True";
        public ViagemOnDb() : base(conexao) { }

        public DbSet<Destino> Destino { get; set; }
        
    }
}