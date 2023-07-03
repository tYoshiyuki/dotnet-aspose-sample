namespace DotnetAsposeSample.Lib.Interfaces
{
    /// <summary>
    /// PDFサービスインターフェース
    /// </summary>
    public interface IAsposePdfService
    {
        /// <summary>
        /// PDFファイルへ変換します。
        /// </summary>
        /// <param name="convertToPdfInput"><see cref="ConvertToPdfInput"/></param>
        /// <returns>変換結果のストリーム</returns>
        Stream ConvertToPdf(ConvertToPdfInput convertToPdfInput);
    }
}
