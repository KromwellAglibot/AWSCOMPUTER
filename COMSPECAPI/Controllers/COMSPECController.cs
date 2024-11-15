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

//    public class COMSPECController : ControllerBase
//    {
//        public static IWebHostEnvironment _webHostEnvironment;
//        public COMSPECController(IWebHostEnvironment webHostEnvironment)
//        {
//            _webHostEnvironment = webHostEnvironment;
//        }

//        private readonly string accessKey = "AKIA47CR3CYVCOTTPDMQ";
//        private readonly string secretKey = "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";
//        private readonly string bucketName = "kaaglibot";
//        private readonly AmazonS3Client client;

//        public COMSPECController()
//        {
//            client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.USEast1);
//        }

//        [HttpPost]
//        public async Task<string> Post([FromForm] User user)
//        { try
//        { if (user.files.Length > 0)
//        {
//         string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
//         if (!Directory.Exists(path))
//        {
//          Directory.CreateDirectory(path);
//         }
//          using (FileStream filestream = System.IO.File.Create(path + user.files.FileName))
//          {
//        user.files.CopyTo(filestream);
//         filestream.Flush();
//          }
//        var fileTransferUtility = new TransferUtility(client);
//         using (var memoryStream = new MemoryStream())
//          {
//          await user.files.CopyToAsync(memoryStream);
//          memoryStream.Position = 0;
//         fileTransferUtility.Upload(memoryStream, bucketName, user.files.FileName);
//          }
//          return "Successfully Uploaded.";
//          } else
//           {return "Sorry there's something wrong.";
//                }
//            }
//            catch (Exception ex)
//            {
//                return ex.Message;
//            }
//        }

//        [HttpGet("{fileName}")]
//        public async Task<IActionResult> Get([FromRoute] string fileName)
//        {
//            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
//            var filePath = path + fileName + ".jpg";
//            if (System.IO.File.Exists(filePath))
//            {
//                byte[] b = System.IO.File.ReadAllBytes(filePath);
//                return File(b, "image/jpg");
//            }
//            return NotFound("File not found.");
//        }
//    }
//}

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

