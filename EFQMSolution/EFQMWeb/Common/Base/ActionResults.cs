using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using EFQMWeb.Common.Util;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using System.Xml.Linq;

namespace EFQMWeb.Common.Base
{
    public abstract class AjaxResponseBase
    {
        public int Status { get; set; }

        public abstract void Jsonize(JsonKeyValueWriter jsonWriter);
    }

    public class Error : AjaxResponseBase
    {
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public Exception Exception { get; set; }

        public override void Jsonize(JsonKeyValueWriter jsonWriter)
        {
            jsonWriter.WriteKeyValue("Status", Status);
            jsonWriter.WriteKeyValue("Message", Message);
            jsonWriter.WriteKeyValue("StackTrace", StackTrace);
        }
    }

    public class FormatActionResult : ActionResult
    {
        public Error Error { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            this.ExecuteResult(context);
        }
    }

    public class SimpleJsonResult : FormatActionResult
    {
        public SimpleJsonResult()
        {
        }

        public SimpleJsonResult(object data)
        {
            this.Data = data;
        }

        public string ContentType { get; set; }

        public Encoding ContentEncoding { get; set; }

        public object Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            byte[] bytes = null;
            StringBuilder sb = new StringBuilder();
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
                response.ContentType = this.ContentType;
            else
                response.ContentType = "application/json";

            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;

            if (this.Data == null)
            {
                using (JsonKeyValueWriter jWriter = new JsonKeyValueWriter(sb))
                using (SPJsonObject jObject = new SPJsonObject(jWriter))
                {
                    jObject.Add("Status", 1 );
                }
            }
            else if (this.Data.GetType() == typeof(PureJson))
            {
                sb = ((PureJson)this.Data).StringBuilder;
            }
            else if (this.Data.GetType() == typeof(String))
            {
                using (JsonKeyValueWriter jWriter = new JsonKeyValueWriter(sb))
                using (SPJsonObject jObject = new SPJsonObject(jWriter))
                {
                    jObject.Add("Status", 0);
                    jObject.Add("Data", this.Data);
                }
            }

