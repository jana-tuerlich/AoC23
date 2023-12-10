using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode4
{
    
    class Program
    {
        static IEnumerable<int> Cards( IEnumerable<string> input )
        {
            foreach (string str in input)
            {
                var match = Regex.Match( str, @"Card( )*(?<id>\d+): ((?<winning>\d+)*( )*)*\|((?<chosen>\d+)*( )*)*" );
                List<int> winning = match.Groups["winning"].Captures.Select(capture => int.Parse(capture.Value)).ToList();
                List<int> chosen = match.Groups["chosen"].Captures.Select(capture => int.Parse(capture.Value)).ToList();

                yield return chosen.Count( number => winning.Contains( number ) );
            }
        }

        static string A(string[] lines)
        {
            return Cards( lines ).Select( c => c == 0 ? 0 : (int)Math.Pow( 2, c - 1 ) ).Sum().ToString();
        }

        static string B(string[] lines)
        {
            List<int> cards = Cards( lines ).ToList();

            List<Tuple<int, int>> cardInstances = new List<Tuple<int, int>>();
            foreach(int card in cards)
            {
                cardInstances.Add( new Tuple<int, int>(card, 1) );
            }
            int result = Evaluate( cardInstances );

            return result.ToString();
        }

        private static int Evaluate( List<Tuple<int, int>> cardInstances )
        {
            if(!cardInstances.Any())
            {
                return 0;
            }

            var firstCard = cardInstances.First();
            int numberOfWins = firstCard.Item1;
            int numberOfCards = firstCard.Item2;
            cardInstances.Remove( firstCard );
            for(int i = 0; i < numberOfWins; i++)
            {
                var number = cardInstances[i].Item2;
                number += numberOfCards;
                cardInstances[i] = new Tuple<int, int>( cardInstances[i].Item1, number );
            } 

            return numberOfCards + Evaluate( cardInstances);
        }


        static void PrintRuntime( Func<string> action )
        {
            var startTime = DateTime.Now;

            Console.WriteLine( action() );

            var endTime = DateTime.Now;

            Console.WriteLine( $"It took {( endTime - startTime ):g}" );
        }

        static void Main( string[] args )
        {
            string[] testInputLines = File.ReadAllLines( "TestInput.txt" );
            string[] inputLines = File.ReadAllLines( "Input.txt" );
            PrintRuntime(() => A( testInputLines ) );
            PrintRuntime(() => A( inputLines ) );
            PrintRuntime(() => B( testInputLines ) );
            PrintRuntime(() => B( inputLines ) );
        }
    }
}
