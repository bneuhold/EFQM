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

        public int UpitnikSave(Upitnik input, Hashtable vrijednosti, int UserId, UpitnikRezultat rezultat, string tip)
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
                        for (var i = 1; i <= 10; i++)
                        {
                            SetParameterValue(command, "@Vrijednost" + i, DBNull.Value);
                        }
                        UpitnikVrijednostRezultat row = (UpitnikVrijednostRezultat)vrijednosti[item];
                        var counter = 0;
                        foreach (int? v in row.Vrijednosti)
                        {
                            counter++;
                            if (v.HasValue)
                                SetParameterValue(command, "@Vrijednost" + counter, v.Value);
                        }
                        SetParameterValue(command, "@UpitnikId", result);
                        SetParameterValue(command, "@PitanjeId", row.PitanjeId);
                        ExecuteNonQuery(command);
                    }

                }
                using (DbCommand command = GetCommand("tblHuogRezultat_Save"))
                {
                    foreach (UpitnikRezultatItem item in rezultat.Grupe)
                    {
                        SetParameterValue(command, "@GrupaId", item.Grupa);
                        SetParameterValue(command, "@UpitnikId", result);
                        SetParameterValue(command, "@Vrijednost", item.Vrijednost);
                        SetParameterValue(command, "@Tip", tip);
                        SetParameterValue(command, "@Vrsta", "QR");
                        ExecuteNonQuery(command);
                    }
                    
                }
                using (DbCommand command = GetCommand(isInsert ? "tblHuogRezultat_insert" : "tblHuogRezultat_update"))
                {
                    
                }
                scope.Complete();
            }

            return result;
        }

        public LoggedUser Register(LoggedUser input)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (DbCommand command = GetCommand("tblHuogUser_insert"))
                {
                    SetParameterValue(command, "@Id", input.Id);
                    GetParameter(command, "@Id").Direction = ParameterDirection.Output;
                    SetParameterValue(command, "@Name", input.Name);
                    SetParameterValue(command, "@Email", input.Email);
                    SetParameterValue(command, "@Password", input.Password);
                    SetParameterValue(command, "@CompanyName", input.CompanyName);
                    SetParameterValue(command, "@City", input.City);
                    SetParameterValue(command, "@Type", input.Type);
                    SetParameterValue(command, "@Employees", input.Employees);
                    SetParameterValue(command, "@Income", input.Income);
                    SetParameterValue(command, "@Newsletter", input.Newsletter);
                    ExecuteNonQuery(command);

                    input.Id =(int)GetParameterValue(command, "Id");
                }
                scope.Complete();
            }
            return input;
        }

        public LoggedUser UserChange(LoggedUser input)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (DbCommand command = GetCommand("tblHuogUser_update"))
                {
                    SetParameterValue(command, "@Id", input.Id);
                    SetParameterValue(command, "@Name", input.Name);
                    SetParameterValue(command, "@Email", input.Email);
                    SetParameterValue(command, "@Password", input.Password);
                    SetParameterValue(command, "@CompanyName", input.CompanyName);
                    SetParameterValue(command, "@City", input.City);
                    SetParameterValue(command, "@Type", input.Type);
                    SetParameterValue(command, "@Employees", input.Employees);
                    SetParameterValue(command, "@Income", input.Income);
                    SetParameterValue(command, "@Newsletter", input.Newsletter);
                    ExecuteNonQuery(command);
                }
                scope.Complete();
            }
            return input;
        }

        public LoggedUser Login(string email, string password)
        {
            DataTable data;
            using (DbCommand command = GetCommand("tblHuogUser_login"))
            {
                SetParameterValue(command, "@Email", email);
                SetParameterValue(command, "@Password", password);
                data = ExecuteDataTable(command);
            }
            if (data.Rows.Count == 0)
                return null;

            return new LoggedUser()
            {
                Id = (int)data.Rows[0]["Id"],
                Name = data.Rows[0]["Name"].ToString(),
                Email = data.Rows[0]["Email"].ToString(),
                Password = data.Rows[0]["Password"].ToString(),
                CompanyName = data.Rows[0]["CompanyName"].ToString(),
                City = data.Rows[0]["City"].ToString(),
                Type = data.Rows[0]["Type"].ToString(),
                Employees = data.Rows[0]["Employees"].ToString(),
                Income = data.Rows[0]["Income"].ToString(),
                Newsletter = (bool)data.Rows[0]["Newsletter"],
                Status = (int)data.Rows[0]["Status"],
            };
        }

        public LoggedUser ActivateUser(int id, string type)
        {
            DataTable data;
            using (DbCommand command = GetCommand("tblHuogUser_Activate"))
            {
                SetParameterValue(command, "@Id", id);
                SetParameterValue(command, "@Type", type);
                data = ExecuteDataTable(command);
            }
            if (data.Rows.Count == 0)
                return null;

            return new LoggedUser()
            {
                Id = (int)data.Rows[0]["Id"],
                Name = data.Rows[0]["Name"].ToString(),
                Email = data.Rows[0]["Email"].ToString(),
                Password = data.Rows[0]["Password"].ToString(),
                CompanyName = data.Rows[0]["CompanyName"].ToString(),
                City = data.Rows[0]["City"].ToString(),
                Type = data.Rows[0]["Type"].ToString(),
                Employees = data.Rows[0]["Employees"].ToString(),
                Income = data.Rows[0]["Income"].ToString(),
                Newsletter = (bool)data.Rows[0]["Newsletter"],
                Status = (int)data.Rows[0]["Status"],
            };
        }

        public IDataReader GetRezultat(int UpitnikId, string Tip)
        {

            using (DbCommand command = GetCommand("tblHuogResult_Rezultat"))
            {
                SetParameterValue(command, "@UpitnikId", UpitnikId);
                SetParameterValue(command, "@Tip", Tip);
                return ExecuteReader(command);
            }
            
        }
    }
}