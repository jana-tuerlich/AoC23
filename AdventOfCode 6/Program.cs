using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode6
{
    
    class Program
    {

        static IEnumerable<(int time, int distance)> Races( IEnumerable<string> input )
        {
            var timeMatches = Regex.Matches( input.ElementAt( 0 ), @"\d+" );
            var distanceMatches = Regex.Matches( input.ElementAt( 1 ), @"\d+" );
            for(int i = 0; i < timeMatches.Count(); i++)
            {
                yield return (int.Parse(timeMatches[i].Value), int.Parse(distanceMatches[i].Value));
            }
        }

        static (long time, long distance) Race( IEnumerable<string> input )
        {
            var timeMatches = Regex.Matches( input.ElementAt( 0 ), @"\d+" );
            var distanceMatches = Regex.Matches( input.ElementAt( 1 ), @"\d+" );
            return (long.Parse( timeMatches.Aggregate("", (x, y) => x + y ) ), long.Parse( distanceMatches.Aggregate( "", ( x, y ) => x + y ) ) );
        }

        static long PossibleWins(long time, long distance)
        {
            double determinant = Math.Sqrt( Math.Pow( time, 2 ) - 4 * distance );

            double firstZero = (time - determinant) * 0.5;
            double secondZero = (time + determinant) * 0.5;

            long firstValid = (long)Math.Floor( firstZero ) + 1;
            long lastValid = (long)Math.Ceiling( secondZero ) - 1;

            return lastValid - firstValid + 1;
        }

        static string A(string[] lines)
        {
            return Races(lines).Select(race => PossibleWins(race.time, race.distance)).Aggregate( 1, ( x, y ) => (int)( x * y ) ).ToString();
        }

        static string B(string[] lines)
        {
            return PossibleWins(Race( lines ).time, Race(lines).distance).ToString(); ;
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
