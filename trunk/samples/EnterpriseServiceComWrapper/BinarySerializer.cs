using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace EnterpriseServiceComWrapper
{
    [ComVisible(true)]
    [Guid("8DCA0554-A5CF-43a3-82D9-BC1B10648BA8")]
    public sealed class BinarySerializer
    {
        public byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            var formatter = new BinaryFormatter();
            var ms = new MemoryStream();
            formatter.Serialize(ms, obj);
            ms.Seek(0, SeekOrigin.Begin);
            return ms.ToArray();
        }

        public object Deserialize(byte[] data)
        {
            if (data == null)
                return null;

            var formatter = new BinaryFormatter();
            var ms = new MemoryStream(data);
            return formatter.Deserialize(ms);
        }
    }
}
