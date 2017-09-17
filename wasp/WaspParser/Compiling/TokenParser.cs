using System;
using System.Collections.Generic;
using wasp.Compiling;
using wasp.enums;
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
            contextStateMachine = new ContextStateMachine(blockStateMachine);
        }
        void Run(IEnumerable<Token> tokens)
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