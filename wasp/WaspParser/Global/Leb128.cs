using System.Collections.Generic;

namespace wasp.Global
{
    /// <summary>
    /// Calculates LEB128 byte[] representations
    /// </summary>
    static class Leb128
    {
        public static byte[] VarUint32(uint value) => PositiveLeb128(value, 4);

        public static byte VarInt7(sbyte value) => (byte) (value & 0x7F);

        public static byte[] VarInt64(long value) => value > 0
            ? PositiveLeb128((ulong) value, 8)
            : FlipBytesBack(PositiveLeb128((ulong) ~value, 8));


        public static byte[] VarInt32(int value) => value > 0
            ? PositiveLeb128((uint) value, 4)
            : FlipBytesBack(PositiveLeb128((uint) ~value, 4));

        static byte[] PositiveLeb128(ulong value, int length)
        {
            var buffer = new List<byte>(length);
            var run = true;
            while (run)
            {
                var current = (byte) (value & 0x7F);
                value >>= 7;
                if (value != 0)
                    current = (byte) (current ^ 0x80);
                else
                    run = false;
                buffer.Add(current);
            }
            return buffer.ToArray();
        }

        static byte[] FlipBytesBack(byte[] flippedBytes)
        {
            for (var i = 0; i < flippedBytes.Length; i++)
                flippedBytes[i] = i == flippedBytes.Length - 1
                    ? (byte) (~flippedBytes[i] & 0x7F)
                    : (byte) (~flippedBytes[i] ^ 0x80);
            return flippedBytes;
        }
    }
}