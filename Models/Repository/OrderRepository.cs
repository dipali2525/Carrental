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
    public class OrderRepository : IOrderRepository
    {
        SqlConnection con;

        public OrderRepository(IConfiguration config)
        {

            var test = config.GetConnectionString("CarConnection");
            con = new SqlConnection(test);
        }
        public bool Add(OrderViewModel order)
        {
            var sql = "insert into dbo.[Order] (CARID,STARTDATE,ENDDATE,PICK_LOCATION,DROP_LOCATION,[CONTACT NO],CONTACT_PERSON) Values(@CarId,@StartDate,@EndDate,@PickLocation,@DropLocation,@ContactNo,@ContactPerson)";
            con.Execute(sql, order);
            return true;
        }

        public bool Add(IEnumerable<OrderViewModel> orders)
        {
            con.Open();
            using (SqlBulkCopy bulkCopy =
                           new SqlBulkCopy(con))
            {
                bulkCopy.DestinationTableName =
                    "dbo.[Order]";
                bulkCopy.ColumnMappings.Add("CarId", "CARID");
                bulkCopy.ColumnMappings.Add("StartDate", "STARTDATE");
                bulkCopy.ColumnMappings.Add("EndDate", "ENDDATE");
                bulkCopy.ColumnMappings.Add("PickLocation", "PICK_LOCATION");
                bulkCopy.ColumnMappings.Add("DropLocation", "DROP_LOCATION");
                bulkCopy.ColumnMappings.Add("ContactNo", "[CONTACT NO]");
                bulkCopy.ColumnMappings.Add("ContactPerson", "CONTACT_PERSON");

                try
                {
                    var sourceTable = orders.ToDataTable();
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

        public bool Delete(OrderViewModel order)
        {
            var sql = $"delete from dbo.[Order] where ID={order.ID}";
            con.Execute(sql, order);
            return true;
        }

        public OrderViewModel Find(int id)
        {
            var sql = $"SELECT o.ID as ID, CARID as CarId,STARTDATE as StartDate ,ENDDATE as EndDate,PICK_LOCATION as PickLocation,DROP_LOCATION as DropLocation,[CONTACT NO] as ContactNo,CONTACT_PERSON as ContactPerson,c.CarName  FROM dbo.[Order] as o Join dbo.Car as c ON o.CARID = c.ID Where o.ID = {id};";
            var result = con.QuerySingle<OrderViewModel>(sql);
            return result;
        }

        public IEnumerable<OrderViewModel> FindByCarId(int id)
        {
            var sql = $"SELECT o.ID as ID, CARID as CarId,STARTDATE as StartDate ,ENDDATE as EndDate,PICK_LOCATION as PickLocation,DROP_LOCATION as DropLocation,[CONTACT NO] as ContactNo,CONTACT_PERSON as ContactPerson,c.CarName  FROM dbo.[Order] as o Join dbo.Car as c ON o.CARID = c.ID Where o.CARID = {id};";
            var result = con.Query<OrderViewModel>(sql);
            return result;
        }

        public IEnumerable<OrderViewModel> FindByDateAndBrand(DateTime startDate, DateTime endDate, string brand)
        {
            var sql = "SELECT o.ID as ID, CARID as CarId,STARTDATE as StartDate ,ENDDATE as EndDate," +
                "PICK_LOCATION as PickLocation,DROP_LOCATION as DropLocation,[CONTACT NO] as ContactNo," +
                "CONTACT_PERSON as ContactPerson,c.CarName  " +
                "FROM dbo.[Order] as o Join dbo.Car as c ON o.CARID = c.ID " +
                $"Where o.STARTDATE >='{startDate.ToString("yyyy-MM-dd HH:mm:ss")}' " +
                $"And o.ENDDATE <= '{endDate.ToString("yyyy-MM-dd HH:mm:ss")}' " +
                $"And c.Brand = '{brand}';";
            var results = con.Query<OrderViewModel>(sql).ToList();
            return results;
        }

        public IEnumerable<OrderViewModel> GetAll()
        {
            var sql = "SELECT o.ID as ID, CARID as CarId,STARTDATE as StartDate ,ENDDATE as EndDate,PICK_LOCATION as PickLocation,DROP_LOCATION as DropLocation,[CONTACT NO] as ContactNo,CONTACT_PERSON as ContactPerson,c.CarName  FROM dbo.[Order] as o Join dbo.Car as c ON o.CARID = c.ID";
            var results = con.Query<OrderViewModel>(sql).ToList();
            return results;
        }

        public bool Update(OrderViewModel order)
        {
            var sql = $"UPDATE dbo.[Order] SET CARID = '{order.CarId}',STARTDATE='{order.StartDate.ToString("yyyy-MM-dd HH:mm:ss")}',ENDDATE='{order.EndDate.ToString("yyyy-MM-dd HH:mm:ss")}',PICK_LOCATION='{order.PickLocation}',DROP_LOCATION='{order.DropLocation}',[CONTACT NO]='{order.ContactNo}',CONTACT_PERSON='{order.ContactPerson}' Where ID = {order.ID};";
            con.Execute(sql, order);
            return true;
        }
    }
}