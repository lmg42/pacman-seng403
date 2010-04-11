﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pacman
{
    public class Edibles: GameCharacter
    {
        private String type;

        public Edibles(int X, int Y, String type) {
            this.x = X;
            this.y = Y;
            this.type = type;
        }

        public String getType() {
            return type ;
        }

        public void setType(String newType) {
            type = newType ;
        }
       
    }
}