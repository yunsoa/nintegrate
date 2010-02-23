using System;

public class RowSelectedEventArgs : EventArgs
{
    private object _selectedRowDataItem = null;

    public RowSelectedEventArgs(Object selectedRowDataItem)
    {
        this._selectedRowDataItem = selectedRowDataItem;
    }

    public Object SelectedRowDataItem
    {
        get { return this._selectedRowDataItem; }
    }    
}
