using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zalidane.com.Models;

namespace ZalidaneSite.Controllers
{
    public class ImageController : Controller
    {
        /*
        public FileContentResult ShowThumbnail(int id)
        {
            var thumbnailData = GrabImage(id, "thumbnail");

            return File(thumbnailData, "image/jpg");
        }

        public FileContentResult ShowImage(int id)
        {
            var imageData = GrabImage(id, "image");
            return File(imageData, "image/jpg");
        }

        private byte[] GrabImage(int id, string image)
        {
            //ZalidaneBlogConnection db = new ZalidaneBlogConnection();
            CodeSample codeSample = db.CodeSamples.Find(id);

            byte[] imageData;

            switch (image)
            {
                case "thumbnail":
                    imageData = codeSample.Thumbnail;
                    break;
                case "image":
                    imageData = codeSample.Image;
                    break;
                default:
                    imageData = new byte[] { };
                    break;
            }

            return imageData;
        }
        */
    }
}