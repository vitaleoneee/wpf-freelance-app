using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreelanceAplication
{
    public partial class FreelancerListWindow : Window
    {
        private string userEmail;

        public FreelancerListWindow(string email)
        {
            InitializeComponent();
            userEmail = email;
            LoadFreelancers();
        }

        private void LoadFreelancers()
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=users.db;Version=3;"))
                {
                    conn.Open();

                    string query = "SELECT username, description FROM users WHERE role = 'Фрілансер'";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader["username"].ToString();
                            string description = reader["description"].ToString();

                            var card = CreateFreelancerCard(name, description);
                            FreelancersPanel.Items.Add(card);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження фрілансерів: " + ex.Message);
            }
        }

        private Border CreateFreelancerCard(string name, string description)
        {
            var nameText = new TextBlock { Text = name, FontSize = 18, FontWeight = FontWeights.Bold };
            var descText = new TextBlock { Text = description, Margin = new Thickness(0, 5, 0, 5), TextWrapping = TextWrapping.Wrap };
            var viewButton = new Button { Content = "Переглянути профіль", Margin = new Thickness(0, 5, 0, 0) };

            viewButton.Click += (sender, e) =>
            {
                var profileWindow = new FreelancerProfileWindow(name, userEmail);
                profileWindow.Show();
            };


            var stack = new StackPanel();
            stack.Children.Add(nameText);
            stack.Children.Add(descText);
            stack.Children.Add(viewButton);

            return new Border
            {
                Background = System.Windows.Media.Brushes.White,
                BorderBrush = System.Windows.Media.Brushes.SteelBlue,
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(5),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 0, 0, 10),
                Child = stack
            };
        }
        private void ProfileTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var customerWindow = new CustomerWindow(userEmail);
            customerWindow.Show();
            this.Close();
        }

        private void ProjectsTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var customerWindow = new CustomerWindow(userEmail);
            customerWindow.Show();
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