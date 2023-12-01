using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode1
{
    
    class Program
    {

        static string A(string[] lines)
        {
            return lines.Sum( line => 
            {
                char[] chars = line.ToCharArray();
                char firstNumber = chars.First( c => char.IsNumber( c ) );
                char lastNumber = chars.Last( c => char.IsNumber( c ) );
                return int.Parse( firstNumber.ToString() + lastNumber.ToString() );
            } 
            ).ToString();
        }

        static string B(string[] lines)
        {
            return A( lines.Select( line => line
                .Replace( "one", "one1one" )
                .Replace( "two", "two2two" )
                .Replace( "three", "three3three" )
                .Replace( "four", "four4four" )
                .Replace( "five", "five5five" )
                .Replace( "six", "six6six" )
                .Replace( "seven", "seven7seven" )
                .Replace( "eight", "eight8eight" )
                .Replace( "nine", "nine9nine" )
                ).ToArray() );
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
