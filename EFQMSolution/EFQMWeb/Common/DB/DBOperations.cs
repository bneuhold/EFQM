using System.Data;
using System.Data.Common;
using System.Transactions;
using EFQMWeb.Common.Entity;
using EFQMWeb.Models;
using Library.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;
using System;

namespace EFQMWeb.Common.DB
{
    public class DBOperations : DataBridge
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
            using (DbCommand command = GetCommand("tblHuogGrupa_List"))
            {
                return ExecuteDataTable(command);
            }
        }

        public DataTable HuogPitanjeList()
        {
            using (DbCommand command = GetCommand("tblHuogPitanje_List"))
            {
                return ExecuteDataTable(command);
            }
        }

        public DataTable HuogUpitnikList(int UserId, int? Id)
        {
            using (DbCommand command = GetCommand("tblHuogUpitnik_List"))
            {
                SetParameterValue(command, "@UserId", UserId);
                SetParameterValue(command, "@Id", Id);
                return ExecuteDataTable(command);
            }
        }

        public DataTable HuogUpitnikVrijednostiList(int? UpitnikId, int? PitanjeId)
        {
            using (DbCommand command = GetCommand("tblHuogUpitnikVrijednosti_List"))
            {
                SetParameterValue(command, "@UpitnikId", UpitnikId);
                SetParameterValue(command, "@PitanjeId", PitanjeId);
                return ExecuteDataTable(command);
            }
        }

        public int UpitnikSave(Upitnik input, Hashtable vrijednosti, int UserId)
        {
            int result = 0;
            bool isInsert = true;
            if (input.UpitnikId.HasValue)
            {
                isInsert = false;
            }
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (DbCommand command = GetCommand(isInsert ? "tblHuogUpitnik_insert" : "tblHuogUpitnik_update"))
                {
                    SetParameterValue(command, "@Id", input.UpitnikId);
                    if (isInsert)
                    {
                        GetParameter(command, "@Id").Direction = ParameterDirection.Output;
                    }
                    SetParameterValue(command, "@UserId", UserId);
                    SetParameterValue(command, "@Naziv", input.Naziv);
                    SetParameterValue(command, "@Opis", input.Opis);
                    SetParameterValue(command, "@Datum", input.Datum);

                    ExecuteNonQuery(command);
                    if (!isInsert)
                    {
                        result = input.UpitnikId.Value;
                    }
                    else
                    {
                        result = (int)GetParameterValue(command, "Id");
                    }
                }
                using (DbCommand command = GetCommand(isInsert ? "tblHuogUpitnikVrijednost_insert" : "tblHuogUpitnikVrijednost_update"))
                {
                    foreach (var item in vrijednosti.Keys)
                    {
                        //clear old values
                        for (var i=1; i<=10;i++)
	                    {
		                    SetParameterValue(command,"@Vrijednost"+i,DBNull.Value);
	                    }
                        UpitnikVrijednostRezultat row = (UpitnikVrijednostRezultat)vrijednosti[item];
                        var counter=0;
                        foreach (int? v in row.Vrijednosti)
	                    {
                            counter++;
		                    if(v.HasValue)
                                SetParameterValue(command,"@Vrijednost"+counter,v.Value);
	                    }
                        SetParameterValue(command, "@UpitnikId", result);
                        SetParameterValue(command,"@PitanjeId",row.PitanjeId);
                        ExecuteNonQuery(command);
                    }
                    
                }
                scope.Complete();
            }

            return result;
        }

        public LoggedUser Login(string username, string password)
        {
            return null;
        }
    }
}