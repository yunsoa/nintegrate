using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace NIntegrate.Web
{
    public class JSONPModule : IHttpModule
    {
        private const string JSON_CONTENT_TYPE = "application/json";
        private const string JS_CONTENT_TYPE = "text/javascript";

        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(HttpApplication app)
        {
            app.ReleaseRequestState += OnReleaseRequestState;
        }

        #endregion

        public void OnReleaseRequestState(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpResponse response = app.Response;
            if (response.ContentType.ToLowerInvariant().Contains(JSON_CONTENT_TYPE)
                && !string.IsNullOrEmpty(app.Request.Params["jsoncallback"]))
            {
                response.ContentType = JS_CONTENT_TYPE;
                response.Filter = new JsonResponseFilter(response.Filter);
            }
        }
    }

    public class JsonResponseFilter : Stream
    {
        private readonly Stream _responseStream;
        private long _position;

        private bool _isContinueBuffer;

        public JsonResponseFilter(Stream responseStream)
        {
            _responseStream = responseStream;
        }

        public override bool CanRead { get { return true; } }

        public override bool CanSeek { get { return true; } }

        public override bool CanWrite { get { return true; } }

        public override long Length { get { return 0; } }

        public override long Position { get { return _position; } set { _position = value; } }

        public override void Write(byte[] buffer, int offset, int count)
        {
            string strBuffer = Encoding.UTF8.GetString(buffer, offset, count);
            strBuffer = AppendJsonpCallback(strBuffer, HttpContext.Current.Request);
            byte[] data = Encoding.UTF8.GetBytes(strBuffer);
            _responseStream.Write(data, 0, data.Length);
        }

        private string AppendJsonpCallback(string strBuffer, HttpRequest request)
        {
            string prefix = string.Empty;
            string suffix = string.Empty;

            if (!_isContinueBuffer)
            {
                strBuffer = RemovePrefixComments(strBuffer);

                if (strBuffer.StartsWith("{"))
                    prefix = request.Params["jsoncallback"] + "(";
            }
            if (strBuffer.EndsWith("}"))
            {
                suffix = ");";
            }
            _isContinueBuffer = true;
            return prefix + strBuffer + suffix;
        }

        private string RemovePrefixComments(string strBuffer)
        {
            var str = strBuffer.TrimStart();
            while (str.StartsWith("/*"))
            {
                var pos = str.IndexOf("*/", 2);
                if (pos <= 0)
                    break;
                str = str.Substring(pos + 2);
                str = str.TrimStart();
            }
            return str;
        }

        public override void Close()
        {
            _responseStream.Close();
        }

        public override void Flush()
        {
            _responseStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _responseStream.Seek(offset, origin);
        }

        public override void SetLength(long length)
        {
            _responseStream.SetLength(length);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _responseStream.Read(buffer, offset, count);
        }
    }
}


