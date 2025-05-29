using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;

namespace FreelanceAplication
{
    public partial class CustomerWindow : Window
    {
        private string userEmail;

        public CustomerWindow(string email)
        {
            InitializeComponent();
            userEmail = email;

            WelcomeTextBlock.Text = $"Ласкаво просимо!";
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

                    string projQuery = "SELECT title FROM projects WHERE customer_email = @Email";
                    using (var cmd = new SQLiteCommand(projQuery, conn))
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
                MessageBox.Show($"Помилка завантаження даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogoutTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void ProfileTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow(userEmail);
            customerWindow.Show();
            this.Close();
        }
        private void FreelancersTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var freelancerListWindow = new FreelancerListWindow(userEmail);
            freelancerListWindow.Show();
            this.Close();
        }
        private void ProjectsTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var projectsWindow = new CustomerProjectsWindow(userEmail);
            projectsWindow.Show();
            this.Close();
        }

    }
}