using System;
using System.ComponentModel.DataAnnotations;

namespace PTIBlazorVideoInsightsCourse.Shared
{
    public class UploadVideoModel
    {
        [Required(ErrorMessageResourceName ="RequiredName",
            ErrorMessageResourceType =typeof(Resources.UploadVideoModel))]
        public string Name { get; set; }
        public string CallbackUrl { get; set; }
        [Required(ErrorMessageResourceName = "RequiredVideoUrl",
            ErrorMessageResourceType = typeof(Resources.UploadVideoModel))]
        [Url(ErrorMessageResourceName = "InvalidVideoUrlFormat",
            ErrorMessageResourceType = typeof(Resources.UploadVideoModel))]
        public string VideoUrl { get; set; }
        public bool SendSuccessEmail { get; set; }
    }
}
