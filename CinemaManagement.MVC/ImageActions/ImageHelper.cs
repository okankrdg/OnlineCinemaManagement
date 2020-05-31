using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaManagement.MVC.ImageActions
{
    public static class ImageHelper
    {
        public static void DeleteImage(this Controller controller, string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var absPhotoPath = Path.Combine(controller.Server.MapPath("~/Upload/"), fileName);

                if (System.IO.File.Exists(absPhotoPath))
                {
                    System.IO.File.Delete(absPhotoPath);
                }
            }
        }

        public static string SaveImage(this Controller controller, HttpPostedFileBase image)
        {
            if (image == null)
                return "";

            string directory = controller.Server.MapPath("~/Upload/");
            string fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            string savePath = Path.Combine(directory, fileName);

            image.SaveAs(savePath);

            return fileName;
        }
        public static string ImagePath(this UrlHelper urlHelper, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return urlHelper.Content("~/Images/img-not-found.png");
            }

            return urlHelper.Content("~/Upload/" + fileName);
        }
    }
}