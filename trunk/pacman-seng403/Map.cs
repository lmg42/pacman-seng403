using System;


class Map {
    public static readonly int BLOCKSIZE = 10;

    static private Directions[,] MapArray;
    static private int rows = 0;
    static private int columns = 0;

    // Returns the value at a certain coordinate in MapArray
    static public Directions getMapEntry(int y, int x)
    {
        return MapArray[x, y];
    }

    // Generates a map based on a preset layout for the specified level
    static public void GenerateMap(int level)
    {
        switch (level)
        {
            case 1:
                rows = 50;
                columns = 50;
                MapArray = new Directions[rows, columns];
                blankMap();
                horCarve(10, 10, 39);
                verCarve(10, 10, 39);
                horCarve(39, 10, 39);
                verCarve(39, 10, 39);
                horCarve(16, 10, 39);
                horCarve(22, 10, 39);
                horCarve(27, 10, 39);
                horCarve(33, 10, 39);
                verCarve(16, 10, 16);
                verCarve(33, 10, 16);
                verCarve(20, 16, 21);
                verCarve(29, 16, 21);
                verCarve(20, 28, 33);
                verCarve(29, 28, 33);
                verCarve(16, 34, 39);
                verCarve(33, 34, 39);
                horCarve(13, 17, 33);
                horCarve(36, 17, 33);
                verCarve(16, 22, 26);
                verCarve(33, 22, 26);

                verCarve(24, 22, 24);
                verCarve(25, 22, 24);
                horCarve(24, 22, 27);
                horCarve(25, 22, 27);
                break;
            case 2:
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
                break;
            default:
                break;
        }
    }

    // Prints the "walls" of the map to console
    // Useful to see hallway stucture
    static public void printBoundaries()
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
    static public void printPathLogic()
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
    static public void printAll()
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
    static private void blankMap()
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
    static private void horCarve(int row, int y1, int y2)
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
    static private void verCarve(int column, int x1, int x2)
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