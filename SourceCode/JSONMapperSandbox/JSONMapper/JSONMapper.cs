using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;
using JSONMapperSandbox.JSONMapper.CObjects;
using System.Threading.Tasks;

namespace JSONMapperSandbox.JSONMapper
{
    public static class JSONMapper
    {


        public static IEnumerable<string> WildcardsJPaths;
        private static List<KeyValuePair<string, string>> _dataPayloadKeywords = new
            List<KeyValuePair<string, string>>();
        public static void Initialize()
        {
            WildcardsJPaths = JsonFieldFinder.JsonFieldFinder.GetJPaths(10);
            /// <summary>
            /// DataPayload Types
            /// anything exclusive of the list is an actual payload for parsing
            /// </summary>
            _dataPayloadKeywords.Add(new KeyValuePair<string, string>("JObject", "Map2RequestedChild"));
            _dataPayloadKeywords.Add(new KeyValuePair<string, string>("EObject", "CustomMethod"));
            _dataPayloadKeywords.Add(new KeyValuePair<string, string>("JArray", "Map2RequestedChildren"));
        }

        public static string Map2RequestedsampleCore(object eventObject)
        {
            MethodInfo methodInfo = eventObject.GetType().GetMethod("GetJsonCollection");
            List<KeyValuePair<string, string>> jsonCollection = (List<KeyValuePair<string, string>>)methodInfo.Invoke(eventObject, BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

            PropertyInfo callername = eventObject.GetType().GetProperty("CallerName");
            if (callername == null) { return null; }
            string caller_name = callername.GetValue(eventObject, null).ToString().ToLower();

            PropertyInfo parentname = eventObject.GetType().GetProperty("ParentObject");
            if (parentname == null) { return null; }
            string parent_object = parentname.GetValue(eventObject, null).ToString().ToLower();

            /// Get Event Map
            var eventMap = JObject.Parse(jsonCollection.Find(x => x.Key == "eventmap").Value.ToString());

            /// Get the name of the parentobject.jsonobject
            var nameOfParentObject = parent_object;

            /// Get Parent Object Structure
            var listOfJsonStructures = JsonFieldFinder.JsonFieldFinder
                .FindFieldsIEnumerable("jsonstructures." + parent_object, jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths);

            /// Get the Json Structure for the Parent Object
            var fieldsOfParentObject = listOfJsonStructures.ToList().First();

            /// Get Parent Object Definitions
            var jobjectOfFieldDefinitions = JsonFieldFinder.JsonFieldFinder
                .FindFieldsJObject("definitions." + caller_name + "." + parent_object,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, nameOfParentObject);

            /// Get Parent Object Definitions
            var rawParentObject = JsonFieldFinder.JsonFieldFinder
                .FindFieldsJObject("jsonstructures." + nameOfParentObject,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, nameOfParentObject);
            (bool required, bool deleteifnull, object fieldvalue) fieldValue;
            /// Hydrate the Parent Object Structure
            /// Parallel.ForEach(myTasks, t => t.DoSomethingInBackground());
            
            foreach (var ea in fieldsOfParentObject.Children().ToArray())
            {

                fieldValue = getFieldValue(jobjectOfFieldDefinitions, nameOfParentObject +
                    "." + ((JProperty)ea).Name, jsonCollection, eventObject, fieldsOfParentObject);
                if (fieldValue.required && (fieldValue.fieldvalue == null ||
                    String.IsNullOrEmpty(fieldValue.fieldvalue.ToString())))
                {
                    ///send error that there is a null for a required field
                    string fieldName = ((JProperty)ea).Name;
                    throw new Exception($"JSONMapper: required field missing. {fieldName} ");
                }
                else if (fieldValue.deleteifnull && (fieldValue.fieldvalue == null ||
                    String.IsNullOrEmpty(fieldValue.fieldvalue.ToString())))
                {
                    ///delete the field if field value is null
                    ea.Remove();
                }
                else
                {
                    ///assign the value to the 
                    ((JProperty)ea).Value = JToken.FromObject(
                        (fieldValue.fieldvalue == null) ? string.Empty :
                        fieldValue.fieldvalue);

                }


            }

   ((JProperty)rawParentObject.First).Value = fieldsOfParentObject;

            return rawParentObject.ToString();
        }


        public static string Map2Requestedsample(object eventObject)
        {
            MethodInfo methodInfo = eventObject.GetType().GetMethod("GetJsonCollection");
           List<KeyValuePair<string, string>> jsonCollection = (List<KeyValuePair<string, string>>)methodInfo.Invoke(eventObject, BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

            PropertyInfo callername = eventObject.GetType().GetProperty("CallerName");
            if (callername == null) { return null; }
            string caller_name = callername.GetValue(eventObject, null).ToString().ToLower();

            PropertyInfo parentname = eventObject.GetType().GetProperty("ParentObject");
            if (parentname == null) { return null; }
            string parent_object = parentname.GetValue(eventObject, null).ToString().ToLower();

            /// Get Event Map
            var eventMap = JObject.Parse(jsonCollection.Find(x => x.Key == "eventmap").Value.ToString());

            /// Get the name of the parentobject.jsonobject
            var nameOfParentObject = parent_object;

            /// Get Parent Object Structure
            var listOfJsonStructures = JsonFieldFinder.JsonFieldFinder
                .FindFieldsIEnumerable("jsonstructures." + parent_object, jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths);

            /// Get the Json Structure for the Parent Object
            var fieldsOfParentObject = listOfJsonStructures.ToList().First();

            /// Get Parent Object Definitions
            var jobjectOfFieldDefinitions = JsonFieldFinder.JsonFieldFinder
                .FindFieldsJObject("definitions." + caller_name + "." + parent_object,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, nameOfParentObject);

            /// Get Parent Object Definitions
            var rawParentObject = JsonFieldFinder.JsonFieldFinder
                .FindFieldsJObject("jsonstructures." + nameOfParentObject,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, nameOfParentObject);
            (bool required, bool deleteifnull, object fieldvalue) fieldValue;
            /// Hydrate the Parent Object Structure
            foreach (var ea in fieldsOfParentObject.Children().ToArray())
            {

                fieldValue = getFieldValue(jobjectOfFieldDefinitions, nameOfParentObject +
                    "." + ((JProperty)ea).Name, jsonCollection, eventObject, fieldsOfParentObject);
                if (fieldValue.required && (fieldValue.fieldvalue == null ||
                    String.IsNullOrEmpty(fieldValue.fieldvalue.ToString())))
                {
                    ///send error that there is a null for a required field
                    string fieldName = ((JProperty)ea).Name;
                    throw new Exception($"JSONMapper: required field missing. {fieldName} ");
                }
                else if (fieldValue.deleteifnull && (fieldValue.fieldvalue == null ||
                    String.IsNullOrEmpty(fieldValue.fieldvalue.ToString())))
                {
                    ///delete the field if field value is null
                    ea.Remove();
                }
                else
                {
                    ///assign the value to the 
                    ((JProperty)ea).Value = JToken.FromObject(
                        (fieldValue.fieldvalue == null) ? string.Empty :
                        fieldValue.fieldvalue);
                                      
                }


            }

           ((JProperty)rawParentObject.First).Value = fieldsOfParentObject;

            return rawParentObject.ToString();
        }

        public static string Map2Requested(object eventObject)
        {
            MethodInfo methodInfo = eventObject.GetType().GetMethod("GetJsonCollection");
            List<KeyValuePair<string, string>> jsonCollection = (List<KeyValuePair<string, string>>)methodInfo.Invoke(eventObject, BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

            /// Get Event Map
            var eventMap = JObject.Parse(jsonCollection.Find(x => x.Key == "eventmap").Value.ToString());

            /// Get Name Parent Object
            var listOfParentObject = JsonFieldFinder.JsonFieldFinder
                .FindFieldsIEnumerable("parentobject.jsonobject", jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths);

            /// Get the name of the parentobject.jsonobject
            var nameOfParentObject = listOfParentObject.ToList().First().ToString();

            /// Get Parent Object Structure
            var listOfJsonStructures = JsonFieldFinder.JsonFieldFinder
                .FindFieldsIEnumerable("jsonstructures." + nameOfParentObject, jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths);

            /// Get the Json Structure for the Parent Object
            var fieldsOfParentObject = listOfJsonStructures.ToList().First();

            /// Get Parent Object Definitions
            var jobjectOfFieldDefinitions = JsonFieldFinder.JsonFieldFinder
                .FindFieldsJObject("definitions." + nameOfParentObject,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, nameOfParentObject);

            /// Get Parent Object Definitions
            var rawParentObject = JsonFieldFinder.JsonFieldFinder
                .FindFieldsJObject("jsonstructures." + nameOfParentObject,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, nameOfParentObject);
            (bool required, bool deleteifnull, object fieldvalue) fieldValue;
            /// Hydrate the Parent Object Structure
            foreach (var ea in fieldsOfParentObject.Children().ToArray())
            {
                fieldValue = getFieldValue(jobjectOfFieldDefinitions, nameOfParentObject +
                    "." + ((JProperty)ea).Name, jsonCollection, eventObject);
                if (fieldValue.required && (fieldValue.fieldvalue == null ||
                    String.IsNullOrEmpty(fieldValue.fieldvalue.ToString())))
                {
                    ///send error that there is a null for a required field
                    string fieldName = ((JProperty)ea).Name;
                    throw new Exception($"JSONMapper: required field missing. {fieldName} ");
                }
                else if (fieldValue.deleteifnull && (fieldValue.fieldvalue == null ||
                    String.IsNullOrEmpty(fieldValue.fieldvalue.ToString())))
                {
                    ///delete the field iff field value is null
                    ea.Remove();
                }
                else
                {
                    ///assign the value to the property
                    ((JProperty)ea).Value = JToken.FromObject(fieldValue.fieldvalue);
                }
                

            }

            ((JProperty)rawParentObject.First).Value = fieldsOfParentObject;

            return rawParentObject.ToString();
        }

        private static (bool required, bool remove, object fieldvalue) getFieldValue(
            JObject Definitions, string path, List<KeyValuePair<string, string>> jsonCollection, object eventObject, JToken populatedObject)
        {
            /// access the eventObject for instantiating the methods for expressions

            /// Parse the Field Definitions
            var datapayload = ((JValue)Definitions
                .SelectToken(path + ".datapayload")).Value.ToString();
            var parentobject = ((JValue)Definitions
                .SelectToken(path + ".parentobject")).Value.ToString();
            var filterexpression = ((JValue)Definitions
                .SelectToken(path + ".filterexpression")).Value.ToString();
            var required = ((JValue)Definitions
                .SelectToken(path + ".required")).Value.ToString();
            var deleteifnull = ((JValue)Definitions
                .SelectToken(path + ".deleteifnull")).Value.ToString();
            var tag = ((JValue)Definitions
                .SelectToken(path + ".tag")).Value.ToString();

            object returnObject = null;
            /// parse datapayload based on a keyword
            if (_dataPayloadKeywords.Any(x => x.Key == datapayload))
            {
                KeyValuePair<string, string> dataPayloadType = _dataPayloadKeywords.FirstOrDefault(x => x.Key == datapayload);
                switch (dataPayloadType.Key)
                {
                    case "JObject": /// another JsonObject Structure
                        {
                            returnObject = Map2RequestedChild(eventObject, parentobject, jsonCollection);
                            return (Convert.ToBoolean(required), Convert.ToBoolean(deleteifnull), returnObject);
                        }
                    case "EObject": /// calling a method of the EventObject
                        {
                            /// filter expression contains eventobject method name      
                            MethodInfo methodInfo = eventObject.GetType().GetMethod(filterexpression);
                            /// get the parent trigger
                            if (parentobject == "ExitObjectMapping")
                            {
                                tag = populatedObject.ToString              (Newtonsoft.Json.Formatting.None);
                            }

                            /// tag contains the arguments for the method
                            returnObject = methodInfo.Invoke(eventObject, (String.IsNullOrEmpty(tag)) ?
                                null : new object[] { tag });
                            return (Convert.ToBoolean(required), Convert.ToBoolean(deleteifnull), returnObject);

                        }
                    case "JArray": /// another JsonObject Structure
                        {
                            DefinitionsObject sourceDefinitions = new DefinitionsObject
                            {
                                datapayload = datapayload,
                                parentobject = parentobject,
                                filterexpression = filterexpression,
                                required = required,
                                deleteifnull = deleteifnull,
                                tag = tag
                            };

                            returnObject = Map2RequestedChildren(eventObject, sourceDefinitions, jsonCollection);
                            return (Convert.ToBoolean(required), Convert.ToBoolean(deleteifnull), returnObject);
                        }
                    default:
                        {
                            ///send error that there is a null for a required field
                            throw new Exception($"JSONMapper: Unknown Custom Object Keyword.");
                        }
                }

            }
            else
            {
                /// Apply Field Definitions
                try
                {
                    JToken rawParentToken = JObject.Parse(jsonCollection.FirstOrDefault(x => x.Key == datapayload)
                        .Value.ToString()).SelectToken(parentobject + "." + filterexpression);
                    //if (rawParentToken == null)
                    //{
                    //    JObject rawParentObject = JsonFieldFinder.JsonFieldFinder
                    //       .FindFieldsJObject((!String.IsNullOrEmpty(parentobject) ?
                    //       parentobject + "." + filterexpression : filterexpression),
                    //       jsonCollection.Find(x => x.Key ==
                    //       datapayload).Value,
                    //       WildcardsJPaths, filterexpression);
                    //    /// test the rawParentObject for a value
                    //    returnObject = (rawParentObject.First != null) ?
                    //        ((JProperty)rawParentObject.First).Value : null;
                    //}
                    //else
                    //{
                        /// test the rawParentObject for a value
                        returnObject = (rawParentToken != null) ?
                            ((JValue)rawParentToken).Value : null;

                    //}
                }
                catch
                {
                    returnObject = null;
                }

                return (Convert.ToBoolean(required), Convert.ToBoolean(deleteifnull),
                    returnObject);
            } 
        }
        private static (bool required, bool remove, object fieldvalue) getFieldValue(
            JObject Definitions, string path, List<KeyValuePair<string, string>> jsonCollection, object eventObject)
        {
            /// access the eventObject for instantiating the methods for expressions
            /// Parse the Field Definitions
            var datapayload = ((JValue)Definitions
                .SelectToken(path + ".datapayload")).Value.ToString();
            var parentobject = ((JValue)Definitions
                .SelectToken(path + ".parentobject")).Value.ToString();
            var filterexpression = ((JValue)Definitions
                .SelectToken(path + ".filterexpression")).Value.ToString();
            var required = ((JValue)Definitions
                .SelectToken(path + ".required")).Value.ToString();
            var deleteifnull = ((JValue)Definitions
                .SelectToken(path + ".deleteifnull")).Value.ToString();
            var tag = ((JValue)Definitions
                .SelectToken(path + ".tag")).Value.ToString();

            object returnObject = null;
            /// parse datapayload based on a keyword
            if (_dataPayloadKeywords.Any(x => x.Key == datapayload))
            {
                KeyValuePair<string, string> dataPayloadType = _dataPayloadKeywords.FirstOrDefault(x => x.Key == datapayload);
                switch (dataPayloadType.Key)
                {
                    case "JObject": /// another JsonObject Structure
                        {
                            returnObject = Map2RequestedChild(eventObject, parentobject, jsonCollection);
                            return (Convert.ToBoolean(required), Convert.ToBoolean(deleteifnull), returnObject);
                        }
                    case "EObject": /// calling a method of the EventObject
                        {
                            /// filter expression contains eventobject method name      
                            MethodInfo methodInfo = eventObject.GetType().GetMethod(filterexpression);
                            /// tag contains the arguments for the method
                            returnObject = methodInfo.Invoke(eventObject, (String.IsNullOrEmpty(tag)) ?
                                null : new object[] { tag });
                            return (Convert.ToBoolean(required), Convert.ToBoolean(deleteifnull), returnObject);

                        }
                    case "JArray": /// another JsonObject Structure
                        {
                            DefinitionsObject sourceDefinitions = new DefinitionsObject
                            {
                                datapayload = datapayload,
                                parentobject = parentobject,
                                filterexpression = filterexpression,
                                required = required,
                                deleteifnull = deleteifnull,
                                tag = tag
                            };

                            returnObject = Map2RequestedChildren(eventObject, sourceDefinitions, jsonCollection);
                            return (Convert.ToBoolean(required), Convert.ToBoolean(deleteifnull), returnObject);
                        }
                    default:
                        {
                            ///send error that there is a null for a required field
                            throw new Exception($"JSONMapper: Unknown Custom Object Keyword.");
                        }
                }

            }
            else
            {
                /// Apply Field Definitions
                try
                {
                    JToken rawParentToken = JObject.Parse(jsonCollection.FirstOrDefault(x => x.Key == datapayload)
                        .Value.ToString()).SelectToken(parentobject + "." + filterexpression);
                    //if (rawParentToken == null)
                    //{
                    //    JObject rawParentObject = JsonFieldFinder.JsonFieldFinder
                    //       .FindFieldsJObject((!String.IsNullOrEmpty(parentobject) ?
                    //       parentobject + "." + filterexpression : filterexpression),
                    //       jsonCollection.Find(x => x.Key ==
                    //       datapayload).Value,
                    //       WildcardsJPaths, filterexpression);
                    //    /// test the rawParentObject for a value
                    //    returnObject = (rawParentObject.First != null) ?
                    //        ((JProperty)rawParentObject.First).Value : null;
                    //}
                    //else
                    //{
                    /// test the rawParentObject for a value
                    returnObject = (rawParentToken != null) ?
                        ((JValue)rawParentToken).Value : null;

                    //}
                }
                catch
                {
                    returnObject = null;
                }

                return (Convert.ToBoolean(required), Convert.ToBoolean(deleteifnull),
                    returnObject);
            }


        }
        /// <summary>
        /// populating a child json object
        /// </summary>
        /// <param name="eventObject"></param>
        /// <param name="objectName"></param>
        /// <param name="jsonCollection"></param>
        /// <returns></returns>
        private static object Map2RequestedChild(object eventObject, string objectName, List<KeyValuePair<string, string>> jsonCollection)
        {
            /// Get Event Map
            var eventMap = JObject.Parse(jsonCollection.Find(x => x.Key == "eventmap").Value.ToString());

            /// Get Parent Object Structure
            var listOfJsonStructures = JsonFieldFinder.JsonFieldFinder
                .FindFieldsIEnumerable("jsonstructures." + objectName,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths);

            /// Get the Json Structure for the Parent Object
            var fieldsOfParentObject = listOfJsonStructures.ToList().First();

            /// Get Parent Object Definitions
            var jobjectOfFieldDefinitions = JsonFieldFinder.JsonFieldFinder
                .FindFieldsJObject("definitions." + objectName,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, objectName);

            /// Get Parent Object Definitions
            var rawParentObject = JsonFieldFinder.JsonFieldFinder
                .FindFieldsJObject("jsonstructures." + objectName,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, objectName);

            (bool required, bool deleteifnull, object fieldvalue) fieldValue;
            /// Hydrate the Parent Object Structure
            foreach (var ea in fieldsOfParentObject.Children().ToArray())
            {

                fieldValue = getFieldValue(jobjectOfFieldDefinitions, objectName +
                    "." + ((JProperty)ea).Name, jsonCollection, eventObject);
                if (fieldValue.required && (fieldValue.fieldvalue == null ||
                    String.IsNullOrEmpty(fieldValue.fieldvalue.ToString())))
                {
                    ///send error that there is a null for a required field
                    string fieldName = ((JProperty)ea).Name;
                    throw new Exception($"JSONMapper: required field missing. {fieldName} ");
                }
                else if (fieldValue.deleteifnull && (fieldValue.fieldvalue == null ||
                    String.IsNullOrEmpty(fieldValue.fieldvalue.ToString())))
                {
                    ///delete the field iff field value is null
                    ea.Remove();
                }
                else
                {
                    ///assign the value to the property
                    if(fieldValue.fieldvalue != null)
                    {
                        ((JProperty)ea).Value = JToken.FromObject(fieldValue.fieldvalue);
                    }
                }
            }

            ((JProperty)rawParentObject.First).Value = fieldsOfParentObject;

            return ((object)((JProperty)rawParentObject.First).Value);
        }
        /// <summary>
        /// populating an array of objects
        /// </summary>
        /// <param name="eventObject"></param>
        /// <param name="objectSource"></param>
        /// <param name="jsonCollection"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        private static object Map2RequestedChildren(object eventObject, DefinitionsObject sourceDefinitions, List<KeyValuePair<string, string>> jsonCollection)
        {
            /// Get Event Map
            var eventMap = JObject.Parse(jsonCollection.Find(x => x.Key == "eventmap").Value.ToString());

            List<KeyValuePair<string, string>> tagList = new List<KeyValuePair<string, string>>();

            /// parse the source definitions for data payload and jstructure

            JObject jobjectTag = JObject.Parse(sourceDefinitions.tag);
            /// collect the data payload objects for consumption
            string sourcePayload = jobjectTag.SelectToken("datapayload").ToString();
            /// structure to populate
            string jstructure = jobjectTag.SelectToken("jstructure").ToString();

            /// Get Parent Object Structures from DataPayload
            var listOfJsonObjectsFromPayload = JsonFieldFinder.JsonFieldFinder
                .FindFieldsIEnumerable(sourceDefinitions.parentobject,
                jsonCollection.Find(x => x.Key == sourcePayload).Value.ToString(), WildcardsJPaths);

            /// Get JStructure Definitions
            var jstructureDefinitions = JsonFieldFinder.JsonFieldFinder
                .FindFieldsJObject("definitions." + jstructure,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, jstructure);

            (bool required, bool deleteifnull, object fieldvalue) fieldValue;

            /// There is an inversion of the driver for objects
            /// in this case the collection queried from the datapayload 
            /// will drive the population of the json object structures.
            /// start a parent loop of each 

            /// will return an array
            JArray returnObject = new JArray();
            //store the internal payload for querying the field value
            List<KeyValuePair<string, string>> datapayloads = new List<KeyValuePair<string, string>>();
            /// Hydrate the JStructure
            foreach (var ea in listOfJsonObjectsFromPayload.Children().ToArray())
            {
                /// Get JStructure Object
                var rawJstructure = JsonFieldFinder.JsonFieldFinder
                    .FindFieldsIEnumerable("jsonstructures." + jstructure,
                    jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths);

                /// establish an instance of the jstructure 
                foreach (var jstruct in rawJstructure.Children().ToArray())
                {
                    // get the value for the field
                    datapayloads.Add(new KeyValuePair<string, string>("internal", ea.ToString(Newtonsoft.Json.Formatting.None)));

                    fieldValue = getFieldValue(jstructureDefinitions,
                       jstructure + "." + ((JProperty)jstruct).Name.ToString(), datapayloads, eventObject);

                    if (fieldValue.required && (fieldValue.fieldvalue == null ||
                        String.IsNullOrEmpty(fieldValue.fieldvalue.ToString())))
                    {
                        ///send error that there is a null for a required field
                        string fieldName = ((JProperty)jstruct).Name;
                        throw new Exception($"JSONMapper: required field missing. {fieldName} ");
                    }
                    else if (fieldValue.deleteifnull && (fieldValue.fieldvalue == null ||
                        String.IsNullOrEmpty(fieldValue.fieldvalue.ToString())))
                    {
                        ///delete the field iff field value is null
                        jstruct.Remove();
                    }
                    else
                    {
                        ///assign the value to the property
                        ((JProperty)jstruct).Value = ((fieldValue.fieldvalue).GetType().Name.ToString() == "JValue") ? fieldValue.fieldvalue.ToString() : JToken.FromObject(fieldValue.fieldvalue);
                    }
                }

                returnObject.Add(rawJstructure);
                rawJstructure = null;
                datapayloads.Clear();

            }

            return (object)returnObject;

        }
        public class DefinitionsObject
        {
            public string datapayload {get; set; }
            public string parentobject { get; set; }
            public string filterexpression { get; set; }
            public string required { get; set; }
            public string deleteifnull { get; set; }
            public string tag { get; set; }
        }
    }
}
