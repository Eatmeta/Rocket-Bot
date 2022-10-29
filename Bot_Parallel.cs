using System;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket)
        {
            var tasks = new Task<Tuple<Turn, double>>[ThreadsCount];

            for (var i = 0; i < ThreadsCount; i++)
            {
                tasks[i] = Task.Run(() =>
                    SearchBestMove(rocket, new Random(new Random(i).Next()), IterationsCount / ThreadsCount));
            }

            while (true)
            {
                if (tasks.All(t => t.IsCompleted))
                    return rocket.Move(tasks.OrderByDescending(r => r.Result.Item2).First().Result.Item1, Level);
            }
        }
    }
}