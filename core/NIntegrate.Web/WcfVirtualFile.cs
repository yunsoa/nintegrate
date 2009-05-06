using System.Globalization;
using System.IO;
using System.Web.Hosting;

namespace NIntegrate.Web
{
    /// <summary>
    /// The VirtualFile implementation for WCF service, 
    /// which is used by the WcfVirtualPathProvider class.
    /// </summary>
    public class WcfVirtualFile : VirtualFile
    {
        private readonly string _service;
        private readonly string _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfVirtualFile"/> class.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="service">The service.</param>
        /// <param name="factory">The factory.</param>
        public WcfVirtualFile(string virtualPath, string service, string factory)

            : base(virtualPath)
        {
            _service = service;
            _factory = factory;
        }

        /// <summary>
        /// When overridden in a derived class, returns a read-only stream to the virtual resource.
        /// </summary>
        /// <returns>A read-only stream to the virtual file.</returns>
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