using System.Windows.Forms;
using Daten;
using MediaProvider.Extensions;

namespace MediaProvider
{
    public partial class SpotifyConfigControl : UserControl
    {
        public SpotifyConfigControl(Daten.DatenInterface data)
        {
            InitializeComponent();
            Data = data;
        }

        private DatenInterface Data { get; }

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            Data.write_to_table("SpotifyData", 0, UserEdit.Text);
            Data.write_to_table("SpotifyData", 1, PasswordEdit.Text.EncryptString());
        }

        private void SpotifyConfigControl_Load(object sender, System.EventArgs e)
        {
            UserEdit.Text = Data.read_from_table("SpotifyData", 0);
            PasswordEdit.Text = Data.read_from_table("SpotifyData", 1).DecryptString();
        }
    }
}
