using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;

namespace FreelanceAplication
{
    public partial class CustomerProjectsWindow : Window
    {
        private string userEmail;

        public CustomerProjectsWindow(string email)
        {
            InitializeComponent();
            userEmail = email;

            EmailTextBlock.Text = $"Email: {userEmail}";

            LoadUserProjects();
        }

        private void LoadUserProjects()
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=users.db;Version=3;"))
                {
                    conn.Open();
                    string query = "SELECT title FROM projects WHERE customer_email = @Email";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", userEmail);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProjectsListBox.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження проєктів: " + ex.Message);
            }
        }

        private void CreateProjectButton_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateProjectWindow(userEmail);
            createWindow.ShowDialog();
        }

        private void ProfileTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var profileWindow = new CustomerWindow(userEmail);
            profileWindow.Show();
            this.Close();
        }

        private void FreelancersTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var freelancersWindow = new FreelancerListWindow(userEmail);
            freelancersWindow.Show();
            this.Close();
        }

        private void LogoutTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
