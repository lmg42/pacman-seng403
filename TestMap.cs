using System;

class TestMap {

    public static void Main(string[] args) 
    {
        Map.GenerateMap(2);
        Map.printBoundaries();
        Map.printPathLogic();
        Map.printAll();
    }
}