using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek2
{
    class Opponent
    {
        public Opponent(Location startingLocation)
        {
            myLocation = startingLocation;
            random = new Random();
        }

        public void Move()
        {
            bool hiddingPlace = false;

            while (!hiddingPlace)
            {
                if (myLocation is IHasExteriorDoor) //he is in a room with a door
                {
                    IHasExteriorDoor doorLocation = myLocation as IHasExteriorDoor;
                    if (random.Next(2) == 1) //he decides to go through that door
                    {
                        myLocation = doorLocation.DoorLocation; //go through the door location in room
                    }
                }

                int number = myLocation.Exits.Length; //check the number of room exits 
                myLocation = myLocation.Exits[random.Next(number)]; //go through the one of the room exits
                                 
                //I am going to hide because my location has a door
                if (myLocation is IHidingPlace)
                {
                    hiddingPlace = true;
                }  
            }
        }

        public bool Check(Location location)
        {
            if(location == myLocation) //both references pointing to the same object
            {
                return true;
            }
            else
            {
                 return false;
            }
        }

        private Location myLocation; //keep track of where he is

        private Random random; //used to help find a random place
    }
}
