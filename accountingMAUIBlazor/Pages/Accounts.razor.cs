using accountingMAUIBlazor.Shared;
using Microsoft.AspNetCore.Components;

namespace accountingMAUIBlazor.Pages;

public partial class Accounts
{
    [CascadingParameter]
    public MainLayout Layout { get; set; }

    //protected override void OnInitialized()
    //{
    //    Layout.hasErrorGlobal = true;
    //}
}
