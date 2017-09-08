using System;
using System.Collections.Generic;
using System.Text;

namespace wasp.Tokenization
{
    class TokenStringComparer : IEqualityComparer<TokenString>
    {
        public bool Equals(TokenString x, TokenString y)
        {
            if (x.Length !=y.Length)
                return false;

            return (x.Value[0] ^ y.Value[0]) +
                   (x.Value[1] ^ y.Value[1]) +
                   (x.Value[2] ^ y.Value[2]) +
                   (x.Value[3] ^ y.Value[3]) +
                   (x.Value[4] ^ y.Value[4]) +
                   (x.Value[5] ^ y.Value[5]) +
                   (x.Value[6] ^ y.Value[6]) +
                   (x.Value[7] ^ y.Value[7]) == 0;
        }

        public int GetHashCode(TokenString obj)
        {
            var counter = 0;
            var result = 0;
            for (var i = obj.Value.Length - 1; i >= 0; i--)
            {
                result += obj.Value[i] << counter;
                counter += 8;
            }
            return result;
        }
    }
}
