﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChatClient.ServiceParking {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceParking.IServiceParking", CallbackContract=typeof(ChatClient.ServiceParking.IServiceParkingCallback))]
    public interface IServiceParking {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/Connect", ReplyAction="http://tempuri.org/IServiceParking/ConnectResponse")]
        void Connect();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/Connect", ReplyAction="http://tempuri.org/IServiceParking/ConnectResponse")]
        System.Threading.Tasks.Task ConnectAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/Disconnect", ReplyAction="http://tempuri.org/IServiceParking/DisconnectResponse")]
        void Disconnect();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/Disconnect", ReplyAction="http://tempuri.org/IServiceParking/DisconnectResponse")]
        System.Threading.Tasks.Task DisconnectAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/SendDB", ReplyAction="http://tempuri.org/IServiceParking/SendDBResponse")]
        string SendDB(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/SendDB", ReplyAction="http://tempuri.org/IServiceParking/SendDBResponse")]
        System.Threading.Tasks.Task<string> SendDBAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/TryLogin", ReplyAction="http://tempuri.org/IServiceParking/TryLoginResponse")]
        bool TryLogin(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/TryLogin", ReplyAction="http://tempuri.org/IServiceParking/TryLoginResponse")]
        System.Threading.Tasks.Task<bool> TryLoginAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/TryRegister", ReplyAction="http://tempuri.org/IServiceParking/TryRegisterResponse")]
        bool TryRegister(string login, string password, string name, string surname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/TryRegister", ReplyAction="http://tempuri.org/IServiceParking/TryRegisterResponse")]
        System.Threading.Tasks.Task<bool> TryRegisterAsync(string login, string password, string name, string surname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/SendMarks", ReplyAction="http://tempuri.org/IServiceParking/SendMarksResponse")]
        string SendMarks();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/SendMarks", ReplyAction="http://tempuri.org/IServiceParking/SendMarksResponse")]
        System.Threading.Tasks.Task<string> SendMarksAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/SendModels", ReplyAction="http://tempuri.org/IServiceParking/SendModelsResponse")]
        string SendModels(string mark);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/SendModels", ReplyAction="http://tempuri.org/IServiceParking/SendModelsResponse")]
        System.Threading.Tasks.Task<string> SendModelsAsync(string mark);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/TryOrder", ReplyAction="http://tempuri.org/IServiceParking/TryOrderResponse")]
        bool TryOrder(int transport, string number, int creator, string creationdate, string endingdate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/TryOrder", ReplyAction="http://tempuri.org/IServiceParking/TryOrderResponse")]
        System.Threading.Tasks.Task<bool> TryOrderAsync(int transport, string number, int creator, string creationdate, string endingdate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/GetTransport", ReplyAction="http://tempuri.org/IServiceParking/GetTransportResponse")]
        int GetTransport(string mark, string model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/GetTransport", ReplyAction="http://tempuri.org/IServiceParking/GetTransportResponse")]
        System.Threading.Tasks.Task<int> GetTransportAsync(string mark, string model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/GetUserID", ReplyAction="http://tempuri.org/IServiceParking/GetUserIDResponse")]
        int GetUserID(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/GetUserID", ReplyAction="http://tempuri.org/IServiceParking/GetUserIDResponse")]
        System.Threading.Tasks.Task<int> GetUserIDAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/GetCount", ReplyAction="http://tempuri.org/IServiceParking/GetCountResponse")]
        int GetCount();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/GetCount", ReplyAction="http://tempuri.org/IServiceParking/GetCountResponse")]
        System.Threading.Tasks.Task<int> GetCountAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/SendNotifications", ReplyAction="http://tempuri.org/IServiceParking/SendNotificationsResponse")]
        string SendNotifications(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/SendNotifications", ReplyAction="http://tempuri.org/IServiceParking/SendNotificationsResponse")]
        System.Threading.Tasks.Task<string> SendNotificationsAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/ClearNotification", ReplyAction="http://tempuri.org/IServiceParking/ClearNotificationResponse")]
        bool ClearNotification(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceParking/ClearNotification", ReplyAction="http://tempuri.org/IServiceParking/ClearNotificationResponse")]
        System.Threading.Tasks.Task<bool> ClearNotificationAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceParkingCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServiceParking/MsgCallback")]
        void MsgCallback(string msg);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServiceParking/MarksCallback")]
        void MarksCallback(string msg);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServiceParking/ModelsCallback")]
        void ModelsCallback(string msg);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceParkingChannel : ChatClient.ServiceParking.IServiceParking, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceParkingClient : System.ServiceModel.DuplexClientBase<ChatClient.ServiceParking.IServiceParking>, ChatClient.ServiceParking.IServiceParking {
        
        public ServiceParkingClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ServiceParkingClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ServiceParkingClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceParkingClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceParkingClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Connect() {
            base.Channel.Connect();
        }
        
        public System.Threading.Tasks.Task ConnectAsync() {
            return base.Channel.ConnectAsync();
        }
        
        public void Disconnect() {
            base.Channel.Disconnect();
        }
        
        public System.Threading.Tasks.Task DisconnectAsync() {
            return base.Channel.DisconnectAsync();
        }
        
        public string SendDB(string login) {
            return base.Channel.SendDB(login);
        }
        
        public System.Threading.Tasks.Task<string> SendDBAsync(string login) {
            return base.Channel.SendDBAsync(login);
        }
        
        public bool TryLogin(string login, string password) {
            return base.Channel.TryLogin(login, password);
        }
        
        public System.Threading.Tasks.Task<bool> TryLoginAsync(string login, string password) {
            return base.Channel.TryLoginAsync(login, password);
        }
        
        public bool TryRegister(string login, string password, string name, string surname) {
            return base.Channel.TryRegister(login, password, name, surname);
        }
        
        public System.Threading.Tasks.Task<bool> TryRegisterAsync(string login, string password, string name, string surname) {
            return base.Channel.TryRegisterAsync(login, password, name, surname);
        }
        
        public string SendMarks() {
            return base.Channel.SendMarks();
        }
        
        public System.Threading.Tasks.Task<string> SendMarksAsync() {
            return base.Channel.SendMarksAsync();
        }
        
        public string SendModels(string mark) {
            return base.Channel.SendModels(mark);
        }
        
        public System.Threading.Tasks.Task<string> SendModelsAsync(string mark) {
            return base.Channel.SendModelsAsync(mark);
        }
        
        public bool TryOrder(int transport, string number, int creator, string creationdate, string endingdate) {
            return base.Channel.TryOrder(transport, number, creator, creationdate, endingdate);
        }
        
        public System.Threading.Tasks.Task<bool> TryOrderAsync(int transport, string number, int creator, string creationdate, string endingdate) {
            return base.Channel.TryOrderAsync(transport, number, creator, creationdate, endingdate);
        }
        
        public int GetTransport(string mark, string model) {
            return base.Channel.GetTransport(mark, model);
        }
        
        public System.Threading.Tasks.Task<int> GetTransportAsync(string mark, string model) {
            return base.Channel.GetTransportAsync(mark, model);
        }
        
        public int GetUserID(string login) {
            return base.Channel.GetUserID(login);
        }
        
        public System.Threading.Tasks.Task<int> GetUserIDAsync(string login) {
            return base.Channel.GetUserIDAsync(login);
        }
        
        public int GetCount() {
            return base.Channel.GetCount();
        }
        
        public System.Threading.Tasks.Task<int> GetCountAsync() {
            return base.Channel.GetCountAsync();
        }
        
        public string SendNotifications(string login) {
            return base.Channel.SendNotifications(login);
        }
        
        public System.Threading.Tasks.Task<string> SendNotificationsAsync(string login) {
            return base.Channel.SendNotificationsAsync(login);
        }
        
        public bool ClearNotification(int id) {
            return base.Channel.ClearNotification(id);
        }
        
        public System.Threading.Tasks.Task<bool> ClearNotificationAsync(int id) {
            return base.Channel.ClearNotificationAsync(id);
        }
    }
}
