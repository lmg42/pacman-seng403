﻿using System;

/*
 * Each entry of MapArray is a 4-Bit Code
 * Each digit of the code corresponds to a direction
 * xxxx = NESW (North-East-South-West) or URDL (Up-Right-Down-Left)
 * 
 * A "0" in one of the digit places means one cannot move in that direction from the current spot
 * A "1" in one of the digit places means one is able to move in that direction from the current spot
 * 
 * Example: 1001 means that one can move North or West from the current position
 *
 * 
 * Bit Codes are translated into hex for identification
 * 
 * Example: 1001 = 0x9
 * 
 */

public enum Directions
{
    NONE = 0x0,
    UP = 0x8,
    DOWN = 0x4,
    LEFT = 0x2,
    RIGHT = 0x1,
    UP_DOWN = 0xC,
    UP_LEFT = 0xA,
    UP_RIGHT = 0x9,
    DOWN_LEFT = 0x6,
    DOWN_RIGHT = 0x5,
    LEFT_RIGHT = 0x3,
    UP_DOWN_LEFT = 0xE,
    UP_DOWN_RIGHT = 0xD,
    UP_LEFT_RIGHT = 0xB,
    DOWN_LEFT_RIGHT = 0x7,
    UP_DOWN_LEFT_RIGHT = 0xF
}


class Map {    
    
    Directions[,] MapArray;
    int rows = 0;
    int columns = 0;

    // Constructor for a Map object
    // "preset" corresponds to a pre-made map layout
    public Map(int preset) 
    {
        if (preset == 1)
        {
            rows = 10;
            columns = 10;
            MapArray = new Directions[rows, columns];
            blankMap();
            
            horCarve(1, 1, 8);
            horCarve(3, 1, 5);
            horCarve(5, 1, 5);
            verCarve(1, 1, 5);
            verCarve(3, 1, 5);
            verCarve(5, 1, 5);
            verCarve(8, 1, 8);
            horCarve(8, 8, 2);
            verCarve(2, 8, 6);
            horCarve(6, 5, 7);
        }
    }

    // Returns the value at a certain coordinate in MapArray
    public Directions getMapEntry(int x, int y)
    {
        return MapArray[x, y];
    }

    // Prints the "walls" of the map to console
    // Useful to see hallway stucture
    public void printBoundaries()
    {
        int i;
        int j;
        for (i = 0; i < rows; i++)
        {
            for (j = 0; j < columns; j++)
            {
                if (MapArray[i, j] == Directions.NONE)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.Write("\n");
        }
    }

    // Prints the MapArray entry of open spaces (as in, not walls)
    // Useful to see if spaces connect properly
    public void printPathLogic()
    {
        int i;
        int j;
        for (i = 0; i < rows; i++)
        {
            for (j = 0; j < columns; j++)
            {
                if (MapArray[i, j] != Directions.NONE)
                {
                    Console.Write(String.Format("{0:x}", (int)MapArray[i,j]));
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.Write("\n");
        }
    }

    // printBoundaries() and printPathLogic() together
    public void printAll()
    {
        int i;
        int j;
        for (i = 0; i < rows; i++)
        {
            for (j = 0; j < columns; j++)
            {
                if (MapArray[i, j] != Directions.NONE)
                {
                    Console.Write(String.Format("{0:x}", (int)MapArray[i, j]));
                }
                else
                {
                    Console.Write("#");
                }
            }

            Console.Write("\n");
        }

    }

    // Converts map into entirely walls / impassable spaces
    private void blankMap()
    {
        int i;
        int j;
        for (i = 0; i < rows; i++)
        {
            for (j = 0; j < columns; j++)
            {
                MapArray[i, j] = Directions.NONE;
            }
        }
    }

    // Carves a horizontal hallway into map
    private void horCarve(int row, int y1, int y2)
    {
        if (y1 > y2)
        {
            int temp = y1;
            y1 = y2;
            y2 = temp;
        }

        int i = y1;
        if (y1 != y2)
        {
            MapArray[row, i] = MapArray[row, i] | Directions.RIGHT;
        }

        for (i = y1; i <= y2; i++)
        {
            // Check up
            if (MapArray[row - 1, i] != Directions.NONE) {
                MapArray[row - 1, i] = MapArray[row - 1, i] | Directions.DOWN;
                MapArray[row, i] = MapArray[row, i] | Directions.UP;
            }

            // Check down
            if (MapArray[row + 1, i] != Directions.NONE)
            {
                MapArray[row + 1, i] = MapArray[row + 1, i] | Directions.UP;
                MapArray[row, i] = MapArray[row, i] | Directions.DOWN;
            }

            // Check left
            if (MapArray[row, i-1] != Directions.NONE)
            {
                MapArray[row, i-1] = MapArray[row, i-1] | Directions.RIGHT;
                MapArray[row, i] = MapArray[row, i] | Directions.LEFT;
            }

            // Check right
            if (MapArray[row, i + 1] != Directions.NONE)
            {
                MapArray[row, i + 1] = MapArray[row, i + 1] | Directions.LEFT;
                MapArray[row, i] = MapArray[row, i] | Directions.RIGHT;
            }
        }

        if (y1 != y2)
        {
            MapArray[row, i-1] = MapArray[row, i-1] | Directions.LEFT;
        }
    }

    // Carves a vertical hallway into map
    private void verCarve(int column, int x1, int x2)
    {
        if (x1 > x2)
        {
            int temp = x1;
            x1 = x2;
            x2 = temp;
        }

        int i = x1;
        if (x1 != x2)
        {
            MapArray[i, column] = MapArray[i, column] | Directions.DOWN;
        }

        for (i = x1; i <= x2; i++)
        {
            // Check up
            if (MapArray[i - 1, column] != Directions.NONE) {
                MapArray[i - 1, column] = MapArray[i - 1, column] | Directions.DOWN;
                MapArray[i, column] = MapArray[i, column] | Directions.UP;
            }

            // Check down
            if (MapArray[i + 1, column] != Directions.NONE) {
                MapArray[i + 1, column] = MapArray[i + 1, column] | Directions.UP;
                MapArray[i, column] = MapArray[i, column] | Directions.DOWN;
            }

            // Check left
            if (MapArray[i, column - 1] != Directions.NONE)
            {
                MapArray[i, column - 1] = MapArray[i, column - 1] | Directions.RIGHT;
                MapArray[i, column] = MapArray[i, column] | Directions.LEFT;
            }

            // Check right
            if (MapArray[i, column + 1] != Directions.NONE)
            {
                MapArray[i, column + 1] = MapArray[i, column + 1] | Directions.LEFT;
                MapArray[i, column] = MapArray[i, column] | Directions.RIGHT;
            }

        }

        if (x1 != x2)
        {
            MapArray[i-1, column] = MapArray[i-1, column] | Directions.UP;
        }
    }
}