using Framework.Configuration;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;

namespace Framework.Services
{

    public class UploadService
    {

        private readonly AppConfiguration _appConfiguration;

        public UploadService(AppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        //public async Task<string> UploadImage(IFormFile file)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        string fName = file.FileName;
        //        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  
        //        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
        //        var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
        //        var extension = ext.ToLower();
        //        if (!AllowedFileExtensions.Contains(extension))
        //        {
        //            return _localizer["jpgerror"];
        //        }
        //        else if (file.Length > MaxContentLength)
        //        {
        //            return string.Format("Please Upload a file upto 1 mb.");
        //        }
        //        else
        //        {
        //            string path = Path.Combine(Path.GetDirectoryName(_appConfiguration.LogfolderURL), "/" + file.FileName);
        //            using (var stream = new FileStream(path, FileMode.Create))
        //            {
        //                await file.CopyToAsync(stream);
        //            }
        //        }
        //    }
        //    return file.FileName;
        //}

        //private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
        //{
        //    using (var image = Image.FromStream(sourcePath))
        //    {
        //        // can given width of image as we want  
        //        var newWidth = (int)(image.Width * scaleFactor);
        //        // can given height of image as we want  
        //        var newHeight = (int)(image.Height * scaleFactor);
        //        var thumbnailImg = new Bitmap(newWidth, newHeight);
        //        var thumbGraph = Graphics.FromImage(thumbnailImg);
        //        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
        //        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
        //        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
        //        thumbGraph.DrawImage(image, imageRectangle);
        //        thumbnailImg.Save(targetPath, image.RawFormat);
        //    }
        //}
    }


}
