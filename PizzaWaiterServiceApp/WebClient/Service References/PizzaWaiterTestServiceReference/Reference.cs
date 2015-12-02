﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18331
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebClient.PizzaWaiterTestServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Restaurant", Namespace="http://schemas.datacontract.org/2004/07/Models")]
    [System.SerializableAttribute()]
    public partial class Restaurant : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PizzaWaiterTestServiceReference.IPizzaWaiterTestService")]
    public interface IPizzaWaiterTestService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/getAllMenues", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/getAllMenuesResponse")]
        WebClient.PizzaWaiterTestServiceReference.Restaurant getAllMenues(int restaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/getAllMenues", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/getAllMenuesResponse")]
        System.Threading.Tasks.Task<WebClient.PizzaWaiterTestServiceReference.Restaurant> getAllMenuesAsync(int restaurantID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPizzaWaiterTestServiceChannel : WebClient.PizzaWaiterTestServiceReference.IPizzaWaiterTestService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PizzaWaiterTestServiceClient : System.ServiceModel.ClientBase<WebClient.PizzaWaiterTestServiceReference.IPizzaWaiterTestService>, WebClient.PizzaWaiterTestServiceReference.IPizzaWaiterTestService {
        
        public PizzaWaiterTestServiceClient() {
        }
        
        public PizzaWaiterTestServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PizzaWaiterTestServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PizzaWaiterTestServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PizzaWaiterTestServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WebClient.PizzaWaiterTestServiceReference.Restaurant getAllMenues(int restaurantID) {
            return base.Channel.getAllMenues(restaurantID);
        }
        
        public System.Threading.Tasks.Task<WebClient.PizzaWaiterTestServiceReference.Restaurant> getAllMenuesAsync(int restaurantID) {
            return base.Channel.getAllMenuesAsync(restaurantID);
        }
    }
}
