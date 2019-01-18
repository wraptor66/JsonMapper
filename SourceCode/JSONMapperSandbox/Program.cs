using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using JSONMapperSandbox.JSONMapper.CObjects;
using System.Diagnostics;
namespace JSONMapperSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            MemberPopulate();
        }

        public static bool MemberPopulate()
        {
            JSONMapper.JSONMapper.Initialize();
            Console.WriteLine("Starting!");
            Stopwatch stopWatch = Stopwatch.StartNew();
            stopWatch.Start();
            List<KeyValuePair<string, string>> jsonCollection = new List<KeyValuePair<string, string>>();
            string eventPayload = File.ReadAllText(@".\JSONMapper\Payloads\ls_user_publication_event.json");
            EventManager em = new EventManager(eventPayload);
            var returnobj = em.ProcessMember();
            Console.WriteLine("Enroll Member Time taken: {0}ms", stopWatch.Elapsed.TotalMilliseconds);
            Console.ReadLine();
            return true;
        }

        public static bool EnrollMember()
        {
            JSONMapper.JSONMapper.Initialize();
            Console.WriteLine("Starting!");
            Stopwatch stopWatch = Stopwatch.StartNew();
            stopWatch.Start();
            List <KeyValuePair<string, string>> jsonCollection = new List<KeyValuePair<string, string>>();
            string eventPayload = File.ReadAllText(@".\JSONMapper\Payloads\ls_enroll_initial_load.json");
            EventManager em = new EventManager(eventPayload);
            var returnobj = em.ProcessEvent();
            Console.WriteLine("Enroll Member Time taken: {0}ms", stopWatch.Elapsed.TotalMilliseconds);
            Console.ReadLine();
            return true;
        }

        public static bool Demo()
        {
            JSONMapper.JSONMapper.Initialize();
            Console.WriteLine("Starting!");
            Stopwatch stopWatch = Stopwatch.StartNew();
            stopWatch.Start();
            List<KeyValuePair<string, string>> jsonCollection = new List<KeyValuePair<string, string>>();
            string servicePayload = File.ReadAllText(@".\JSONMapper\EventMaps\getMember.json");
            jsonCollection.Add(new KeyValuePair<string, string>("eventmap", servicePayload));
            servicePayload = File.ReadAllText(@".\JSONMapper\Payloads\sample_client.json");
            jsonCollection.Add(new KeyValuePair<string, string>("sample_client", servicePayload));
            EventObject eventObject = new EventObject();
            eventObject._jsonCollection = jsonCollection;
            string returnJson = JSONMapper.JSONMapper.Map2Requested(eventObject);
            Console.WriteLine(returnJson);
            Console.WriteLine("Demo Time taken: {0}ms", stopWatch.Elapsed.TotalMilliseconds);
            Console.ReadLine();
            return true;
        }

    }
}

