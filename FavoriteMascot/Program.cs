using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace FavoriteMascot
{
    class Program
    {
        public class Ballot
        {
            public int Id { get; set; }
            public List<string> Vote { get; set; }
        }

        static void Main(string[] args)
        {
            //Read files from args
            foreach (var arg in args)
            { 
                List<Ballot> votes = JsonConvert.DeserializeObject<List<Ballot>>(File.ReadAllText(arg));
                Console.WriteLine($"{arg}: {GetWinner(votes)} is the winner!");
            }
        }
        public static string GetWinner(List<Ballot> votes)
        {
            //Calculate results
            var results = CalculateResults(votes);
            //Rule 1: the one with 50% (or more) is named the winner
            if (results.Any(r => r.Value >= 50))
                return results.FirstOrDefault(r => r.Value >= 50).Key;
            //Rule 2: the most first-choiced is named the winner (only if all are below 15%)
            if (results.All(r => r.Value < 15))
                return results.OrderByDescending(r => r.Value).FirstOrDefault().Key;
            //Rule 3.1: clear the ones below 15% & recursive calculation
            var clearedVotes = votes.Select(ballot =>
            {
                ballot.Vote.RemoveAll(mascot => !results.ContainsKey(mascot) || results[mascot] < 15);
                return ballot;
            }).ToList();
            //Exercise states that I "may assume that any ballot data provided will result in a winner"
            //"In other words, it is impossible to end up in an infinite loop", so no need to add initial condition
            return GetWinner(clearedVotes);
        }
        public static Dictionary<string, double> CalculateResults(List<Ballot> votes)
        {
            //Remove empty votes
            votes.RemoveAll(ballot => !ballot.Vote.Any());
            //First group by first-choice, then calculate % of votes and create dictionary with results
            return votes
                .GroupBy(ballot => ballot.Vote.FirstOrDefault())
                .ToDictionary(mascot => mascot.Key,
                mascot => (double)mascot.Count() / votes.Count * 100);
        }
    }
}
