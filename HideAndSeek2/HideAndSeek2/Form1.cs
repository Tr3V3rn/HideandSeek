using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HideAndSeek2
{
    public partial class Form1 : Form
    {

        Location currentLocation;
        Opponent opponent;
        int moves; //To track player moves in the game

        RoomWithDoor livingRoom;
        RoomWithHidingPlace diningRoom;
        RoomWithDoor kitchen;
        Room stairs;
        RoomWithHidingPlace hallway;
        RoomWithHidingPlace bathroom;
        RoomWithHidingPlace masterBedroom;
        RoomWithHidingPlace secondBedroom;

        OutsideWithDoor frontYard;
        OutsideWithDoor backYard;
        OutsideWithHidingPlace garden;
        OutsideWithHidingPlace driveway;

        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            opponent = new Opponent(frontYard);
            InitalizeGame();
            //SetGame();
        }


        private void CreateObjects()
        {
            livingRoom = new RoomWithDoor("Living Room", "an antique carpet",
                              "inside the closet", "an oak door with a brass handle");
            diningRoom = new RoomWithHidingPlace("Dining Room", "a crystal chandelier",
                       "in the tall armoire");
            kitchen = new RoomWithDoor("Kitchen", "stainless steel appliances",
                      "in the cabinet", "a screen door");
            stairs = new Room("Stairs", "a wooden bannister");
            hallway = new RoomWithHidingPlace("Upstairs Hallway", "a picture of a dog",
                      "in the closet");
            bathroom = new RoomWithHidingPlace("Bathroom", "a sink and a toilet",
                      "in the shower");
            masterBedroom = new RoomWithHidingPlace("Master Bedroom", "a large bed",
                      "under the bed");
            secondBedroom = new RoomWithHidingPlace("Second Bedroom", "a small bed",
                      "under the bed");

            frontYard = new OutsideWithDoor("Front Yard", false, "a heavy-looking oak door");
            backYard = new OutsideWithDoor("Back Yard", true, "a screen door");
            garden = new OutsideWithHidingPlace("Garden", false, "inside the shed");
            driveway = new OutsideWithHidingPlace("Driveway", true, "in the garage");

            diningRoom.Exits = new Location[] { livingRoom, kitchen };
            livingRoom.Exits = new Location[] { diningRoom, stairs };
            kitchen.Exits = new Location[] { diningRoom };
            stairs.Exits = new Location[] { livingRoom, hallway };
            hallway.Exits = new Location[] { stairs, bathroom, masterBedroom, secondBedroom };
            bathroom.Exits = new Location[] { hallway };
            masterBedroom.Exits = new Location[] { hallway };
            secondBedroom.Exits = new Location[] { hallway };
            frontYard.Exits = new Location[] { backYard, garden, driveway };
            backYard.Exits = new Location[] { frontYard, garden, driveway };
            garden.Exits = new Location[] { backYard, frontYard };
            driveway.Exits = new Location[] { backYard, frontYard };

            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;

            kitchen.DoorLocation = backYard;
            backYard.DoorLocation = kitchen;
            
        }

        private void SetGame()
        {
            bool found = false;
            while(!found)
            {

                //play the game





                //if found
                if (opponent.Check(currentLocation) == true)
                {
                    found = true;
                    
                }

            }

            //Game Reset


        }

 

        private void MoveToANewLocation(Location newLocation)
        {
            currentLocation = newLocation;
            RedrawForm();
        }

        private void goHere_Click(object sender, EventArgs e)
        {
            moves++;
            Location newLocation;
            newLocation = currentLocation.Exits[exits.SelectedIndex];
            MoveToANewLocation(newLocation);        
        }

        private void goThroughTheDoor_Click(object sender, EventArgs e)
        {
            moves++;
            IHasExteriorDoor hasDoor = currentLocation as IHasExteriorDoor;
            MoveToANewLocation(hasDoor.DoorLocation);  
        }

        private void check_Click(object sender, EventArgs e)
        {
            moves++;
            //this check for the opponent in the indicated hiding spot
            //if he is found then we will reset the game calling the ResetGame()
            //description.Text = currentLocation.Description + " Player Moves:" + moves;  
            RedrawForm();
            if (opponent.Check(currentLocation) == true)
            {
                ResetGame(true);
            }
            else
            {
                ResetGame(false);
            }            
        }

        private void hide_Click(object sender, EventArgs e)
        {
            hide.Visible = false; //make the button go away after it has been clicked
            for(int i=1;i<=10;i++)
            {
                opponent.Move();
                description.Text = i+"...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }

            description.Text += "Ready or not, here I come!";
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
            MoveToANewLocation(livingRoom);
            RedrawForm();
           
        }

        private void InitalizeGame()
        {
            goHere.Visible = false;
            goThroughTheDoor.Visible = false;
            check.Visible = false;
            hide.Visible = true;
            exits.Visible = false;
        }

        private void RedrawForm()
        {
            //the goHere and exits should be visible when the game starts
            goHere.Visible = true;
            exits.Visible = true;
            check.Visible = false;
            if (currentLocation is IHasExteriorDoor)
            {
                goThroughTheDoor.Visible = true;
            }
            else
            {
                goThroughTheDoor.Visible = false;
            }
            if(currentLocation is IHidingPlace) //if you are in a room with a hiding place
            {
                //we need an interface reference variable of IHidingPlace
                IHidingPlace hidingLocation;
                //Cast current location as IHidingPlace to access the hiding place Name property
                hidingLocation = currentLocation as IHidingPlace;
                //Now we can access the hiding location name
                check.Text = "Check " + hidingLocation.HidingPlaceName; //display the label on the button
                check.Visible = true; //make the button visible to the form
                
            }
            exits.Items.Clear();
            for (int i = 0; i < currentLocation.Exits.Length; i++)
            {
                exits.Items.Add(currentLocation.Exits[i].Name);
            }
            exits.SelectedIndex = 0;

            //each time this RedDraw function gets called it needs to keep track of moves
            description.Text = currentLocation.Description + " Player Moves:" + moves;      

        }

        private void ResetGame(bool gameState)
        {   
            if(gameState)
            {
                //start the opponent in the front yard again
                opponent = new Opponent(frontYard);
                //Initialize the game once again
                InitalizeGame();
                //Display for information about my opponent and how many moves it took me to find him
                IHidingPlace hidingPlace = currentLocation as IHidingPlace;
                description.Text = "I found my opponent " + hidingPlace.HidingPlaceName + ".";
                description.Text += "It took " + moves + " moves";
                //Diplay a popup message box to the user
                MessageBox.Show("You found me in " + moves + " moves!");

            }
            

        }
   
    }
}
