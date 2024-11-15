﻿using Devart.Data.PostgreSql;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ObjectsService" in both code and config file together.
    public class ObjectsService : IObjectsService
    {
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];
        public int AddObject(int hoaId,int typeObject, string objectNumber, int? parentId)
        {
            int objectId = -1;
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO objects (hoa_id, type_object, " +
                    "parent_id, identificator) VALUES (@hoa_id,@type_object,@parent_id,@identificator) returning id", conn);
                pgSqlCommand.Parameters.Add("@hoa_id", hoaId);
                pgSqlCommand.Parameters.Add("@type_object", typeObject);
                pgSqlCommand.Parameters.Add("@parent_id", parentId);
                pgSqlCommand.Parameters.Add("@identificator", objectNumber);
                using (PgSqlDataReader reader = pgSqlCommand.ExecuteReader())
                {
                    reader.Read();
                    if (reader.HasRows)
                    {
                        objectId = reader.GetInt32(0);
                    }
                }
                conn.Close();
            }
            return objectId;
        }

        public void DeleteObject(int objectId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from objects where id = @id_value", conn);
                pgSqlCommand.Parameters.Add("@id_value", objectId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void EditObject(int objectId, string objectNumber, int? parentId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE objects SET identificator = @identificator, " +
                    "parent_id = @parent_id where id = @id", conn);
                pgSqlCommand.Parameters.Add("@id", objectId);
                pgSqlCommand.Parameters.Add("@identificator", objectNumber);
                pgSqlCommand.Parameters.Add("@parent_id", parentId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet GetAllObjects(int hoaId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select parent_id, objects.id,types_objects.name, types_objects.id,identificator" +
                    "  from (types_objects inner join objects " +
                    "on (objects.type_object = types_objects.id))  " +
                    "where objects.hoa_id = @hoaId order by identificator ", conn);
                pgSqlCommand.Parameters.Add("@hoaId", hoaId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetObjectsParents(int hoaId, int typeObject)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select types_objects.id,types_objects.name || ' ' || identificator as name, objects.id " +
                    "from types_objects " +
                    "join objects on objects.type_object = types_objects.id  " +
                    "where types_objects.hoa_id = @hoaId and types_objects.id = " +
                    "(select parent_type from types_objects where id = @parent_type and hoa_id = @hoaId)", conn);
                pgSqlCommand.Parameters.Add("@hoaId", hoaId);
                pgSqlCommand.Parameters.Add("@parent_type", typeObject);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetObjectsStructure(int hoaId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * from types_objects where hoa_id = @hoaId", conn);
                pgSqlCommand.Parameters.Add("@hoaId", hoaId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetAllChilds(int objectId, int typeObject)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("WITH RECURSIVE search_tree(id,type_object,identificator, parent_id) AS ( " +
                   "SELECT objects.id,objects.type_object,objects.identificator, objects.parent_id" +
                   " FROM objects where objects.id = @objectId " +
                   "UNION ALL " +
                   "SELECT t.id, t.type_object, t.identificator,t.parent_id " +
                   "FROM objects t, search_tree st " +
                   "WHERE st.id = t.parent_id " +
                   ") " +
                   "SELECT search_tree.*,name, identificator FROM search_tree " +
                   "join types_objects on types_objects.id = search_tree.type_object " +
                   "where type_object = @typeObject ", conn);
                pgSqlCommand.Parameters.Add("@typeObject", typeObject);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetPathObject(int objectId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("WITH RECURSIVE search_tree(id,type_object,identificator, parent_id) AS ( " +
                    "SELECT objects.id,objects.type_object,objects.identificator, objects.parent_id" +
                    " FROM objects where objects.id = @objectId " +
                    "UNION ALL " +
                    "SELECT t.id, t.type_object, t.identificator,t.parent_id " +
                    "FROM objects t, search_tree st " +
                    "WHERE st.parent_id = t.id " +
                    ") " +
                    "SELECT search_tree.*,name || ' ' || identificator as Name FROM search_tree " +
                    "join types_objects on types_objects.id = search_tree.type_object", conn);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetTypesOfChilds(int typeObject)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("WITH RECURSIVE search_tree(id,name,parent_type, hoa_id) AS ( " +
                    "SELECT types_objects.id,types_objects.name,types_objects.parent_type, types_objects.hoa_id " +
                    "FROM types_objects " +
                    "where types_objects.id = @typeObject " +
                    "UNION ALL " +
                    "SELECT t.id, t.name, t.parent_type,t.hoa_id " +
                    "FROM types_objects t, search_tree st " +
                    "WHERE st.id = t.parent_type " +
                    ") " +
                    "SELECT search_tree.* FROM search_tree " +
                    "WHERE id != @typeObject", conn);
                pgSqlCommand.Parameters.Add("@typeObject", typeObject);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetObject(int objectId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select objects.*, name from objects" +
                    " join types_objects on type_object = types_objects.id " +
                    " where objects.id = @objectId", conn);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }
    }
}
