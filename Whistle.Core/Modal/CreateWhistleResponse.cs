﻿
namespace Whistle.Core.Modal
{
    public class CreateWhistleResponse
    {
        public Whistle NewWhistle { get; set; }


        public MatchingWhistle[] MatchingWhisltes { get; set; }
    }
}
