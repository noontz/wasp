using System;
using wasp.enums;
using wasp.Tokenization;

namespace wasp.Global
{
    class ValueTypeMap
    {
        public static byte GetCode(Token token)
        {
            if (token.Group != TokenGroups.ValueType)
                throw new ArgumentException($"{nameof(token)} is not a value valueType");
            switch (token.ID)
            {
                case Tokens.Int32:
                    return Leb128.VarInt7((sbyte) LanguageTypes.Int32);
                default:
                    throw new ArgumentException($"{nameof(token)} is not valid");
            }
        }
    }
}