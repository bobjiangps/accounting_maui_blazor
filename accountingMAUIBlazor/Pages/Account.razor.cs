using Microsoft.AspNetCore.Components;

namespace accountingMAUIBlazor.Pages;


public partial class Account
{

    [Parameter]
    public int? Id { get; set; }

    protected override void OnParametersSet()
    {
        Id = Id ?? 0;
    }
}

