using System.Collections.ObjectModel;
using System.Reflection.PortableExecutable;
using Maui_iOS_TableView_Bug.DynamicTable;

namespace Maui_iOS_TableView_Bug;

public partial class MainPage : ContentPage
{

    public ObservableCollection<Section> Sections { get; private set; }
    public MainPage()
    {
        InitializeComponent();

        var sectionHelper = new SectionsHelper();
        sectionHelper.SectionChanged += (s, e) => OnSectionChanged(sectionHelper);
        OnPropertyChanged(nameof(Sections));

        Sections = sectionHelper.Sections;
        BindingContext = this;
    }


    private void OnSectionChanged(SectionsHelper sectionHelper)
    {
        Sections = sectionHelper.Sections;
        OnPropertyChanged(nameof(Sections));
    }
}


