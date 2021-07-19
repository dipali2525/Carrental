﻿using System;
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
    public class CarRepository : ICarRepository
    {
        SqlConnection con;

        public CarRepository(IConfiguration config)
        {

            var test = config.GetConnectionString("CarConnection");
            con = new SqlConnection(test);
        }

        public bool Add(CarViewModel car)
        {
            var sql = "insert into dbo.Car (TypeID,Price,BRAND,Photo) Values(@TypeId,@Price,@Brand,@Photo)";
            con.Execute(sql, car);
            return true;
        }

        public bool Add(IEnumerable<CarViewModel> cars)
        {
            con.Open();
            using (SqlBulkCopy bulkCopy =
                           new SqlBulkCopy(con))
            {
                bulkCopy.DestinationTableName =
                    "dbo.Type";
                bulkCopy.ColumnMappings.Add("TypeId", "TypeId");
                bulkCopy.ColumnMappings.Add("Price", "Price");
                bulkCopy.ColumnMappings.Add("Brand", "Brand");
                bulkCopy.ColumnMappings.Add("Photo", "Photo");

                try
                {
                    var sourceTable = cars.ToDataTable();
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

        public bool Delete(CarViewModel car)
        {
            var sql = $"delete from dbo.Car where ID={car.ID}";
            con.Execute(sql, car);
            return true;
        }

        public CarViewModel Find(int id)
        {
            var sql = $"SELECT c.ID,TypeID,Price,BRAND,Photo, t.Title as TypeName FROM dbo.Car as c Join dbo.Type t ON c.TypeID = t.ID  Where c.ID = {id};";
            var result = con.QuerySingle<CarViewModel>(sql);
            return result;
        }

        public IEnumerable<CarViewModel> GetAll()
        {
            var sql = "SELECT c.ID,TypeID,Price,BRAND,Photo, t.Title as TypeName from dbo.Car as c Join dbo.Type t ON c.TypeID = t.ID;";
            var results = con.Query<CarViewModel>(sql).ToList();
            return results;
        }

        public bool Update(CarViewModel car)
        {
            var sql = $"UPDATE dbo.Car SET TypeID = '{car.TypeId}',Price='{car.Price}',BRAND='{car.Brand}',Photo='{car.Photo}' Where ID = {car.ID};";
            con.Execute(sql, car);
            return true;
        }
    }
}
