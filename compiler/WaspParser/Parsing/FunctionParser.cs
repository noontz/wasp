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
        readonly Token[] body;

        public FunctionParser(Token[] body, Signature signature, Context context)
        {
            function = new Function(signature, context);
            this.body = body;
        }

        public Function Parse()
        {
            var variablesBuffer = new List<byte>();
            var codeBuffer = new List<byte>();
            var currentExpressionValueType = Tokens.None;
            for (var i = body.Length - 1; i >= 0; i--)
            {
                //TODO local variables
                if (body[i].Group == TokenGroups.Operator) // expression betweeen two operators
                {
                    codeBuffer.Add(0x20);

                    if (body[i - 1].Group == TokenGroups.Literal)
                    {
                        // find position in locals array
                        var parameter =
                            function.Signature.Parameters.Single(a => a.Identifier.Value.Equals(body[i - 1].Value));
                        codeBuffer.AddRange(Leb128.VarUint32(parameter.Position));
                        currentExpressionValueType = parameter.InputType.ID;
                    }
                    codeBuffer.Add(0x20);
                    if (body[i + 1].Group == TokenGroups.Literal)
                    {
                        // find position in locals array
                        var parameter =
                            function.Signature.Parameters.Single(a => a.Identifier.Value.Equals(body[i + 1].Value));
                        codeBuffer.AddRange(Leb128.VarUint32(parameter.Position));
                        if (currentExpressionValueType != parameter.InputType.ID)
                            throw new ArgumentException("Expression values are not of the same valuetype");
                    }
                    codeBuffer.Add(OperatorMap.GetCode(body[i], currentExpressionValueType));
                }
            }
            if (function.Signature.ReturnType.Group == TokenGroups.ValueType)
                codeBuffer.Add(0x0f);
            codeBuffer.Add(0x0b);
            var numberOfLocalVariables = Leb128.VarUint32(variablesBuffer.Count);
            var bodyLength = Leb128.VarUint32(numberOfLocalVariables.Length + variablesBuffer.Count + codeBuffer.Count);
            var codeBody = new List<byte>(bodyLength);
            codeBody.AddRange(numberOfLocalVariables);
            codeBody.AddRange(variablesBuffer);
            codeBody.AddRange(codeBuffer);
            function.Codebody = codeBody.ToArray();
            return function;
        }
    }
}