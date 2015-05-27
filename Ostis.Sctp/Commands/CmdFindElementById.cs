﻿using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{

    internal class CmdFindElementById : ACommand
    {

        public CmdFindElementById (Identifier identifier)
            : base(0xa0,0)
        {
           
            UInt32 argsize = 0;
          
            UInt32 contlenght = (uint)identifier.BytesStream.Length;
            Argument<UInt32> _argidlenght = new Argument<UInt32>(contlenght);
            base._arguments.Add(_argidlenght);

            Argument<Identifier> _argid = new Argument<Identifier>(identifier);
            base._arguments.Add(_argid);

            foreach (IArgument arg in base._arguments)
            {
                argsize += arg.Length;
            }
            base._header.ArgSize = argsize;
        }

    }
}