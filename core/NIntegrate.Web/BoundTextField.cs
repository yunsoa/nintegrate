using System.Web.UI.WebControls;

namespace NIntegrate.Web
{
    public class BoundTextField : BoundFieldBase
    {
        protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            base.InitializeDataCell(cell, rowState);

            if (cell != null && cell.Controls.Count > 0)
            {
                var textBox = cell.Controls[0] as TextBox;
                if (textBox != null)
                {
                    textBox.Height = FieldHeight;
                    textBox.Width = FieldWidth;
                }
            }
        }
    }
}
