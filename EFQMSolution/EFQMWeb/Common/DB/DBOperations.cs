using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EFQMWeb.Common.Entity;
using System.Data;

namespace EFQMWeb.Common.DB
{
    public class DBOperations: DataBridge
    {
        private static DBOperations _dataBase = null;

        public static DBOperations Database
        {
            get
            {
                //return getContainer().Resolve<DBOperations>();
                return _dataBase;
            }
            set
            {
                _dataBase = value;
            }
        }

        public DBOperations(Database db)
        {
            this._dbView = db;
        }


        public DataTable HuogGrupaList()
        {
            using (IDbCommand command = GetCommand("tblHuogGrupa_List"))
            {
                return ExecuteDataTable(command);
            }
        }

        public DataTable HuogPitanjeList()
        {
            using (IDbCommand command = GetCommand("tblHuogPitanje_List"))
            {
                return ExecuteDataTable(command);
            }
        }

        public LoggedUser Login(string username, string password)
        {
            return null;
        }

    }
}