using System;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable NonReadonlyMemberInGetHashCode
// hash code is used in TokensMap where values are unchanged during application life
namespace wasp.Tokenization
{
    /// <summary>
    ///     Encapsulates logic for encompasing 8 bytes representing UTF8 characters in a long
    /// </summary>
    struct TokenString : IEquatable<TokenString>
    {
        /// <summary>
        ///     Indicatees if the TokenString holding any value
        /// </summary>
        public bool HasValue => value != 0;

        /// <summary>
        ///     Indicates if the TokenStrings value is a number
        /// </summary>
        public bool IsNumber { get; private set; }

        /// <summary>
        ///     The current lenght of the value
        /// </summary>
        byte currentLength;

        /// <summary>
        ///     The TokenStrings 8 bytes represented in a long
        /// </summary>
        long value;

        /// <summary>
        ///     Instantiates a TokenString with its first character
        /// </summary>
        /// <param name="firstCharacter"></param>
        public TokenString(byte firstCharacter)
        {
            value = firstCharacter;
            currentLength = 1;
            IsNumber = firstCharacter > 47 && firstCharacter < 58;
        }

        /// <summary>
        ///     Instantiates a TokenString with a 1 - 8 length string of letters
        /// </summary>
        /// <param name="token"></param>
        public TokenString(string token)
        {
            if (Regex.IsMatch(token, @"[0-9]+"))
                throw new ArgumentOutOfRangeException($"{nameof(token)} \"{token}\" cannot include numbers");
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

        /// <summary>
        ///     Adds a character to the TokenString and returns true if successfull
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public bool AddCharacter(byte character)
        {
            if (currentLength == 8)
                return false;
            if (character > 47 && character < 58)
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

        /// <summary>
        ///     Clears the TokenString Value
        /// </summary>
        public void Clear()
        {
            value = 0;
            currentLength = 0;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = "";
            for (var i = currentLength - 1; i >= 0; i--)
                result += Convert.ToChar((value >> (i * 8)) & 0x00_00_00_00_00_00_00_FF);
            return result;
        }

        /// <inheritdoc />
        public bool Equals(TokenString other) => value == other.value;

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (int) (value ^ (value >> 32));
            }
        }
    }
}