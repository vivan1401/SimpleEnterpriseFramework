using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework{
    public interface DatabaseConnection{
        SqlDataReader readData(String tableName);
        int insert(String tableName, Object[] values);
        int update(String tableName,Object[] values);
        int delete(String tableName,Object obj);
        Dictionary<String,Type> getField();
    }
}
