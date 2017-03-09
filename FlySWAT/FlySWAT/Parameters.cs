using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlySWAT
{
    class Parameters
    {
        private int screenWidth;
        private int screenHeight;
        private bool isMouseVisible;
        public int ScreenWidth 
        {
            get { return screenWidth; }
            set { screenWidth = value; }

            }
        public int ScreenHeight
        {
            get { return screenHeight; }
            set { screenHeight = value; } 
        
        }
        public bool IsMouseVisible
        {
            set { isMouseVisible = value;  }
            get {  return isMouseVisible; }

            }
        
        public Parameters(int newScreenWidth, int newScreenHeight)
        {
            screenWidth = newScreenWidth;
            screenHeight = newScreenHeight;
        }
        }
}
