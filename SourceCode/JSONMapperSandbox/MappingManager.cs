using System;
using System.Collections.Generic;
using System.Text;
using JSONMapperSandbox;
using JSONMapperSandbox.JSONMapper;
using System.IO;
using Newtonsoft.Json.Linq;
using JSONMapperSandbox.JSONMapper.CObjects;

namespace JSONMapperSandbox
{
    /// <summary>
    /// This Class should provide to wrapper of the JSONMapperfunctionality. It should
    /// also provide interface for instructions as to what type of mapping that should be 
    /// performed.
    /// </summary>
    public class MappingManager
    {
        EventObject _eventObject = null;
        public MappingManager(EventObject eventObject)
        {
            _eventObject = eventObject;
        }

        public string ExecuteDataMapping()
        {
            Console.WriteLine("Starting ExecuteDataMapping()");

            var jsonCollection = _eventObject.GetJsonCollection();

            JObject joEventMap = JObject.Parse(jsonCollection
                .Find(x => x.Key == "eventmap").Value.ToString());
            string primaryobject = ((JValue)joEventMap
                .SelectToken("event.primaryobject")).Value.ToString();
            string returnobject = string.Empty;
            _eventObject.ParentObject = primaryobject;
            returnobject = JSONMapper.JSONMapper
                .Map2Requested(_eventObject);
            Console.WriteLine("Finished ExecuteDataMapping()");
            return returnobject;
        }
        public string  ExecuteDataProcessing()
        {
            /// parse the data payload to get info for schemas
            Console.WriteLine("Starting ExecuteDataProcessing()!");

            ///determine the type of event call
            var jsonCollection = _eventObject.GetJsonCollection();
            /// the code below is to find the event.type anywhere in the event map
            //var calltype = Newtonsoft.Json.JsonConvert.DeserializeObject(JsonFieldFinder.JsonFieldFinder
            //    .FindFieldsJObject("event.type",
            //    jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), JSONMapper.JSONMapper. WildcardsJPaths,"").GetValue("").ToString(Newtonsoft.Json.Formatting.None));
            JObject joEventMap = JObject.Parse(jsonCollection
                .Find(x => x.Key == "eventmap").Value.ToString());
            string calltype = ((JValue)joEventMap
                .SelectToken(".event.type")).Value.ToString();
            string returnobject = string.Empty;
            switch (calltype)
            {
                case "insert":
                    {
                        /// execute the services first routine
                        /// collect the services collection
                        var services = joEventMap
                            .SelectTokens(".event.services");
                        /// assign the parent object from the service jobject
                        ServiceInfo serviceInfo;
                        foreach (var ea in services.Children())
                        {
                            serviceInfo = new ServiceInfo
                            {
                                 servicename = ((JValue)ea
                                    .SelectToken(".servicename")).Value.ToString(),
                                 serviceroute = ((JValue)ea
                                        .SelectToken(".serviceroute")).Value.ToString(),
                                 parentobject = ((JValue)ea
                                        .SelectToken(".parentobject")).Value.ToString(),
                                 serviceargs = ((JValue)ea
                                        .SelectToken(".serviceargs")).Value.ToString(),
                                 servicetype = ((JValue)ea
                                        .SelectToken(".servicetype")).Value.ToString()
                            };

                            /// call the jsonmapper to map the parent object for insert
                            ///callername defines the definitions.callername object definitions
                            string caller_name = _eventObject.CallerName;
                            _eventObject.ParentObject = serviceInfo.parentobject;
                            returnobject = JSONMapper.JSONMapper
                                .Map2Requestedsample(_eventObject);
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
            //Console.WriteLine(returnobject);
            return returnobject;
        }

        public enum MappingAction
        {
            ValidatePayload, /// does payload has values for all the required fields
            PopulatePayload, /// traditional query data payloads and assign
            UpdatePayload, /// update exact payload with new values
            PopulateMasterObject /// assign data payoad values to Master Object
        }

        private class ServiceInfo
        {
            public string servicename { get; set; } //name for referencing within the jsoncollection
            public string serviceroute { get; set; }//the route is the equivalent of method call
            public string parentobject { get; set; }//json string for arguments (must be serialized)
            public string serviceargs { get; set; }//json object for deserializing to crud requirements
            public string servicetype { get; set; }//since the calls to crud will be methods this may be deprecated
        }
    }
}
