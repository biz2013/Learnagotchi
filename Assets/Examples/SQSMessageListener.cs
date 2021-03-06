﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Amazon;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon.SQS;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SQSMessageListener
{
    private string IdentityPoolId;
    private string queueName;
    private string queueUrl;
    private ActionInputCallback callback;

    public string CognitoIdentityRegion = RegionEndpoint.USEast1.SystemName;

    private RegionEndpoint _CognitoIdentityRegion
    {
        get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
    }

    public string SQSRegion = RegionEndpoint.USEast1.SystemName;

    private RegionEndpoint _SQSRegion
    {
        get { return RegionEndpoint.GetBySystemName(SQSRegion); }
    }


    //name of the queue you want to create

    private AWSCredentials _credentials;

    private AWSCredentials Credentials
    {
        get
        {
            if (_credentials == null)
                _credentials = new CognitoAWSCredentials(this.IdentityPoolId, _CognitoIdentityRegion);
            return _credentials;
        }
    }

    private IAmazonSQS _sqsClient;

    private IAmazonSQS SqsClient
    {
        get
        {
            if (_sqsClient == null)
                _sqsClient = new AmazonSQSClient(Credentials, _SQSRegion);
            return _sqsClient;
        }
    }

    internal SQSMessageListener(string id, string queueName, string sqsUrl, ActionInputCallback callbackObj)
    {
        this.IdentityPoolId = id;
        this.queueName = queueName;
        this.queueUrl = sqsUrl;
        this.callback = callbackObj;

    }

    internal IEnumerator RepeatRetrieveMessage(float waitTime)
    {
        bool checkSQS = true;
        while (checkSQS)
        {
            yield return new WaitForSeconds(waitTime);

            if (!string.IsNullOrEmpty(queueUrl))
            {
                SqsClient.ReceiveMessageAsync(queueUrl, (result) => {
                    if (result.Exception == null)
                    {
                        //Read the message
                        var messages = result.Response.Messages;
                        messages.ForEach(m => {
                            Debug.Log(@"New Message Id  = " + m.MessageId);
                            Debug.Log(@"New Mesage = " + m.Body);
                            this.callback.TakeAction(m.Body);

                            //Delete the message
                            var delRequest = new Amazon.SQS.Model.DeleteMessageRequest
                            {
                                QueueUrl = queueUrl,
                                ReceiptHandle = m.ReceiptHandle

                            };

                            SqsClient.DeleteMessageAsync(delRequest, (delResult) => {
                                if (delResult.Exception == null)
                                {
                                }
                                else
                                {
                                }
                            });




                        });

                    }
                    else
                    {
                        Debug.LogException(result.Exception);
                    }


                });
            }
            else
            {
                Debug.Log(@"Queue Url is empty, make sure that the queue is created first");
            }

            //Debug.Log (".");
        }
    }
}
