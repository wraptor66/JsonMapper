using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Dynamic;
using Newtonsoft.Json.Linq;


namespace JSONMapperSandbox.JsonFieldFinder
{
    public static class JsonFieldFinder
    {
        public static IEnumerable<string> GetJPaths(int levels)
        {
            string[] rootPath = new string[3];
            rootPath[0] = String.Format("0");
            rootPath[1] = String.Format("1");
            rootPath[2] = String.Format("2");
            var perms = getAllPermutations(rootPath, levels);
            StringBuilder sb = new StringBuilder("");
            var filterPerms = perms.Where(x => !x.Contains("00")
            && !x.Contains("22") && !x.Contains("11"));            
            string actualPath = string.Empty;
            List<string> JPaths = new List<string>();
            foreach (var ea in filterPerms)
            {
                sb.Clear().Append(ea);
                actualPath = sb
                     .Replace("0", "*")
                     .Replace("1", ".")
                     .Replace("2", "[*].").ToString();
                JPaths.Add(actualPath);
                
            }           
            return JPaths.AsEnumerable<string>();
        }
        public static IEnumerable<JToken> GetFieldsList(string JsonLocation, string FieldName, IEnumerable<string> filterPermutations)
        {
            var jsonString = File.ReadAllText(JsonLocation);
            var jtokens = FindFieldsIEnumerable(FieldName, jsonString, filterPermutations);
            return jtokens;
        }
        public enum ReturnType
        {
            IEnumerable,
            JEnumerable
        }
        public static IEnumerable<JToken> FindFieldsIEnumerable(string fieldname, string payload, IEnumerable<string> filterPermutations)
        {
            try
            {
                List<JToken> returnObj = new List<JToken>();
                // read by categories
                JObject rss = JObject.Parse(payload);
                string wildcardPath = string.Empty;
                foreach (var ea in filterPermutations)
                {
                    wildcardPath = ea + fieldname;
                    var _sTokens = rss.SelectTokens(wildcardPath);
                    if (_sTokens.ToList().Count > 0)
                    {
                        bool exists = false;
                        //write the found to a list 
                        foreach (var _jtkn in _sTokens)
                        {
                            foreach (var jtoken in returnObj)
                            {

                                if (_jtkn == jtoken)
                                {
                                    //DO SOMETHING
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static JObject FindFieldsJObject(string fieldname, string payload, IEnumerable<string> filterPermutations, string parentobjectname)
        {
            try
            {
                JObject returnObj = new JObject();
                // read by categories
                JObject rss = JObject.Parse(payload);
                string wildcardPath = string.Empty;
                foreach (var ea in filterPermutations)
                {
                    wildcardPath = ea + fieldname;
                    var _sTokens = rss.SelectTokens(wildcardPath);
                    if (_sTokens.ToList().Count > 0)
                    {
                        bool exists = false;
                        //write the found to a list 
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static JEnumerable<JToken> FindFieldsJEnumerable(string fieldname, string payload, IEnumerable<string> filterPermutations)
        {
            try
            {
                JEnumerable<JToken> returnObj = new JEnumerable<JToken>();
                // read by categories
                JObject rss = JObject.Parse(payload);
                string wildcardPath = string.Empty;
                foreach (var ea in filterPermutations)
                {
                    wildcardPath = ea + fieldname;
                    var _sTokens = rss.SelectTokens(wildcardPath);
                    if (_sTokens.ToList().Count > 0)
                    {
                        bool exists = false;
                        //write the found to a list 
                        foreach (var _jtkn in _sTokens)
                        {
                            foreach (var jtoken in returnObj)
                            {

                                if (_jtkn == jtoken)
                                {
                                    //DO SOMETHING
                                    exists = true;
                                    break;
                                }
                            }

                            if (exists == false)
                            {
                                returnObj.Append(_jtkn);
                            }
                        }
                    }
                }
                var a = returnObj.Count();
                return returnObj;
            }
            catch (Exception ex)
            {
                throw ex;
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

        private static List<string> getAllPermutationsRecursive(
               string[] chars, List<string> aggPerms, int iter)
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
