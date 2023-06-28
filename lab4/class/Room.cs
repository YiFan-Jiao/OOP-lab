using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4 
{
    public class Room
    {
        private string _number;

        private int _capacity;

        private Boolean _occupied;

        private List<Reservation> _reservations = new List<Reservation>();
        public void AddReservation(Reservation reservation)
        {
            _reservations.Add(reservation);
        }
        public Room(string number,int capactiy,Boolean occupied) 
        {
            _number = number;
            _capacity = capactiy;
            _occupied = occupied;
        }
    }
}
