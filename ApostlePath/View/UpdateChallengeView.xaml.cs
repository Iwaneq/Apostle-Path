using CommunityToolkit.Maui.Views;

namespace ApostlePath.View;

public partial class UpdateChallengeView : Popup
{
	public UpdateChallengeView()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await CloseAsync(true);
    }
}