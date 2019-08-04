using System.ComponentModel.Design;
using System.Security.Authentication.ExtendedProtection;

namespace JSON.JsonMapping.Structures
{
    public class MappingManager
    {
         EventObject _eventObject = null;

         public MappingManager(EventObject eventobject)
         {
             _eventObject = eventobject;
         }

         public string ExecuteDataMapping()
         {
             try
             {
                 var jsonCollection = _eventObject.GetJsonCollection();
                 JObject joEventMap = JObject.Parse(jsonCollection.Find(x => x.Key == "eventmap").Value.toString());
                 string orimaryobject = ((JValue) joEventMap.Selecttoken("definitions.parentobject.jsonobject"));
                 string returnobject = string.Empty;
                 _eventObject.ParentObject = primaryobject;
                 returnobject = JSONMapper.Map2Requested_(_eventObject);
                 return $"Success-MappingManager.ExecuteDataMapping: {returnobject}";
             }
             catch ()
             {

             }
         }

         public string ExecuteDataProcessing()
         {
             // parse the data payload to get info for schemas 
             
             // determine the type of event call 
             var jsonCollection = _eventObject.GetJsonCollection();

             JObject joEventMap = JObject.Parse(jsonCollection,"eventmap");
             string calltype = (joEventMap, ".event.type");
             switch (calltype)
             {
                 case "insert":
                 {
                     /// execute the services first routine
                     /// collect the services collection

                     var services = joEventMap.SelectToken("event.services");

                     foreach (var ea in services.Childres())
                     {
                         serviceinfo = new serviceinfo
                         {
                             servicename = (ea, ".servicename"),
                             serviceroute,
                             parentobject,
                             serviceargs,
                             servicetype
                         };

                         string callername = _eventObject.callername;
                         _eventObject.parentObject = serviceinfo.parentobject;
                         returnobject = JSONMapper.Map2Requested_(_eventObject);
                     }

                     break;
                 }

                 case "get":
                 {
                     break;
                 }

                 case "update":
                 {
                     break;
                 }

                 case "delete":
                 {
                     break;
                 }
             }

             return returnobject;
         }

         public enum MappingAction
         {
             ValidatePayload,
             PopulatePayload,
             UpdatePayload,
             PopulateMasterObject
             
         }

         public class ServiceInfo
         {
             public string servicename { get; set; }
             public string serviceroute { get; set; }
             public string parentobject { get; set; }
             public string serviceargs { get; set; }
             public string servicetype { get; set; }
         }

    }
}