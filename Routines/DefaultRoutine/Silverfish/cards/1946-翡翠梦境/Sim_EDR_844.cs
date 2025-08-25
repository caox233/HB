using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 中立 费用：7 种族：无效的 卡牌稀有度：传说 攻击力：7 生命值：7
	//Naralex, Herald of the Flights
	//纳拉雷克斯，龙群先锋
	//Your Dragons cost (1).
	//你的龙牌法力值消耗为（1）点。
	class Sim_EDR_844 : SimTemplate
	{
		public virtual void onAuraStarts(Playfield p, Minion m)
        {
            if (m.own) p.anzOwnDragonConsort=1;
        }
		
		public virtual void onAuraEnds(Playfield p, Minion m)
        {
            if (m.own) p.anzOwnDragonConsort++;
        }
	}
}
