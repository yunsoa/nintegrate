using System.Globalization;
using System.IO;
using System.Web.Hosting;

namespace NIntegrate.Web
{
    public class WcfVirtualFile : VirtualFile
    {
        private readonly string _service;
        private readonly string _factory;

        public WcfVirtualFile(string virtualPath, string service, string factory)

            : base(virtualPath)
        {
            _service = service;
            _factory = factory;
        }

        public override Stream Open()
        {
            var ms = new MemoryStream();
            var tw = new StreamWriter(ms);
            tw.Write(string.Format(CultureInfo.InvariantCulture,
              "<%@ServiceHost Service=\"{0}\" Factory=\"{1}\"%>",
              _service, _factory));
            tw.Flush();
            ms.Position = 0;
            return ms;
        }
    }
}