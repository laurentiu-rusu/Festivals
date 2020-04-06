using System;
using System.Collections.Generic;
using Gtk;
using MusicFestivals.domains;
using MusicFestivals.services;

namespace MusicFestivals.ui
{
    public class MainWindow : Window
    {
        private Window loginWindow;

        private ServiceConcert _serviceConcert;
        private ServiceTicket _serviceTicket;

        private TreeView concertTable;
        private ListStore concertWithDateTable;
        private Entry clientNameEntry;
        private Entry seatsEntry;
        private Entry dateEntry;

        private TreeView concertList;
        private ListStore concertWithDateList;
        
        private int selectedConcertId = -1;

        public MainWindow(string title, ServiceConcert serviceConcert, ServiceTicket serviceTicket) : base(title)
        {
            this._serviceConcert = serviceConcert;
            this._serviceTicket = serviceTicket;
            
            Resize(800, 600);
            SetPosition(WindowPosition.Center);
            InitUserInterface();
            InitTable();
        }

        public void SetLoginWindow(Window loginWindow)
        {
            this.loginWindow = loginWindow;
        }

        private void InitUserInterface()
        {
            Fixed fix = new Fixed();
            
            // Left side
            concertTable = new TreeView();
            concertTable.Selection.Changed += SelectionChanged;
            
            TreeViewColumn idCol = new TreeViewColumn();
            idCol.Title = "Id";
            CellRendererText idCell = new CellRendererText();
            idCol.PackStart(idCell, true);
            
            TreeViewColumn ArtistCol = new TreeViewColumn();
            ArtistCol.Title = "Artist";
            CellRendererText ArtistCell = new CellRendererText();
            ArtistCol.PackStart(ArtistCell, true);
            
            TreeViewColumn destinationCol = new TreeViewColumn();
            destinationCol.Title = "Location";
            CellRendererText destinationCell = new CellRendererText();
            destinationCol.PackStart(destinationCell, true);
            
            TreeViewColumn dateCol = new TreeViewColumn();
            dateCol.Title = "Date";
            CellRendererText dateCell = new CellRendererText();
            dateCol.PackStart(dateCell, true);
            
            TreeViewColumn takenSeatsCol = new TreeViewColumn();
            takenSeatsCol.Title = "TakenSeats";
            CellRendererText takenSeatsCell = new CellRendererText();
            takenSeatsCol.PackStart(takenSeatsCell, true);
            
            TreeViewColumn emptySeatsCol = new TreeViewColumn();
            emptySeatsCol.Title = "TakenSeats";
            CellRendererText emptySeatsCell = new CellRendererText();
            emptySeatsCol.PackStart(emptySeatsCell, true);

            concertTable.AppendColumn(idCol);
            concertTable.AppendColumn(ArtistCol);
            concertTable.AppendColumn(destinationCol);
            concertTable.AppendColumn(dateCol);
            concertTable.AppendColumn(takenSeatsCol);
            concertTable.AppendColumn(emptySeatsCol);
            
            idCol.AddAttribute(idCell, "text", 0);
            ArtistCol.AddAttribute(ArtistCell, "text", 1);
            destinationCol.AddAttribute(destinationCell, "text", 2);
            dateCol.AddAttribute(dateCell, "text", 3);
            takenSeatsCol.AddAttribute(takenSeatsCell, "text", 4);
            emptySeatsCol.AddAttribute(emptySeatsCell, "text", 5);

            
            concertWithDateTable = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            concertTable.Model = concertWithDateTable;
                    

            clientNameEntry = new Entry();
            seatsEntry = new Entry();
            Button bookBtn = new Button("Buy now");
            bookBtn.Clicked += BookingHandler;
            Button logoutBtn = new Button("Logout");
            logoutBtn.Clicked += LogoutHandler;
            
            fix.Put(new Label("Buy tickets"), 14, 20);
            fix.Put(concertTable, 14, 46);
            fix.Put(new Label("Buyer name:"), 14, 472);
            fix.Put(clientNameEntry, 118, 467);
            fix.Put(new Label("Number of seats:"), 14, 518);
            fix.Put(seatsEntry, 118, 513);
            fix.Put(logoutBtn, 14, 562);
            fix.Put(bookBtn, 118, 562);
            
            // Right side
            
            dateEntry = new Entry();
            Button searchBtn = new Button("Search now");
            searchBtn.Clicked += SearchHandler;
            Button clearBtn = new Button("Clear inputs");
            clearBtn.Clicked += ClearSearchFields;
            
            concertList = new TreeView();
            
            TreeViewColumn artistCol = new TreeViewColumn();
            artistCol.Title = "Artist";
            CellRendererText artistCell = new CellRendererText();
            artistCol.PackStart(artistCell, true);
            
            TreeViewColumn locationCol = new TreeViewColumn();
            locationCol.Title = "Location";
            CellRendererText locationCell = new CellRendererText();
            locationCol.PackStart(locationCell, true);
            
            TreeViewColumn hourCol = new TreeViewColumn();
            hourCol.Title = "Start Hour";
            CellRendererText hourCell = new CellRendererText();
            hourCol.PackStart(hourCell, true);
            
            TreeViewColumn seatsCol = new TreeViewColumn();
            seatsCol.Title = "Empty Seats";
            CellRendererText seatsCell = new CellRendererText();
            seatsCol.PackStart(seatsCell, true);

            concertList.AppendColumn(artistCol);
            concertList.AppendColumn(locationCol);
            concertList.AppendColumn(hourCol);
            concertList.AppendColumn(seatsCol);

            artistCol.AddAttribute(artistCell, "text", 0);
            locationCol.AddAttribute(locationCell, "text", 1);
            hourCol.AddAttribute(hourCell, "text", 2);
            seatsCol.AddAttribute(seatsCell, "text", 3);
            
            concertWithDateList = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string));
            concertList.Model = concertWithDateList;
            
