using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JSON.JsonMapping.EventObjects
{
    public class EventObject
    {
        public List<KeyValuePair<string, string>> _jsoncollection { get; set; }

        public EventObject()
        {
            
        }

        public Object GetDateTime()
        {
            return DateTime.UtcNow;
        }

        public List<KeyValuePair<string, string>> GetJsonCollection()
        {
            return _jsoncollection;
        }

        public object GetJsonObject(string StringValue)
        {
            return JObject.Parse("{ \"jsonobject\" : \"" + StringValue + "\"}");
        }

        public string GetEventMap(EventMapType eventMapType)
        {
            var a = GetJsonObject(null);
            return File.ReadAllText(@"");
        }

        public string Publish(string jObject)
        {
            // _eventtrigger is the field calling this method
            // needs to be removed from the jschema before submitting
            try
            {
                //JToken jmember = JToken.Parse(jObject);
                //foreach (var ea in jmember.Children())
                //{
                //    if (((JProperty) ea).Name == "eventtrigger")
                //    {
                //        ea.Remove();
                //        Console.WriteLine("call the publish class" + jmember);

                //        // instantiate the class for integration with SNOW

                //        string uri = string.Empty;
                //        if ()
                //        {
                //            uri = "";
                //        }

                //        string serializedJsonObject = JsonConvert.SerializeObject(jmember);
                //        NameValueCollection nvc = new NameValueCollection();
                //        var encod = System.Text.Encoding.UTF8.GetBytes("Rest-api-item");
                //        nvc.Add("Authorization");

                //        var response = Rest.Post();
                //        jobject = response.toObject();
                //        return $"Success-EventObject.Publish: {jObject.ToString()}";
                //    }


                //}

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
            
        }

        public string getArtifactFilePath()
        {
            return $"http://";
        }

        public string getArtifactFilename()
        {
            return $"http://name";
        }

        public dynamic getApplicationServices(string tag)
        {
            return $"ApplicationServices";
        }
        
        public string CallerName { get; set; }
        public string ParentObject { get; set; }

        public enum EventMapType
        {
            CMDB_Object
        }
        
        
    }
}