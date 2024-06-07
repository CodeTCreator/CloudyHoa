﻿using Devart.Data.PostgreSql;
using DevExpress.XtraEditors.SyntaxEditor;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary.ExpensesService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ExpensesService" in both code and config file together.
    public class ExpensesService : IExpensesService
    {

        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];

        public void AddCategory(string name)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO expenses_categories (name) " +
                    "VALUES (@name)"
                    , conn);
                pgSqlCommand.Parameters.Add("@name", name);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void AddExpense(int categoryId, string name, float quantity, float cost, float resultCost)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO expenses (category_id,name,quantity," +
                    "cost,result_cost) " +
                    "VALUES (@category_id,@name,@quantity,@cost,@result_cost)"
                    , conn);
                pgSqlCommand.Parameters.Add("@category_id", categoryId);
                pgSqlCommand.Parameters.Add("@name", name);
                pgSqlCommand.Parameters.Add("@quantity", quantity);
                pgSqlCommand.Parameters.Add("@cost", cost);
                pgSqlCommand.Parameters.Add("@result_cost", resultCost);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from expenses_categories where id = @categoryId", conn);
                pgSqlCommand.Parameters.Add("@categoryId", categoryId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteExpense(int expenseId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from expenses where id = @expenseId", conn);
                pgSqlCommand.Parameters.Add("@expenseId", expenseId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void EditCategory(int categoryId, string name)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE expenses_categories SET name = @name " +
                    " where id = @categoryId", conn);
                pgSqlCommand.Parameters.Add("@categoryId", categoryId);
                pgSqlCommand.Parameters.Add("@name", name);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void EditExpense(int expenseId, int categoryId, string name, float quantity, float cost, float resultCost)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE expenses SET category_id = @categoryId, name = @name," +
                    "quantity = @quantity, cost = @cost, resultCost = @resultCost" +
                    " where id = @id", conn);
                pgSqlCommand.Parameters.Add("@id", expenseId);
                pgSqlCommand.Parameters.Add("@categoryId", categoryId);
                pgSqlCommand.Parameters.Add("@name", name);
                pgSqlCommand.Parameters.Add("@quantity", quantity);
                pgSqlCommand.Parameters.Add("@cost", cost);
                pgSqlCommand.Parameters.Add("@resultCost", resultCost);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet GetCategories()
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select expenses_categories.* from expenses_categories "
                    , conn);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetExpenses(DateTime dateTime)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select expenses.*, expenses_categories.name as category_name from expenses " +
                    "join expenses_categories on expenses.id = expenses_categories.id " +
                    "where date = @date", conn);
                pgSqlCommand.Parameters.Add("@date", dateTime);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetAllExpenses()
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select expenses.*, expenses_categories.name from expenses" +
                    "join expenses_categories on expenses.id = expenses_categories.id ", conn);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }
    }
}
