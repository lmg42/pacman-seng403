using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pacman
{
    public class GameData
    {
        public static int score = 0 ;
        public static int level = 1 ;
        public static int numLives = 3;
        public static Boolean gameDone = false ;

       public GameData() {
            score = 0;
            level = 1;
            numLives = 3;
            gameDone = false;
        }

        public int getScore() {
            return score;
        }

        public int getLevel() {
            return level;
        }

        public int getNumLives() {
            return numLives;
        }

        public static void incrementScore(int points) {
            if (points > 0)
                score += points;
        }

        public static void decrementScore(int points) {
            if (points > 0)
                score -= points ;
        }

        public static void incrementLevel(){
            level++;
        }

        public static void decrementLevel() {
            level--;
        }

        public static void incrementNumLives() {
            numLives++;
        }

        public static void decrementNumLives() {
            if (numLives >= 0)
                numLives--;
            if (numLives == -1)
                gameDone = true;
        }

        public Boolean getGameStatus() {
            return gameDone;
        }
        /*
        static void Main() {
        }
         */
    }
         
}