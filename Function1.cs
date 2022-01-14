using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TimerTrigger
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task RunAsync([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            string fileshare = Environment.GetEnvironmentVariable("FILE_SHARE"); // Fileshare name Example: myfileshare
            string folderPath = Environment.GetEnvironmentVariable("FOLDER_PATH"); // Path to subfolder Example: myfolder/mysubfolder
            string connectionString = Environment.GetEnvironmentVariable("CON_STR"); // Connection String from Storage Account -> Access Keys -> Connection String

            try
            {                
                ShareClient share = new ShareClient(connectionString, fileshare);  // Instantiate a ShareClient which will be used to manipulate the file share

                ShareDirectoryClient directory = share.GetDirectoryClient(folderPath);   // Get a reference to the directory supplied in the POST

                // Track the remaining directories to walk, starting from the folder path provided in the POST
                Queue<ShareDirectoryClient> remaining = new Queue<ShareDirectoryClient>();
                remaining.Enqueue(directory);

                log.LogInformation(remaining.Count.ToString());

                while (remaining.Count > 0)
                {
                    ShareDirectoryClient dir = remaining.Dequeue(); // Get all of the next directory's files and subdirectories
                    if (dir.GetFilesAndDirectories() != null)
                    {
                        log.LogInformation("Dir: " + dir.Name);
                        foreach (ShareFileItem item in dir.GetFilesAndDirectories())
                        {
                            log.LogInformation("File: " + item.Name);
                            break;
                        }
                    } else
                    {
                        log.LogInformation("No files in the directory");
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
            }


            return;
        }
    }
}
