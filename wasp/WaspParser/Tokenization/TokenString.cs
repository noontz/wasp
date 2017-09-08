using System;
using System.Text;

namespace wasp.Tokenization
{
    class TokenString : IEquatable<TokenString>
    {
        readonly int hashCode;

        readonly byte[] value;

        public TokenString(byte[] token)
        {
            if(token == null || token.Length == 0 || token.Length > 8)
                throw new ArgumentOutOfRangeException(nameof(token));
            value = token;
        }
        public TokenString(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentOutOfRangeException(nameof(token));

            var byteBuffer = Encoding.UTF8.GetBytes(token);

            if (byteBuffer.Length > 8)
                throw new ArgumentOutOfRangeException(nameof(token));

            value = byteBuffer;

            var counter = 0;
            var result = 0;
            for (var i = value.Length - 1; i >= 0; i--)
            {
                result += value[i] << counter;
                counter += 8;
            }
           hashCode = result;
        }

        public int Length => value.Length;

        public bool Equals(TokenString other)
        {
            if (value.Length != other.Length)
                return false;
            switch (value.Length)
            {
                case 1:
                    return Test1();
                case 2:
                    return Test2();
                case 3:
                    return Test3();
                case 4:
                    return Test4();
                case 5:
                    return Test5();
                case 6:
                    return Test6();
                case 7:
                    return Test7();
                case 8:
                    return Test8();
                default:
                    throw new ArgumentOutOfRangeException(nameof(value.Length));
            }

            bool Test1() => (value[0] ^ other.value[0]) == 0;
            bool Test2() => Test1() && (value[0] ^ other.value[0]) == 0;
            bool Test3() => Test2() && (value[0] ^ other.value[0]) == 0;
            bool Test4() => Test3() && (value[0] ^ other.value[0]) == 0;
            bool Test5() => Test4() && (value[0] ^ other.value[0]) == 0;
            bool Test6() => Test5() && (value[0] ^ other.value[0]) == 0;
            bool Test7() => Test6() && (value[0] ^ other.value[0]) == 0;
            bool Test8() => Test7() && (value[0] ^ other.value[0]) == 0;
        }

        public override int GetHashCode() => hashCode;
       
        public override string ToString()
        {
            var buffer = new char[value.Length];
            for (var i = value.Length - 1; i >= 0; i -= 1)
                buffer[i] = Convert.ToChar(value[i]);
            return new string(buffer);
        }
    }
}