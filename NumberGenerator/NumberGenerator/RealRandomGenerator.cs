using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace NumberGenerator
{
    class RealRandomGenerator
    {
        RNGCryptoServiceProvider generator = null;

        public RealRandomGenerator()
        {
            generator = new RNGCryptoServiceProvider();
        }

        public uint NextInt()
        {
            byte[] data = new byte[10];
            generator.GetBytes(data);
            return (uint)data.GetHashCode();
        }

        public ushort NextShort()
        {
            byte[] data = new byte[10];
            generator.GetBytes(data);
            return (ushort)data.GetHashCode();
        }
    }
}
