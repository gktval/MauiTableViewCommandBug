using System;

namespace Maui_iOS_TableView_Bug.DynamicTable;

public static class DynamicTableSections
{
    public static readonly BindableProperty AttachBehaviorProperty =
        BindableProperty.CreateAttached("SectionsProperty",
            typeof(IList<Section>),
            typeof(DynamicTableSections),
            null,
            BindingMode.OneWay,
            propertyChanged: SectionsChanged);

    public static IList<Section> GetAttachBehavior(BindableObject view)
    {
        return (IList<Section>)view.GetValue(AttachBehaviorProperty);
    }

    public static void SetAttachBehavior(BindableObject view, IList<Section> value)
    {
        view.SetValue(AttachBehaviorProperty, value);
    }

    static void SectionsChanged(BindableObject view, object oldVal, object newVal)
    {
        // when sections change we need to rebuild our TableView content
        TableView tableView = view as TableView;
        var newSections = (IList<Section>)newVal;

        tableView.Root.Clear();

        if (newSections == null)
        {
            return;
        }

        foreach (var section in newSections)
        {
            tableView.Root.Add(SectionsFactory.CreateSection(section));
        }
    }
}

