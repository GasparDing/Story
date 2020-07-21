using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Story.WebApplication.Controllers
{
    [DataContract]
    public class ChunkMetaData
    {
        [DataMember(Name = "uploadUid")]
        public string UploadUid { get; set; }
        [DataMember(Name = "fileName")]
        public string FileName { get; set; }
        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }
        [DataMember(Name = "chunkIndex")]
        public long ChunkIndex { get; set; }
        [DataMember(Name = "totalChunks")]
        public long TotalChunks { get; set; }
        [DataMember(Name = "totalFileSize")]
        public long TotalFileSize { get; set; }
    }

    public class FileResult
    {
        public bool uploaded { get; set; }
        public string fileUid { get; set; }
    }

    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        //public ActionResult Save(HttpPostedFileBase file)
        public ActionResult ChunkSave(IEnumerable<HttpPostedFileBase> files, string metaData)
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(metaData));
            var serializer = new DataContractJsonSerializer(typeof(ChunkMetaData));
            ChunkMetaData somemetaData = serializer.ReadObject(ms) as ChunkMetaData;
            string path = String.Empty;
            if (files != null)
            {
                foreach (var file in files)
                {
                    //path = Path.Combine(Server.MapPath("~/App_Data"), somemetaData.FileName);
                    //AppendToFile(path, file.InputStream);
                }
            }

            FileResult fileBlob = new FileResult();
            fileBlob.uploaded = somemetaData.TotalChunks - 1 <= somemetaData.ChunkIndex;
            fileBlob.fileUid = somemetaData.UploadUid;

            return Json(fileBlob);

            //if (Request.Files["Attachment"] is HttpPostedFileBase file)
            //{
            //    var fileName = Path.GetFileName(file.FileName);
            //    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

            //    file.SaveAs(physicalPath);
            //}

            //return Content(string.Empty);
        }

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Show(string id)
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
