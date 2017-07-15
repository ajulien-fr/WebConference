using Windows.UI.Xaml.Controls;

namespace WebConferenceClient.Views
{
    public sealed partial class AddressDialog : ContentDialog
    {
        private string _address;
        public string Address { get => _address; set => _address = value; }

        private string _username;
        public string Username { get => _username; set => _username = value; }

        public AddressDialog()
        {
            this.InitializeComponent();
        }

        private void ConnectButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // Ensure the user name and password fields aren't empty. If a required field
            // is empty, set args.Cancel = true to keep the dialog open.
            if (string.IsNullOrEmpty(this.AddressTextBox.Text))
            {
                args.Cancel = true;
                errorTextBlock.Text = "Address is required.";
            }

            else if(string.IsNullOrEmpty(this.UsernameTextBox.Text))
            {
                args.Cancel = true;
                errorTextBlock.Text = "Username is required.";
            }

            else
            {
                _address = this.AddressTextBox.Text.Trim();
                _username = this.UsernameTextBox.Text.Trim();
            }
        }
    }
}
