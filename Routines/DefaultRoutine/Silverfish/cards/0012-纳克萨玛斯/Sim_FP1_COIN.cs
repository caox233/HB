using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_FP1_COIN : SimTemplate
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 为当前玩家增加1点临时法力值
            if (ownplay)
            {
                p.mana++;       // 增加己方当前可用法力
            }
        }
    }
}
