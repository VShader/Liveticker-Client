﻿using System;
using System.Windows;

namespace Liveticker_Client
{
    /// <summary>
    /// Interaktionslogik für TickErstellen.xaml
    /// </summary>
    public partial class TickErstellen : Window
    {
        private LiveTickerService liveTickerService = new LiveTickerService();
        private int sport;

        public TickErstellen(int sport)
        {
            InitializeComponent();
            this.sport = sport;
        }

        private void btnTickErstellen_Click(object sender, RoutedEventArgs e)
        {
            if (sport == -1)
            {
                MessageBox.Show("Kein Event ausgewählt!");
                this.Close();
                return;
            }
            if (tbxAutor.Text != "" && tbxTitle.Text != "" && tbxBeschreibung.Text != "")
            {
                try
                {
                    Event[] events = liveTickerService.getEvents();
                    liveTickerService.addTick(events[sport].id, DateTime.UtcNow, tbxAutor.Text, tbxTitle.Text, tbxBeschreibung.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Server ist nicht erreichbar!");
                }
            }
            else
            {
                MessageBox.Show("Es müssen noch Daten eingegeben werden.");
            }

            this.Close();
        }

        private void btnAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //private void cbxSportart_Loaded(object sender, RoutedEventArgs e)
        //{
        //    refreshEvents();
        //}

        //private void cbxSportart_DropDownOpened(object sender, EventArgs e)
        //{
        //    refreshEvents();
        //}

        //private void refreshEvents()
        //{
        //    cbxSportart.Items.Clear();
        //    Event[] events = liveTickerService.getEvents();
        //    foreach (Event ev in events)
        //    {
        //        cbxSportart.Items.Add(ev.text);
        //    }
        //}
    }
}