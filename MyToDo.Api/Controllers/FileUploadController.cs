using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace MyToDo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Upload a file.");
            }

            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

            // 检查目录是否存在，如果不存在，则创建
            if (!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            var filePath = Path.Combine(uploadFolderPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { filePath });
        }


        [HttpGet]
        [Route("download/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("Filename is not specified.");
            }

            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            var filePath = Path.Combine(uploadFolderPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.CopyTo(memoryStream);
            }
            // 重置流的位置到开始，以便正确地读取文件内容
            memoryStream.Position = 0;

            // 返回文件流
            // 第一个参数是文件流
            // 第二个参数是MIME类型，这里可以根据文件类型动态指定，例如"application/pdf"，这里使用"application/octet-stream"使其更通用
            // 第三个参数是下载时显示的文件名
            return File(memoryStream, "application/octet-stream", Path.GetFileName(filePath));
        }

    }
}
