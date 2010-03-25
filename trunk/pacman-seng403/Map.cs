using System;


    class Map
    {

        static private Directions[,] MapArray;
        static private int rows = 0;
        static private int columns = 0;

        // Returns the value at a certain coordinate in MapArray
        static public Directions getMapEntry(int x, int y)
        {
            return MapArray[y, x];
        }

        // Generates a map based on a preset layout for the specified level
        static public void GenerateMap(int level)
        {
            switch (level)
            {
                case 1:
                    rows = 25;
                    columns = 25;
                    MapArray = new Directions[rows, columns];
                    blankMap();

                    // Ghost Box
                    // verCarve(12, 11, 13);
                    // horCarve(12, 10, 14);
                    // horCarve(13, 10, 14);
                    verCarve(12, 10, 15);


                    // Mid Segment
                    horCarve(10, 8, 16);
                    verCarve(8, 10, 15);
                    verCarve(16, 10, 15);
                    horCarve(15, 8, 16);


                    // Top Segment
                    horCarve(1, 1, 8);
                    horCarve(1, 16, 23);
                    verCarve(1, 1, 4);
                    verCarve(8, 1, 6);
                    verCarve(16, 1, 6);
                    verCarve(23, 1, 4);
                    horCarve(3, 8, 16);
                    horCarve(4, 1, 8);
                    horCarve(4, 16, 23);
                    horCarve(6, 8, 11);
                    horCarve(6, 13, 16);
                    verCarve(11, 6, 8);
                    verCarve(13, 6, 8);
                    verCarve(10, 8, 10);
                    verCarve(14, 8, 10);
                    verCarve(3, 4, 8);
                    horCarve(8, 4, 6);
                    verCarve(6, 8, 11);
                    horCarve(11, 6, 8);
                    verCarve(21, 4, 8);
                    horCarve(8, 18, 21);
                    verCarve(18, 8, 11);
                    horCarve(11, 16, 18);


                    // Bottom Segment
                    horCarve(23, 1, 5);
                    horCarve(23, 10, 14);
                    horCarve(23, 19, 23);
                    horCarve(22, 5, 10);
                    horCarve(22, 14, 19);
                    horCarve(21, 1, 5);
                    horCarve(21, 19, 23);
                    horCarve(19, 1, 23);
                    verCarve(1, 19, 23);
                    verCarve(5, 15, 23);
                    verCarve(12, 19, 23);
                    verCarve(19, 15, 23);
                    verCarve(23, 19, 23);
                    horCarve(15, 5, 19);
                    verCarve(9, 17, 19);
                    verCarve(15, 17, 19);
                    horCarve(17, 9, 15);
                    verCarve(12, 16, 16);


                    // Left Segment
                    horCarve(11, 1, 3);
                    horCarve(13, 1, 8);
                    verCarve(3, 11, 13);
                    verCarve(1, 6, 11);
                    horCarve(6, 1, 3);
                    verCarve(1, 13, 16);
                    horCarve(16, 1, 4);


                    // Right Segment
                    horCarve(11, 21, 23);
                    horCarve(13, 16, 23);
                    verCarve(21, 11, 13);
                    verCarve(23, 6, 11);
                    horCarve(6, 21, 23);
                    verCarve(23, 13, 16);
                    horCarve(16, 20, 23);


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
                        Console.Write(String.Format("{0:x}", (int)MapArray[i, j]));
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
                if (MapArray[row - 1, i] != Directions.NONE)
                {
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
                if (MapArray[row, i - 1] != Directions.NONE)
                {
                    MapArray[row, i - 1] = MapArray[row, i - 1] | Directions.RIGHT;
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
                MapArray[row, i - 1] = MapArray[row, i - 1] | Directions.LEFT;
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
                if (MapArray[i - 1, column] != Directions.NONE)
                {
                    MapArray[i - 1, column] = MapArray[i - 1, column] | Directions.DOWN;
                    MapArray[i, column] = MapArray[i, column] | Directions.UP;
                }

                // Check down
                if (MapArray[i + 1, column] != Directions.NONE)
                {
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
                MapArray[i - 1, column] = MapArray[i - 1, column] | Directions.UP;
            }
        }
    }
