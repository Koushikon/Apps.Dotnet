using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Web.Models;

namespace Web.App_Data
{
    public class DataAccessLayer
    {
        /**
         * Database Aceess Fields
         */

        private static readonly string _spName = "CRUD_Into_Customer";
        private readonly Dictionary<string, int> _spSection = new Dictionary<string, int>
        {
            ["InsertData"] = 1,
            ["UpdateData"] = 2,
            ["DeleteData"] = 3,
            ["GetAllData"] = 4,
            ["GetOneData"] = 5
        };

        private static readonly SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConString"].ToString());
        private readonly SqlCommand _cmd = new SqlCommand(_spName, _connection);

        /**
         * Database Aceess Methods
         */

        #region Insert Data Query call
        public int InsertData(Customer pCustomer)
        {
            try
            {
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("@Name", pCustomer.Name);
                _cmd.Parameters.AddWithValue("@Address", pCustomer.Address);
                _cmd.Parameters.AddWithValue("@Mobileno", pCustomer.Mobileno);
                _cmd.Parameters.AddWithValue("@Birthdate", pCustomer.Birthdate);
                _cmd.Parameters.AddWithValue("@EmailID", pCustomer.EmailID);
                _cmd.Parameters.AddWithValue("@Query", _spSection["InsertData"]);

                _connection.Open();
                int result = Convert.ToInt32(_cmd.ExecuteScalar());
                return result;
            }

            catch { return -1; }
            finally { _connection.Close(); }
        }
        #endregion

        #region Update Data Query call
        public int UpdateData(Customer pCustomer)
        {
            try
            {
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("@CustomerID", pCustomer.CustomerID);
                _cmd.Parameters.AddWithValue("@Name", pCustomer.Name);
                _cmd.Parameters.AddWithValue("@Address", pCustomer.Address);
                _cmd.Parameters.AddWithValue("@Mobileno", pCustomer.Mobileno);
                _cmd.Parameters.AddWithValue("@Birthdate", pCustomer.Birthdate);
                _cmd.Parameters.AddWithValue("@EmailID", pCustomer.EmailID);
                _cmd.Parameters.AddWithValue("@Query", _spSection["UpdateData"]);

                _connection.Open();
                int result = Convert.ToInt32(_cmd.ExecuteScalar());
                return result;
            }

            catch { return -1; }
            finally { _connection.Close(); }
        }
        #endregion

        #region Delete Data Query call
        public int DeleteData(String ID)
        {
            try
            {
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("@CustomerID", ID);
                //cmd.Parameters.AddWithValue("@Name", null);
                //cmd.Parameters.AddWithValue("@Address", null);
                //cmd.Parameters.AddWithValue("@Mobileno", null);
                //cmd.Parameters.AddWithValue("@Birthdate", null);
                //cmd.Parameters.AddWithValue("@EmailID", null);
                _cmd.Parameters.AddWithValue("@Query", _spSection["DeleteData"]);

                _connection.Open();
                int result = Convert.ToInt32(_cmd.ExecuteScalar());
                return result;
            }

            catch { return -1; }
            finally { _connection.Close(); }
        }
        #endregion

        #region Get All Data Query call
        public List<Customer> Selectalldata()
        {
            var filledData = new List<Customer>();
            try
            {
                _cmd.CommandType = CommandType.StoredProcedure;
                //_cmd.Parameters.AddWithValue("@CustomerID", null);
                //_cmd.Parameters.AddWithValue("@Name", null);
                //_cmd.Parameters.AddWithValue("@Address", null);
                //_cmd.Parameters.AddWithValue("@Mobileno", null);
                //_cmd.Parameters.AddWithValue("@Birthdate", null);
                //_cmd.Parameters.AddWithValue("@EmailID", null);
                _cmd.Parameters.AddWithValue("@Query", _spSection["GetAllData"]);

                _connection.Open();
                var ds = new DataSet();
                var da = new SqlDataAdapter { SelectCommand = _cmd };
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Customer cObj = new Customer();
                    cObj.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"].ToString());
                    cObj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    cObj.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    cObj.Mobileno = ds.Tables[0].Rows[i]["Mobileno"].ToString();
                    cObj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                    cObj.Birthdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Birthdate"].ToString());

                    filledData.Add(cObj);
                }
                return filledData;
            }

            catch { return filledData; }
            finally { _connection.Close(); }
        }
        #endregion

        #region Get Search Data Query call
        public Customer SelectDatabyID(string CustomerID)
        {
            Customer filledData = null;
            try
            {
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                //cmd.Parameters.AddWithValue("@Name", null);
                //cmd.Parameters.AddWithValue("@Address", null);
                //cmd.Parameters.AddWithValue("@Mobileno", null);
                //cmd.Parameters.AddWithValue("@Birthdate", null);
                //cmd.Parameters.AddWithValue("@EmailID", null);
                _cmd.Parameters.AddWithValue("@Query", _spSection["GetOneData"]);

                _connection.Open();
                var ds = new DataSet();
                var da = new SqlDataAdapter { SelectCommand = _cmd };
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    filledData = new Customer();
                    filledData.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"].ToString());
                    filledData.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    filledData.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    filledData.Mobileno = ds.Tables[0].Rows[i]["Mobileno"].ToString();
                    filledData.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                    filledData.Birthdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Birthdate"].ToString());

                }
                return filledData;
            }

            catch { return filledData; }
            finally { _connection.Close(); }
        }
        #endregion
    }
}