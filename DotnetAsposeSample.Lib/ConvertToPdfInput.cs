using DotnetAsposeSample.Lib.Interfaces;

namespace DotnetAsposeSample.Lib
{
    /// <summary>
    /// <see cref="IAsposePdfService.ConvertToPdf"/> の入力モデル
    /// </summary>
    public class ConvertToPdfInput
    {
        /// <summary>
        /// 変換対象ストリーム
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// 変換対象ファイル名
        /// </summary>
        public string FileName { get; set; }
    }
}
