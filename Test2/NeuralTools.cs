using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
    public  class NeuralTools
    {

        public float Aktivasyon(float x) // Sigmoid fonksiyonu kullandık.
        {
            return (float)(1.0f / (1.0f + Math.Exp(-x)));
        }
    }
}
