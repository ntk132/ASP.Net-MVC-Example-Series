using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace WebApp.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        // GET
        public string LoadMedia()
        {
            string root = "/Images";
            string realRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            List<String> list = LoadFiles(root, realRoot);

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(list); // Convert to JSON

            return json;
        }

        // GET
        public ActionResult Upload()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files">List of files is going to be uploaded to the server</param>
        /// <returns>//List of pathes 's all file in server (included new list)</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid && files != null)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    try
                    {
                        if (file.ContentLength > 0)
                        {
                            string _path = Path.Combine(Server.MapPath("~/Images/Desktop/Origin"), file.FileName);
                            file.SaveAs(_path);
                        }

                        ViewBag.Message = "Upload successful!";
                    }
                    catch
                    {
                        ViewBag.Message = "Upload fail!";
                    }
                }
            }

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">The path is going to be used to display img</param>
        /// <param name="realPath">The real path - full directories to the exactly path</param>
        /// <returns>List of pathes's all files uploaded in server</returns>
        public List<String> LoadFiles(String path, String realPath)
        {
            DirectoryInfo dir = new DirectoryInfo(realPath);
            List<String> list = new List<string>();

            if (dir.GetDirectories().Count() > 0)
            {
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    list.AddRange(LoadFiles(path + "/" + subDir.Name, Path.Combine(realPath, subDir.Name).ToString()));
                }
            }
            else
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    list.Add(path + "/" + file.Name);
                }
            }

            return list;
        }
    }
}