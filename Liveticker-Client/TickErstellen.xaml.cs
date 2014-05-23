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
        private Tick tick;
        
        public TickErstellen(ref Tick tick)
        {
            InitializeComponent();
            this.tick = tick;
        }

        private void cbxSportart_Loaded(object sender, RoutedEventArgs e)
        {
            Event[] events = liveTickerService.getEvents();
            foreach (Event ev in events)
            {
                cbxSportart.Items.Add(ev.text);
            }
        }

        private void btnTickErstellen_Click(object sender, RoutedEventArgs e)
        {
            string message = new TextRange(rtbxBeschreibung.Document.ContentStart, rtbxBeschreibung.Document.ContentEnd).Text;
            if (tbxTitle.Text != "" && cbxSportart.Text != "" && message != "")
            {
                DateTime currentDateTime = DateTime.UtcNow;

                tick.author = "LiveTicker-Client";
                tick.created = currentDateTime;
                tick.event_id = cbxSportart.SelectedIndex;
                tick.id = 0;
                tick.is_deleted = false;
                tick.is_published = false;
                tick.message = message;
                tick.modified = currentDateTime;
                tick.reported = currentDateTime;
                tick.title = tbxTitle.Text;
            }

            this.Close();
        }
    }
}