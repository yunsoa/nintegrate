using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EnterpriseServiceComWrapper
{
    [ComVisible(true)]
    [Guid("8FFEE765-C6F9-467f-BDBF-16A08AEA244C")]
    public class Service
    {
        public int Service_id { get; set; }
        public string ServiceName { get; set; }
        public string HostXML { get; set; }
    }

    [ComVisible(true)]
    [Guid("FDB90BC8-8AEA-42b4-A83E-1AFE195DC509")]
    public class ServiceCollection : ICollection<Service>
    {
        #region Private Fields

        private readonly List<Service> _list = new List<Service>();

        #endregion

        #region ICollection<Service> Members

        public void Add(Service item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(Service item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(Service[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Service item)
        {
            return _list.Remove(item);
        }

        #endregion

        #region IEnumerable<Service> Members

        public IEnumerator<Service> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion
    }
}
