using System;
using System.Collections.Generic;

namespace PTIBlazorVideoInsightsCourse.Shared.Models
{
    public class GetAllPersonsModel
    {
        public List<GettAllPersonsModelItem> Persons { get; set; }
    }

    public class GettAllPersonsModelItem
    {
        public string PersonName { get; set; }
        public string PersonPictureBase64 { get; set; }
    }
}
