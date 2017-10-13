using System;
using System.Collections.Generic;
using wasp.enums;
using wasp.Tokenization;

namespace wasp.Global
{
    class OperatorMap
    {
        static readonly Dictionary<Tokens, byte> Int32Operators = new Dictionary<Tokens, byte>
        {
            {Tokens.Plus, 0x6a}
        };

        public static byte GetCode(Token operation, Tokens valueType)
        {
            if (operation.Group != TokenGroups.Operator)
                throw new ArgumentException($"{nameof(operation)} is not an operator");
            switch (valueType)
            {
                case Tokens.Int32:
                    return Int32Operators[operation.ID];
                default:
                    throw new ArgumentException($"{nameof(valueType)} is not valid valuetype");
            }
        }
    }
}