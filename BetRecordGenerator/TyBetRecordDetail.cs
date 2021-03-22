using System;
using System.Collections.Generic;
using System.Text;

namespace BetRecordGenerator
{
    public class TyBetRecordDetail
    {
        public int KindID { get; set; }
        public decimal CurScore { get; set; }
        public decimal AllBet { get; set; }
        public short UserCount { get; set; }
        public string LineCode { get; set; }
        public DateTime GameEndTime { get; set; }
        public string CardValue { get; set; }
        public int ServerID { get; set; }
        public int ChairID { get; set; }
        public decimal Profit { get; set; }
        public decimal Revenue { get; set; }
        public string GameID { get; set; }
        public decimal CellScore { get; set; }
        public int TableID { get; set; }
        public string Accounts { get; set; }
        public int ChannelID { get; set; }
        public DateTime GameStartTime { get; set; }
    }
}
