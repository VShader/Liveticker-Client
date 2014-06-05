using System;
using System.Windows;

namespace Liveticker_Client
{
    /// <summary>
    /// Interaktionslogik für TickUpdaten.xaml
    /// </summary>
    public partial class TickUpdaten : Window
    {
        private LiveTickerService liveTickerService = new LiveTickerService();
        private Tick tick;

        public TickUpdaten(ref Tick tick)
        {
            InitializeComponent();
            this.tick = tick;

            this.tbxAutor.Text = tick.author;
            this.tbxTitle.Text = tick.title;
            this.cbVeroeffentlicht.IsChecked = tick.is_published;
            int index = 0;
            foreach (Event ev in liveTickerService.getEvents())
            {
                if (ev.id == tick.event_id)
                {
                    break;
                }
                index++;
            }
            this.cbxSportart.SelectedIndex = index;
            this.tbxBeschreibung.Text = tick.message;
        }

        private void btnTickUpdaten_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbVeroeffentlicht.IsChecked == true && tick.is_published == false)
            {
                liveTickerService.publishTick(tick.id);
            }
            
            if (tbxAutor.Text != "" && tbxTitle.Text != "" && cbxSportart.SelectedIndex != -1 && tbxBeschreibung.Text != "")
            {
                Event[] events = liveTickerService.getEvents();
                if (tbxAutor.Text != tick.author || tbxTitle.Text != tick.title || events[cbxSportart.SelectedIndex].id != tick.event_id || tbxBeschreibung.Text != tick.message)
                {
                    liveTickerService.deleteTick(tick.id);
                    liveTickerService.addTick(events[this.cbxSportart.SelectedIndex].id, tick.reported, tbxAutor.Text, tbxTitle.Text, tbxBeschreibung.Text);
                    //TODO: publish?!
                }
                else if (this.cbVeroeffentlicht.IsChecked == false && tick.is_published == true)
                {
                    liveTickerService.deleteTick(tick.id);
                    liveTickerService.addTick(tick.event_id, tick.reported, tick.author, tick.title, tick.message);
                }
            }
            else
            {
                MessageBox.Show("Es müssen noch Daten eingegeben werden.");
                return;
            }

            this.Close();
        }

        private void btnTickLoeschen_Click(object sender, RoutedEventArgs e)
        {
            liveTickerService.deleteTick(tick.id);
            this.Close();
        }

        private void btnAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void addEvent_Click(object sender, RoutedEventArgs e)
        {
            EventHinzufuegen eh = new EventHinzufuegen();
            eh.ShowDialog();
        }
    }
}