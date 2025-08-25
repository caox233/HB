using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    // 原始卡牌：亮石旋岩虫 (未锻造)
    class Sim_DEEP_024 : SimTemplate
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
        {
            // 快枪效果检测（仅本回合抽到时触发）
            if (hc.isQuickdrawActive && p.prozis.gTurn == hc.drawnTurn)
            {
                ApplyEffects(p, ownplay, target);
            }

            // 锻造处理（替换为锻造后的卡牌）
            if (hc.card.Forge && hc.temporary)
            {
                ReplaceCardInHand(p, hc, CardDB.cardIDEnum.DEEP_024t);
            }
        }

        protected void ApplyEffects(Playfield p, bool ownplay, Minion target)
        {
            // 固定5点伤害
            int damage = 5;
            p.minionGetDamageOrHeal(target, damage);

            // 吸血效果
            Minion healTarget = ownplay ? p.ownHero : p.enemyHero;
            p.minionGetDamageOrHeal(healTarget, -damage);
            
            // 更新全局计数器
            if (ownplay) p.prozis.anzOwnLifesteal++;
            else p.prozis.anzEnemyLifesteal++;
        }

        private void ReplaceCardInHand(Playfield p, Handmanager.Handcard oldCard, CardDB.cardIDEnum newCardID)
        {
            int index = p.owncards.IndexOf(oldCard);
            if (index >= 0)
            {
                CardDB.Card forgedCard = CardDB.Instance.getCardDataFromID(newCardID);
                p.owncards[index] = new Handmanager.Handcard(forgedCard)
                {
                    position = oldCard.position,
                    entity = oldCard.entity,
                    temporary = false
                };
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
        new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),    // 条件1：有可用目标时必须选择
        new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),           // 条件2：目标必须是敌方角色
        new PlayReq(CardDB.ErrorType2.REQ_MINION_OR_ENEMY_HERO)    // 条件3：目标类型限制（随从或敌方英雄）
            };
        }
    }
}
