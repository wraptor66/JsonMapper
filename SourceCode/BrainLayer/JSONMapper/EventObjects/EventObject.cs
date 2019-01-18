using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Linq;
using System.Threading.Tasks;
using System.IO;

using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Util;

using JSONMapperSandbox;
using JSONMapperSandbox.JSONMapper;
using JSONMapperSandbox.JSONMapper.CObjects;
using System.Diagnostics;

namespace JSONMapperSandbox.JSONMapper.CObjects
{
    public class EventObject
    {
        public List<KeyValuePair<string, string>> _jsonCollection { get; set; }
        public EventObject()
        {
        }
        public object GetDateTime()
        {
            return DateTime.UtcNow;
        }

        public List<KeyValuePair<string, string>> GetJsonCollection()
        {
            return _jsonCollection;
        }
        public object GetJsonObject(string StringValue)
        {
            return JObject.Parse("{ \"jsonobject\" : \"" + StringValue + "\"}");
        }

        public string InsertMember(string Member)
        {
            /// call the crud
            /// sample_eventtrigger is the field calling this method
            /// needs to be removed from the jschema before submitting
            
            JToken jmember = JToken.Parse(Member);
            foreach (var ea in jmember.Children())
            {
                if(((JProperty)ea).Name == "sample_eventtrigger")
                {
                    ea.Remove();
                    Console.WriteLine("call the crud: " + jmember);
                    var member = new MemberTable
                    {
                        MemberID = 0, /// snowflake id
                        Attributes = jmember.ToString(Formatting.None),// cast to string
                        InsertUser = 304, /// example with the system - 
                        /// if the user needs to be dynamic it can be added to the jsoncollection  
                        /// as payload and then parsed in the method.
                        InsertDate = DateTime.Now
                    };
                    /// call the crud method to insert new member
                    //PutPayload("json-mapper-poc", "insertMember.json", jmember.ToString(Formatting.None));


                    //cal the crud with the membersubscriotion
                    //is good then call process
                    //
                    break;
                }
            }

            return null;
        }

        public async Task<string> GetEventMap(string BucketName, string FileKey)
        {
            IAmazonS3 S3Client = new AmazonS3Client("AKIAIAYIN7WKAGAFBTDA", "zxa+XMi4ORN8sk5cXsqEozq2Q1Wcwac1xOPrkwyB", "USEast1");
                var response = await S3Client.GetObjectAsync(BucketName, FileKey);

            JSONMapper.Initialize();
            Console.WriteLine("Starting!");
            Stopwatch stopWatch = Stopwatch.StartNew();
            stopWatch.Start();
            List<KeyValuePair<string, string>> jsonCollection = new List<KeyValuePair<string, string>>();
            string eventPayload = string.Empty;

            using (var stream = response.ResponseStream)
            {
                TextReader tr = new StreamReader(stream);
                eventPayload = tr.ReadToEnd();
                Console.WriteLine("Handler: Got Data from Key");
            }

            return eventPayload;
        }

        public void PutPayload(string BucketName, string FileKey, string JsonPayload)
        {
            //IAmazonS3 S3Client = new AmazonS3Client("AKIAIAYIN7WKAGAFBTDA", "zxa+XMi4ORN8sk5cXsqEozq2Q1Wcwac1xOPrkwyB", "USEast1");
            //using (var memoryStream = new MemoryStream())
            //{
            //    using (var jsondocument = new StreamWriter(JsonPayload))
            //    {
            //        Amazon.S3.Model.PutObjectRequest pr = new Amazon.S3.Model.PutObjectRequest();
            //        pr.BucketName = BucketName;
            //        pr.Key = FileKey;
            //        pr.InputStream = jsondocument.BaseStream;
            //        var x = S3Client.PutObjectAsync(pr).Result;
            //    }
            //}
        }

        public string CallerName { get; set; }
        public string ParentObject { get; set; }

        private class MemberTable
        {
            public long MemberID { get; set; }
            public string Attributes  { get; set; }
            public long InsertUser   { get; set; }
            public DateTime InsertDate { get; set; }
            public long? EditUser     { get; set; }
            public DateTime? EditDate { get; set; }
            public long? VoidUser   { get; set; }
            public DateTime? VoidDate { get; set; }
        }
    }
}
