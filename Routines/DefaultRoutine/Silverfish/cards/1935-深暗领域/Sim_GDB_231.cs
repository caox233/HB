using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//武器 战士 费用：2 攻击力：2 耐久度：2
	//Crystalline Greatmace
	//晶石巨槌
	//After your hero attacks, give all Draenei in your hand +2 Attack.
	//在你的英雄攻击后，使你手牌中的所有德莱尼获得+2攻击力。
	class Sim_GDB_231 : SimTemplate
	{
		public virtual void onHeroattack(Playfield p, Minion own, Minion target)
        {
            foreach (var item in p.owncards)
            {
                if (item.card.race == CardDB.Race.DRAENEI)
                {
                    item.addattack += 2;
                }

            }
        }
		
	}
}
