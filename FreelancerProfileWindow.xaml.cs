using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;

namespace FreelanceAplication
{
    public partial class FreelancerProfileWindow : Window
    {
        private string dbPath = "users.db";
        private string username;
        private string customerEmail;

        public FreelancerProfileWindow(string freelancerUsername, string customerEmail)
        {
            InitializeComponent();
            this.username = freelancerUsername;
            this.customerEmail = customerEmail;
            LoadFreelancerData();
        }

        private void LoadFreelancerData()
        {
            try
            {
                using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    conn.Open();

                    string query = "SELECT username, email, phone, description FROM users WHERE username = @username AND role = 'Фрілансер' LIMIT 1";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UsernameTextBlock.Text = reader["username"].ToString();
                                DescriptionTextBlock.Text = reader["description"].ToString();
                                EmailTextBlock.Text = reader["email"].ToString();
                                PhoneTextBlock.Text = reader["phone"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Фрілансер не знайдений!");
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження профілю: " + ex.Message);
                this.Close();
            }
        }


        private void JoinTeamButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string freelancerEmail = EmailTextBlock.Text;

                using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    conn.Open();
                    string getProjectQuery = "SELECT id FROM projects WHERE customer_email = @customerEmail ORDER BY id DESC LIMIT 1";
                    int? projectId = null;

                    using (var getCmd = new SQLiteCommand(getProjectQuery, conn))
                    {
                        getCmd.Parameters.AddWithValue("@customerEmail", customerEmail);
                        var result = getCmd.ExecuteScalar();
                        if (result != null)
                        {
                            projectId = Convert.ToInt32(result);
                        }
                    }

                    if (projectId == null)
                    {
                        MessageBox.Show("У вас ще немає проектів для додавання фрілансера.");
                        return;
                    }

       
                    string checkQuery = "SELECT COUNT(*) FROM freelancer_projects WHERE freelancer_email = @freelancerEmail AND project_id = @projectId";
                    using (var checkCmd = new SQLiteCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@freelancerEmail", freelancerEmail);
                        checkCmd.Parameters.AddWithValue("@projectId", projectId.Value);
                        long count = (long)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Цей фрілансер вже доданий до проекту.");
                            return;
                        }
                    }


                    string insertQuery = "INSERT INTO freelancer_projects (freelancer_email, project_id) VALUES (@freelancerEmail, @projectId)";
                    using (var insertCmd = new SQLiteCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@freelancerEmail", freelancerEmail);
                        insertCmd.Parameters.AddWithValue("@projectId", projectId.Value);
                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Фрілансера успішно додано до вашого проекту!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка додавання фрілансера до проекту: " + ex.Message);
            }
        }


    }
}