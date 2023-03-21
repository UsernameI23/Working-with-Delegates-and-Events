using System;

namespace delegatesAndEvents
{
    // create a delegate
    public delegate void myDelegate();

    public class Race
    {
        // create a delegate event object
        public event myDelegate Completed;

        public void Racing(int contestants, int laps)
        {
            Console.WriteLine("Ready\nSet\nGo!");
            Random r = new Random();
            int[] participants = new int[contestants];
            bool done = false;
            int champ = -1;
            // first to finish specified number of laps wins
            while (!done)
            {
                for (int i = 0; i < contestants; i++)
                {

                    if (participants[i] <= laps)
                    {
                        participants[i] += r.Next(1, 5);
                    }
                    else
                    {
                        champ = i;
                        done = true;
                        continue;
                    }
                }

            }

            TheWinner(champ);
        }
        private void TheWinner(int champ)
        {
            Console.WriteLine("We have a winner!");
            // invoke the delegate event object and pass champ to the method
            Completed?.Invoke(); 

        }
    }
    class Program
    {
        public static void Main()
        {

            // create a class object
            Race round1 = new Race();
            // register with an event
            round1.Completed += footRace;
            // trigger the event
            round1.Racing(20);
            // register with a different event
            round1.Completed -= footRace;
            round1.Completed += carRace;
            //trigger the event
            round1.Racing(20);
            // register with a different event using a lambda expression
            round1.Completed -= carRace;
            round1.Completed += (winner) => Console.WriteLine($"The competition winner is {winner}");
            // trigger the event
            round1.Racing(20);

        }

        // event handlers
        public static void carRace(int winner)
        {
            Console.WriteLine($"Car number {winner} is the winner.");
        }
        public static void footRace(int winner)
        {
            Console.WriteLine($"Racer number {winner} is the winner.");
        }
    }
}
