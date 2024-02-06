using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsClient_Admin_.DAO
{
    internal class DataObject
    {
        DataTable dataTable;

        public DataObject() 
        { 
            dataTable = new DataTable();
        }

        public DataObject(DataTable dataTable)
        {
            this.DataTable = dataTable;
        }
        DataTable DataTable 
        { 
            get { return dataTable; } 
            set { dataTable = value; }
        }
    }
}
