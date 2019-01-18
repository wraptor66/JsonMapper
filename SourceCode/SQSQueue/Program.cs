/*******************************************************************************
* Copyright 2009-2013 Amazon.com, Inc. or its affiliates. All Rights Reserved.
* 
* Licensed under the Apache License, Version 2.0 (the "License"). You may
* not use this file except in compliance with the License. A copy of the
* License is located at
* 
* http://aws.amazon.com/apache2.0/
* 
* or in the "license" file accompanying this file. This file is
* distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
* KIND, either express or implied. See the License for the specific
* language governing permissions and limitations under the License.
*******************************************************************************/

using System;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSQueue
{
    class Program
    {
        public static void Main(string[] args)
        {
            var sqs = new AmazonSQSClient();

            try
            {
                Console.WriteLine("===========================================");
                Console.WriteLine("Getting Started with Amazon SQS");
                Console.WriteLine("===========================================\n");

                //Creating a queue
                Console.WriteLine("Create a queue called MyQueue.\n");
                var sqsRequest = new CreateQueueRequest { QueueName = "MyQueue" };
                var createQueueResponse = sqs.CreateQueue(sqsRequest);
                string myQueueUrl = createQueueResponse.QueueUrl;

                //Confirming the queue exists
                var listQueuesRequest = new ListQueuesRequest();
                var listQueuesResponse = sqs.ListQueues(listQueuesRequest);

                Console.WriteLine("Printing list of Amazon SQS queues.\n");
                if (listQueuesResponse.QueueUrls != null)
                {
                    foreach (String queueUrl in listQueuesResponse.QueueUrls)
                    {
                        Console.WriteLine("  QueueUrl: {0}", queueUrl);
                    }
                }
                Console.WriteLine();

                //Sending a message
                Console.WriteLine("Sending a message to MyQueue.\n");
                var sendMessageRequest = new SendMessageRequest
                    {
                        QueueUrl = myQueueUrl, //URL from initial queue creation
                        MessageBody = "This is my message text."
                    };
                sqs.SendMessage(sendMessageRequest);

                //Receiving a message
                var receiveMessageRequest = new ReceiveMessageRequest { QueueUrl = myQueueUrl };
                var receiveMessageResponse = sqs.ReceiveMessage(receiveMessageRequest);
                if (receiveMessageResponse.Messages != null)
                {
                    Console.WriteLine("Printing received message.\n");
                    foreach (var message in receiveMessageResponse.Messages)
                    {
                        Console.WriteLine("  Message");
                        if (!string.IsNullOrEmpty(message.MessageId))
                        {
                            Console.WriteLine("    MessageId: {0}", message.MessageId);
                        }
                        if (!string.IsNullOrEmpty(message.ReceiptHandle))
                        {
                            Console.WriteLine("    ReceiptHandle: {0}", message.ReceiptHandle);
                        }
                        if (!string.IsNullOrEmpty(message.MD5OfBody))
                        {
                            Console.WriteLine("    MD5OfBody: {0}", message.MD5OfBody);
                        }
                        if (!string.IsNullOrEmpty(message.Body))
                        {
                            Console.WriteLine("    Body: {0}", message.Body);
                        }

                        foreach (string attributeKey in message.Attributes.Keys)
                        {
                            Console.WriteLine("  Attribute");
                            Console.WriteLine("    Name: {0}", attributeKey);
                            var value = message.Attributes[attributeKey];
                            Console.WriteLine("    Value: {0}", string.IsNullOrEmpty(value) ? "(no value)" : value);
                        }
                    }

                    var messageRecieptHandle = receiveMessageResponse.Messages[0].ReceiptHandle;

                    //Deleting a message
                    Console.WriteLine("Deleting the message.\n");
                    var deleteRequest = new DeleteMessageRequest { QueueUrl = myQueueUrl, ReceiptHandle = messageRecieptHandle };
                    sqs.DeleteMessage(deleteRequest);
                }

            }
            catch (AmazonSQSException ex)
            {
                Console.WriteLine("Caught Exception: " + ex.Message);
                Console.WriteLine("Response Status Code: " + ex.StatusCode);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Error Type: " + ex.ErrorType);
                Console.WriteLine("Request ID: " + ex.RequestId);
            }

            Console.WriteLine("Press Enter to continue...");
            Console.Read();
        }
    }
}