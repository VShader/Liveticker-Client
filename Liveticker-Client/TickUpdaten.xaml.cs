using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            this.rtbxBeschreibung.AppendText(tick.message);
        }

        private void btnTickUpdaten_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbVeroeffentlicht.IsChecked == true)
            {
                liveTickerService.publishTick(tick.id);
            }
            //TODO: modify Tick
            this.Close();
        }

        private void btnTickLoeschen_Click(object sender, RoutedEventArgs e)
        {
            //TODO: später aktiveren
            //liveTickerService.deleteTick(tick.id);
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