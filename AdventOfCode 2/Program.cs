using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2
{
    
    class Program
    {
        static IEnumerable<( int id, int red, int green, int blue )> Games( IEnumerable<string> input )
        {
            foreach (string str in input)
            {
                var match = Regex.Match( str, @"Game (?<id>\d+):(( (?<blue>\d+) blue| (?<green>\d+) green| (?<red>\d+) red|,)*(;|))*" );
                var id = int.Parse( match.Groups["id"].Value );
                var red = match.Groups["red"].Captures.Select(c => int.Parse(c.Value)).Max();
                var green = match.Groups["green"].Captures.Select( c => int.Parse( c.Value ) ).Max();
                var blue = match.Groups["blue"].Captures.Select( c => int.Parse( c.Value ) ).Max();

                yield return (id, red, green, blue);
            }
        }

        static string A(string[] lines)
        {
            return Games(lines)
                .Where(game => game.red <= 12 && game.green <= 13 && game.blue <= 14)
                .Select(game => game.id)
                .Sum().ToString();
        }

        static string B(string[] lines)
        {
            return Games(lines)
                .Select(game => game.red * game.green * game.blue)
                .Sum().ToString();
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
