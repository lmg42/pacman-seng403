using System;

class TestMap {

    public static void Main(string[] args) 
    {
        Map M = new Map(1);
        M.printBoundaries();
        M.printPathLogic();
        M.printAll();
    }
}