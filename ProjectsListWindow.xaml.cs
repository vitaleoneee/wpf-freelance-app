using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FreelanceAplication.Models;

namespace FreelanceAplication
{
    public partial class ProjectsListWindow : Window
    {
        private const string ConnectionString = "Data Source=users.db";

        private string userEmail;

        private List<Project> projects;

        public ProjectsListWindow(string email)
        {
            InitializeComponent();
            userEmail = email;
            LoadProjects();
        }

        private void LoadProjects()
        {
            projects = new List<Project>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT id, title, description, category FROM projects";
                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        projects.Add(new Project
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Title = reader["title"].ToString(),
                            Description = reader["description"].ToString(),
                            Category = reader["category"].ToString()
                        });
                    }
                }
            }

            ProjectsList.ItemsSource = projects;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var project = button.DataContext as Project;
            if (project == null) return;

            var detailsWindow = new ProjectDetailsWindow(project, userEmail);
            detailsWindow.ShowDialog();
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
