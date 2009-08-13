using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml;

namespace NIntegrate.Web
{
    public class BoundXmlField : BoundTextField
    {
        protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            base.InitializeDataCell(cell, rowState);

            if (cell != null && cell.Controls.Count > 0)
            {
                var textBox = cell.Controls[0] as TextBox;
                if (textBox != null)
                {
                    textBox.TextMode = TextBoxMode.MultiLine;
                }
            }
        }

        protected override object GetValue(System.Web.UI.Control controlContainer)
        {
            var xml = base.GetValue(controlContainer) as string;

            if (!string.IsNullOrEmpty(xml))
            {
                return FormatXml(xml);
            }

            return xml;
        }

        private string FormatXml(string sUnformattedXml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(sUnformattedXml);

            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw) {Formatting = Formatting.Indented};
                doc.WriteTo(xtw);
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
            }

            return sb.ToString();
        }
    }
}
