using Devart.Data.PostgreSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using UHoaAdmin.ParametrWindows;
using UniversalHoa_WF.Classes;

namespace UHoaAdmin.Classes
{
    static class CalculateProperty
    {
        /// <summary>
        /// Обработка и вычисление формулы
        /// Для поиска и дальнейшего вычисления необходим id свойства и id объекта
        /// Парсинг строки до первой (, дальше сравнение с имеющимися функциями и вызов функции, если найдена. 
        /// Сразу считывается значение в пределах найденных ( )
        /// </summary>
        /// <param name="property_id"></param>
        /// <param name="object_id"></param>
        /// <param name="databaseManager"></param>
        /// <returns></returns>
        public static double Calculate(int property_id,int object_id, int paId, DateTime period, DatabaseManager databaseManager)
        {
            double result = 0;

            List<Tuple<int,string>> allProperty = new List<Tuple<int, string>> ();

            PgSqlCommand pgSqlCommand = new PgSqlCommand("select formula,type_object from metadata" +
                " where id = @id", databaseManager.Connection);
            pgSqlCommand.Parameters.Add("@id", property_id);
            PgSqlDataReader pgSqlDataReader = pgSqlCommand.ExecuteReader();
            pgSqlDataReader.Read();


            string formula = pgSqlDataReader.GetString(0);
            string resultExpression = string.Empty;
            string intermediate = string.Empty;
            //DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            float value = 0;

            formula += ' ';
            //int i = 0;
            for (int i = 0; i < formula.Length; i++)
            {
                if (formula[i] == '+' | formula[i] == '-' | formula[i] == '/' | formula[i] == '*' | formula[i] == '(' | formula[i] == ')' | formula[i] == ' ')
                {
                    if(Regex.IsMatch(intermediate, @"^\d+(.|,)?\d+$") | Regex.IsMatch(intermediate, @"^\d+$"))
                    {
                        resultExpression += intermediate;
                        intermediate = string.Empty;
                        resultExpression += formula[i];
                    }
                    else
                    {
                        if ((formula[i] == '+' | formula[i] == '-' | formula[i] == '/' | formula[i] == '*' | formula[i] == '(' | formula[i] == ')' 
                            | formula[i] == ' ') & intermediate.Length == 0)
                        {
                            resultExpression += formula[i];
                        }
                        else
                        {
                            // Получение всех имещихся методов класса и вызов метода, с которым совпадает строка
                            DBRequest dBRequest = new DBRequest(databaseManager,period);
                            Type dbRequest = typeof(DBRequest);
                            MethodInfo[] methods = dbRequest.GetMethods();

                            for(int j = 0; j < methods.Length; j++)
                            {
                                if(intermediate == methods[j].Name)
                                {
                                    string val = formula.Substring(i+1, formula.Substring(i+2).IndexOf(')') + 1);
                                    object[] ob = new object[3];
                                    ob[0] = val;
                                    ob[1] = object_id;
                                    ob[2] = paId;
                                    MethodInfo m = dBRequest.GetType().GetMethod(intermediate);
                                    if(m.GetParameters().Length == 1)
                                    {
                                        value = (float)m.Invoke(dBRequest, new object[1] { ob[0] });

                                    }
                                    else
                                    {
                                        value = (float)m.Invoke(dBRequest, ob);
                                    }
                                    i += 2 + formula.Substring(i + 2).IndexOf(')');
                                    break;
                                }
                            }
                            {
                                // Получение значения поля static для свойства
                                //pgSqlCommand = new PgSqlCommand("select static,id from metadata " +
                                //    "where system_name = @sysname", databaseManager.Connection);
                                ////pgSqlCommand.Parameters.Add("@propId", property_id);
                                //pgSqlCommand.Parameters.Add("@sysname", intermediate);
                                //pgSqlDataReader = pgSqlCommand.ExecuteReader();
                                //pgSqlDataReader.Read();
                                //int prop_id = pgSqlDataReader.GetInt32(1);
                                //if (!pgSqlDataReader.GetBoolean(0))
                                //{
                                //    // Получение значения с таблицы динамических параметров
                                //    pgSqlCommand = new PgSqlCommand("select value from dynamic_params where object_id = @object_id and period = @period and " +
                                //        "property_id = @property_id ", databaseManager.Connection);
                                //    pgSqlCommand.Parameters.Add("@object_id", object_id);
                                //    pgSqlCommand.Parameters.Add("@period", dateTime);
                                //    pgSqlCommand.Parameters.Add("@property_id", property_id);
                                //    //pgSqlCommand.Parameters.Add("@sysname", intermediate);
                                //    pgSqlDataReader = pgSqlCommand.ExecuteReader();
                                //    pgSqlDataReader.Read();
                                //    if (pgSqlDataReader.HasRows)
                                //    {
                                //        value = pgSqlDataReader.GetDouble(0);
                                //    }
                                //    else
                                //    {
                                //        value = 0;
                                //    }
                                //}
                                //else
                                //{
                                //    pgSqlCommand = new PgSqlCommand("select number_value from static_params where type_object = @type and " +
                                //        "property_id = @property_id and identificator = @seq", databaseManager.Connection);
                                //    //pgSqlCommand.Parameters.Add("@object_id", object_id);
                                //    //pgSqlCommand.Parameters.Add("@period", dateTime);
                                //    pgSqlCommand.Parameters.Add("@type", 1);
                                //    pgSqlCommand.Parameters.Add("@property_id", prop_id);
                                //    pgSqlCommand.Parameters.Add("@seq", 1);
                                //    //pgSqlCommand.Parameters.Add("@sysname", intermediate);
                                //    pgSqlDataReader = pgSqlCommand.ExecuteReader();
                                //    pgSqlDataReader.Read();
                                //    if (pgSqlDataReader.HasRows)
                                //    {
                                //        value = pgSqlDataReader.GetDouble(0);
                                //    }
                                //    else
                                //    {
                                //        value = 0;
                                //    }
                                //}
                            }
                            resultExpression += value;
                            intermediate = string.Empty;
                        }
                    }
                    //if(formula[i] != ' ')
                    //{
                    //    resultExpression += formula[i];
                    //}
                    
                }
                else
                {
                    intermediate += formula[i];
                }
            }
            // Вычисление математического значения, которое записано в строке
            result = Convert.ToDouble (resultExpression != " " ?new DataTable().Compute(resultExpression, "") : 0);

            return result;
        }
    }
}
