using System;
using System.Windows;

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
            if (tbxAutor.Text != "" && tbxTitle.Text != "" && cbxSportart.SelectedIndex != -1 && tbxBeschreibung.Text != "")
            {
                Event[] events = liveTickerService.getEvents();
                liveTickerService.addTick(events[this.cbxSportart.SelectedIndex].id, DateTime.UtcNow, tbxAutor.Text, tbxTitle.Text, tbxBeschreibung.Text);
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