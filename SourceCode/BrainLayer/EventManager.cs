using System;
using System.Collections.Generic;
using System.Text;
using JSONMapperSandbox.JSONMapper;
using System.IO;
using Newtonsoft.Json.Linq;
using JSONMapperSandbox.JSONMapper.CObjects;

namespace BrainLayer
{
    public class EventManager
    {
        ///caller name
        ///event/action
        ///json structures

        ///based on the 3 parameters above, 
        ///the structures, maps and payloads 
        ///can be collected

        string _dataPayload = string.Empty;
        public EventManager(string DataPayload)
        {
            _dataPayload = DataPayload;
        }

        public string ProcessEvent()
        {
            JObject EventDetails = JObject.Parse(_dataPayload);
            /// Parse the initial event payload
            var event_type = ((JValue)EventDetails
                .SelectToken("event_type")).Value.ToString();
            var scope = ((JValue)EventDetails
                .SelectToken("scope")).Value.ToString();
            var client = ((JValue)EventDetails
                .SelectToken("caller.client_id")).Value.ToString().ToLower();
            var message_object = ((JValue)EventDetails
                .SelectToken("message.object")).Value.ToString().ToString();
            var message_subject = ((JValue)EventDetails
                .SelectToken("message.subject")).Value.ToString().ToString();
            var message_action = ((JValue)EventDetails
                .SelectToken("message.action")).Value.ToString().ToString();

            /// query for the event map
            string servicePayload = File.ReadAllText(@".\JSONMapper\EventMaps\insertMember.json");

            List<KeyValuePair<string, string>> jsonCollection = new List<KeyValuePair<string, string>>();

            jsonCollection.Add(new KeyValuePair<string, string>("eventmap", _dataPayload));

            EventObject eventObject = new EventObject();
            eventObject._jsonCollection = jsonCollection;

            return "";
        }
        


        ///Call the EventMap
        /// GET
        ///Map the JsonPayload to sampleObject
        ///EventMap will include the JSchema and field definitions 
        ///Call JSONMapper to complete Mapping
        ///Validate the Hydrated JObject for Required Fields (success/failure)
        ///Map the sample Object to a Business Object for manipulation
        ///Execute business logic return Business Object 
        ///map the Business Object sample Objects for submission to 
        ///Call Services Logic
        ///
        /// INSERT
        ///Map the JsonPayload to sampleObject
        ///EventMap will include the JSchema and field definitions 
        ///Call JSONMapper to complete Mapping
        ///Validate the Hydrated JObject for Required Fields (success/failure)
        ///Map the sample Object to a Business Object for manipulation
        ///Execute business logic return Business Object 
        ///map the Business Object sample Objects for submission to 
        ///Call Services Logic
        ///
        /// UPDATE
        ///Map the JsonPayload to sampleObject
        ///EventMap will include the JSchema and field definitions 
        ///Call JSONMapper to complete Mapping
        ///Validate the Hydrated JObject for Required Fields (success/failure)
        ///Map the sample Object to a Business Object for manipulation
        ///Execute business logic return Business Object 
        ///map the Business Object sample Objects for submission to 
        ///Call Services Logic
        ///
        /// DELETE
        ///Map the JsonPayload to sampleObject
        ///EventMap will include the JSchema and field definitions 
        ///Call JSONMapper to complete Mapping
        ///Validate the Hydrated JObject for Required Fields (success/failure)
        ///Map the sample Object to a Business Object for manipulation
        ///Execute business logic return Business Object 
        ///map the Business Object sample Objects for submission to 
        ///Call Services Logic
        ///


    }
}
