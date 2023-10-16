using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_iOS_TableView_Bug.DynamicTable;

public interface ISectionRow
{
    string Title { get; set; }
    string Value { get; set; }
}

public class TextValueRow : ISectionRow
{
    public string Title { get; set; }
    public string Value { get; set; }
    public Command ClickCommand { get; set; }
}

public class ButtonRow : ISectionRow
{
    public string Title { get; set; }
    public string Value { get; set; }
    public Action OnClickAction { get; set; }
}

public class Section
{
    public string Title { get; set; }
    public ISectionRow RowHeader { get; set; }
    public IList<ISectionRow> Rows { get; } = new List<ISectionRow>();
}

