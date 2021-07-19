using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Carrental.Models;
using Dapper;
using Carrental.Extensons;

namespace Carrental.Models
{
    public class CarTypeRepository : ICarTypeRepository
    {
        SqlConnection con;

        public CarTypeRepository(IConfiguration config)
        {

            var test = config.GetConnectionString("CarConnection");
            con = new SqlConnection(test);
        }

        public bool Add(CarTypeViewModel carType)
        {
            var sql = "insert into dbo.Type (Title) Values(@Title)";
            con.Execute(sql, carType);
            return true;
        }

        public bool Add(IEnumerable<CarTypeViewModel> carTypes)
        {
            con.Open();
            using (SqlBulkCopy bulkCopy =
                           new SqlBulkCopy(con))
            {
                bulkCopy.DestinationTableName =
                    "dbo.Type";
                bulkCopy.ColumnMappings.Add("Title", "Title");

                try
                {
                    var sourceTable = carTypes.ToDataTable();
                    sourceTable.TableName = "SourceTable";
                    sourceTable.Columns.Remove("ID");
                    sourceTable.AcceptChanges();
                    bulkCopy.WriteToServer(sourceTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    // Close the SqlDataReader. The SqlBulkCopy
                    // object is automatically closed at the end
                    // of the using block.
                    con.Close();
                }
                return true;
            }
        }

        public bool Delete(CarTypeViewModel carType)
        {
            var sql = $"delete from dbo.Type where ID={carType.ID}";
            con.Execute(sql, carType);
            return true;
        }

        public CarTypeViewModel Find(int id)
        {
            var sql = $"SELECT * FROM dbo.Type Where ID = {id};";
            var result = con.QuerySingle<CarTypeViewModel>(sql);
            return result;
        }

        public IEnumerable<CarTypeViewModel> GetAll()
        {
            var sql = "select * from dbo.Type;";
            var results = con.Query<CarTypeViewModel>(sql).ToList();
            return results;
        }

        public bool Update(CarTypeViewModel carType)
        {
            var sql = $"UPDATE dbo.Type SET Title = '{carType.Title}' Where ID = {carType.ID};";
            con.Execute(sql, carType);
            return true;
        }
    }
}