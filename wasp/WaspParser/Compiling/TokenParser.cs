using System;
using System.Collections.Generic;
using wasp.Compiling;
using wasp.enums;
using wasp.Parsing;
using wasp.Tokenization;

namespace wasp.Linking
{
    class TokenParser
    {
        BlockStateMachine blockStateMachine;
        ContextStateMachine contextStateMachine;

        public TokenParser()
        {
            blockStateMachine = new BlockStateMachine();
            blockStateMachine.BracketBlockReady += BlockStateMachine_BracketBlockReady;
            blockStateMachine.ParensBlockReady += BlockStateMachine_ParensBlockReady;
            contextStateMachine = new ContextStateMachine();
        }

        HashSet<SignatureIntermediate> signatureTypes = new HashSet<SignatureIntermediate>();

        private void BlockStateMachine_ParensBlockReady(int arg1, int arg2)
        {
            if (tokenList[arg1 - 1].ID == Tokens.Identifier) // function signature 
            {
                var signature = new SignatureIntermediate();
                if (tokenList[arg1 - 2].Group == TokenGroups.ValueType) // return type
                    signature.ReturnType = tokenList[arg1 - 2];
                var numberOfArguments = (arg2 - arg1) / 3;
                if (numberOfArguments != 0)
                {
                    signature.Arguments = new ArgumentIntermediate[numberOfArguments];
                    for (var i = arg1 + 1; i < arg2; i += 3)
                        signature.Arguments[(i - (arg1 + 1))/3] = new ArgumentIntermediate(tokenList[i], tokenList[i + 1]);
                }
                signatureTypes.Add(signature);
            }
        }

        private void BlockStateMachine_BracketBlockReady(int arg1, int arg2)
        {
            throw new NotImplementedException();
        }

        List<Token> tokenList = new List<Token>();

        public void Run(IEnumerable<Token> tokens)
        {
           
            // maps signature type to identifier
            var typeList = new Dictionary<byte[], uint>();

            // maps function identifier with signature type identifier
            var functionList = new Dictionary<uint, uint>();

            // maps function identifier with code block
            var codeList = new Dictionary<uint, byte[]>();

            // maps function identifier with export
            var exportList = new Dictionary<uint, byte[]>();


            var export = false;

            var function = false;

            var functionSignature = new List<Token>();

            var currentPattern = new List<Token>(8);

            foreach (var token in tokens)
            {
                tokenList.Add(token);
                switch (TokenGroupMap.GetGroup(token))
                {
                    case TokenGroups.None:
                        break;
                    case TokenGroups.Literal:
                        break;
                    case TokenGroups.Keyword:
                        break;
                    case TokenGroups.Operator:
                        break;
                    case TokenGroups.Punctuator:
                        blockStateMachine.Set(token);
                        break;
                    case TokenGroups.ValueType:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}