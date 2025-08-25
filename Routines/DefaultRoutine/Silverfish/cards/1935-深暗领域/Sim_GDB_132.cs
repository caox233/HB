using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_GDB_132 : SimTemplate //* 躁动的愤怒卫士 
    {
        //<b>Battlecry:</b> Deal 2 damage to an enemy minion. If it dies, <b>Discover</b> a Demon.
        //<b>战吼：</b>对一个敌方随从造成2点伤害。如果该随从死亡，<b>发现</b>一张恶魔牌。
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            if (target != null) p.minionGetDamageOrHeal(target, 2);
            if (target.Hp <= 0) p.drawACard(CardDB.cardIDEnum.None, m.own);

        }
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
            };
        }
    }
}
