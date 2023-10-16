using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_iOS_TableView_Bug.DynamicTable;

public static class SectionsFactory
{
    public static TableSection CreateSection(Section section)
    {
        var tableSection = new TableSection(section.Title);

        foreach (var row in section.Rows)
        {
            if (row is TextValueRow)
                tableSection.Add(GetTextValueRow(row as TextValueRow));
            else if (row is ButtonRow)
                tableSection.Add(GetButtonRow(row as ButtonRow));
        }

        return tableSection;
    }

    private static Cell GetTextValueRow(TextValueRow row)
    {
        var grid = new Grid
        {
            HeightRequest = 32,
            Margin = new Thickness(15, 2, 0, 2),
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) },
                new ColumnDefinition()
            }
        };

        grid.Add(new Label() { Text = row.Title, VerticalTextAlignment = TextAlignment.Center }, 0, 0);
        Application.Current.Resources.TryGetValue("TextValue", out var style);
        grid.Add(new Label
        {
            Text = row.Value,
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            Style = style as Style
        }, 1, 0);

        grid.GestureRecognizers.Add(new TapGestureRecognizer() { Command = row.ClickCommand });

        return new ViewCell { View = grid };
    }

    private static Cell GetButtonRow(ButtonRow row)
    {
        var button = new Button
        {
            Text = row.Title,
            HeightRequest = 45,
            WidthRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(10, 0),
            Padding = new Thickness(0)
        };
        button.Clicked += (sender, e) => row.OnClickAction?.Invoke();

        return new ViewCell { View = button };
    }
}


