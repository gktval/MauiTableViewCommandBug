using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Maui_iOS_TableView_Bug.DynamicTable;

public class SectionsHelper : BindableObject
{
    public event EventHandler<Section> SectionChanged;
    public ObservableCollection<Section> Sections { get; private set; }

    public SectionsHelper()
    {
        this.SetModel();
    }

    private void SetModel()
    {
        var basicSection = new Section { Title = "Settings" };
        var buttonSection = new Section { Title = "" };
        for (int i = 0; i < 10; i++)
        {
            this.AddTextRowIfNotEmpty(basicSection.Rows, "Content " + i.ToString());
        }

        buttonSection.Rows.Add(new ButtonRow
        {
            Title = "Click me",
            OnClickAction = () => this.CreateTemplate()
        });

        var sections = new List<Section> {
            basicSection,
            buttonSection
        };

        var nonEmptySections = sections.Where(x => x.Rows.Any()).ToList();
        this.Sections = new ObservableCollection<Section>(nonEmptySections);

        // notify view, that sections have been changed
        this.OnPropertyChanged(nameof(this.Sections));
    }

    private async void CreateTemplate()
    {
        await App.Current.MainPage.DisplayAlert("This does nothing", "Click the rows above to test. Then navigate back to this page.", "OK");
    }

    private void AddTextRowIfNotEmpty(IList<ISectionRow> rows, string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            rows.Add(new TextValueRow
            {
                Title = name,
                Value = name,
                ClickCommand = new Command(() => OnClick(name))
            });
        }
    }

    private async void OnClick(string name)
    {
        await App.Current.MainPage.DisplayAlert("Click", name + " was clicked", "OK");
        SetModel();
        SectionChanged?.Invoke(this, null);
    }
}
