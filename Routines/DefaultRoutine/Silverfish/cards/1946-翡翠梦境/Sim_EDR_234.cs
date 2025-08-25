using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 萨满祭司 费用：2
	//Emerald Bounty
	//翡翠厚赠
	//Draw 2 cards.You can't play themfor 2 turns.
	//抽两张牌。你无法在2回合内使用这些牌。
	class Sim_EDR_234 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
			// 记录当前手牌数量
            int currentHandCount = p.owncards.Count;

            p.drawACard(CardDB.cardIDEnum.None, ownplay);
            p.drawACard(CardDB.cardIDEnum.None, ownplay);
            
			// 标记新增的手牌
            for (int i = currentHandCount; i < p.owncards.Count; i++)
            {
                // 添加锁定回合数 = 当前回合 + 2（当前回合结束时+1，下回合结束再+1）
                p.owncards[i].lockedUntilTurn = p.turn + 2;
            }
        }
		
	}
}
