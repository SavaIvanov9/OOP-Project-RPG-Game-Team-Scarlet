using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_ConsoleGame.Map
{
    public class Map
    {
        public char[,] ReadMap(string path)
        {
            StreamReader reader = new StreamReader(path);

            using (reader)
            {
                int lineNumber = 0;
                string line = reader.ReadLine();

                int cols = line.Length;

                List<string> elements = new List<string>();

                while (line != null)
                {
                    elements.Add(line);
                    lineNumber++;
                    line = reader.ReadLine();
                }

                int rows = lineNumber;
                char[,] mapMatrix = new char[rows, cols];

                int counterRows = 0;
                int counterCols = 0;

                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        if (counterCols == cols)
                            counterCols = 0;

                        mapMatrix[row, col] = elements[counterRows][counterCols];
                        counterCols++;
                    }
                    counterRows++;
                }
                return mapMatrix;
            }

        }
    }
}
