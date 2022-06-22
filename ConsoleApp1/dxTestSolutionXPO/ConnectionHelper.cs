﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using dxTestSolutionXPO.Module.BusinessObjects;
using DevExpress.Xpo.DB;

namespace dxTestSolutionXPO {
    public static class ConnectionHelper {
        static Type[] persistentTypes = new Type[] {
            typeof(Order),typeof(OrderItem)
        };
        public static Type[] GetPersistentTypes() {
            Type[] copy = new Type[persistentTypes.Length];
            Array.Copy(persistentTypes, copy, persistentTypes.Length);
            return copy;
        }
        static string ConnectionString;
        static bool UseInMemoryStore;
        public static void Connect(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption, bool threadSafe = false) {
            ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            if(threadSafe) {
                var provider = XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOption);
                var dictionary = new DevExpress.Xpo.Metadata.ReflectionDictionary();
                dictionary.GetDataStoreSchema(persistentTypes);
                XpoDefault.DataLayer = new ThreadSafeDataLayer(dictionary, provider);
            } else {
                XpoDefault.DataLayer = XpoDefault.GetDataLayer(ConnectionString, autoCreateOption);
            }
            UseInMemoryStore = true;
            if(UseInMemoryStore) {
                XpoDefault.DataLayer = new SimpleDataLayer(new InMemoryDataStore());
            }
            XpoDefault.Session = null;
        }
        public static DevExpress.Xpo.DB.IDataStore GetConnectionProvider(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption) {
            return XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOption);
        }
        public static DevExpress.Xpo.DB.IDataStore GetConnectionProvider(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect) {
            return XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOption, out objectsToDisposeOnDisconnect);
        }
        public static IDataLayer GetDataLayer(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption) {
            return XpoDefault.GetDataLayer(ConnectionString, autoCreateOption);
        }
        public static Order AddOrder(UnitOfWork _uow, string _firstName) {
            var c = new Order(_uow);
            c.OrderName = _firstName;
            return c;
        }
        public static Order AddOrder(UnitOfWork _uow, string _firstName, string _description) {
            var c = AddOrder(_uow, _firstName);
            c.Description = _description;
            return c;
        }

        internal static Order AddOrder(UnitOfWork _uow, string _firstName, int _age) {
            var c = AddOrder(_uow, _firstName);
            c.Price = _age;
            return c;

        }
        public static Order AddOrder(UnitOfWork _uow, string _firstName, int _age, bool _isActive) {
            var c = AddOrder(_uow, _firstName, _age);
            c.IsActive = _isActive;
            return c;
        }
        public static OrderItem AddOrderItem(UnitOfWork _uow, Order _parent, string _subject) {
            var t = new OrderItem(_uow);
            t.OrderItemName = _subject;
            t.Order = _parent;
            return t;
        }
        public static OrderItem AddOrderItem(UnitOfWork _uow, Order _parent, string _subject, int _price) {
            var t = AddOrderItem(_uow, _parent, _subject);
            t.ItemPrice = _price;
            return t;
        }
        public static OrderItem AddOrderItem(UnitOfWork _uow, Order _parent, string _subject, int _price, int _id) {
            var t = AddOrderItem(_uow, _parent, _subject);
            t.Oid = _id;
            t.ItemPrice = _price;
            return t;
        }
        public static OrderItem AddOrderItem(UnitOfWork _uow, Order _parent, string _subject, int _price, bool _isAvailable) {
            var t = AddOrderItem(_uow, _parent, _subject, _price);
            t.IsAvailable = _isAvailable;
            return t;
        }
    }

}
