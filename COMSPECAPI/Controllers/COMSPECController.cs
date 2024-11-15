using Microsoft.AspNetCore.Mvc;
using COMSPECSBUSINESS;
using System.IO;
using COMSPECSMODEL;
using Microsoft.AspNetCore.Hosting;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;

namespace COMSPECAPI.Controllers
{
    [ApiController]
    [Route("api/Userid")]
    public class COMSPECController : ControllerBase
    {
        private readonly IAmazonS3 _s3Client;

        private readonly ILogger<COMSPECController> _logger;
        public COMSPECController
            (ILogger<COMSPECController> logger,
            IAmazonS3 s3Client
            )
        {
            _logger = logger;
            _s3Client = s3Client;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("Sorry, error!");
            }
            string bucketName = "kaaglibot";
            string fileKey = "uploads/" + file.FileName;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var putRequest = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = bucketName, 
                    Key = fileKey,
                    InputStream = memoryStream
                };
                var response = await _s3Client.PutObjectAsync(putRequest);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                { return Ok("Uploaded Successfully"); }
                else
                {
                    return StatusCode(500, "Sorry, Something Went Wrong");
                }
            }


        }

    }
    }


//public class COMSPECController : ControllerBase
//{
//    ModelGetServices Comp;
//    ModelTransactionServices comps;
//    public COMSPECController()
//    {
//        Comp = new ModelGetServices();
//        comps = new ModelTransactionServices();

//    }
//    [HttpGet]
//    public IEnumerable<COMSPECAPI.User> GetUser()
//    {
//        var user = Comp.GetAllUsers();
//        List<COMSPECAPI.User> usser = new List<COMSPECAPI.User>();

//        foreach (var model in user)
//        {
//            usser.Add(new COMSPECAPI.User { Userid = model.Userid, password = model.password });
//        }
//        return usser;
//    }
// [HttpPost]
//public JsonResult AddModel(User request)
//{
//    var result = comps.CreateUser(request.Userid, request.password);
//    return new JsonResult(result);

//}



//        [HttpPatch]
//        public JsonResult UpdateModel(User request)
//        {
//            var result = comps.UpdateModel(request.Userid, request.password);
//            return new JsonResult(result);

//        }
//        [HttpDelete]
//        public JsonResult DeleteCOmp(COMSPECAPI.User request)
//        {

//            var Compdelete = new COMSPECSMODEL.User
//            {
//                Userid = request.Userid

//            };

//            var result = comps.DeleteModel(Compdelete);

//            return new JsonResult(result);
//        }
//    }
//}

