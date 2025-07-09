using System.Globalization;
using System.Security.Claims;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Model.Internal.MarshallTransformations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwsFileController : ControllerBase
    {
        private readonly IAmazonS3 s3Service;

        public AwsFileController(IAmazonS3 s3service)
        {
            s3Service = s3service;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFilesAsync(IFormFile file)
        {
            var userName= User.FindFirst(ClaimTypes.Name)?.Value;

            var bucketName = "studyhub-bucket-s3";
            var existingBucket = s3Service.EnsureBucketExistsAsync(bucketName);
            var prefix = userName;
            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key =  $"{prefix}/DisplayPicture/{userName}-Pfp",
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };
            //request.Metadata.Add("Content-Type", file.ContentType);
            await s3Service.PutObjectAsync(request);
            return Ok(new { message = "File Uploaded Successfully" });
        }

        [HttpGet("userPfp")]
        public async Task<IActionResult> GetUserPfpAsync(String userName)
        {
            var bucketName = "studyhub-bucket-s3";
            var path = $"{userName}/DisplayPicture/{userName}-Pfp";
            var urlRequest = new GetPreSignedUrlRequest()
            {
                BucketName=bucketName,
                Key= path,
                Expires= DateTime.UtcNow.AddDays(1),

            };

            string preSignedUrl= s3Service.GetPreSignedURL(urlRequest);
            return Ok(new {url= preSignedUrl } );
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUserPfpAsync()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var bucketName = "studyhub-bucket-s3";
            var path = $"{username}/DisplayPicture/{username}-Pfp";
            await s3Service.DeleteObjectAsync(bucketName, path);
            return Ok(new {message="Image deleted!"});


        }
        
    }
}
