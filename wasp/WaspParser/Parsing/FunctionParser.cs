using System;
using System.Collections.Generic;
using System.Linq;
using wasp.enums;
using wasp.Global;
using wasp.Intermediate;
using wasp.Tokenization;

namespace wasp.Parsing
{
    class FunctionParser
    {
        readonly Function function;
        readonly Token[] tokens;

        public FunctionParser(Token[] tokens, Signature signature, Context context)
        {
            function = new Function(signature, context);
            this.tokens = tokens;
        }

        public Function Parse()
        {
            var codeBuffer = new List<byte>();
            var currentExpressionValueType = Tokens.None;
            for (var i = tokens.Length - 1; i >= 0; i--)
            {
                if (tokens[i].Group == TokenGroups.Operator) // expression betweeen two operators
                {
                    codeBuffer.Add(0x20);

                    if (tokens[i - 1].Group == TokenGroups.Literal)
                    {
                        // find position in locals array
                        var parameter =
                            function.Signature.Parameters.Single(a => a.Identifier.Value.Equals(tokens[i - 1].Value));
                        codeBuffer.AddRange(Leb128.VarUint32(parameter.Position));
                        currentExpressionValueType = parameter.InputType.ID;
                    }
                    codeBuffer.Add(0x20);
                    if (tokens[i + 1].Group == TokenGroups.Literal)
                    {
                        // find position in locals array
                        var parameter =
                            function.Signature.Parameters.Single(a => a.Identifier.Value.Equals(tokens[i + 1].Value));
                        codeBuffer.AddRange(Leb128.VarUint32(parameter.Position));
                        if (currentExpressionValueType != parameter.InputType.ID)
                            throw new ArgumentException("Expression values are not of the same valuetype");
                    }
                    codeBuffer.Add(OperatorMap.GetCode(tokens[i], currentExpressionValueType));
                }
            }
            if (function.Signature.ReturnType.Group == TokenGroups.ValueType)
                codeBuffer.Add(0x0f);
            codeBuffer.Add(0x0b);
            function.Codebody = codeBuffer.ToArray();
            return function;
        }
    }
}