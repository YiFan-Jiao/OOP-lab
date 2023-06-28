using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4 
{
    public class Reservation
    {
        private DateTime _date;

        private int _occupants;

        private bool _isCurrent;

        private Client _client;
        
        private Room _room;
        public Reservation(DateTime date, int occupants, bool isCurrent, Client client, Room room)
        {
            _date = date;
            _occupants = occupants;
            _isCurrent = isCurrent;
            _client = client;
            _room = room;
        }
    }
}
