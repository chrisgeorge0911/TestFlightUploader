using System;
using System.Data;
using System.IO;
using System.Net;
using RestSharp;

namespace TestFlightUploader
{
    public class TestFlightApi
    {
        private const string c_TestFlightUrl = "http://testflightapp.com";
        private const string c_TestFlightApiPath = "api/builds.json";
        
        public static void Upload(TestFlightPackage package)
        {
            ValidatePackage(package);
            
            var testflight = new RestClient(c_TestFlightUrl);
            RestRequest request = MakeRestRequest(package);

            var response = testflight.Execute(request);

            if ( response.StatusCode != HttpStatusCode.OK )
                throw new Exception("Could not upload build, TestFlight error: " + response.StatusDescription);
        }
        
        private static void ValidatePackage(TestFlightPackage package)
        {
            if ( string.IsNullOrEmpty(package.ApiToken) )
                throw new NoNullAllowedException("Api Token must be specified");

            if ( string.IsNullOrEmpty(package.TeamToken) )
                throw new NoNullAllowedException("Team Token must be specified");

            if ( string.IsNullOrEmpty(package.File) )
                throw new Exception(".ipa file must be specified");

            if ( string.IsNullOrEmpty(package.File) )
                throw new Exception(".ipa file must be specified");
            
            if ( !File.Exists(package.File))
                throw new FileNotFoundException(".ipa file does not exist", package.File);
        }
        
        private static RestRequest MakeRestRequest(TestFlightPackage package)
        {
            var request = new RestRequest(c_TestFlightApiPath, Method.POST);

            request.AddParameter("api_token", package.ApiToken);
            request.AddParameter("team_token", package.TeamToken);
            request.AddParameter("notes", package.Notes);
            request.AddParameter("distribution_lists", package.DistributionLists);
            request.AddParameter("notify", package.Notify);
            request.AddParameter("replace", package.Replace);
            request.AddFile("file", package.File);

            return request;
        }
    }
}
