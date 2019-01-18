using System;
using System.Collections.Generic;
using System.Text;
using JSONMapperSandbox.JSONMapper;
using System.IO;
using Newtonsoft.Json.Linq;
using JSONMapperSandbox.JSONMapper.CObjects;

namespace BrainLayer
{
    /// <summary>
    /// This Class should provide to wrapper of the JSONMapperfunctionality. It should
    /// also provide interface for instructions as to what type of mapping that should be 
    /// performed.
    /// </summary>
    public class MappingManager
    {
        string _dataPayload = string.Empty;
        public MappingManager(string DataPayload)
        {
            _dataPayload = DataPayload;
        }

        public string  ExecuteDataProcessing()
        {
            /// parse the data payload to get info for schemas
            Console.WriteLine("Starting ExecuteDataProcessing()!");

            //string returnJson = JSONMapper.Map2Requested(eventObject);
            return "";

        }

        public enum MappingAction
        {
            ValidatePayload, /// does payload has values for all the required fields
            PopulatePayload, /// traditional query data payloads and assign
            UpdatePayload, /// update exact payload with new values
            PopulateMasterObject /// assign data payoad values to Master Object
        }


    }
}