            bytes = response.ContentEncoding.GetBytes(sb.ToString());
            response.OutputStream.Write(bytes, 0, bytes.Length);
        }
    }

    
    /*
    public class JsonResultSP : FormatActionResult
    {
        public JsonResultSP()
        {
        }

        public JsonResultSP(Exception ex)
        {
            this.Error = new Error();
            this.Error.Message = ex.Message;
            this.Error.StackTrace = ex.StackTrace;
            this.Error.Exception = ex;
        }

        public JsonResultSP(Exception ex, bool isHandled)
            : this(ex)
        {
            this.Error.IsHandled = isHandled;
        }

        public JsonResultSP(DataTable dataTable)
        {
            this.Data = dataTable;
        }

        public JsonResultSP(String strData)
        {
            this.Data = strData;
        }

        public JsonResultSP(object data)
        {
            this.Data = data;
        }

        public string ContentType { get; set; }

        public Encoding ContentEncoding { get; set; }

        public object Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            byte[] bytes = null;
            StringBuilder sb = new StringBuilder();
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
                response.ContentType = this.ContentType;
            else
                response.ContentType = "application/json";

            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;

            if (Error != null)
            {
                using (JsonKeyValueWriter jWriter = new JsonKeyValueWriter(sb))
                using (SPJsonObject jObject = new SPJsonObject(jWriter))
                {
                    this.Error.Jsonize(jWriter);
                    jObject.Add("IsTestEnvironment", MyConfig.IsTestEnvironment);
                }
                bytes = response.ContentEncoding.GetBytes(sb.ToString());
                response.OutputStream.Write(bytes, 0, bytes.Length);
                return;
            }
            if (this.Data == null)
            {
                using (JsonKeyValueWriter jWriter = new JsonKeyValueWriter(sb))
                using (SPJsonObject jObject = new SPJsonObject(jWriter))
                {
                    SuccessList head = new SuccessList() { Status = 1 };
                    head.Jsonize(jWriter);
                }
            }
            else if (this.Data.GetType() == typeof(EditionViolation))
            {
                using (JsonKeyValueWriter jWriter = new JsonKeyValueWriter(sb))
                using (SPJsonObject jObject = new SPJsonObject(jWriter))
                {
                    this.Error.Jsonize(jWriter);
                    jObject.Add("IsTestEnvironment", MyConfig.IsTestEnvironment);
                }

                bytes = response.ContentEncoding.GetBytes(sb.ToString());
                response.OutputStream.Write(bytes, 0, bytes.Length);
                return;
            }

            //else if (this.Data.GetType() == typeof(ValidationResponse))
            //{
            //     using (JsonKeyValueWriter jWriter = new JsonKeyValueWriter(sb))
            //     using (SPJsonObject jObject = new SPJsonObject(jWriter))
            //     {
            //         ((ValidationResponse)this.Data).Jsonize(jWriter);
            //     }
            //}
            else if (this.Data.GetType() == typeof(PureJson))
            {
                sb = ((PureJson)this.Data).StringBuilder;
            }
            else if (this.Data.GetType() == typeof(DataInRow))
            {
                DataInRow data = (DataInRow)this.Data;
                using (JsonKeyValueWriter jWriter = new JsonKeyValueWriter(sb))
                using (SPJsonObject jObject = new SPJsonObject(jWriter))
                {
                    SuccessList head = new SuccessList() { Status = 0 };
                    if (!data.HasData()) head.Status = 1;
                    head.Jsonize(jWriter);
                    JSONAssembler.GetJSONResultObject(data.Entity, jWriter);
                }
            }
            else if (this.Data.GetType() == typeof(PagedDataList))
            {
                PagedDataList pagedDataList = (PagedDataList)this.Data;
                using (JsonKeyValueWriter jWriter = new JsonKeyValueWriter(sb))
                using (SPJsonObject jObject = new SPJsonObject(jWriter))
                {
                    SuccessList head = new SuccessList() { Status = 0, Count = pagedDataList.Count, Page = pagedDataList.Page, PageSize = pagedDataList.PageSize, PageCount = pagedDataList.PageCount, HasMore = pagedDataList.HasMore };
                    if (!string.IsNullOrEmpty(pagedDataList.Extra))
                        head.Extra = pagedDataList.Extra;
                    head.Jsonize(jWriter);
                    if (pagedDataList.RowDelegate != null)
                    {
                        JSONAssembler.GetJSONResultList(pagedDataList.GetCurrentPage(), jWriter, "List", pagedDataList.RowDelegate);
                    }
                    else
                    {
                        JSONAssembler.GetJSONResultList(pagedDataList.GetCurrentPage(), jWriter, "List");
                    }
                }
            }
            else if (this.Data.GetType() == typeof(String))
            {
                using (JsonKeyValueWriter jWriter = new JsonKeyValueWriter(sb))
                using (SPJsonObject jObject = new SPJsonObject(jWriter))
                {
                    Success head = new Success() { Status = 0 };
                    head.Jsonize(jWriter);
                    jObject.Add("Data", this.Data);
                }
            }
            else if (this.Data.GetType() == typeof(DataTable))
            {
                DataTable table = (DataTable)this.Data;
                using (JsonKeyValueWriter jWriter = new JsonKeyValueWriter(sb))
                using (SPJsonObject jObject = new SPJsonObject(jWriter))
                {
                    SuccessList head = new SuccessList() { Status = 0, Count = table.Rows.Count, Page = 0, PageSize = table.Rows.Count, HasMore = false };
                    head.Jsonize(jWriter);
                    JSONAssembler.GetJSONResultList(table, jWriter, "List");
                }
            }
            else if (this.Data.GetType() == typeof(DataTable))
            {
                DataTable table = (DataTable)this.Data;
                using (JsonKeyValueWriter jWriter = new JsonKeyValueWriter(sb))
                using (SPJsonObject jObject = new SPJsonObject(jWriter))
                {
                    SuccessList head = new SuccessList() { Status = 0, Count = table.Rows.Count, Page = 0, PageSize = table.Rows.Count, HasMore = false };
                    head.Jsonize(jWriter);
                    JSONAssembler.GetJSONResultList(table, jWriter, "List");
                }
            }
            else
            {
                DataContractJsonSerializer serializer =
                  new DataContractJsonSerializer(this.Data.GetType());
                serializer.WriteObject(response.OutputStream, this.Data);
            }

            bytes = response.ContentEncoding.GetBytes(sb.ToString());
            response.OutputStream.Write(bytes, 0, bytes.Length);
        }
    }

     * */
    public class ImageResult : FormatActionResult
    {
        public string ContentType { get; set; }

        public Encoding ContentEncoding { get; set; }

        public Image Image { get; set; }

        public ImageResult(Image image)
        {
            Image = image;
        }

        private string ResolveFileSufix()
        {
            ImageFormat format = this.Image.RawFormat;

            if (format == ImageFormat.Jpeg)
                return "jpg";

            if (format == ImageFormat.Png)
                return "png";

            if (format == ImageFormat.Bmp)
                return "bmp";

            if (format == ImageFormat.Gif)
                return "gif";

            if (format == ImageFormat.Icon)
                return "ico";

            return "jpeg";
        }

        private string ResolveContentType()
        {
            ImageFormat format = this.Image.RawFormat;

            if (format == ImageFormat.Jpeg)
                return "image/jpeg";

            if (format == ImageFormat.Png)
                return "image/png";

            if (format == ImageFormat.Bmp)
                return "image/bmp";

            if (format == ImageFormat.Gif)
                return "image/gif";

            if (format == ImageFormat.Icon)
                return "image/icon";

            return "image/jpeg";
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
                response.ContentType = this.ContentType;
            else
                response.ContentType = ResolveContentType();

            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;

            if (!string.IsNullOrEmpty(context.RequestContext.HttpContext.Request["Download"]))
            {
                string download = context.RequestContext.HttpContext.Request["Download"];
                bool res = false;
                if (Boolean.TryParse(download, out res))
                {
                    string name = Guid.NewGuid().ToString();
                    context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + name + "." + ResolveFileSufix());
                }
            }

            response.Cache.SetExpires(DateTime.Today.AddYears(10));

            if (Image != null)
            {
                this.Image.Save(response.OutputStream, ImageFormat.Jpeg);
            }
        }
    }

    public class ByteResult : FormatActionResult
    {
        public string ContentType { get; set; }

        public Encoding ContentEncoding { get; set; }

        public byte[] Bytes { get; set; }

        public ByteResult(byte[] bytes)
        {
            Bytes = bytes;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = this.ContentType;
            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;

            if (Bytes != null)
            {
                response.OutputStream.Write(Bytes, 0, Bytes.Length);
            }
        }
    }

    public class TextResult : FormatActionResult
    {
        public string ContentType { get; set; }

        public Encoding ContentEncoding { get; set; }

        public string Data { get; set; }

        public bool AsDownLoad { get; set; }

        public string FileName { get; set; }

        public TextResult(String strData)
        {
            this.Data = strData; AsDownLoad = false; FileName = "file";
        }

        public TextResult(String strData, bool download)
        {
            this.Data = strData; AsDownLoad = download; FileName = "file";
        }

        public override void ExecuteResult(ControllerContext context)
        {
            StringBuilder sb = new StringBuilder();
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
                response.ContentType = this.ContentType;
            else
                response.ContentType = "text/plain";

            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;

            if (AsDownLoad)
            {
                context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".csv");
            }
            response.Write('\uFEFF');
            byte[] bytes = response.ContentEncoding.GetBytes(this.Data);
            response.OutputStream.Write(bytes, 0, bytes.Length);
        }
    }
}