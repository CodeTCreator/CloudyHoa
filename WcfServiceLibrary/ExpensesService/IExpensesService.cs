using System;
using System.Data;
using System.ServiceModel;

namespace WcfServiceLibrary.ExpensesService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IExpensesService" in both code and config file together.
    [ServiceContract]
    public interface IExpensesService
    {
        [OperationContract]
        void AddCategory(string name, int hoa_id);

        [OperationContract]
        void EditCategory(int categoryId, string name);

        [OperationContract]
        void DeleteCategory(int categoryId);


        [OperationContract]
        void AddExpense(int categoryId, string name, float quantity, float cost, float resultCost, DateTime date);

        [OperationContract]
        void AddExternalExpense(int categoryId, string name, float quantity, float cost, float resultCost, DateTime date,int objectId);

        [OperationContract]
        void EditExpense(int expenseId, int categoryId, string name, float quantity, float cost, float resultCost, DateTime date);
        [OperationContract]
        void EditExternalExpense(int expenseId, int categoryId, string name, float quantity, float cost, float resultCost, DateTime date, int objectId);

        [OperationContract]
        void DeleteExpense(int expenseId);

        [OperationContract]
        DataSet GetCategories(int hoa_id);

        [OperationContract]
        DataSet GetExpenses(DateTime dateTime);

        [OperationContract]
        DataSet GetAllExpenses(int hoa_id);
    }
}
