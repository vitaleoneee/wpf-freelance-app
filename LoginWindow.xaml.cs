using System;
using System.Data.SQLite;
using System.Windows;
using FreelanceAplication.Models;

namespace FreelanceAplication
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Будь ласка, введіть email і пароль.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string role = GetUserRoleByEmailAndPassword(email, password);

            if (role == null)
            {
                MessageBox.Show("Невірний email або пароль.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Window mainWindow;

            if (role == "Фрілансер")
            {
                mainWindow = new FreelancerWindow(email);
            }
            else if (role == "Замовник")
            {
                string userName = GetUserNameByEmail(email);
                if (string.IsNullOrEmpty(userName))
                {
                    userName = "Користувач";
                }
                mainWindow = new CustomerWindow(email);
            }
            else
            {
                MessageBox.Show("Невідома роль користувача.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            mainWindow.Show();
            this.Close();
        }

        private string GetUserRoleByEmailAndPassword(string email, string password)
        {
            string role = null;

            try
            {
                using (var conn = new SQLiteConnection("Data Source=users.db;Version=3;"))
                {
                    conn.Open();
                    string query = "SELECT role FROM users WHERE email=@Email AND password=@Password";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                            role = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка підключення до бази: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return role;
        }

        private string GetUserNameByEmail(string email)
        {
            string name = null;

            try
            {
                using (var conn = new SQLiteConnection("Data Source=users.db;Version=3;"))
                {
                    conn.Open();
                    string query = "SELECT username FROM users WHERE email=@Email";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                            name = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка підключення до бази: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return name;
        }

        private void RegisterLink_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
