//------------------------------------------------------------------------------
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
namespace AnungooODataService.Anungoo
{
    public static class ConnectionHelper
    {
        public const string ConnectionString = @"XpoProvider=MSSqlServer;Data Source=feb37b8b-660a-4677-8aff-a3060014027b.sqlserver.sequelizer.com;Initial Catalog=dbfeb37b8b660a46778affa3060014027b;UID=arspuzoljbkwzkak;PWD=g3ytScgJjeiPAxdVgGeeCEsvqoUspXzAXhosT8S6JhzSVZ778LWvu45HHccDqVuu;";
        public static void Connect(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption)
        {
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(ConnectionString, autoCreateOption);
            XpoDefault.Session = null;
        }
        public static DevExpress.Xpo.DB.IDataStore GetConnectionProvider(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption)
        {
            return XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOption);
        }
        public static DevExpress.Xpo.DB.IDataStore GetConnectionProvider(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        {
            return XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOption, out objectsToDisposeOnDisconnect);
        }
        public static IDataLayer GetDataLayer(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption)
        {
            return XpoDefault.GetDataLayer(ConnectionString, autoCreateOption);
        }
    }

}
