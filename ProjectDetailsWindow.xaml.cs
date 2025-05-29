using System;
using System.Data.SQLite;
using System.Windows;
using FreelanceAplication.Models;

namespace FreelanceAplication
{
    public partial class ProjectDetailsWindow : Window
    {
        private Project project;
        private string currentFreelancerEmail;

        public ProjectDetailsWindow(Project project, string freelancerEmail)
        {
            InitializeComponent();

            this.project = project;
            this.currentFreelancerEmail = freelancerEmail;

            TitleText.Text = project.Title;
            CategoryText.Text = project.Category;
            DescriptionText.Text = project.Description;

            JoinButton.Click += JoinButton_Click;
        }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var conn = new SQLiteConnection($"Data Source=users.db;Version=3;"))
                {
                    conn.Open();

                    // Проверка, если уже есть такая запись
                    string checkQuery = "SELECT COUNT(*) FROM freelancer_projects WHERE freelancer_email=@freelancerEmail AND project_id=@projectId";
                    using (var checkCmd = new SQLiteCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@freelancerEmail", currentFreelancerEmail);
                        checkCmd.Parameters.AddWithValue("@projectId", project.Id);

                        object result = checkCmd.ExecuteScalar();
                        long count = Convert.ToInt64(result);
                        if (count > 0)
                        {
                            MessageBox.Show("Ви вже приєдналися до цього проєкту.");
                            return;
                        }
                    }

                    // Добавление записи
                    string insertQuery = "INSERT INTO freelancer_projects (freelancer_email, project_id) VALUES (@freelancerEmail, @projectId)";
                    using (var insertCmd = new SQLiteCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@freelancerEmail", currentFreelancerEmail);
                        insertCmd.Parameters.AddWithValue("@projectId", project.Id);

                        insertCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Ви успішно приєдналися до проєкту!");

                // Закрыть окно с деталями
                this.Close();

                // Если нужно — открыть главное окно, например ProjectsListWindow
                var mainWindow = new ProjectsListWindow(currentFreelancerEmail); // если нужен email
                mainWindow.Show();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Помилка при приєднанні до проєкту: " + ex.Message);
            }
        }
    }
}
