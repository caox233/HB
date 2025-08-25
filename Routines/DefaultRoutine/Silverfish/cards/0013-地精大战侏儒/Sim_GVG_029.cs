using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_GVG_029 : SimTemplate //* Ancestor's Call
    {

        //    Put a random minion from each player's hand into the battlefield.

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 找到手牌中法力值消耗最高的随从
            Handmanager.Handcard highestCostMinion = null;

            foreach (Handmanager.Handcard hc in p.owncards)
            {
                if (hc.card.type == CardDB.cardtype.MOB)
                {
                    highestCostMinion = hc;
                }
            }
            if (ownplay)
            {
                if (highestCostMinion != null)
                {
                    int pos = p.ownMinions.Count;
                    p.callKid(highestCostMinion.card, pos, ownplay); // 召唤该随从
                    p.removeCard(highestCostMinion); // 从手牌中移除该随从
                }
            }
        }
    }
}