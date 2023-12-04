using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode3
{
    
    class Program
    {

        static string A(string[] lines)
        {
            return lines.Select(line => {
                var numberMatches = Regex.Matches( line, @"\d+" );
                return numberMatches.Select( numberMatch => {

                    int lineIndex = Array.IndexOf( lines, line );
                    int firstLineIndex = lineIndex == 0 ? 0 : lineIndex - 1;
                    int lastLineIndex = lineIndex == lines.Length - 1 ? lineIndex : lineIndex + 1;

                    int firstCharIndex = numberMatch.Index == 0 ? numberMatch.Index : numberMatch.Index - 1;
                    int lastCharIndex = numberMatch.Index + numberMatch.Length == line.Length ? numberMatch.Index + numberMatch.Length - 1: numberMatch.Index + numberMatch.Length;
                    
                    bool isPart = false;
                    for (int i = firstLineIndex; i <= lastLineIndex; i++)
                    {
                        for(int j = firstCharIndex; j <= lastCharIndex; j++)
                        {
                            var adjacentChar = lines[i][j];
                            isPart |= !char.IsNumber( adjacentChar ) && adjacentChar != '.';
                        }
                    }
                    return isPart ? int.Parse( numberMatch.Value ) : 0;

                    } ).Sum();
            } ).Sum().ToString();
        }

        static string B(string[] lines)
        {
            return lines.Select(line => {
                var gearMatches = Regex.Matches( line, @"\*" );
                return gearMatches.Select( gearMatch =>
                {
                    
                    int lineIndex = Array.IndexOf( lines, line );
                    int firstLineIndex = lineIndex == 0 ? 0 : lineIndex - 1;
                    int lastLineIndex = lineIndex == lines.Length - 1 ? lineIndex : lineIndex + 1;
                    
                    var gearIndex = gearMatch.Index;
                    var gearRatio = 1;
                    int parts = 0;
                    for (int i = firstLineIndex; i <= lastLineIndex; i++)
                    {
                        MatchCollection numberMatches = Regex.Matches( lines[i], @"\d+" );
                        foreach (Match numberMatch in numberMatches)
                        {
                            if (gearIndex >= numberMatch.Index - 1 && gearIndex <= numberMatch.Index + numberMatch.Length)
                            {
                                gearRatio *= int.Parse( numberMatch.Value );
                                parts++;
                            }
                        }
                    }
                    return parts == 2 ? gearRatio : 0;
                } ).Sum();
            } ).Sum().ToString();
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
