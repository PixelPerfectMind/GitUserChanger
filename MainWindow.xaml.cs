using GitUserChanger.Templates;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace GitUserChanger {

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            try {
                if (!System.IO.File.Exists(filePath)) {
                    XmlDocument xmlDocument = new XmlDocument();
                    XmlElement xmlElement = xmlDocument.CreateElement("Users");
                    xmlDocument.AppendChild(xmlElement);
                    xmlDocument.Save(filePath);
                }
                LoadUserList();
            } catch (Exception ex) {
                MessageBox.Show("An error occured while trying to load the user list. Please try again later.\n\nError message: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GitConfigChanger.xml";
        public List<UserChooserItem> userChooserItems = new List<UserChooserItem>();

        /// <summary>
        /// Add the credentials to the credential list
        /// </summary>
        private void LoadUserList() {
            try {
                // Clear the list
                userChooserItems.Clear();
                sp_list.Children.Clear();

                // Display the list
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);
                if(xmlDocument.SelectSingleNode("Users/User") != null) {
                    XmlNodeList xmlNodeList = xmlDocument.SelectNodes("Users/User");
                    int index = 0;
                    DateTime dateTime = DateTime.Now;
                    foreach (XmlNode xmlNode in xmlNodeList) {
                        // Try to parse the date
                        try {
                            dateTime = DateTime.Parse(xmlNode.SelectSingleNode("CreatedDate").InnerText);
                        } catch {
                            dateTime = DateTime.Now;
                        }

                        UserChooserItem userChooserItem = new UserChooserItem(
                            xmlNode.SelectSingleNode("Username").InnerText,
                            xmlNode.SelectSingleNode("Email").InnerText,
                            dateTime,
                            xmlNode.SelectSingleNode("GPGKey").InnerText,
                            index
                        );
                        userChooserItems.Add(userChooserItem);
                        index++;
                    }
                }

                // Add the items to the list
                foreach (UserChooserItem userChooserItem in userChooserItems) {
                    sp_list.Children.Add(userChooserItem);
                }
            } catch (Exception ex) {
                MessageBox.Show("An error occured while trying to load the user list. Please try again later.\n\nError message: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txt_linkToCredits1_MouseDown(object sender, MouseButtonEventArgs e) {
            // Open Autism2B2B's Github page in web browser
            System.Diagnostics.Process.Start("https://github.com/Autism2B2B");
        }

        private void txt_linkToCredits2_MouseDown(object sender, MouseButtonEventArgs e) {
            // Open jonasp2004's Github page in web browser
            System.Diagnostics.Process.Start("https://github.com/jonasp2004");
        }

        /// <summary>
        /// Change the credentials in the git config file using cmd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_applyCredentialsDirectly_Click(object sender, RoutedEventArgs e) {
            if(txt_directUsername.Text != "" || txt_email.Text != "") {
                GitConfigChanger gitConfigChanger = new GitConfigChanger();
                gitConfigChanger.ChangeCredentials(txt_directUsername.Text, txt_email.Text);
                if(txt_gpgKey.Text != "") {
                    gitConfigChanger.ChangeGPGKey(txt_gpgKey.Text);
                }
            } else {
                txt_error.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Add the credentials to the list
        /// </summary>
        private void btn_addToList_Click(object sender, RoutedEventArgs e) {
            if (txt_directUsername.Text != "" || txt_email.Text != "") {
                // Create the xml document
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);

                // Create the elements
                XmlElement xmlElement = xmlDocument.CreateElement("User");
                XmlElement xmlElementUsername = xmlDocument.CreateElement("Username");
                XmlElement xmlElementEmail = xmlDocument.CreateElement("Email");
                XmlElement xmlElementCreatedDate = xmlDocument.CreateElement("CreatedDate");
                XmlElement xmlElementGPGKey = xmlDocument.CreateElement("GPGKey");


                // Set the inner text of the elements
                xmlElementUsername.InnerText = txt_directUsername.Text;
                xmlElementEmail.InnerText = txt_email.Text;
                xmlElementCreatedDate.InnerText = DateTime.Now.ToString();
                xmlElementGPGKey.InnerText = txt_gpgKey.Text;

                // Add the elements to the xml document
                xmlElement.AppendChild(xmlElementUsername);
                xmlElement.AppendChild(xmlElementEmail);
                xmlElement.AppendChild(xmlElementCreatedDate);
                xmlElement.AppendChild(xmlElementGPGKey);
                xmlDocument.SelectSingleNode("Users").AppendChild(xmlElement);

                // Save the xml document
                xmlDocument.Save(filePath);

            } else {
                txt_error.Visibility = Visibility.Visible;
            }
            LoadUserList();
        }

        /// <summary>
        /// Hide the error message when the user types in the textbox
        /// </summary>
        private void txt_directUsername_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) { txt_error.Visibility = Visibility.Collapsed; }

        private void scr_list_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e) {
            if (scr_list.VerticalOffset == 0) {
                brdr_fadeOut0.Visibility = Visibility.Collapsed;
            } else {
                brdr_fadeOut0.Visibility = Visibility.Visible;
            }

            if (scr_list.VerticalOffset == scr_list.ScrollableHeight) {
                brdr_fadeOut1.Visibility = Visibility.Collapsed;
            } else {
                brdr_fadeOut1.Visibility = Visibility.Visible;
            }
        }
    }
}