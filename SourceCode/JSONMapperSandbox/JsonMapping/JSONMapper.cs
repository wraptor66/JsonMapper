using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JSON.JsonMapping
{
    public class JSONMapper
    {

        public static IEnumerable<string> WildcardsJPaths;

        private static List<KeyValuePair<string, string>> _dataPayloadKeywords =
            new List<KeyValuePair<string, string>>();

        public static void Initialize()
        {
            WildcardsJPaths = JsonMapping.JsonFieldFinder.GetJPaths(10);
            /// DataPayloads Types
            /// anything exclusive of the list is an actual payload for parsing

            _dataPayloadKeywords.Add(new KeyValuePair<string, string>("JObject", "Map2RequestedChild"));
            _dataPayloadKeywords.Add(new KeyValuePair<string, string>("EObject", "CustomMethod"));
            _dataPayloadKeywords.Add(new KeyValuePair<string, string>("JArray", "Map2RequestedChildren"));

        }

        public static string Map2Requested_Core(object eventObject)
        {
            MethodInfo methodInfo = eventObject.GetType().GetMethod("GetJsonCollection");
            List<KeyValuePair<string, string>> jsonCollection =
                (List<KeyValuePair<string, string>>) methodInfo.Invoke(eventObject,
                    BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

            PropertyInfo callername = eventObject.GetType().GetProperty("CallerName");
            if (callername == null)
            {
                return null;
            }

            string caller_name = callername.GetValue(eventObject, null).ToString().ToLower();

            // extract parentobj similarly

            // Get Event Map
            var eventMap = JObject.Parse(jsonCollection.Find(x => x.Key == "eventmap").Value.ToString());

            // Get Parent object Structure

            var listofJsonStructures = JsonMapping.JsonFieldFinder.findFieldsIEnumerable(
                "jsonstructures." + parent_object, jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(),
                WildcardsJPaths);

            // Get the Json structure for the parent object
            var fieldsofPrentObject = listofJsonStructures.toList().First();

            // Get PArent Object Definitions
            var jobjectOfFieldDefinitions = JsonMapping.JsonFieldFinder.FindFieldsJObject(
                "definitions." + caller_name + parent_object,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, nameofParentObject);

            // Get PArent Object Definitions
            var rawParentObject = JsonMapping.JsonFieldFinder.FindFieldsJObject(
                "jsonstructures." + caller_name + parent_object,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, nameofParentObject);
            (bool required, bool deleteifnull, object fieldvalue) fieldvalue;

            foreach (var ea in fieldsofPrentObject.Children().ToArray())
            {
                fieldValue =
                    getFieldValue(jobjectOfFieldDefinitions, nameofParentObject + (JProperty) ea)
                        .Name, jsonCollection, eventObject,fieldsofPrentObject,null,false);
                if (fieldValue.required && (fieldValue.fieldValue == null ||
                                            String.IsNullOrEmpty(fieldvalue.fieldvalue.ToString())))
                {
                    // send error that there is null for required field
                }
                else if ()
                {
                    // delete the field if field value is null
                }
                else
                {
                    // assign the value 
                    ((JProperty) ea).Value = JToke.FromObject(
                        (fieldValue.fieldValue == null) ? string.Empty : fieldvalue.fieldvalue);
                }
            }

            ((JProperty) rawParentObject.First).Value = fieldsofPrentObject;

            return rawParentObject.Tostring();
        }

        public static string Map2Requested_(object eventObject)

        {

            MethodInfo methodInfo = eventObject.GetType().GetMethod("GetJsonCollection");
            List<KeyValuePair<string, string>> jsonCollection =
                (List<KeyValuePair<string, string>>) methodInfo.Invoke(eventObject,
                    BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

            PropertyInfo callername = eventObject.GetType().GetProperty("CallerName");
            if (callername == null)
            {
                return null;
            }

            string caller_name = callername.GetValue(eventObject, null).ToString().ToLower();

            // extract parentobj similarly

            // Get Event Map
            var eventMap = JObject.Parse(jsonCollection.Find(x => x.Key == "eventmap").Value.ToString());

            // Get Parent object Structure

            var listofJsonStructures = JsonMapping.JsonFieldFinder.findFieldsIEnumerable(
                "jsonstructures." + parent_object, jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(),
                WildcardsJPaths);

            // Get the Json structure for the parent object
            var fieldsofPrentObject = listofJsonStructures.toList().First();

            // Get PArent Object Definitions
            var jobjectOfFieldDefinitions = JsonMapping.JsonFieldFinder.FindFieldsJObject(
                "definitions." + caller_name + parent_object,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, nameofParentObject);

            // Get PArent Object Definitions
            var rawParentObject = JsonMapping.JsonFieldFinder.FindFieldsJObject(
                "jsonstructures." + caller_name + parent_object,
                jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, nameofParentObject);
            (bool required, bool deleteifnull, object fieldvalue) fieldvalue;

            foreach (var ea in fieldsofPrentObject.Children().ToArray())
            {
                fieldValue =
                    getFieldValue(jobjectOfFieldDefinitions, nameofParentObject + (JProperty) ea)
                        .Name, jsonCollection, eventObject,fieldsofPrentObject,null,false);
                if (fieldValue.required && (fieldValue.fieldValue == null ||
                                            String.IsNullOrEmpty(fieldvalue.fieldvalue.ToString())))
                {
                    // send error that there is null for required field
                }
                else if ()
                {
                    // delete the field if field value is null
                }
                else
                {
                    // assign the value 
                    ((JProperty) ea).Value = JToke.FromObject(
                        (fieldValue.fieldValue == null) ? string.Empty : fieldvalue.fieldvalue);
                }
            }

            ((JProperty) rawParentObject.First).Value = fieldsofPrentObject;

            return $"success-JSONMapper.Map2Requested_:Handled | {rawParentObject.ToString()}";
        }
    }

    public static string Map2Requested(object eventObject)
    {

    MethodInfo methodInfo = eventObject.GetType().GetMethod("GetJsonCollection");

    List<KeyValuePair<string, string>> jsonCollection =
        (List<KeyValuePair<string, string>>) methodInfo.Invoke(eventObject,
            BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

    PropertyInfo callername = eventObject.GetType().GetProperty("CallerName");
        if (callername == null)
    {
        return null;
    }

    string caller_name = callername.GetValue(eventObject, null).ToString().ToLower();

    // extract parentobj similarly

    // Get Event Map
    var eventMap = JObject.Parse(jsonCollection.Find(x => x.Key == "eventmap").Value.ToString());

    // Get Parent object Structure

    var listofJsonStructures = JsonMapping.JsonFieldFinder.findFieldsIEnumerable(
        "jsonstructures." + parent_object, jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(),
        WildcardsJPaths);

    // Get the Json structure for the parent object
    var fieldsofPrentObject = listofJsonStructures.toList().First();

    // Get PArent Object Definitions
    var jobjectOfFieldDefinitions = JsonMapping.JsonFieldFinder.FindFieldsJObject(
        "definitions." + caller_name + parent_object,
        jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, nameofParentObject);

    // Get PArent Object Definitions
    var rawParentObject = JsonMapping.JsonFieldFinder.FindFieldsJObject(
        "jsonstructures." + caller_name + parent_object,
        jsonCollection.Find(x => x.Key == "eventmap").Value.ToString(), WildcardsJPaths, nameofParentObject);

    (bool required, bool deleteifnull, object fieldvalue) fieldvalue;

        foreach (var ea in fieldsofPrentObject.Children().ToArray())
    {
        fieldValue =
            getFieldValue(jobjectOfFieldDefinitions, nameofParentObject + (JProperty) ea)
                .Name, jsonCollection, eventObject,fieldsofParentObject,null,false);
        if (fieldValue.required && (fieldValue.fieldValue == null ||
                                    String.IsNullOrEmpty(fieldvalue.fieldvalue.ToString())))
        {
            // send error that there is null for required field
        }
        else if ()
        {
            // delete the field if field value is null
        }
        else
        {
            // assign the value 
            ((JProperty) ea).Value = JToke.FromObject(
                (fieldValue.fieldValue == null) ? string.Empty : fieldvalue.fieldvalue);
        }
    }

    ((JProperty) rawParentObject.First).Value = fieldsofPrentObject;

    return rawParentObject.ToString();
    }
    
    private static (bool required, bool remove, object fieldvalue) getFieldValue(
    JObject Definitions, string path, List<KeyValuePair<string,string>> jsonCollection, object eventObject,
    JToken populatedObject, JObject ea, bool ChildrenOf)
    {

    /// access the eventObject for instantiating the methods for expressions
    /// Parse the Field Definitions
    var datapayload = ((JValue) Definitions.SelectToken(path).value.toString());

    var parentobject = ((JValue) Definitions.SelectToken(path).value.toString());
    var filterexpression = ((JValue) Definitions.SelectToken(path).value.toString());
    var required = ((JValue) Definitions.SelectToken(path).value.toString());
    var deleteifnull = ((JValue) Definitions.SelectToken(path).value.toString());

    var tag = ((JValue) Definitions.SelectToken(path).value.toString());

        /// parse datapayload based on a keyword
        if(_dataPayloadKeywords.Any(x=> x.key == datapayload))
    {
        KeyValuePair<string, string> dataPayloadType = _dataPayloadKeywords.FirstOrDefault(x => x.key == datapayload);
        switch (dataPaloadType.Key)
        {
            case "JObject": // another JObject structure
            {
                returnObject = Map2RequestedChild(eventObject, parentobject, jsonCollection);
                return (Convert.ToBoolean(required), Convert.ToBoolean(deleteifnull), returnObject);
            }

            case "EObject": // calling a method of the EventObject
            {
                // filter expression contains eventobject method name 
                MethodInfo methodInfo = eventObject.GetType().GetMethod(filterexpression);
                if (parentobject == "exitobjectMapping")
                {
                    tag = populatedObject.ToString(Newtonsoft.Json.Formatting.None);
                }

                // tag contains the arguments for the method
                returnObject = methodInfo.Invoke(eventObject, (String.IsNullOrEmpty(tag)) ? null : new object[] {tag});
                return (Convert.ToBoolean(required), Convert.ToBoolean(deleteifnull), returnObject);
            }

            case "JArray": // another JsonObject Structure
            {

                DefinitionsObject sourceDefinitions = new DefinitionsObject
                {
                    dataPayload = datapayload,
                    parentobject = parentobject,
                    filterexpression = filterexpression,
                    required = required,
                    deleteifnull = deleteifnull,
                    tag = tag
                };

                returnObject = (ChildrenOf)
                    ? Map2RequestedChildrenOf(eventObject, sourceDefinitions, jsonCollection, ChildrenOf, ea)
                    : Map2RequestedChildrenOf(eventObject, sourceDefinitions, jsonCollection, ChildrenOf);
                return (Convert.ToBoolean(required), Convert.ToBoolean(deleteifnull), returnObject);

            }

            default:
            {
                // send error that there is a null for a required field
            }
        }
    }
    else
    {
        // Apply field definitions
        try
        {
            JToken rawParentToken = (ChildrenOf) ? ea.SelectToken(parentobject + filterexpression);
            returnObject = (rawParentToken != null) ? ((JValue) rawParentToken).Value : null;
        }
        catch ()
        {

        }

        return (Convert.ToBoolean(required), Convert.ToBoolean(deleteifnull), returnObject);
    }
    }
    

}