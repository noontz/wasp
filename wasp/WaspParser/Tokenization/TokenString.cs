using System;
using System.Text;

// ReSharper disable NonReadonlyMemberInGetHashCode
// hash code is used in TokensMap where values are unchanged during application life
namespace wasp.Tokenization
{
    struct TokenString : IEquatable<TokenString>
    {
        public bool HasValue => value != 0;

        public bool IsNumber { get; private set; }

        byte currentLength;

        long value;

        public TokenString(byte firstCharacter)
        {
            value = firstCharacter;
            currentLength = 1;
            IsNumber = firstCharacter > 47 && firstCharacter < 58;
        }

        public TokenString(string token)
        {
            //TODO check for numbers with REGEX and throw exception if any
            value = 0;
            currentLength = 0;
            IsNumber = false;
            if (token.Length > 8)
                throw new ArgumentOutOfRangeException(nameof(token));
            if (string.IsNullOrEmpty(token))
                return;
            var byteBuffer = Encoding.UTF8.GetBytes(token);
            for (var i = 0; i < byteBuffer.Length; i++)
                value += (long) byteBuffer[i] << ((byteBuffer.Length - i - 1) * 8);
            currentLength = (byte) byteBuffer.Length;
        }

        public bool AddCharacter(byte character)
        {
            if (currentLength == 8)
                return false;
            if (character > 47 || character < 58)
            {
                if (!IsNumber && currentLength > 0)
                    return false;
                IsNumber = true;
            }
            value = value << 8;
            value += character;
            currentLength++;
            return true;
        }

        public void Clear()
        {
            value = 0;
            currentLength = 0;
        }

        public override string ToString()
        {
            var result = "";
            for (var i = currentLength - 1; i >= 0; i--)
                result += Convert.ToChar((value >> (i * 8)) & 0x00_00_00_00_00_00_00_FF);
            return result;
        }

        public bool Equals(TokenString other) => value == other.value;

        public override int GetHashCode()
        {
            unchecked
            {
                return (int) (value ^ (value >> 32));
            }
        }
    }
}