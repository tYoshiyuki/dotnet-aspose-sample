using System.Runtime.Serialization;

namespace DotnetAsposeSample.Lib
{
    /// <summary>
    /// PDFサービスの例外クラス
    /// </summary>
    public class AsposePdfServiceException : Exception
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AsposePdfServiceException(){ }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="message"></param>
        public AsposePdfServiceException(string? message) : base(message) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public AsposePdfServiceException(string? message, Exception exception) : base(message, exception) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected AsposePdfServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
