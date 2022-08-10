


namespace Starship.Captain
{
    class Program
    {
        static void Main(string[] args)
        {
            //File stream and stream writer for text file output
            FileStream fs;
            StreamWriter sw;
            TextWriter tw = Console.Out;

            try
            {
                //Where to write the file to and modes like open if the file exists or create if not
                fs = new FileStream(@"C:/Test/StartshipCaptain.txt", FileMode.OpenOrCreate, FileAccess.Write);
                sw = new StreamWriter(fs);
                Console.SetOut(sw);
                //Variable for counting planets colonized
                int planetsColonized = 0;

                //Planet visited during trip
                int planetsVisited = 0;

                //Colonized space
                int colonizedSpace = 0;

                //24 hours of time left in seconds
                double timeLeft = 24 * 60 * 60; //First planet must be 4018604.65116 m^2 or less surface area to be colonizable or total area of planets to be inhabited

                //Homeworld coordinates and colonization rate for variables
                string currentWorld = "123.123.99.1 X & 098.098.11.1 Y & 456.456.99.9 Z";
                double colonizationRate = 0.043;

                //Call create universe function to get start and end coordinates
                createUniverse();

                //Call generate coordinates functions and store it in coords variable for use
                var coords = generateCoords();

                while (timeLeft > 0)
                {
                    //Get minimum distanced planet or monster from current planet
                    double minDistance = double.MaxValue;
                    int indexOfMin = 0;
                    for (int i = 0; i < coords.Length; i++)
                    {
                        double distance = calculateDistance(Convert.ToInt32(currentWorld.Substring(0, 3) + currentWorld.Substring(4, 3) + currentWorld.Substring(8, 2) + currentWorld.Substring(11, 1)),
                        Convert.ToInt32(coords[i].Substring(0, 3) + coords[i].Substring(4, 3) + coords[i].Substring(8, 2) + coords[i].Substring(11, 1)),
                        Convert.ToInt32(currentWorld.Substring(17, 3) + currentWorld.Substring(21, 3) + currentWorld.Substring(25, 2) + currentWorld.Substring(28, 1)),
                        Convert.ToInt32(coords[i].Substring(17, 3) + coords[i].Substring(21, 3) + coords[i].Substring(25, 2) + coords[i].Substring(28, 1)),
                        Convert.ToInt32(currentWorld.Substring(34, 3) + currentWorld.Substring(38, 3) + currentWorld.Substring(42, 2) + currentWorld.Substring(45, 1)),
                        Convert.ToInt32(coords[i].Substring(34, 3) + coords[i].Substring(38, 3) + coords[i].Substring(42, 2) + coords[i].Substring(45, 1)));
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            indexOfMin = i;
                        }
                    }
                    //Make current world the new world/monster visited
                    currentWorld = coords[indexOfMin];
                    Console.WriteLine("Planet or monster visited coordinates + surface area: " + currentWorld);
                    //If the entity is a monster, subtract 20 minutes travel time to next entity
                    if (Convert.ToInt32(coords[indexOfMin].Substring(49, 9)) == 0)
                    {
                        timeLeft -= 20 * 60;
                    }
                    //If the entity is a planet, inhabit the planet and subtract 10 minutes travel time + inhabition time
                    else
                    {
                        planetsVisited++;
                        timeLeft -= 10 * 60;
                        if (colonizePlanet(Convert.ToInt32(coords[indexOfMin].Substring(49, 9)), colonizationRate, timeLeft) > 0)
                        {
                            planetsColonized++;
                            timeLeft = colonizePlanet(Convert.ToInt32(coords[indexOfMin].Substring(49, 9)), colonizationRate, timeLeft);
                            colonizedSpace += Convert.ToInt32(coords[indexOfMin].Substring(49, 9));
                        }
                    }
                    coords[indexOfMin] = "0".PadLeft(55, '0');
                }
                Console.WriteLine("Planets Colonized: " + planetsColonized + " Planets visited: " + planetsVisited + " Colonized Space: " + colonizedSpace);
                Console.SetOut(tw);
                sw.Close();
                fs.Close();
                Console.WriteLine("Planets Colonized: " + planetsColonized + " Planets visited: " + planetsVisited + " Colonized Space: " + colonizedSpace);
                Console.WriteLine();
                Console.WriteLine("Flight plan taken was going to the nearest location of a planet or monster");
                Console.WriteLine("and checking if it's possible, if it's a planet, the colonize it given"); 
                Console.WriteLine("the time left from the 24 hours.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot open StarshipCaptain.txt for writing");
                Console.WriteLine(ex.Message);
                return;
            }
        }

