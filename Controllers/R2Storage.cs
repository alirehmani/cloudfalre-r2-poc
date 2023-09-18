using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace cloudfalre_r2_poc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class R2Storage : ControllerBase
    {

        [HttpGet]
        [Route(nameof(ListBuckets))]
        
        public async Task<IActionResult> ListBuckets()
        {
            try
            {
                var res=await cloudflare_r2.s3client().ListBucketsAsync();
                
                cloudflare_r2.s3client().Dispose();


                return Ok(res.Buckets.ToList());
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpGet]
        [Route(nameof(GetBucketObject))]
        public async Task<IActionResult> GetBucketObject(string bucketName)
        {
            //bucketName = "mytestbucket";
            var req=new ListObjectsV2Request { BucketName = bucketName};
            var res = await cloudflare_r2.s3client().ListObjectsV2Async(req);
            Dictionary<string,string> result = new Dictionary<string,string>();

            foreach (var obj in res.S3Objects)
            {

                result.Add("url", "https://anotherbucket.devops-ali.xyz/" + obj.Key);

            }
            
            cloudflare_r2.s3client().Dispose();
            return Ok(result);
        }

        [HttpPost]
        [Route(nameof(UploadObject))]
        public async Task<IActionResult> UploadObject(IFormFile file)
        {

            try
            {
                var filepath=Path.GetTempFileName();
                using (var stream=new FileStream(filepath,FileMode.Create))
                {

                    await file.CopyToAsync(stream);
                }

                var request = new PutObjectRequest
                {

                    FilePath = filepath,
                    BucketName = "anotherbucket",
                    DisablePayloadSigning = true,
                    ContentType = file.ContentType,
                    Key=file.FileName
                    
                };
                var res = await cloudflare_r2.s3client().PutObjectAsync(request);

                
                return Ok("https://anotherbucket.devops-ali.xyz/"+file.FileName);
                

            }
            catch (Exception e)
            {
                    
                throw;
            }

            

        }


    }
}
