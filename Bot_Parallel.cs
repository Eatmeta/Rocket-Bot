using System;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket)
        {
            // TODO: распараллелить запуск SearchBestMove
            var bestMove = SearchBestMove(rocket, new Random(Random.Next()), IterationsCount);
            var newRocket = rocket.Move(bestMove.Item1, Level);
            return newRocket;
        }
    }
}