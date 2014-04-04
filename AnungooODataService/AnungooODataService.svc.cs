using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using DevExpress.Xpo;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq.Expressions;
using AnungooODataService.Anungoo;
using System.Web;

namespace AnungooODataService
{
    public class AnungooODataServiceContext : XpoContext
    {
        public AnungooODataServiceContext(string s1, string s2, IDataLayer dataLayer)
            : base(s1, s2, dataLayer)
        {
        }
        // Place Actions here.
        // [Action]
        // public bool ActionName(EntityType entity) {
        //     return true;
        // }
    }
    /// <summary>
    /// http://localhost:62685/AnungooODataService.svc/Test?Test1=Bold1&$format=json
    /// </summary>
#if DEBUG
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    [JSONPSupportBehavior]
#else
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [JSONPSupportBehavior]
#endif
    public class AnungooODataServiceService : XpoDataServiceV3
    {
        static readonly AnungooODataServiceContext serviceContext = new AnungooODataServiceContext("AnungooODataServiceService", "AnungooODataService.Anungoo", CreateDataLayer());

        Dictionary<string, string> QueryParameters;
        DataServiceOperationContext OpsContext;

        public AnungooODataServiceService() : base(serviceContext, serviceContext) { }

        static IDataLayer CreateDataLayer()
        {
            DevExpress.Xpo.Metadata.XPDictionary dict = new DevExpress.Xpo.Metadata.ReflectionDictionary();
            dict.GetDataStoreSchema(typeof(AnungooODataServiceService).Assembly);
            IDataLayer dataLayer = new ThreadSafeDataLayer(dict, global::AnungooODataService.Anungoo.ConnectionHelper.GetConnectionProvider(DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists));
            XpoDefault.DataLayer = dataLayer;
            XpoDefault.Session = null;
            return dataLayer;
        }

        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            OpsContext = args.OperationContext;
            QueryParameters = GetQueryParameterBag(OpsContext);
            base.OnStartProcessingRequest(args);
        }

        // This method is called just once to initialize global service policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.AcceptAnyAllRequests = true;
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.SetServiceActionAccessRule("*", ServiceActionRights.Invoke);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.DataServiceBehavior.AcceptProjectionRequests = true;
            config.DataServiceBehavior.AcceptCountRequests = true;
            config.AnnotationsBuilder = CreateAnnotationsBuilder(() => serviceContext);
            config.DataServiceBehavior.AcceptReplaceFunctionInQuery = true;
            config.DataServiceBehavior.AcceptSpatialLiteralsInQuery = true;
            config.DisableValidationOnMetadataWrite = true;
#if DEBUG
            config.UseVerboseErrors = true;
#endif
        }

        Dictionary<string, string> GetQueryParameterBag(DataServiceOperationContext serviceOperationContext)
        {
            Dictionary<string, string> ParameterBag = null;
            string query = serviceOperationContext.AbsoluteRequestUri.Query;
            if (!string.IsNullOrEmpty(query))
            {
                ParameterBag = new Dictionary<string, string>();
                query = query.Remove(0, 1);
                string[] param = query.Split('&');

                string paramName;
                string paramValue;

                foreach (var item in param)
                {
                    //Skip OData supported parameters [see Ronald`s Comment]
                    if (!item.StartsWith("$"))
                    {
                        paramName = item.Substring(0, item.IndexOf("="));
                        paramValue = item.Substring(item.IndexOf("=") + 1);
                        ParameterBag.Add(paramName, paramValue);
                    }
                }
            }
            return ParameterBag;
        }

        // Place OData Interceptors here, one for each Entity type.
        [QueryInterceptor("Test")]
        public Expression<Func<Test, bool>> OnQuery()
        {
            return o => true;
            //VerifyEmployeesInterceptorParameters(this.QueryParameters);

           // string FirstName = QueryParameters["Test1"];
          //  return o => (o.Test1.StartsWith(FirstName));
        }

        //void VerifyEmployeesInterceptorParameters(Dictionary<string, string> QueryParams)
        //{
        //    if (QueryParams == null)
        //    {
        //        throw new ArgumentException("Expecting parameter 'Test1' but not found");
        //    }
        //}

        // This method is called on each service call and returns a token.
        public override object Authenticate(ProcessRequestArgs args)
        {
            return base.Authenticate(args);
        }

        // Place a single XPO Interceptor that here, one for all Entity types.
        public override LambdaExpression GetQueryInterceptor(Type entityType, object token)
        {
            //Example interceptor for the Employee class
            //if (token != null && entityType == typeof(Employee)){
            //    return (Expression<Func<Employee, bool>>)(o => o.UserName != "Admin");
            //}
            return base.GetQueryInterceptor(entityType, token);
        }

        // Place Service Operations here.
        //[WebGet]
        //public void OperationName(Type arg1, ...) {
        //}
    }
}
