using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;

namespace FreelanceAplication
{
    public partial class CreateProjectWindow : Window
    {
        private string dbPath = "users.db";
        private string currentUserEmail;

        public CreateProjectWindow(string customerEmail)
        {
            InitializeComponent();
            currentUserEmail = customerEmail;
        }

        private void CreateProjectButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text.Trim();
            string description = DescriptionTextBox.Text.Trim();
            string category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string budgetStr = BudgetTextBox.Text.Trim();
            DateTime? deadline = DeadlinePicker.SelectedDate;
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) ||
                string.IsNullOrEmpty(category) || string.IsNullOrEmpty(budgetStr) || !deadline.HasValue)
            {
                MessageBox.Show("Будь ласка, заповніть усі поля.");
                return;
            }
            if (!double.TryParse(budgetStr, out double budget))
            {
                MessageBox.Show("Некоректний формат бюджету.");
                return;
            }
            if (budget <= 0)
            {
                MessageBox.Show("Бюджет повинен бути додатнім числом.");
                return;
            }
            if (deadline.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Дата дедлайна не може бути в минулому.");
                return;
            }
            try
            {
                using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    conn.Open();
                    string query = @"INSERT INTO projects (customer_email, title, description, category, budget, deadline)
                             VALUES (@customer_email, @title, @description, @category, @budget, @deadline)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@customer_email", currentUserEmail);
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@budget", budget);
                        cmd.Parameters.AddWithValue("@deadline", deadline.Value.ToString("yyyy-MM-dd"));

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Проєкт успішно створено!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

    }
}
