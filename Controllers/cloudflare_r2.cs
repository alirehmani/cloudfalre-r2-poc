using Amazon.Runtime;
using Amazon.S3;

namespace cloudfalre_r2_poc.Controllers
{
    public static class cloudflare_r2
    {
        public static AmazonS3Client s3client()  {
            var accessKey = "908d06c839990ab34a1e0a0ff802327f";
            var secretKey = "e0f9e6345f4c7193410c17aae3b9e72186fd01cd53e4c0ce200d132fb23524ae";
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var s3Client = new AmazonS3Client(credentials, new AmazonS3Config
            {
                ServiceURL = "https://692e82121988a4afdd33c6080043d72e.r2.cloudflarestorage.com",
            });

            return s3Client;

        }
    }
}
