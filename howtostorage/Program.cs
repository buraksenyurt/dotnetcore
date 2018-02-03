using System;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace howtostorage
{
    class Program
    {
        static StorageClient storageClient;

        static void Main(string[] args)
        {
            var credential = GoogleCredential.FromFile("my-starwars-game-project-credentials.json");
            storageClient = StorageClient.Create(credential);
            var projectId = "subtle-seer-193315";
            var bucketName = "article-images-bucket";
            CreateBucket(projectId, bucketName);
            WriteBucketList(projectId);
            UploadObject(bucketName, "legom.jpg", "legom");
            UploadObject(bucketName, "pinkfloyd.jpg", "pink-floyd");
            WriteBucketObjects(bucketName);
            DownloadObject(bucketName, "legom","lego-from-google.jpg");
            DownloadObject(bucketName, "pink-floyd","pink-floyd-from-google.jpg");
            
            Console.WriteLine("Yüklenen nesneler silinecek");
            Console.ReadLine();

            DeleteObject(bucketName, "pink-floyd");
            DeleteObject(bucketName, "legom");
            DeleteBucket(bucketName);
        }
        static void CreateBucket(string projectId, string bucketName)
        {
            try
            {
                storageClient.CreateBucket(projectId, bucketName);
                Console.WriteLine($"{bucketName} oluşturuldu.");
            }
            catch (Google.GoogleApiException e)
            when (e.Error.Code == 409)
            {
                Console.WriteLine(e.Error.Message);
            }
        }
        static void WriteBucketList(string projectId)
        {
            foreach (var bucket in storageClient.ListBuckets(projectId))
            {
                Console.WriteLine($"{bucket.Name},{bucket.TimeCreated}");
            }
        }
        static void UploadObject(string bucketName, string filePath,string objectName = null)
        {
            using (var stream = File.OpenRead(filePath))
            {
                objectName = objectName ?? Path.GetFileName(filePath);
                storageClient.UploadObject(bucketName, objectName, null, stream);
                Console.WriteLine($"{objectName} yüklendi.");
            }
        }
        static void WriteBucketObjects(string bucketName)
        {
            foreach (var obj in storageClient.ListObjects(bucketName, ""))
            {
                Console.WriteLine($"{obj.Name}({obj.Size})");
            }
        }
        static void DownloadObject(string bucketName, string objectName,string filePath = null)
        {
            filePath = filePath ?? Path.GetFileName(objectName);
            using (var stream = File.OpenWrite(filePath))
            {
                storageClient.DownloadObject(bucketName, objectName, stream);
            }
            Console.WriteLine($"{objectName}, {filePath} olarak indirildi.");
        }
        static void DeleteObject(string bucketName, string objectName)
        {
            storageClient.DeleteObject(bucketName, objectName);
            Console.WriteLine($"{objectName} silindi.");
        }
        static void DeleteBucket(string bucketName)
        {
            storageClient.DeleteBucket(bucketName);
            Console.WriteLine($"{bucketName} silindi.");
        }
    }
}