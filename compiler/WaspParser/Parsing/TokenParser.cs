using System;
using System.Collections.Generic;
using wasp.Compiling;
using wasp.enums;
using wasp.Intermediate;
using wasp.Tokenization;

namespace wasp.Parsing
{
    class TokenParser
    {
        readonly BlockStateMachine blockStateMachine;
        readonly ContextStateMachine context;
        readonly List<Function> functions;

        readonly List<Token> tokenList;

        public TokenParser()
        {
            blockStateMachine = new BlockStateMachine();
            blockStateMachine.BracketBlockReady += BlockStateMachine_BracketBlockReady;
            blockStateMachine.ParensBlockReady += BlockStateMachine_ParensBlockReady;
            context = new ContextStateMachine();
            tokenList = new List<Token>();
            functions = new List<Function>();
        }

        void BlockStateMachine_ParensBlockReady(int arg1, int arg2)
        {
            if (tokenList[arg1 - 1].ID == Tokens.Identifier) // function signature 
            {
                var signature = new Signature(tokenList[arg1 - 1]);
                if (tokenList[arg1 - 2].Group == TokenGroups.ValueType) // return type
                    signature.ReturnType = tokenList[arg1 - 2];
                var numberOfArguments = (arg2 - arg1) / 3;
                if (numberOfArguments != 0) // extract arguments
                    for (var i = arg1 + 1; i < arg2; i += 3)
                        signature.AddParameter(tokenList[i], tokenList[i + 1],  (i - (arg1 + 1)) / 3);
                context.SetFunction(signature);
            }
        }

        void BlockStateMachine_BracketBlockReady(int arg1, int arg2)
        {
            switch(context.Context)
            {
                case Context.Export:
                    context.Export = false;
                    break;
                case Context.Function:
                    var functionBody = tokenList.GetRange(arg1 + 1, arg2 - arg1 - 1).ToArray();
                    var functionContext = context.Export ? Context.Export : Context.None;
                    var functionParser = new FunctionParser(functionBody, context.CurrentSignature, functionContext);
                    functions.Add(functionParser.Parse());
                    context.RemoveFunction();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IEnumerable<ISection> Run(IEnumerable<Token> tokens)
        {
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
                        if (token.ID == Tokens.Export)
                            context.Export = true;
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
            var functionsCompiler = new FunctionsCompiler();
            return functionsCompiler.CompileFunctions(functions);
        }
    }
}