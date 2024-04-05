using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBot.Core.NoDbModels
{
    public class Paggination
    {
        int _minSize = 10;
        int _maxSize = 30;
        int _pageSize = 10;
        public int Current { get; set; } = 1;
        public int PageSize 
        {
            get { return _pageSize; }
            set
            {
                if(_minSize > value)
                {
                    _pageSize = _minSize;
                }
                if(_maxSize < value)
                {
                    _pageSize = _maxSize;
                }
                _pageSize = value;
            }
        }

    }
}
