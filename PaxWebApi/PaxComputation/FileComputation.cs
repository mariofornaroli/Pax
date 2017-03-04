using Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using Microsoft.Azure; // Namespace for Azure Configuration Manager
using Microsoft.WindowsAzure.Storage; // Namespace for Storage Client Library
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage
using Microsoft.WindowsAzure.Storage.File; // Namespace for File storage

namespace PaxComputation
{
    public class FileComputation
    {
        private string STORAGE_CONN_STRING = "DefaultEndpointsProtocol=https;AccountName=paxappfiles;AccountKey=8D8UMfQwpBcN88VlXeSmh/avTpaHFTyuAY48g9+lAHw/XSTveN0RA2Lgx61Inknf8ALMuJhjQSRNiSvlGKd3dQ==";
        private string DEFAULT_FILESHARE = "paxappfileshare";
        private string DEFAULT_DIRECTORY = "books";
        private string DEFAULT_FILENAME = "heartBooks.txt";



        #region FileComputation Methods

        public void writetFile()
        {

        }

        private void fillDefaultValues(ref string fileShare, ref string dir, ref string fileName)
        {
            if (string.IsNullOrEmpty(fileShare))
            {
                fileShare = DEFAULT_FILESHARE;
            }
            if (string.IsNullOrEmpty(dir))
            {
                dir = DEFAULT_DIRECTORY;
            }
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = DEFAULT_FILENAME;
            }
        }

        public string getFile(string fileShare = "", string dir = "", string fileName = "")
        {
            fillDefaultValues(ref fileShare, ref dir, ref fileName);

            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(STORAGE_CONN_STRING);

            // Create a CloudFileClient object for credentialed access to File storage.
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();

            // Get a reference to the file share we created previously.
            CloudFileShare share = fileClient.GetShareReference(fileShare);

            // Ensure that the share exists.
            if (share.Exists())
            {
                // Get a reference to the root directory for the share.
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();

                // Get a reference to the directory we created previously.
                CloudFileDirectory sampleDir = rootDir.GetDirectoryReference(dir);

                // Ensure that the directory exists.
                if (sampleDir.Exists())
                {
                    // Get a reference to the file we created previously.
                    CloudFile file = sampleDir.GetFileReference(fileName);

                    // Ensure that the file exists.
                    if (file.Exists())
                    {
                        // Write the contents of the file to the console window.
                        return file.DownloadTextAsync().Result;
                    }
                }
            }
            return string.Empty;
        }

        public void writeFile(string fileShare = "", string dir = "", string fileName = "", string contentToWrite = "")
        {
            fillDefaultValues(ref fileShare, ref dir, ref fileName);

            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(STORAGE_CONN_STRING);

            // Create a CloudFileClient object for credentialed access to File storage.
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();

            // Get a reference to the file share we created previously.
            CloudFileShare share = fileClient.GetShareReference(fileShare);

            // Ensure that the share exists.
            if (share.Exists())
            {
                // Get a reference to the root directory for the share.
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();

                // Get a reference to the directory we created previously.
                CloudFileDirectory sampleDir = rootDir.GetDirectoryReference(dir);

                // Ensure that the directory exists.
                if (sampleDir.Exists())
                {
                    // Get a reference to the file we created previously.
                    CloudFile file = sampleDir.GetFileReference(fileName);

                    // Ensure that the file exists.
                    if (file.Exists())
                    {
                        // Write the contents to the file.
                        Stream streamContent = AgilityTool.GenerateStreamFromString(contentToWrite);
                        file.UploadFromStream(streamContent);
                    }
                }
            }
        }

        #endregion

    }
}