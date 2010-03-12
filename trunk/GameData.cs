using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pacman
{
    public class GameData
    {
        private int score;
        private int level;
        private int numLives;
        private Boolean gameDone;

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

        public void incrementScore(int points) {
            if (points > 0)
                score += points;
        }

        public void decrementScore(int points) {
            if (points > 0)
                score -= points ;
        }

        public void incrementLevel(){
            level++;
        }

        public void decrementLevel() {
            level--;
        }

        public void incrementNumLives() {
            numLives++;
        }

        public void decrementNumLives() {
            if (numLives >= 0)
                numLives--;
            if (numLives == -1)
                gameDone = true;
        }

        public Boolean getGameStatus() {
            return gameDone;
        }

        static void Main() {
        }
    }
}