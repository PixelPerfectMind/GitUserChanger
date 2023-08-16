using System;
using System.Windows;

namespace GitUserChanger {
    internal class GitConfigChanger {

        /// <summary>
        /// Change the credentials in the git config file using cmd
        /// </summary>
        /// <param name="username">The username, the user wants to apply</param>
        /// <param name="email">The e-mail adress, the user wants to apply</param>
        public void ChangeCredentials(string username, string email) {
            if (MessageBox.Show("Do you want to apply the credentials \"" + username + "\" and \"" + email + "\" in your git config?", "Apply credentials", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                try {
                    // Run cmd commands
                    System.Diagnostics.Process.Start("cmd.exe", "/C git config --global user.name \"" + username + "\"");
                    System.Diagnostics.Process.Start("cmd.exe", "/C git config --global user.email \"" + email + "\"");
                    MessageBox.Show("The credentials were applied successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } catch (Exception ex) {
                MessageBox.Show("An error occured while trying to apply the credentials directly. Please try again later.\n\nError message: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        }
    }
}
