using System.ComponentModel.DataAnnotations;

namespace QR_Builder.Models
{
    public class QRCodeModel
    {
        [Display(Name = "Enter QRCode Text")]
        public string QRCodeText { get; set; }
    }
}