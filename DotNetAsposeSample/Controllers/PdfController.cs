using System.Net;
using DotnetAsposeSample.Lib;
using DotnetAsposeSample.Lib.Interfaces;
using DotNetAsposeSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAsposeSample.Controllers
{
    /// <summary>
    /// PDF変換・マージコントローラ
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : Controller
    {
        private readonly IAsposePdfService _asposePdfService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="adobePdfService"><see cref="IAsposePdfService"/></param>
        public PdfController(IAsposePdfService adobePdfService)
        {
            _asposePdfService = adobePdfService;
        }

        /// <summary>
        /// PDFファイルへ変換します。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("ConvertToPdf")]
        [Produces("application/octet-stream", Type = typeof(FileResult))]
        public async Task<IActionResult> ConvertToPdf([FromForm] ConvertToPdfRequest request)
        {
            try
            {
                // 変換処理の実行
                var outputStream = await ConvertToPdfInternal(request.File);

                return File(outputStream, "application/octet-stream", fileDownloadName: Path.GetFileNameWithoutExtension(request.File.FileName) + DateTime.Now.Ticks + ".pdf");
            }
            catch (AsposePdfServiceException ex)
            {
                return Problem(ex.Message, statusCode: (int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// PDF変換の内部処理です。
        /// </summary>
        /// <param name="file"><see cref="IFormFile"/></param>
        /// <returns><see cref="Stream"/>の<see cref="Task"/></returns>
        private async Task<Stream> ConvertToPdfInternal(IFormFile file)
        {
            var inputStream = new MemoryStream();
            await file.CopyToAsync(inputStream);
            return _asposePdfService.ConvertToPdf(new ConvertToPdfInput
            {
                Stream = inputStream,
                FileName = file.FileName
            });
        }
    }
}
