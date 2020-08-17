using System;
namespace PTIBlazorVideoInsightsCourse.Shared
{
    public class UploadVideoModel
    {
        public string Name { get; set; }
        public string CallbackUrl { get; set; }
        public string VideoUrl { get; set; }
        public bool SendSuccessEmail { get; set; }
    }
}
