using System;
using System.Windows;
using System.Windows.Documents;

namespace Liveticker_Client
{
    /// <summary>
    /// Interaktionslogik für TickErstellen.xaml
    /// </summary>
    public partial class TickErstellen : Window
    {
        private LiveTickerService liveTickerService = new LiveTickerService();
        
        public TickErstellen()
        {
            InitializeComponent();
        }

        private void btnTickErstellen_Click(object sender, RoutedEventArgs e)
        {
            string message = new TextRange(rtbxBeschreibung.Document.ContentStart, rtbxBeschreibung.Document.ContentEnd).Text.TrimEnd();

            if (tbxTitle.Text != "" && cbxSportart.SelectedIndex != -1 && message != "")
            {
                liveTickerService.addTick(this.cbxSportart.SelectedIndex, DateTime.UtcNow, "Liveticker-Client", tbxTitle.Text, message);
            }
            else
            {
                MessageBox.Show("Es müssen noch Daten eingegeben werden.");
                return;
            }
            
            this.Close();
        }

        private void btnAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addEvent_Click(object sender, RoutedEventArgs e)
        {
            EventHinzufuegen eh = new EventHinzufuegen();
            eh.ShowDialog();
        }

        private void cbxSportart_Loaded(object sender, RoutedEventArgs e)
        {
            refreshEvents();
        }

        private void cbxSportart_DropDownOpened(object sender, EventArgs e)
        {
            refreshEvents();
        }

        private void refreshEvents()
        {
            cbxSportart.Items.Clear();
            Event[] events = liveTickerService.getEvents();
            foreach (Event ev in events)
            {
                cbxSportart.Items.Add(ev.text);
            }
        }
    }
}
