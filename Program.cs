using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Services;
using Google.Apis.Drive.v2;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2.Data;


namespace ConsoleApplication4
{
   class Program
    {

        static void Main(string[] args)
        {
           
            
        }

        public class Gauth
        {
            public string userEmail = "danielkell26@gmail.com";
            private const string SERVICE_ACCOUNT_EMAIL = "679772523633-582f3qgu7hh4ch6up1n1f3kkpjo385gr@developer.gserviceaccount.com";
            private const string SERVICE_ACCOUNT_PKCS12_FILE_PATH = @"\C:\drive\ytstream-84da4839b15a.p12";

            /// <summary>
            /// Build a Drive service object authorized with the service account
            /// that acts on behalf of the given user.
            /// </summary>
            /// @param userEmail The email of the user.
            /// <returns>Drive service object.</returns>
            static DriveService BuildService(String userEmail)
            {
               
                X509Certificate2 certificate = new X509Certificate2(SERVICE_ACCOUNT_PKCS12_FILE_PATH,
                    "notasecret", X509KeyStorageFlags.Exportable);
                ServiceAccountCredential credential = new ServiceAccountCredential(
                    new ServiceAccountCredential.Initializer(SERVICE_ACCOUNT_EMAIL)
                    {
                        Scopes = new[] { DriveService.Scope.Drive },
                        User = userEmail
                    }.FromCertificate(certificate));

                // Create the service.
                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Drive API Service Account",
                });


                return service;
            }

        }

        public class FileGet
        {

            // ...

            /// <summary>
            /// Retrieve a list of File resources.
            /// </summary>
            /// <param name="service">Drive API service instance.</param>
            /// <returns>List of File resources.</returns>
            public static List<File> retrieveAllFiles(DriveService service)
            {
                List<File> result = new List<File>();
                FilesResource.ListRequest request = service.Files.List();

                do
                {
                    try
                    {
                        FileList files = request.Execute();

                        result.AddRange(files.Items);
                        request.PageToken = files.NextPageToken;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An error occurred: " + e.Message);
                        request.PageToken = null;
                    }
                } while (!String.IsNullOrEmpty(request.PageToken));
                return result;
            }

            // ...
        }

    }
}



