using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TestFirst.OXGame.ConsoleApp
{
    public class BoardGame
    {
        public bool IsDraw { get; set; }
        public int OScore { get; set; }
        public int XScore { get; set; }
        public string[,] Slots { get; set; }
        public string CurrentTurn { get; set; }

        public BoardGame()
        {
            Slots = new string[3, 3];
        }

        public bool Place(string symbol, int row, int column)
        {
            Slots[row, column] = symbol;

            var isEvenNumber = Slots.Cast<string>().Count(it => it != null) % 2 == 0;
            CurrentTurn = isEvenNumber ? "X" : "O";

            return true;
        }

        public string GetWinner()
        {
            var allPossibilities = getRowPossibilities()
                .Union(getColumnPossibilities())
                .Union(getCrossLinePossibilities());

            const int MinimumRequiredCharacters = 3;
            var winnerSpot = allPossibilities
                .Where(it => it.Length == MinimumRequiredCharacters)
                .FirstOrDefault(it => it.All(c => c == it.First()));

            var winnerSymbol = winnerSpot?.First().ToString();

            const string XSymbol = "X";
            const string OSymbol = "O";

            if (winnerSymbol == XSymbol)
            {
                XScore++;
            }
            else if (winnerSymbol == OSymbol)
            {
                OScore++;
            }
            else
            {
                var anyEmptySpace = Slots.Cast<string>().Any(it => it == null);
                IsDraw = !anyEmptySpace;
            }

            return winnerSymbol;
        }

        private IEnumerable<string> getRowPossibilities()
        {
            return new string[]
            {
                $"{Slots[0,0]}{Slots[0,1]}{Slots[0,2]}",
                $"{Slots[1,0]}{Slots[1,1]}{Slots[1,2]}",
                $"{Slots[2,0]}{Slots[2,1]}{Slots[2,2]}",
            };
        }

        private IEnumerable<string> getColumnPossibilities()
        {
            return new string[]
            {
                $"{Slots[0,0]}{Slots[1,0]}{Slots[2,0]}",
                $"{Slots[0,1]}{Slots[1,1]}{Slots[2,1]}",
                $"{Slots[0,2]}{Slots[1,2]}{Slots[2,2]}",
            };
        }

        private IEnumerable<string> getCrossLinePossibilities()
        {
            return new string[]
            {
                $"{Slots[0,0]}{Slots[1,1]}{Slots[2,2]}",
                $"{Slots[2,0]}{Slots[1,1]}{Slots[0,2]}",
            };
        }
    }
}
