﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemo.Id
{
    public class UniqueNegativeNumberGenerator : IIdGenerator
    {
        private int _seed;
        private readonly int _origSeed;
        private bool _throwWhenOutOfNumbers = false;

        public UniqueNegativeNumberGenerator() 
        { 
            _seed = (int)DateTime.Now.Ticks * 100;
            _origSeed = _seed;
        }
        public UniqueNegativeNumberGenerator(bool throwWhenOutOfNumbers)
            : this()
        {
            _throwWhenOutOfNumbers = throwWhenOutOfNumbers;
        }

        public object Generate()
        {
            int n = _seed;  
            do 
            {    
                n ^= (n << 13);
                n ^= (int)((uint)n >> 17); //performs unsigned right shift    
                n ^= (n << 5);    
                _seed = n;    
                if (n == _origSeed && _throwWhenOutOfNumbers) 
                {      
                    throw new Exception("Run out of numbers!");    
                }  
            } while (n > 0);  
            return n;
        }
    }
}
