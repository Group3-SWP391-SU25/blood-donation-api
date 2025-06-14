using BloodDonation.Application.IntegrationServices.Interfaces;
using BloodDonation.Application.Models.Files;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> PostFiles([FromServices] IFirebaseService firebaseService,
            [FromForm] FileUploadModel fileUploadModel)
        {

            if (string.IsNullOrEmpty(fileUploadModel.Directory))
            {
                var res = await firebaseService.SaveFileAsync(fileUploadModel.File, "blood-donation/static-files");
                return Ok(res);
            }
            else
            {
                var res = await firebaseService.SaveFileAsync(fileUploadModel.File, fileUploadModel.Directory);
                var response = new FileResponseModel()
                {
                    FileName = res.FileName,
                    FileUrl = res.Url
                };
                return Ok(response);
            }

        }
    }
}
