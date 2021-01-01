using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradox
{
    class Door : IDoor
    {
        public bool isGifted;
        public bool isOpened;
        public bool isSelected;

        public Door(bool _isGifted, bool _isOpened)
        {
            this.isGifted = _isGifted;
            this.isOpened = _isOpened;
            this.isSelected = false;
        }
    }    
}
