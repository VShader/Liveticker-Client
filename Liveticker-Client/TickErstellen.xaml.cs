using System.Windows;

namespace Liveticker_Client
{
    /// <summary>
    /// Interaktionslogik für TickErstellen.xaml
    /// </summary>
    public partial class TickErstellen : Window
    {
        public TickErstellen()
        {
            InitializeComponent();
        }

        private void btnTickErstellen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}