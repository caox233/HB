using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{

    //* 转生 Reincarnate
    //Destroy a minion, then return it to life with full Health.
    //消灭一个随从，然后将其复活，并具有所有生命值。
    class Sim_FP1_025 : SimTemplate
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            CardDB.Card d = target.handcard.card;
            if (ownplay && target != null)
            {
                p.minionGetDestroyed(target);
                int place = target.zonepos;
                p.callKid(d, place, ownplay);
            }
        }
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),     // 需要指定目标               
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),     // 目标必须是随
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),   // 目标必须是友方随从
            };
        }
    }
}