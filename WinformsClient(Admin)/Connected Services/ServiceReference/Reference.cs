﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WinformsClient_Admin_.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IServiceHoaAccount")]
    public interface IServiceHoaAccount {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHoaAccount/CheckAccout", ReplyAction="http://tempuri.org/IServiceHoaAccount/CheckAccoutResponse")]
        bool CheckAccout(string Name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHoaAccount/CheckAccout", ReplyAction="http://tempuri.org/IServiceHoaAccount/CheckAccoutResponse")]
        System.Threading.Tasks.Task<bool> CheckAccoutAsync(string Name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHoaAccount/GetTableOfHoa", ReplyAction="http://tempuri.org/IServiceHoaAccount/GetTableOfHoaResponse")]
        System.Data.DataTable GetTableOfHoa();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHoaAccount/GetTableOfHoa", ReplyAction="http://tempuri.org/IServiceHoaAccount/GetTableOfHoaResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetTableOfHoaAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHoaAccount/AddAccount", ReplyAction="http://tempuri.org/IServiceHoaAccount/AddAccountResponse")]
        void AddAccount(string Name, string Login, string Password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHoaAccount/AddAccount", ReplyAction="http://tempuri.org/IServiceHoaAccount/AddAccountResponse")]
        System.Threading.Tasks.Task AddAccountAsync(string Name, string Login, string Password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHoaAccount/DeleteAccount", ReplyAction="http://tempuri.org/IServiceHoaAccount/DeleteAccountResponse")]
        void DeleteAccount(int Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHoaAccount/DeleteAccount", ReplyAction="http://tempuri.org/IServiceHoaAccount/DeleteAccountResponse")]
        System.Threading.Tasks.Task DeleteAccountAsync(int Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHoaAccount/EditAccount", ReplyAction="http://tempuri.org/IServiceHoaAccount/EditAccountResponse")]
        void EditAccount(int Id, string Name, string Login, string Password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceHoaAccount/EditAccount", ReplyAction="http://tempuri.org/IServiceHoaAccount/EditAccountResponse")]
        System.Threading.Tasks.Task EditAccountAsync(int Id, string Name, string Login, string Password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceHoaAccountChannel : WinformsClient_Admin_.ServiceReference.IServiceHoaAccount, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceHoaAccountClient : System.ServiceModel.ClientBase<WinformsClient_Admin_.ServiceReference.IServiceHoaAccount>, WinformsClient_Admin_.ServiceReference.IServiceHoaAccount {
        
        public ServiceHoaAccountClient() {
        }
        
        public ServiceHoaAccountClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceHoaAccountClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceHoaAccountClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceHoaAccountClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool CheckAccout(string Name) {
            return base.Channel.CheckAccout(Name);
        }
        
        public System.Threading.Tasks.Task<bool> CheckAccoutAsync(string Name) {
            return base.Channel.CheckAccoutAsync(Name);
        }
        
        public System.Data.DataTable GetTableOfHoa() {
            return base.Channel.GetTableOfHoa();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetTableOfHoaAsync() {
            return base.Channel.GetTableOfHoaAsync();
        }
        
        public void AddAccount(string Name, string Login, string Password) {
            base.Channel.AddAccount(Name, Login, Password);
        }
        
        public System.Threading.Tasks.Task AddAccountAsync(string Name, string Login, string Password) {
            return base.Channel.AddAccountAsync(Name, Login, Password);
        }
        
        public void DeleteAccount(int Id) {
            base.Channel.DeleteAccount(Id);
        }
        
        public System.Threading.Tasks.Task DeleteAccountAsync(int Id) {
            return base.Channel.DeleteAccountAsync(Id);
        }
        
        public void EditAccount(int Id, string Name, string Login, string Password) {
            base.Channel.EditAccount(Id, Name, Login, Password);
        }
        
        public System.Threading.Tasks.Task EditAccountAsync(int Id, string Name, string Login, string Password) {
            return base.Channel.EditAccountAsync(Id, Name, Login, Password);
        }
    }
}