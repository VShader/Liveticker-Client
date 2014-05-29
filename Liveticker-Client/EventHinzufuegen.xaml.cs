using System;
using System.Windows;

namespace Liveticker_Client
{
    /// <summary>
    /// Interaktionslogik für EventHinzufuegen.xaml
    /// </summary>
    public partial class EventHinzufuegen : Window
    {
        private LiveTickerService liveTickerService = new LiveTickerService();
        private Event[] events;

        public EventHinzufuegen()
        {
            InitializeComponent();
        }

        private void btnAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            events = liveTickerService.getEvents();
            foreach (Event ev in events)
            {
                if (this.tbxEvent.Text.ToLower() == ev.text.ToLower())
                {
                    MessageBox.Show("Sportart schon vorhanden!");
                    return;
                }
            }
            liveTickerService.addEvent(this.tbxEvent.Text, "Sportart Beschreibung", null, DateTime.UtcNow);
            this.Close();
        }
    }
}