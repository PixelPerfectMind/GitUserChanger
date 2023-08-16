using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GitUserChanger.Templates {

    public partial class UserChooserItem : UserControl {

        /// <summary>
        /// Set the username and e-mail adress of the user
        /// </summary>
        public UserChooserItem(string userName, string eMail, DateTime createdDate, int index) {
            InitializeComponent();
            txt_userName.Text = userName;
            txt_email.Text = eMail;
            txt_createdDate.Text = "Added: " + createdDate.ToString("dd. MMM. yyyy");
            ApplyBackgroundColor(index);
        }

        /// <summary>
        /// Set the background of this template dynamically, if the index is even or odd
        /// </summary>
        /// <param name="index">Index of the instance of this template in the list</param>
        private void ApplyBackgroundColor(int index) {
            if (index % 2 == 0) {
                this.Background = new SolidColorBrush(Color.FromArgb(125, 240, 240, 240));
            }
        }

        /// <summary>
        /// Set the git username and e-mail adress of the user
        /// </summary>
        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e) {
            GitConfigChanger gitConfigChanger = new GitConfigChanger();
            gitConfigChanger.ChangeCredentials(txt_userName.Text, txt_email.Text);
        }
    }
}
