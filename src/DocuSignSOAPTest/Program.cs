// See https://aka.ms/new-console-template for more information
using FI.ESign.DocuSignSoap;
using FI.ESign.Common;
using FI.ESign.Model;

string[] input = Environment.GetCommandLineArgs();

Console.WriteLine($"Using the following arguments: \nUserId: {input[1]}, \npassword: {input[2]}, \nintegratorKey: {input[3]}, \nexternalEnvelopeId: {input[4]}, \nSOAPSvcUrl: {input[5]}");
Dictionary<string, string> endpoints = new Dictionary<string, string>
{
    { "DSSoapSvc", input[5] }
};

FIDSEnvelopeSvc svc = new FIDSEnvelopeSvc(new FILoginCredential(input[1], input[2], null, input[3], null, null, null, null), endpoints);

var envStatus = svc.GetEnvelopeStatus(new FIEnvelope() { ExternalID = input[4] }, "SOAP Test App");

Console.WriteLine($"EnvelopeStatus: {envStatus.Status}");