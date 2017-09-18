using System;
using System.Collections.Generic;
using wasp.enums;
using wasp.Tokenization;

namespace wasp.Compiling
{
    class BlockStateMachine
    {
        readonly Stack<int> leftBrackets = new Stack<int>();

        readonly Stack<int> leftParens = new Stack<int>();

        public void Set(Token token)
        {
            switch (token.ID)
            {
                case Tokens.Lbracket:
                    leftBrackets.Push(token.Position);
                    break;
                case Tokens.Rbracket:
                    BracketBlockReady?.Invoke(leftBrackets.Pop(), token.Position);
                    break;
                case Tokens.Lparens:
                    leftParens.Push(token.Position);
                    break;
                case Tokens.Rparens:
                    ParensBlockReady?.Invoke(leftParens.Pop(), token.Position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(token.ID), token, null);
            }
        }

        public event Action<int, int> BracketBlockReady;

        public event Action<int, int> ParensBlockReady;

        
    }
}