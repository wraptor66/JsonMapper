using System.Collections.Generic;

namespace JSON.JsonMapping
{
    public class EventManager
    {
        /// caller name
        /// event/action
        /// json structures
        ///
        /// based on the 3 parameters above,
        /// the sturctures, maps and payloads
        /// can be collected
        private string _dataPayload = string.Empty;

        public EventManager(string DataPayload)
        {
            __dataPayload = DataPayload;
        }

        public string ProcessMapping(List<KeyValuePair<string, string>> payloads, string servicename,
            string _environment)
        {
            try
            {
                payloads.Add(new KeyValuePair<string, string>("eventpayload",_dataPayload));
                
                EventObject eventObject = new EventObject();
                eventObject.servicename = servicename;
                eventObject._jsoncollection = payloads;
                MappingManager mm = new MappingManager(eventObject);
                string returnobject = mm.ExecuteDataMapping();
                StringBuilder sb = new StringBuilder();
                if (returnobject.Substring(0, 5) == "Error")
                {
                    sb.AppendLine();
                    sb.AppendLine(returnobject);
                    return sb.ToString();
                }
            }
        }
        
        /// Call the EventMap
        /// GET
        /// Map the JsonPayload to _Object
        /// EventMap will include the JSchema and field definitions
        /// Call JsonMapper to complete Mapping
        /// validate the Hydrated JObject for required fields
        /// Map the _Object to a business object for manipulation
        /// execute business logic return Business Object
        /// map the business object _Objects for submission to
        /// call services logic
        ///
        
        
    }
}