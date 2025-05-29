using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;

namespace FreelanceAplication
{
    public partial class FreelancerWindow : Window
    {
        private string userEmail;

        public FreelancerWindow(string email)
        {
            InitializeComponent();
            userEmail = email;
            LoadUserData();
        }

        private void LoadUserData()
        {
            EmailTextBlockMain.Text = $"Email: {userEmail}";

            try
            {
                using (var conn = new SQLiteConnection("Data Source=users.db;Version=3;"))
                {
                    conn.Open();

                    string projQuery = @"
                                        SELECT p.title
                                        FROM freelancer_projects fp
                                        JOIN projects p ON fp.project_id = p.id
                                        WHERE fp.freelancer_email = @Email
                                        ";
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
            FreelancerWindow freelanceWindow = new FreelancerWindow(userEmail);
            freelanceWindow.Show();
            this.Close();
        }

        private void ProjectsTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectsListWindow projectsWindow = new ProjectsListWindow(userEmail);
            projectsWindow.Show();
            this.Close();
        }


    }
}
