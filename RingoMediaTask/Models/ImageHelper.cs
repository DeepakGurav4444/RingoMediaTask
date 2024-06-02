using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace RingoMediaTask.Models
{
    public static  class ImageHelper
    {
        public static string ConvertByteArrayToBase64(byte[] imageBytes) 
        {
            return Convert.ToBase64String(imageBytes);
        }

        public static string GetMimeTypeFromImageByteArray(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
            using (Image image = Image.FromStream(stream))
            {
                return ImageCodecInfo.GetImageEncoders().First(codec => codec.FormatID == image.RawFormat.Guid).MimeType;
            }
        }
    }
}
