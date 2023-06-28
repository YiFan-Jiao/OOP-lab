using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4 
{
    public class Client
    {
        private string _number;

        private long _creditCard;

        private List<Reservation> _reservations = new List<Reservation>();
        public void AddReservation(Reservation reservation)
        {
            _reservations.Add(reservation);
        }
        public Client(string number,long creditCard) 
        {
            _number = number;
            _creditCard = creditCard;
        }
    }
}
