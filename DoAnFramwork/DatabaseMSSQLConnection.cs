﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    class DatabaseMSSQLConnection: DatabaseConnection
    {
        public String databaseName { get; set; }
        public String username { get; set; }
        public String password { get; set; }

        public DatabaseMSSQLConnection(String databaseName,String username = null,String password = null) {
            this.databaseName = databaseName;
            this.username = username;
            this.password = password;
        }

        private SqlConnection createConnection() {
            SqlConnection cnn;
            string connetionString = @"Data Source=AKWOJQZOBE2VFUU\SQLEXPRESS;Initial Catalog="+this.databaseName;
            if (this.username != null && this.password!=null) {
                connetionString += ";User ID=" + this.username + ";Password="+ this.password;
            }
            try
            {
                cnn = new SqlConnection(connetionString);
            }
            catch (Exception ex) {
                return null;
            }
            return cnn;
        }

        public SqlDataReader readData(String tableName)
        {
            SqlConnection cnn = null;
            SqlCommand cmd;
            SqlDataReader reader;
            String sql;
            try
            {
                sql = "Select * from "+tableName;
                cnn = this.createConnection();
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for(int i=0;i<reader.FieldCount;++i){
                        Console.WriteLine(reader.GetValue(i));
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally{
                if(cnn!=null)
                    cnn.Close();
            }
            //Console.ReadLine();
            return reader;
        }

        public int insert(String tableName, Object[] objs)
        {
            SqlConnection cnn = null;
            SqlCommand cmd;
            

            try
            {
                List<String> fields = this.getFields(tableName);

                if (objs.Length != fields.Count) {
                    return 0;
                }

                // create command
                cnn = this.createConnection();
                cnn.Open();

                String fieldsString = this.createFieldsInsertString(fields);
                String paramsString = this.createParamsInsertString(fields.Count);

                cmd = cnn.CreateCommand();

                cmd.CommandText = "INSERT INTO "+tableName + fieldsString + " VALUES" + paramsString;
                //cmd.Parameters.AddWithValue("@tableName", tableName);
                for (int i = 0; i < objs.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@param" + i, objs[i]);
                }
                Console.Write(cmd.CommandText);

                return cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex) {
                return 0;
            }
        }

        public int update(String tableName, Object[] objs)
        {
            SqlConnection cnn = null;
            SqlCommand cmd;


            try
            {
                List<String> fields = this.getFields(tableName);

                if (objs.Length != fields.Count)
                {
                    return 0;
                }

                // create command
                cnn = this.createConnection();
                cnn.Open();

                String paramsString = this.createParamsUpdateString(fields);

                cmd = cnn.CreateCommand();

                cmd.CommandText = "update " + tableName + " set " + paramsString + " where " + fields[0] + " = " +objs[0] ;
                //cmd.Parameters.AddWithValue("@tableName", tableName);
                for (int i = 0; i < objs.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@param" + i, objs[i]);
                }
                cmd.Parameters.AddWithValue("@keyField", fields[0]);
                cmd.Parameters.AddWithValue("@paramKey", objs[0]);
                Console.Write(cmd.CommandText);

                return cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int delete(String tableName, Object keyPrimaryValue)
        {
            SqlConnection cnn = null;
            SqlCommand cmd;

            try
            {
                List<String> fields = this.getFields(tableName);

                // create command
                cnn = this.createConnection();
                cnn.Open();

                String paramsString = this.createParamsUpdateString(fields);

                cmd = cnn.CreateCommand();

                cmd.CommandText = "delete from " + tableName + " where " + fields[0] + " = @param";
                //cmd.Parameters.AddWithValue("@tableName", tableName);

                cmd.Parameters.AddWithValue("@param", keyPrimaryValue);

                return cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Dictionary<String, Type> getField()
        {
            //throw new NotImplementedException();
            Dictionary<String, Type> dict = new Dictionary<String, Type>();
            dict.Add("id", typeof(Int32));
            dict.Add("name", typeof(String));
            dict.Add("gpa", typeof(String));
            return dict;
        }

        public List<String> getTables() {
            SqlConnection cnn = null;
            SqlCommand cmd;
            SqlDataReader reader;
            //string query;

            try
            {
                cnn = this.createConnection();
                cnn.Open();
                List<String> listTables = new List<string>();
                DataTable dt = cnn.GetSchema("Tables");
                foreach (DataRow row in dt.Rows)
                {
                    listTables.Add((string)row[2]);
                    Console.WriteLine((string)row[2]);
                }
                //query = "select * from information_schema.tables";
                //cmd = new SqlCommand(query, cnn);
                //reader = cmd.ExecuteReader();
                //while (reader.Read()) {
                //    listTables.Add(reader.GetString(0));
                //    Console.WriteLine(reader.GetString(0));
                //}
            }
            catch (Exception ex)
            {
                return null;
            }
            finally {
                if (cnn != null) {
                    cnn.Close();
                }
            }
            return null;
        }

        public List<String> getFields(String tableName) {
            SqlConnection cnn = null;
            SqlCommand cmd;
            SqlDataReader reader;
            //string query;

            try
            {
                cnn = this.createConnection();
                cnn.Open();
                List<String> listCols = new List<string>();

                cmd = cnn.CreateCommand();
                //cmd.CommandText = "SELECT Column_Name FROM information_schema.columns where TABLE_NAME = 'Employees'";
                cmd.CommandText = "select c.name from sys.columns c inner join sys.tables t on t.object_id = c.object_id and t.name = @tableName";
                cmd.Parameters.AddWithValue("@tableName", tableName);

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listCols.Add(reader.GetString(0));
                        Console.WriteLine(reader.GetString(0));
                    }
                }
                return listCols;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally {
                if (cnn != null) {
                    cnn.Close();
                }
            }
        }
        public Type getTypeofField(String tableName, String field) {
            SqlConnection cnn = null;
            SqlCommand cmd;
            SqlDataReader reader;
            String typeString = null;

            try
            {
                cnn = this.createConnection();
                cnn.Open();
                cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @field";
                cmd.Parameters.AddWithValue("@tableName", tableName);
                cmd.Parameters.AddWithValue("@field", field);

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        typeString = reader.GetString(0);
                        Console.WriteLine(reader.GetString(0));
                    }
                }
                return getType(typeString);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }
        }
        private Type getType(string typeString){
            switch (typeString)
            { 
                case "nvarchar":
                case "nchar":
                case "ntext":
                case "text":
                case "varchar":
                    return typeof(String);
                case "int":
                case "Int32":
                    return typeof(Int32);
                case "Int16":
                    return typeof(Int16);
                case "float":
                case "double":
                    return typeof(Double);
                case "datetime":
                    return typeof(DateTime);
                case "image":
                    return typeof(Byte[]);
                case "real":
                    return typeof(Single);
                case "tinyint":
                case "binary":
                    return typeof(Byte);
                case "money":
                    return typeof(Decimal);
                default:
                    return typeof(Nullable);
            }
        }
        private String createParamsString() {
            return null;
        }

        private String createFieldsInsertString(List<String> fields){
            StringBuilder paramsString = new StringBuilder();
            if (fields.Count < 1)
                return "";
            paramsString.Append("(");
            for (int i = 0; i < fields.Count; ++i)
            {
                paramsString.Append(fields[i]);
                if (i < fields.Count - 1)
                {
                    paramsString.Append(",");
                }
            }
            paramsString.Append(")");
            return paramsString.ToString();
        }

        private String createParamsInsertString(int countParams) {
            StringBuilder paramsString = new StringBuilder();
            if (countParams < 1)
                return "";
            paramsString.Append("(");
            for (int i = 0; i < countParams; ++i)
            {
                paramsString.Append("@param")
                    .Append(i);
                if (i < countParams - 1)
                {
                    paramsString.Append(",");
                }
            }
            paramsString.Append(")");
            return paramsString.ToString();
        }
        private String createParamsUpdateString(List<String> fields)
        {
            StringBuilder paramsString = new StringBuilder();
            if (fields.Count < 1)
                return "";
            for (int i = 0; i < fields.Count; ++i)
            {
                paramsString.Append(fields[i])
                    .Append(" = ")
                    .Append("@param")
                    .Append(i);
                if (i < fields.Count - 1)
                {
                    paramsString.Append(",");
                }
            }
            paramsString.Append(" ");
            return paramsString.ToString();
        }
    }

    
}
