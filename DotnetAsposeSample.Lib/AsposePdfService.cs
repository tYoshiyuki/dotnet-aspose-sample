using Aspose.Words;
using DotnetAsposeSample.Lib.Interfaces;

namespace DotnetAsposeSample.Lib
{
    /// <summary>
    /// PDFサービスクラス
    /// </summary>
    public class AsposePdfService : IAsposePdfService
    {
        /// <summary>
        /// 一時ファイル出力先のルートディレクトリ
        /// </summary>
        public string TmpPathRoot { get; set; } = Path.GetTempPath();

        /// <inheritdoc />
        /// <exception cref="AsposePdfServiceException"></exception>
        public Stream ConvertToPdf(ConvertToPdfInput convertToPdfInput)
        {
            try
            {
                return ConvertToPdfInternal(convertToPdfInput);
            }
            catch (Exception ex)
            {
                throw new AsposePdfServiceException("ConvertToPdf failed.", ex);
            }
        }

        /// <summary>
        /// PDFファイルへ変換する内部処理です。
        /// </summary>
        /// <param name="convertToPdfInput"><see cref="ConvertToPdfInput"/></param>
        /// <returns>変換結果のストリーム</returns>
        /// <exception cref="AsposePdfServiceException"></exception>
        private Stream ConvertToPdfInternal(ConvertToPdfInput convertToPdfInput)
        {
            string extension = Path.GetExtension(convertToPdfInput.FileName);
            var tmpPath = Path.Combine(TmpPathRoot, Guid.NewGuid().ToString("N"));

            // NOTE 拡張子によってメディアタイプを選定します。必要に応じて判定処理を追加します。
            if (extension.EndsWith(".doc", StringComparison.OrdinalIgnoreCase) || extension.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
            {
                var document = new Document(convertToPdfInput.Stream);
                document.Save(tmpPath, SaveFormat.Pdf);
            }
            else if (extension.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) || extension.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                var workbook = new Aspose.Cells.Workbook(convertToPdfInput.Stream);
                workbook.Save(tmpPath, Aspose.Cells.SaveFormat.Pdf);
                workbook.Dispose();
            }
            else
            {
                throw new ArgumentException($"Not supported file type:{extension}.");
            }

            var outputStream = new MemoryStream();
            var fileStream = new FileStream(tmpPath, FileMode.Open);
            fileStream.CopyTo(outputStream);
            fileStream.Dispose();

            File.Delete(tmpPath);

            outputStream.Position = 0;
            return outputStream;
        }
    }
}
