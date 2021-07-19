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
                    "dbo.Type";
                bulkCopy.ColumnMappings.Add("CarId", "CarId");
                bulkCopy.ColumnMappings.Add("StartDate", "StartDate");
                bulkCopy.ColumnMappings.Add("EndDate", "EndDate");
                bulkCopy.ColumnMappings.Add("PickLocation", "PickLocation");
                bulkCopy.ColumnMappings.Add("DropLocation", "DropLocation");
                bulkCopy.ColumnMappings.Add("ContactNo", "ContactNo");
                bulkCopy.ColumnMappings.Add("ContactPerson", "ContactPerson");

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
            var sql = $"SELECT ID as ID, CARID as CarId,STARTDATE as StartDate ,ENDDATE as EndDate,PICK_LOCATION as PickLocation,DROP_LOCATION as DropLocation,[CONTACT NO] as ContactNo,CONTACT_PERSON as ContactPerson FROM dbo.[Order] Where ID = {id};";
            var result = con.QuerySingle<OrderViewModel>(sql);
            return result;
        }

        public IEnumerable<OrderViewModel> GetAll()
        {
            var sql = "SELECT ID as ID, CARID as CarId,STARTDATE as StartDate ,ENDDATE as EndDate,PICK_LOCATION as PickLocation,DROP_LOCATION as DropLocation,[CONTACT NO] as ContactNo,CONTACT_PERSON as ContactPerson FROM dbo.[Order];";
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