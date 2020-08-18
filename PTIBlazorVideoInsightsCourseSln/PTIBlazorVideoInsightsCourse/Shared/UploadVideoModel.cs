using System;
using System.ComponentModel.DataAnnotations;

namespace PTIBlazorVideoInsightsCourse.Shared
{
    public class UploadVideoModel
    {
        [Required]
        public string Name { get; set; }
        public string CallbackUrl { get; set; }
        [Required]
        [Url]
        public string VideoUrl { get; set; }
        public bool SendSuccessEmail { get; set; }
    }
}
