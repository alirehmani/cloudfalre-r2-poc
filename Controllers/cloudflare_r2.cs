using Amazon.Runtime;
using Amazon.S3;

namespace cloudfalre_r2_poc.Controllers
{
    public static class cloudflare_r2
    {
        public static AmazonS3Client s3client()  {
            var accessKey = "";
            var secretKey = "";
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var s3Client = new AmazonS3Client(credentials, new AmazonS3Config
            {
                ServiceURL = ".r2.cloudflarestorage.com",
            });

            return s3Client;

        }
    }
}
