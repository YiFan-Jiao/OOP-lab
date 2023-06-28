using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public static class Hotel
    {
        private static string _name;

        private static string _address;

        private static List<Room> _rooms = new List<Room>();

        private static List<Client> _clients = new List<Client>();

        private static List<Reservation> _reservations = new List<Reservation>();
        public static void creatAndAddRoom(string number, int capactiy, Boolean occupied)
        {
            Room room = new Room(number, capactiy, occupied);
            _rooms.Add(room);
        }
        public static void creatAndAddClient(string number, long creditCard) 
        { 
            Client client = new Client(number, creditCard);
            _clients.Add(client);
        }
        public static void AddReservation(DateTime date, int occupants, bool isCurrent, Client client, Room room) 
        {
            Reservation reservation = new Reservation(date, occupants, isCurrent, client, room);

            room.AddReservation(reservation);
            client.AddReservation(reservation);
            _reservations.Add(reservation);
        }

    }
}