            fix.Put(new Label("Search"), 416, 20);
            fix.Put(new Label("Date:"), 416, 91);
            fix.Put(dateEntry, 520, 86);
            fix.Put(new Label("(yyyy-mm-dd)"), 520, 120);
            fix.Put(searchBtn, 519, 150);
            fix.Put(clearBtn, 638, 150);
            fix.Put(concertList, 420, 200);

            Add(fix);
        }

        private void InitTable()
        {
            List<Concert> concerts = _serviceConcert.findAll();
            
            concertWithDateTable.Clear();
            concerts.ForEach(concert =>
            {
                concertWithDateTable.AppendValues(concert.Id, concert.Name, concert.Date, concert.Location,
                    concert.TakenSeats, concert.EmptySeats);
            });
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            if (concertTable.Selection != null)
            {
                try
                {
                    TreeIter iter;
                    concertTable.Selection.GetSelected(out iter);
                    string id = (string)concertWithDateTable.GetValue(iter, 0);
                    selectedConcertId = Int32.Parse(id);
                }
                catch (Exception ex)
                {
                    selectedConcertId = -1;
                }
            }
        }
        private void BookingHandler(object sender, EventArgs e)
        {
            try
            {
                int seats = Int32.Parse(seatsEntry.Text);
                _serviceTicket.save(clientNameEntry.Text, seats, selectedConcertId);
                Concert concert = _serviceConcert.findOne(selectedConcertId);
                _serviceConcert.update(selectedConcertId, concert.TakenSeats + seats, concert.EmptySeats - seats);
                ClearTicketFields();
                InitTable();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void ClearTicketFields()
        {
            clientNameEntry.Text = "";
            seatsEntry.Text = "";
        }

        private void SearchHandler(object sender, EventArgs e)
        {
            string data = dateEntry.Text;
            List<Concert> concerts = _serviceConcert.findAll();
            concerts.ForEach(concert =>
            {
                if (data == concert.Date)
                {
                    concertWithDateList.AppendValues(concert.Name, concert.Time, concert.Location, concert.EmptySeats);
                }
            });
        }

        private void ClearSearchFields(object sender, EventArgs e)
        {
            dateEntry.Text = "";
            concertWithDateList.Clear();
        }

        private void LogoutHandler(object sender, EventArgs e)
        {
            this.Hide();
            loginWindow.ShowAll();
        }
    }
}