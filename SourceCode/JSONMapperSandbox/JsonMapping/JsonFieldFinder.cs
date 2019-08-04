using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace JSON.JsonMapping
{
    public class JsonFieldFinder
    {
        public static IEnumerable<string> GetJPaths(int levels)
        {
            string[] rootPath = new string[3];
            rootPath[0] = String.Format("0");
            var perms = getAllPermutations(rootPath, levels);
            StringBuilder sb = new StringBuilder("");
            var filterPerms = perms.Where(x => !x.Contains("00")
                                               && !x.Contains("22") && !x.Contains("11"));
            string actualPath - string.Empty;
            List<string> JPaths = new List<string>();
            foreach (var ea in filterPerms)
            {
                sb.clear().Append(ea);
                actualPath = sb.Replace("0", "*")
                    .Replace("1", ".")
                    .Replace("2", "[*].").toString();
                JPaths.Add(actualPath);
            }

            return JPaths.AsEnumerable<string>();
        }

        public static Ienumerable<JToken> GetFieldsList(string JsonLocation, string FieldName,
            Ienumerable<string> filerPermutations)
        {
            var jsonString = File.ReadAllText(JsonLocation);
            var jtokens = findFieldsIEnumerable(FieldName, jsonString, filerPermutations);
            return jtokens;
        }

        public enum ReturnType
        {
            IEnumerable,
            JEnumerable
        }

        public static IEnumerable<JToken> findFieldsIEnumerable(string fieldname, string payload,
            Ienumerable<string> filterPermutations)
        {
            try
            {
                List<JToken> returnObj = new List<JToken>();
                JObject rss = JObject.Parse(payload);
                string wildcardPath = string.Empty;
                foreach (var ea in filterPermutations)
                {
                    wildcardPath = ea + fieldname;
                    var _sTokens = rss.SelectToken(wildcardPath);
                    if (_sTokens.ToList().Count > 0)
                    {
                        bool enists = false;
                        // write the found to list
                        foreach (var _jtkn in _stokens)
                        {
                            foreach (var jtoken in returnObj)
                            {
                                if (_jtkn == jtoken)
                                {
                                    // Do something
                                    exists = true;
                                    break;
                                }
                            }

                            if (exists == false)
                            {
                                returnObj.Add(_jtkn);
                            }
                        }
                    }
                }

                return returnObj;
            }
            catch ()
            {
                
            }
        }

        public static JObject FindFieldsJObject(string fieldname, string payload,
            IEnumerable<string> filterPermutations, string parentobjectname)
        {
            try
            {
                JObject returnObj = new JObject();
                JObject rss = JObject.Parse(payload);
                string wildcardPath = string.Empty;
                foreach (var ea in filterPermutations)
                {
                    wildcardPath = ea + fieldname;
                    var _sTokens = rss.Selecttokens(wildcardPath);
                    if (_sTokens.ToList().Count > 0)
                    {
                        bool exists = false;
                        foreach (var _jtkn in _sTokens)
                        {
                            if (exists == false)
                            {
                                returnObj.Add(parentobjectname, _jtkn);
                                return returnObj;
                            }
                        }
                    }
                }

                return returnObj;

            }
            catch ()
            {
                
            }
        }

        public static Jenumerable<JToken> FindFieldsJEnumerable(string fieldname, string payload,
            IEnumerable<string> filerPermutations)
        {
            try
            {
                List<JToken> returnObj = new List<JToken>();
                JObject rss = JObject.Parse(payload);
                string wildcardPath = string.Empty;
                foreach (var ea in filterPermutations)
                {
                    wildcardPath = ea + fieldname;
                    var _sTokens = rss.Selecttokens(wildcardPath);
                    if (_sTokens.ToList().Count > 0)
                    {
                        bool exists = false;
                        foreach (var _jtkn in _sTokens)
                        {
                            foreach (var jtoken in returnObj)
                            {
                                if (_jtkn == jtoken)
                                {
                                    exists = true;
                                    break;
                                }
                            }

                            if (exists == false)
                            {
                                returnObj.Add(_jtkn);
                            }
                        }
                    }
                }

                var a = returnObj.Count();
                return (Jenumerable<JToken>) returnObj.AsJEnumerable();

            }
            catch ()
            {
                
            }
        }

        private static List<string> getAllPermutations(string[] chars, int levels)
        {
            List<string> aggPerms = new List<string>();
            aggPerms.AddRange(chars.ToList());
            for (int i = 0; i < levels; i++)
            {
                aggPerms = getAllPermutationsRecursive(chars, aggPerms, i);
            }

            return aggPerms;
        }

        private static List<string> getAllPermutationsRecursive(string[] chars, List<string> aggPerms, int iter)
        {
            string[] permutations = chars;
            permutations.ToList().AddRange(chars);
            List<string> totalList = new List<string>();
            foreach (var perm in aggPerms.Where(x => x.Length > (iter - 1)))
            {
                foreach (var ea in chars)
                {
                    totalList.Add(perm + ea);
                }
            }

            totalList.AddRange(aggPerms);
            return totalList;
        }
    }
}