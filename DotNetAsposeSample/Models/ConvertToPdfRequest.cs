﻿using System.ComponentModel.DataAnnotations;

namespace DotNetAsposeSample.Models
{
    /// <summary>
    /// PDF変換リクエスト
    /// </summary>
    public class ConvertToPdfRequest
    {
        /// <summary>
        /// 変換対象となるファイル
        /// </summary>
        [Required]
        public IFormFile File { get; set; } = null!;
    }
}
