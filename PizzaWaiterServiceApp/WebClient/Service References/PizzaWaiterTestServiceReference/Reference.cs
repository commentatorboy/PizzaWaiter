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
    [System.Runtime.Serialization.DataContractAttribute(Name="PartOrder", Namespace="http://schemas.datacontract.org/2004/07/Models")]
    [System.SerializableAttribute()]
    public partial class PartOrder : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AmountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WebClient.PizzaWaiterTestServiceReference.Dish DishField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int DishIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WebClient.PizzaWaiterTestServiceReference.Order OrderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int OrderIDField;
        
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
        public int Amount {
            get {
                return this.AmountField;
            }
            set {
                if ((this.AmountField.Equals(value) != true)) {
                    this.AmountField = value;
                    this.RaisePropertyChanged("Amount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WebClient.PizzaWaiterTestServiceReference.Dish Dish {
            get {
                return this.DishField;
            }
            set {
                if ((object.ReferenceEquals(this.DishField, value) != true)) {
                    this.DishField = value;
                    this.RaisePropertyChanged("Dish");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int DishID {
            get {
                return this.DishIDField;
            }
            set {
                if ((this.DishIDField.Equals(value) != true)) {
                    this.DishIDField = value;
                    this.RaisePropertyChanged("DishID");
                }
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
        public WebClient.PizzaWaiterTestServiceReference.Order Order {
            get {
                return this.OrderField;
            }
            set {
                if ((object.ReferenceEquals(this.OrderField, value) != true)) {
                    this.OrderField = value;
                    this.RaisePropertyChanged("Order");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int OrderID {
            get {
                return this.OrderIDField;
            }
            set {
                if ((this.OrderIDField.Equals(value) != true)) {
                    this.OrderIDField = value;
                    this.RaisePropertyChanged("OrderID");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Dish", Namespace="http://schemas.datacontract.org/2004/07/Models")]
    [System.SerializableAttribute()]
    public partial class Dish : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal PriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RestaurantMenuIDField;
        
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Number {
            get {
                return this.NumberField;
            }
            set {
                if ((this.NumberField.Equals(value) != true)) {
                    this.NumberField = value;
                    this.RaisePropertyChanged("Number");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RestaurantMenuID {
            get {
                return this.RestaurantMenuIDField;
            }
            set {
                if ((this.RestaurantMenuIDField.Equals(value) != true)) {
                    this.RestaurantMenuIDField = value;
                    this.RaisePropertyChanged("RestaurantMenuID");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Order", Namespace="http://schemas.datacontract.org/2004/07/Models")]
    [System.SerializableAttribute()]
    public partial class Order : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AddressIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WebClient.PizzaWaiterTestServiceReference.OrderStatus StatusIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserIDField;
        
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
        public int AddressID {
            get {
                return this.AddressIDField;
            }
            set {
                if ((this.AddressIDField.Equals(value) != true)) {
                    this.AddressIDField = value;
                    this.RaisePropertyChanged("AddressID");
                }
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
        public WebClient.PizzaWaiterTestServiceReference.OrderStatus StatusID {
            get {
                return this.StatusIDField;
            }
            set {
                if ((this.StatusIDField.Equals(value) != true)) {
                    this.StatusIDField = value;
                    this.RaisePropertyChanged("StatusID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserID {
            get {
                return this.UserIDField;
            }
            set {
                if ((this.UserIDField.Equals(value) != true)) {
                    this.UserIDField = value;
                    this.RaisePropertyChanged("UserID");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OrderStatus", Namespace="http://schemas.datacontract.org/2004/07/Models")]
    public enum OrderStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Default = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        WAITING = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        COOKING = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TRANPORTING = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RECIEVED = 4,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DishIngredient", Namespace="http://schemas.datacontract.org/2004/07/Models")]
    [System.SerializableAttribute()]
    public partial class DishIngredient : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WebClient.PizzaWaiterTestServiceReference.Dish DishField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int DishIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WebClient.PizzaWaiterTestServiceReference.Ingredient IngredientField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IngredientIDField;
        
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
        public WebClient.PizzaWaiterTestServiceReference.Dish Dish {
            get {
                return this.DishField;
            }
            set {
                if ((object.ReferenceEquals(this.DishField, value) != true)) {
                    this.DishField = value;
                    this.RaisePropertyChanged("Dish");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int DishID {
            get {
                return this.DishIDField;
            }
            set {
                if ((this.DishIDField.Equals(value) != true)) {
                    this.DishIDField = value;
                    this.RaisePropertyChanged("DishID");
                }
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
        public WebClient.PizzaWaiterTestServiceReference.Ingredient Ingredient {
            get {
                return this.IngredientField;
            }
            set {
                if ((object.ReferenceEquals(this.IngredientField, value) != true)) {
                    this.IngredientField = value;
                    this.RaisePropertyChanged("Ingredient");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IngredientID {
            get {
                return this.IngredientIDField;
            }
            set {
                if ((this.IngredientIDField.Equals(value) != true)) {
                    this.IngredientIDField = value;
                    this.RaisePropertyChanged("IngredientID");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Ingredient", Namespace="http://schemas.datacontract.org/2004/07/Models")]
    [System.SerializableAttribute()]
    public partial class Ingredient : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RestaurantMenu", Namespace="http://schemas.datacontract.org/2004/07/Models")]
    [System.SerializableAttribute()]
    public partial class RestaurantMenu : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WebClient.PizzaWaiterTestServiceReference.Menu MenuField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MenuIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PositionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WebClient.PizzaWaiterTestServiceReference.Restaurant RestaurantField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RestaurantIDField;
        
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
        public WebClient.PizzaWaiterTestServiceReference.Menu Menu {
            get {
                return this.MenuField;
            }
            set {
                if ((object.ReferenceEquals(this.MenuField, value) != true)) {
                    this.MenuField = value;
                    this.RaisePropertyChanged("Menu");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MenuID {
            get {
                return this.MenuIDField;
            }
            set {
                if ((this.MenuIDField.Equals(value) != true)) {
                    this.MenuIDField = value;
                    this.RaisePropertyChanged("MenuID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Position {
            get {
                return this.PositionField;
            }
            set {
                if ((this.PositionField.Equals(value) != true)) {
                    this.PositionField = value;
                    this.RaisePropertyChanged("Position");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WebClient.PizzaWaiterTestServiceReference.Restaurant Restaurant {
            get {
                return this.RestaurantField;
            }
            set {
                if ((object.ReferenceEquals(this.RestaurantField, value) != true)) {
                    this.RestaurantField = value;
                    this.RaisePropertyChanged("Restaurant");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RestaurantID {
            get {
                return this.RestaurantIDField;
            }
            set {
                if ((this.RestaurantIDField.Equals(value) != true)) {
                    this.RestaurantIDField = value;
                    this.RaisePropertyChanged("RestaurantID");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Menu", Namespace="http://schemas.datacontract.org/2004/07/Models")]
    [System.SerializableAttribute()]
    public partial class Menu : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
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
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/TestHeavyObject", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/TestHeavyObjectResponse")]
        bool TestHeavyObject(string[][] heavyObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/TestHeavyObject", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/TestHeavyObjectResponse")]
        System.Threading.Tasks.Task<bool> TestHeavyObjectAsync(string[][] heavyObject);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/ProcessOrder", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/ProcessOrderResponse")]
        bool ProcessOrder(WebClient.PizzaWaiterTestServiceReference.PartOrder[] partOrders, string phoneNr, string address);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/ProcessOrder", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/ProcessOrderResponse")]
        System.Threading.Tasks.Task<bool> ProcessOrderAsync(WebClient.PizzaWaiterTestServiceReference.PartOrder[] partOrders, string phoneNr, string address);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/GetIngredientsByDishId", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/GetIngredientsByDishIdResponse")]
        WebClient.PizzaWaiterTestServiceReference.DishIngredient[] GetIngredientsByDishId(int dishID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/GetIngredientsByDishId", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/GetIngredientsByDishIdResponse")]
        System.Threading.Tasks.Task<WebClient.PizzaWaiterTestServiceReference.DishIngredient[]> GetIngredientsByDishIdAsync(int dishID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/GetDishesByRestaurantMenuId", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/GetDishesByRestaurantMenuIdResponse")]
        WebClient.PizzaWaiterTestServiceReference.Dish[] GetDishesByRestaurantMenuId(int restaurantMenuID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/GetDishesByRestaurantMenuId", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/GetDishesByRestaurantMenuIdResponse")]
        System.Threading.Tasks.Task<WebClient.PizzaWaiterTestServiceReference.Dish[]> GetDishesByRestaurantMenuIdAsync(int restaurantMenuID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/GetRestaurantMenues", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/GetRestaurantMenuesResponse")]
        WebClient.PizzaWaiterTestServiceReference.RestaurantMenu[] GetRestaurantMenues(int restaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/GetRestaurantMenues", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/GetRestaurantMenuesResponse")]
        System.Threading.Tasks.Task<WebClient.PizzaWaiterTestServiceReference.RestaurantMenu[]> GetRestaurantMenuesAsync(int restaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/GetLocalRestaurants", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/GetLocalRestaurantsResponse")]
        WebClient.PizzaWaiterTestServiceReference.Restaurant[] GetLocalRestaurants(decimal latitude, decimal longtitude);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPizzaWaiterTestService/GetLocalRestaurants", ReplyAction="http://tempuri.org/IPizzaWaiterTestService/GetLocalRestaurantsResponse")]
        System.Threading.Tasks.Task<WebClient.PizzaWaiterTestServiceReference.Restaurant[]> GetLocalRestaurantsAsync(decimal latitude, decimal longtitude);
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
        
        public bool TestHeavyObject(string[][] heavyObject) {
            return base.Channel.TestHeavyObject(heavyObject);
        }
        
        public System.Threading.Tasks.Task<bool> TestHeavyObjectAsync(string[][] heavyObject) {
            return base.Channel.TestHeavyObjectAsync(heavyObject);
        }
        
        public bool ProcessOrder(WebClient.PizzaWaiterTestServiceReference.PartOrder[] partOrders, string phoneNr, string address) {
            return base.Channel.ProcessOrder(partOrders, phoneNr, address);
        }
        
        public System.Threading.Tasks.Task<bool> ProcessOrderAsync(WebClient.PizzaWaiterTestServiceReference.PartOrder[] partOrders, string phoneNr, string address) {
            return base.Channel.ProcessOrderAsync(partOrders, phoneNr, address);
        }
        
        public WebClient.PizzaWaiterTestServiceReference.DishIngredient[] GetIngredientsByDishId(int dishID) {
            return base.Channel.GetIngredientsByDishId(dishID);
        }
        
        public System.Threading.Tasks.Task<WebClient.PizzaWaiterTestServiceReference.DishIngredient[]> GetIngredientsByDishIdAsync(int dishID) {
            return base.Channel.GetIngredientsByDishIdAsync(dishID);
        }
        
        public WebClient.PizzaWaiterTestServiceReference.Dish[] GetDishesByRestaurantMenuId(int restaurantMenuID) {
            return base.Channel.GetDishesByRestaurantMenuId(restaurantMenuID);
        }
        
        public System.Threading.Tasks.Task<WebClient.PizzaWaiterTestServiceReference.Dish[]> GetDishesByRestaurantMenuIdAsync(int restaurantMenuID) {
            return base.Channel.GetDishesByRestaurantMenuIdAsync(restaurantMenuID);
        }
        
        public WebClient.PizzaWaiterTestServiceReference.RestaurantMenu[] GetRestaurantMenues(int restaurantID) {
            return base.Channel.GetRestaurantMenues(restaurantID);
        }
        
        public System.Threading.Tasks.Task<WebClient.PizzaWaiterTestServiceReference.RestaurantMenu[]> GetRestaurantMenuesAsync(int restaurantID) {
            return base.Channel.GetRestaurantMenuesAsync(restaurantID);
        }
        
        public WebClient.PizzaWaiterTestServiceReference.Restaurant[] GetLocalRestaurants(decimal latitude, decimal longtitude) {
            return base.Channel.GetLocalRestaurants(latitude, longtitude);
        }
        
        public System.Threading.Tasks.Task<WebClient.PizzaWaiterTestServiceReference.Restaurant[]> GetLocalRestaurantsAsync(decimal latitude, decimal longtitude) {
            return base.Channel.GetLocalRestaurantsAsync(latitude, longtitude);
        }
    }
}
