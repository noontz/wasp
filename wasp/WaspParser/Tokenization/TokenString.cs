using System;
using System.Linq;
using System.Text;

namespace wasp.Tokenization
{
    class TokenString
    {
        readonly int currentLength;

        public TokenString(byte theByte)
        {
            Value = new byte[8];
            Value[0] = theByte;
            currentLength = 1;
        }

        public TokenString(byte[] bytes)
        {
            Value = new byte[8];
            if (bytes == null || bytes.Length == 0 || bytes.Length > 8)
                throw new ArgumentOutOfRangeException(nameof(bytes));
            Buffer.BlockCopy(bytes, 0, Value, 0, bytes.Length);
            currentLength = bytes.Length;
        }

        public TokenString(string token)
        {
            Value = new byte[8];
            if (string.IsNullOrEmpty(token))
                throw new ArgumentOutOfRangeException(nameof(token));
            var byteBuffer = Encoding.UTF8.GetBytes(token);
            if (byteBuffer.Length > 8)
                throw new ArgumentOutOfRangeException(nameof(token));
            Buffer.BlockCopy(byteBuffer, 0, Value, 0, byteBuffer.Length);
            currentLength = byteBuffer.Length;
        }

        public void AddCharacter(byte character) => Value[currentLength] = character;

        public byte[] Value { get; }

        public int Length => Value.Length;

        public override string ToString() => new string(Value
            .TakeWhile(b => b != 0x00)
            .Select(Convert.ToChar)
            .ToArray());
    }
}