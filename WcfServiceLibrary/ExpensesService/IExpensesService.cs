using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary.ExpensesService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IExpensesService" in both code and config file together.
    [ServiceContract]
    public interface IExpensesService
    {
        [OperationContract]
        void AddCategory(string name);

        [OperationContract]
        void EditCategory(int categoryId, string name);

        [OperationContract]
        void DeleteCategory(int categoryId);


        [OperationContract]
        void AddExpense(int categoryId, string name, float quantity, float cost, float resultCost);

        [OperationContract]
        void EditExpense(int expenseId, int categoryId, string name, float quantity, float cost, float resultCost);

        [OperationContract]
        void DeleteExpense(int expenseId);

        [OperationContract]
        DataSet GetCategories(int hoa_id);

        [OperationContract]
        DataSet GetExpenses(DateTime dateTime);

        [OperationContract]
        DataSet GetAllExpenses();
    }
}
