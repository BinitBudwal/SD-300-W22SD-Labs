//Lab 4

//Create new Hotel 
Hotel MITTWINNIPEGHotel = new Hotel("MITT WINNIPEG Hotel", "Henloy Bay ");


//Populating hotel with rooms 
MITTWINNIPEGHotel.CreateNewRoom("1001", 1, "Regular");
MITTWINNIPEGHotel.CreateNewRoom("1002", 2, "Regular");
MITTWINNIPEGHotel.CreateNewRoom("1003", 2, "Premium");


//Create clients
Client client1 = new Client("Honey", 9898656545458787);
Client client2 = new Client("Bunny", 9898232245696161);
Client client3 = new Client("Money", 8084333312984577);



//Populating rooms with Reservation information from Clients information
MITTWINNIPEGHotel.MakeReservation(client3, "1001", 1, new DateTime(2022, 7, 29, 13, 0, 0));
MITTWINNIPEGHotel.MakeReservation(client2, "1003", 2, new DateTime(2022, 9, 29, 13, 0, 0));
MITTWINNIPEGHotel.MakeReservation(client1, "1002", 2, new DateTime(2022, 7, 29, 13, 0, 0));

MITTWINNIPEGHotel.CurrentStatus();


class Hotel
{
    public static string Name { get; set; }
    public static string Address { get; set; }
    public Room Room { get; set; }
    public List<Room> Rooms { get; set; } = new List<Room>();
    public List<Client> Clients { get; set; } = new List<Client>();
    public Reservation Reservation { get; set; }
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();

    public Hotel(string name, string address)
    {
        Name = name;
        Address = address;
        Room = new Room("1001", 2, "Economy", this);
        Reservation = Room.Reservation;
        Room.Hotel = this;
    }

    public void CreateNewRoom(string roomNumber, int capacity, string rating)
    {
        Room room = new Room(roomNumber, capacity, rating, this);
        Room = room;
        Reservation = Room.Reservation;
        Room.Hotel = this;
    }
    // Gives total number of clients currently are there in the hotel
    public void CurrentStatus ()
    {
        Console.WriteLine($"There are a total of {Clients.Count} clients in {Name}");
    }

    public void MakeReservation(Client client, string roomNumber, int occupants, DateTime date)
    {
 
        foreach (Room r in Rooms)
        {
            //checks for availabity of the room (if room is empty)
            if (r.Number == roomNumber && !r.Occupied)
            {
                Reservation newReserve = new Reservation(r, this);
                if (newReserve.Room.Capacity >= occupants)
                {
                    newReserve.Occupants = occupants;
                    newReserve.Date = date;
                    newReserve.IsCurrent = true;

                    newReserve.Client = client;
                    client.Reservation = newReserve;
                    client.Hotel = this;
                    Clients.Add(client);
                    Reservations.Add(newReserve);

                    newReserve.Room.Occupants = occupants;


                    Console.WriteLine($"Reservation details :");
                    Console.WriteLine($"Client Name: {newReserve.Client.Name}");
                    Console.WriteLine($"Room Alloted : {newReserve.Room.Number}");
                    Console.WriteLine($"Number of Guests: {newReserve.Occupants}");
                    Console.WriteLine($"Reservation Date: {newReserve.Date}");
                    Console.WriteLine("\nReservation Created Successfully ! ");
                    Console.WriteLine("\n--------------------------------------------------------------");
                    Console.WriteLine("--------------------------------------------------------------");
                    
                    break;
                }
            }
        }
    }
}

class Room
{
    public Hotel Hotel { get; set; }
    public string Number { get; set; }
    public int Capacity { get; set; }
    public bool Occupied { get; set; } = false;
    public string Rating { get; set; }
    public int Occupants { get; set; }
    public List<Reservation> Reservations { get; set; }
    public Reservation Reservation { get; set; }
    public Room(string number, int capacity, string rating, Hotel hotel)
    {
        Number = number;
        Capacity = capacity;
        Rating = rating;
        Hotel = hotel;

        //Poopulates hotel with rooms and link reservation and clients
        Hotel.Rooms.Add(this);
        Reservation = new Reservation(this, hotel);
        Reservation.Clients = Hotel.Clients;
        Reservation.Room = this;
        Reservation.Hotel = hotel;
    }
}
class Client
{
    public Hotel Hotel { get; set; }
    public Room Room { get; set; }
    public string Name { get; set; }
    public long CreditCard { get; set; }
    public string Membership { get; set; }
    public List<Reservation> Reservations { get; set; }
    public Reservation Reservation { get; set; }
    public Client(string name, long creditCard)
    {
        Name = name;
        CreditCard = creditCard;
    }
}

class Reservation
{
    public DateTime Date { get; set; }
    public int Occupants { get; set; }
    public bool IsCurrent { get; set; } = false;
    public Hotel Hotel { get; set; }
    public Client Client { get; set; }
    public List<Client> Clients { get; set; }
    public Room Room { get; set; }

    public Reservation(Room room, Hotel hotel)
    {
        Room = room;
        Hotel = hotel;
    }
}
