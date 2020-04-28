using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
h
namespace Testowy
{
    class Program
    {
        static void Main(string[] args)
        {
            // szyld graficzny
            Graphic();

            // tworze liste aktorów  - 12 pozycji już z automatu jest zainstalowanych
            List<Actor> listOfActors = new List<Actor>();
            listOfActors = Actor.CreateListOfActors();   // metoda w klasie Actor tworząca 12 obiektów Actor

            // powitanie, wytłumaczenie

            Console.WriteLine("\n\t\t\tHello producer !");

            Console.WriteLine(" As you know : movie producer is a person who oversees film production.\n" +
                " It's role is to ensure the financial success of the film.\n" +
                " Actors playing in your film will be very important for Box Office results.\n" +
                " As a world wide known producer you can choose between most trendy actors.\n" +
                " Some of them already confirmed their interest");

            Console.WriteLine("\n Right now 12 famous actors want to cooperate with you.");
            Console.WriteLine("   Here is a list of them:");

            foreach (var item in listOfActors) // wyświetlam listę aktorów
            {
                Console.WriteLine("\t{0,35}",
                    item.GetActorNameAndLastName());
            }
            Console.WriteLine();


            // dodawanie kolejnych aktorów

            bool aditionalActors = true;
            do
            {
                Console.WriteLine("  Would you like to add any aditional actors to this list?\n");

                Console.Write("  Press 'A' to add an actor or 'S' to skip further on ... ");
                string answer1 = Console.ReadLine().ToLower();
                if (answer1 == "a")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.WriteLine("\n+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    Console.ResetColor();

                    Console.WriteLine("\n  Let's add another actor.");
                    bool additionalActorFName = true;
                    string givenActorFName;

                    do
                    {
                        Console.Write("\nType full name of chosen actor. First name ? ... ");
                        givenActorFName = Console.ReadLine();
                        if (givenActorFName == "")
                        {
                            additionalActorFName = false;
                        }
                        else additionalActorFName = true;

                    } while (additionalActorFName == false);

                    bool additionalActorLName = true;
                    string givenActorLName;
                    do
                    {
                        Console.Write("Last name ? ... ");
                        givenActorLName = Console.ReadLine();
                        if (givenActorLName == "")
                        {
                            additionalActorLName = false;
                        }
                        else additionalActorLName = true;

                    } while (additionalActorLName == false);


                    Actor actor = new Actor(givenActorFName, givenActorLName); // kostruktor Actora
                    listOfActors.Add(actor);   // dodanie do listy
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.WriteLine("\n+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    Console.ResetColor();
                    Console.WriteLine($"\n\tYou have just added {actor.GetActorNameAndLastName()} to your list.\n");



                }
                else if (answer1 == "s")
                {
                    aditionalActors = false;
                }
                else
                {
                    Console.WriteLine("\n\tLet me ask you again.\n");
                }
            } while (aditionalActors == true);

            Console.WriteLine();

            // nadanie priorytów Actorom z listy

            Console.Clear();
            Console.WriteLine("\n\tAlright Producer ! Now is time to set your Priority Rating.\n");
            Console.WriteLine("  You can rate your actors by assigning them values between 1 and 4\n" +
                 "  according to your plans of hiring them in your future movies.\n");
            Console.WriteLine(" Prioirty Rating '1': you definitely want this actor for your next production");
            Console.WriteLine(" Prioirty Rating '4': they must still wait for a chance to cooperate with you");
            Console.WriteLine(" Deafault Priority Rating is set to '4'.\n" +
                "\n  Pass another value to change actor's rating\n" +
                "  or press 'Enter' to skip further on.\n");
            Console.WriteLine("Press any key to see actors and set their Priority Rating");
            Console.ReadKey();


            foreach (Actor actor in listOfActors)
            {

                actor.SetActorRating();  // metoda z klasy Actor
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("  ________________________________________________________________________\n");
            Console.ResetColor();

            // utworzenie kolejki z listy według pola priorityRating

            Queue<Actor> queueOfActors = new Queue<Actor>();
            queueOfActors = QueuePriority.GetQueuePriority(listOfActors);  // metoda dodająca Actorów do listy według parametru

            Console.WriteLine("  You have set Priority Ranking of all yours actors." +
                "\nHere is your final list of {0} actors starting from your most desirable actors:\n", queueOfActors.Count);


            foreach (var item in queueOfActors)  // wyświetlam listę posortowaną Actorów
            {
                Console.WriteLine("\t {0,35},\t PriorityRating ___  {1}",
                    item.GetActorNameAndLastName(), item.GetActorRating());
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n  ________________________________________________________________________\n");
            Console.ResetColor();

            // wykolejkowywanie czyli   zatrudnianie kolejnych z listy 

            Console.Write("  Do you want to hire your favorite actor or exit ?\n" +
                "  Press 'H' to hire ot 'E' to exit  ");

            bool hireAnotherActor = true;
            do
            {
                string answer3 = Console.ReadLine().ToLower();

                if (answer3 == "h")
                {
                    if (queueOfActors.Count == 0)
                    {
                        Actor.HireTheActor(queueOfActors);
                        hireAnotherActor = false;
                    }
                    else
                    {
                        Actor.HireTheActor(queueOfActors);

                        Console.Write("\n\nDo you want to hire your next favorite actor or exit ?\n" +
                                    "Press 'H' to hire ot 'E' to exit  ");
                    }

                }
                else if (answer3 == "e")
                {
                    hireAnotherActor = false;
                }
                else
                {
                    Console.Write("Press 'H' to hire an actor or 'E' to exit ... ");
                }

            } while (hireAnotherActor == true);


            //   zupełnie nie dałem rady do tej tablicyhashującej podejść ;((( 
            // zrobiłe coś takiego ( i efekt jest chyba podobny ) .   no ale wiadomo ;(

            int countOf1Priority = 0;
            int countOf2Priority = 0;
            int countOf3Priority = 0;
            int countOf4Priority = 0;

            foreach (var item in queueOfActors)
            {
                if (item.GetActorRating() == 1)
                {
                    countOf1Priority++;
                }

                if (item.GetActorRating() == 2)
                {
                    countOf2Priority++;
                }

                if (item.GetActorRating() == 3)
                {
                    countOf3Priority++;
                }

                if (item.GetActorRating() == 4)
                {
                    countOf4Priority++;
                }
            }

            Console.Clear();
            Console.WriteLine("\n\tBefore exiting take a look at the stats of your list:");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n\n  ________________________________________________________________________\n");
            Console.ResetColor();
            Console.WriteLine("\n\nThere are {0} actors on your Priority Rating List", queueOfActors.Count);
            Console.WriteLine("You got {0} actors with Priority Rating 1", countOf1Priority);
            Console.WriteLine("You got {0} actors with Priority Rating 2", countOf2Priority);
            Console.WriteLine("You got {0} actors with Priority Rating 3", countOf3Priority);
            Console.WriteLine("You got {0} actors with Priority Rating 4", countOf4Priority);




            // pożegnanie 


            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n\n  ________________________________________________________________________\n");
            Console.ResetColor();
            Console.WriteLine("Thank you Producer. Wish you gigantic revenues of your next movie ;) .\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("  ________________________________________________________________________\n");
            Console.ResetColor();


            Console.ReadLine();
        }


        static void Graphic() // metoda do rysowania szyldu na początku
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("+++++++     ALWAYS HIRE YOUR MOST DESIRABLE ACTORS        +++++++++++");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("++++  30 years of helping Movie Producers make their choice !!!  ++++");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
            Console.ResetColor();
            Console.Write("\t\t\t\t  press any key to continue ... ");
            Console.ReadKey();
        }
    }


    public class QueuePriority  // to chyba niepotrzebnie jest klasą. wystarczyłoby jako metoda chyba
    {   // przynajmniej do tego co ja tu zaproponowałem

        public static Queue<Actor> GetQueuePriority(List<Actor> list)
        {
            Queue<Actor> actorsQueue = new Queue<Actor>();

            int i = 1;
            while (i <= 4)     // cztery  okrążenia , tyle ile jest możliwości stopni priorityRating
            {

                foreach (Actor actor in list)  // najpierwsz szukam tych co mają 1 , potem 2 .. aż do 4
                {
                    if (actor.GetActorRating() == i && actor.alreadyOnPriorityList == false)
                    {
                        actorsQueue.Enqueue(actor);  // dodanie do kolejki
                        actor.alreadyOnPriorityList = true;
                    }


                }
                i++;
            }

            return actorsQueue;   // zwrócenie kolejki

        }
    }

    public class Actor
    {
        private string firstName;
        private string lastName;
        private int priorityRating;
        public bool alreadyOnPriorityList = false;
        public int salaryIn2019 = 0;

        public Actor(string firstName, string lastName, int priorityRating = 4)  // kostruktor
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.priorityRating = priorityRating;
        }


        public void SetActorRating()   // ta metoda mi się osobiście bardzo podoba ;) robi co trzeba i wygląda nieźle
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("  ________________________________________________________________________");
            Console.WriteLine("  ------------------------------------------------------------------------\n");
            Console.ResetColor();
            bool changingPriorityRanking = false;
            do
            {

                Console.WriteLine("{0,35}   | current Priority Ranking: {1,3}",
                GetActorNameAndLastName(), priorityRating);



                Console.Write("\nProvide new value of Priority Rating or press 'Enter' to skip further on ...");
                string answer2 = Console.ReadLine();
                bool isParsable = Int32.TryParse(answer2, out int newPriorityRating);
                if (isParsable && (newPriorityRating >= 1 && newPriorityRating <= 4))
                {
                    priorityRating = newPriorityRating;
                    Console.WriteLine("\nPriority Ranking changed");

                    changingPriorityRanking = true;
                    Console.Clear();
                }
                else if (isParsable)
                {
                    Console.WriteLine("\n\tPriority Ranking value must be between 1 and 4\n");
                }
                else if (!isParsable && answer2 == "")
                {
                    Console.WriteLine("\nPriority Ranking confirmed");
                    changingPriorityRanking = true;
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Let me ask you one more time.");
                }

            } while (changingPriorityRanking == false);


        }

        public int GetActorRating()
        {
            return priorityRating;
        }


        public string GetActorNameAndLastName()
        {
            string actorFNameaAndLName = string.Format($"{firstName} {lastName}");
            return actorFNameaAndLName;
        }

        public static void HireTheActor(Queue<Actor> actorsQueue)   // ta sama funkcja co dequue - u mnie zatrudnianie
        {
            if (actorsQueue.Count == 0)
            {
                Console.WriteLine("You have already hired all actors on your list.");
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n--------------------------------------------------------");
                Console.ResetColor();
                Console.WriteLine("\n  You have hired {0} {1}. Congratulations !",
                    actorsQueue.Peek().firstName, actorsQueue.Peek().lastName);


                if (actorsQueue.Peek().salaryIn2019 > 1)
                {
                    Console.WriteLine("Unfortunatly last year (2019) earnings of this actor was :" +
                    "\n\t\t {0} milion $. I hope you can handle it ;)",
                    actorsQueue.Peek().salaryIn2019);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.WriteLine("\n--------------------------------------------------------");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("You are lucky! This actor is not one of the 6 best paid actors in 2019." +
                        "\n   You will save some money on salaries ;)");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.WriteLine("\n--------------------------------------------------------");
                    Console.ResetColor();
                }
                actorsQueue.Dequeue();  // zdjęcie z kolejki
            }

        }

        public static List<Actor> CreateListOfActors()    // metoda wstawiająca 12 aktorów na listę
        {
            List<Actor> listOfActors = new List<Actor>();

            // aktorzy powyżej 50 mln dolarow za 2019
            Actor actor1 = new Actor("Dwayne", "Johnson");
            actor1.salaryIn2019 = 89;
            listOfActors.Add(actor1);
            Actor actor2 = new Actor("Chris", "Hemsworth");
            actor2.salaryIn2019 = 76;
            listOfActors.Add(actor2);
            Actor actor3 = new Actor("Robert", "Downey Jr");
            actor3.salaryIn2019 = 66;
            listOfActors.Add(actor3);
            Actor actor4 = new Actor("Akshay", "Kumar");
            actor4.salaryIn2019 = 65;
            listOfActors.Add(actor4);
            Actor actor5 = new Actor("Jackie", "Chan");
            actor5.salaryIn2019 = 58;
            listOfActors.Add(actor5);
            Actor actor6 = new Actor("Bradley", "Cooper");
            actor6.salaryIn2019 = 57;
            listOfActors.Add(actor6);
            // aktorki powyżej 25 mln dolarow za 2019
            Actor actor7 = new Actor("Scarlett", "Johansson");
            actor7.salaryIn2019 = 56;
            listOfActors.Add(actor7);
            Actor actor8 = new Actor("Sofia", "Vergara");
            actor8.salaryIn2019 = 44;
            listOfActors.Add(actor8);
            Actor actor9 = new Actor("Reese", "Witherspoon");
            actor9.salaryIn2019 = 35;
            listOfActors.Add(actor9);
            Actor actor10 = new Actor("Nicole", "Kidman");
            actor10.salaryIn2019 = 34;
            listOfActors.Add(actor10);
            Actor actor11 = new Actor("Jennifer", "Aniston");
            actor11.salaryIn2019 = 28;
            listOfActors.Add(actor11);
            Actor actor12 = new Actor("Kaley", "Cuoco");
            actor12.salaryIn2019 = 25;
            listOfActors.Add(actor12);

            return listOfActors;
        }


    }
    //  ufff.  było ciężko .  a do tego jeszcze jakaś tablica hasjująca miała być a w sumie nie ma ;(
}
