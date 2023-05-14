using System.Diagnostics;
Random random = new Random();
Stopwatch stopwatch = new Stopwatch();
TimeSpan targetReaction;
TimeSpan signal = TimeSpan.FromMilliseconds(random.Next(5000, 10000));
TimeSpan reactionTime = default;
string input;
bool clickedFast = false;
bool clicked = false;

const string menu = @"
Quick Draw
Face your opponent and wait for the signal. Once the
signal is given, shoot your opponent by pressing [space] before they shoot you. It's all about your reaction time.
Choose Your Opponent:
[1] Easy....1000 milliseconds
[2] Medium...500 milliseconds
[3] Hard.....250 milliseconds
[4] Harder...125 milliseconds";
const string wait = @"

          _O       O_
        |/|_ wait _|\|
        /\          /\
        / |         | \
------------------------------------------------------";
const string start = @"

        ********
        * FIRE *
     _O ******** O_
    |/|_        _|\|
    /\  spacebar  /\
   / |            | \
------------------------------------------------------";
const string tooSlow = @"

                 > ╗__O
     // Too Slow      / \
    O/__/\ You Lose /\
          \        | \
------------------------------------------------------";
const string tooFast = @"

        Too Fast > ╗__O
     // You Missed   / \
    O/__/\ You Lose /\
          \        | \
------------------------------------------------------";
const string W = @"

    O__╔ <
    / \                 \\
      /\ You Win      /\__\O
     / |             /
------------------------------------------------------";
Console.Write(menu);
while (true)
{
    input = Console.ReadLine();
    switch (input)
    {
        case "1": targetReaction = TimeSpan.FromMilliseconds(1000); break;
        case "2": targetReaction = TimeSpan.FromMilliseconds(500); break;
        case "3": targetReaction = TimeSpan.FromMilliseconds(250); break;
        case "4": targetReaction = TimeSpan.FromMilliseconds(125); break;
        default:
        continue;
    }
    Console.Clear();
    Console.Write(wait);
    stopwatch.Start();
    while (stopwatch.Elapsed < signal && !clickedFast)
    {
        if(Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Spacebar){clickedFast = true;}
    }
    Console.Clear();
    if (clickedFast) {Console.Write(tooFast);}
    else
    {
        Console.Write(start);
        stopwatch.Restart();

        while (stopwatch.Elapsed < targetReaction && !clicked)
        {
            if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
                clicked = true;
                reactionTime = stopwatch.Elapsed;
                stopwatch.Stop();
            }
        }
        Console.Clear();
        if (!clicked) {Console.Write(tooSlow);}
        else
        {
            Console.Write(W);
            Console.WriteLine($"Reaction Time:" + $"{reactionTime.TotalMilliseconds} miliseconds");
        }
    }
}