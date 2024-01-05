using System.ComponentModel.DataAnnotations;

namespace GenerateQRCode.Models
{
    public class QRCodeModel
    {
        [Display(Name = "QRCode Metnini Girin")]
        public string QRCodeText { get; set; }
    }
}
