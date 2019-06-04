﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingDemo.Loop8Algo.EngineIMPL
{
    public class TidalSeg2Engine : Engine
    {
        public TidalSeg2Engine(string RuleName) : base(RuleName) { }
        public override decimal? CalculationIMPL(DateTime t1, DateTime t2, bool LetGo = false)
        {
            if (LetGo) return base.LetGoPrice;
            throw new NotImplementedException("关门放狗 必须给钱放行~");
        }
    }
}