        static string[,,] createUniverse()
        {
            //Create a new universe 

                //X coordinates start and end for each component

            int firstPartXStart = 0;
            int firstPartXEnd = 999;
            int secondPartXStart = 0;
            int secondPartXEnd = 999;
            int thirdPartXStart = 0;
            int thirdPartXEnd = 99;
            int fourthPartXStart = 0;
            int fourthPartXEnd = 9;

                //Y coordinates start and end for each component

            int firstPartYStart = 0;
            int firstPartYEnd = 999;
            int secondPartYStart = 0;
            int secondPartYEnd = 999;
            int thirdPartYStart = 0;
            int thirdPartYEnd = 99;
            int fourthPartYStart = 0;
            int fourthPartYEnd = 9;

                //Z coordinates start and end for each component

            int firstPartZStart = 0;
            int firstPartZEnd = 999;
            int secondPartZStart = 0;
            int secondPartZEnd = 999;
            int thirdPartZStart = 0;
            int thirdPartZEnd = 99;
            int fourthPartZStart = 0;
            int fourthPartZEnd = 9;

                //Put the coordinates together in a string

            string XcoordMin = firstPartXStart.ToString().PadLeft(3, '0') + secondPartXStart.ToString().PadLeft(3, '0') + thirdPartXStart.ToString().PadLeft(2, '0') + fourthPartXStart.ToString();
            string XcoordMax = firstPartXEnd.ToString().PadLeft(3, '0') + secondPartXEnd.ToString().PadLeft(3, '0') + thirdPartXEnd.ToString().PadLeft(2, '0') + fourthPartXEnd.ToString();
            string YcoordMin = firstPartYStart.ToString().PadLeft(3, '0') + secondPartYStart.ToString().PadLeft(3, '0') + thirdPartYStart.ToString().PadLeft(2, '0') + fourthPartYStart.ToString();
            string YcoordMax = firstPartYEnd.ToString().PadLeft(3, '0') + secondPartYEnd.ToString().PadLeft(3, '0') + thirdPartYEnd.ToString().PadLeft(2, '0') + fourthPartYEnd.ToString();
            string ZcoordMin = firstPartZStart.ToString().PadLeft(3, '0') + secondPartZStart.ToString().PadLeft(3, '0') + thirdPartZStart.ToString().PadLeft(2, '0') + fourthPartZStart.ToString();
            string ZcoordMax = firstPartZEnd.ToString().PadLeft(3, '0') + secondPartZEnd.ToString().PadLeft(3, '0') + thirdPartZEnd.ToString().PadLeft(2, '0') + fourthPartZEnd.ToString();
            
                //Store coordinate bounds in 3 dimensional array

            string[,,] universeBounds = new string[1, 3, 2] { { { XcoordMin, XcoordMax }, { YcoordMin, YcoordMax }, { ZcoordMin, ZcoordMax } } };




            return universeBounds;
        }

        // Generate coordinates randomly for 15000 locations
        static string[] generateCoords()
        {
            Random rnd = new Random();
            string[] coords = new string[15000];

            for (int i = 0; i < 15000; i++)
            {
                //Assign planets with 60% chance and 40% chance of being a space monster
                //Declare planet area
                int planetArea = 0;
                int planetOrMonster = rnd.Next(1, 11);
                //Adjust greater than integer to change monster/planet ratio
                if(planetOrMonster > 4)
                {
                    planetArea = rnd.Next(1000000, 100000000);
                }

                int X1 = rnd.Next(1, 1000);
                int X2 = rnd.Next(1, 1000);
                int X3 = rnd.Next(1, 100);
                int X4 = rnd.Next(1, 10);

                int Y1 = rnd.Next(1, 1000);
                int Y2 = rnd.Next(1, 1000);
                int Y3 = rnd.Next(1, 100);
                int Y4 = rnd.Next(1, 10);

                int Z1 = rnd.Next(1, 1000);
                int Z2 = rnd.Next(1, 1000);
                int Z3 = rnd.Next(1, 100);
                int Z4 = rnd.Next(1, 10);

                string XcoordMin = X1.ToString().PadLeft(3, '0') + '.' + X2.ToString().PadLeft(3, '0') + '.' + X3.ToString().PadLeft(2, '0') + '.' + X4.ToString();
                string YcoordMin = Y1.ToString().PadLeft(3, '0') + '.' + Y2.ToString().PadLeft(3, '0') + '.' + Y3.ToString().PadLeft(2, '0') + '.' + Y4.ToString();
                string ZcoordMin = Z1.ToString().PadLeft(3, '0') + '.' + Z2.ToString().PadLeft(3, '0') + '.' + Z3.ToString().PadLeft(2, '0') + '.' + Z4.ToString();

                coords[i] = XcoordMin + " X & " + YcoordMin + " Y & " + ZcoordMin + " Z " + planetArea.ToString().PadLeft(9, '0');
            }
            //return coordinates and if it is a planet or space monster at the end of the string (based on 0 for space monster for planetArea or more than 1 million for planet)
            return coords; 
        }
        static double colonizePlanet(double planetArea, double rate, double timeLeft)
        {
            double areaCovered = 0;
            while(timeLeft > 0)
            {
                if ((planetArea / 2) > areaCovered)
                {
                    areaCovered++;
                }
                else
                {
                    return timeLeft;
                }
                timeLeft -= rate;
            }
            
            return 0;
        }

        static double calculateDistance(int X1, int X2, int Y1, int Y2, int Z1, int Z2)
        {
            double distance = (Math.Sqrt(Math.Pow((X2 - X1), 2) + Math.Pow((Y2 - Y1), 2) + Math.Pow((Z2 - Z1), 2)));
            return distance;
        }
    }
}
