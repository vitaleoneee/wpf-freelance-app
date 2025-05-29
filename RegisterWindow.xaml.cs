using FreelanceAplication.Data;
using FreelanceAplication.Models;
using System;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace FreelanceAplication
{
    public partial class RegisterWindow : Window, IDisposable
    {
        private string dbPath = "users.db";

        public RegisterWindow()
        {
            InitializeComponent();
            CreateDatabaseIfNotExists();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User
            {
                Username = UsernameTextBox.Text.Trim(),
                Email = EmailTextBox.Text.Trim(),
                Phone = PhoneTextBox.Text.Trim(),
                Description = DescriptionTextBox.Text.Trim(),
                Password = PasswordBox.Password,
                Role = (RoleComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content?.ToString()
            };
            if (string.IsNullOrEmpty(newUser.Username) || string.IsNullOrEmpty(newUser.Email) ||
                string.IsNullOrEmpty(newUser.Password) || string.IsNullOrEmpty(newUser.Role))
            {
                MessageBox.Show("Будь ласка, заповніть усі обов’язкові поля.");
                return;
            }
            if (!IsValidEmail(newUser.Email))
            {
                MessageBox.Show("Введено некоректну електронну адресу.");
                return;
            }
            if (newUser.Password.Length < 6)
            {
                MessageBox.Show("Пароль повинен містити мінімум 6 символів.");
                return;
            }
            if (!string.IsNullOrEmpty(newUser.Phone) && !IsValidPhone(newUser.Phone))
            {
                MessageBox.Show("Введено некоректний номер телефону.");
                return;
            }

            try
            {
                using (var db = new RegisterDbManager())
                {
                    db.RegisterUser(newUser);
                }

                MessageBox.Show("Користувача зареєстровано!");
                ClearForm();
                new LoginWindow().Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPhone(string phone)
        {
            string pattern = @"^[\d\s\+\-\(\)]{5,20}$";
            return Regex.IsMatch(phone, pattern);
        }

        private void CreateDatabaseIfNotExists()
        {
            if (!File.Exists(dbPath))
                SQLiteConnection.CreateFile(dbPath);

            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();

                string createUsersTable = @"
                    CREATE TABLE IF NOT EXISTS users (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        username TEXT NOT NULL,
                        email TEXT NOT NULL UNIQUE,
                        phone TEXT,
                        description TEXT,
                        password TEXT NOT NULL,
                        role TEXT NOT NULL
                    );
                ";
                new SQLiteCommand(createUsersTable, conn).ExecuteNonQuery();

                string createProjectsTable = @"
                    CREATE TABLE IF NOT EXISTS projects (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        customer_email TEXT NOT NULL,
                        title TEXT NOT NULL,
                        description TEXT,
                        category TEXT,
                        budget REAL,
                        deadline DATE,
                        FOREIGN KEY (customer_email) REFERENCES users(email)
                    );
                ";
                new SQLiteCommand(createProjectsTable, conn).ExecuteNonQuery();

                string createFreelancerProjectsTable = @"
                    CREATE TABLE IF NOT EXISTS freelancer_projects (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        freelancer_email TEXT NOT NULL,
                        project_id INTEGER NOT NULL,
                        assigned_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (freelancer_email) REFERENCES users(email),
                        FOREIGN KEY (project_id) REFERENCES projects(id)
                    );
                ";
                new SQLiteCommand(createFreelancerProjectsTable, conn).ExecuteNonQuery();
            }
        }

        private void ClearForm()
        {
            UsernameTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneTextBox.Text = "";
            DescriptionTextBox.Text = "";
            PasswordBox.Password = "";
            RoleComboBox.SelectedIndex = -1;
        }

        public void Dispose() { }
    }

    public class RegisterDbManager : DatabaseManager, IDisposable
    {
        public void RegisterUser(User user)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO users (username, email, phone, description, password, role) VALUES (@username, @email, @phone, @description, @password, @role)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@phone", user.Phone);
                    cmd.Parameters.AddWithValue("@description", user.Description);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Dispose() { }
    }
}
