using System;
using System.IO;
using System.Threading.Tasks;
using Application.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionAppBlazor.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FileController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<string>> Upload([FromBody] FileDataDto file)
        {
            try
            {
                var result = await FileUpload.UploadFile(file);

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpDelete("{fileName}")]
        public ActionResult<bool> Delete(string fileName)
        {
            try
            {
                var result = FileUpload.DeleteFile(fileName);

                return result ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}