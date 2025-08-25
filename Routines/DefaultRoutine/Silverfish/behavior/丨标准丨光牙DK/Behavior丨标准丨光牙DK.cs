using System.Collections.Generic;
using System;
using Logger = Triton.Common.LogUtilities.Logger;
using log4net;
using System.Linq;

namespace HREngine.Bots
{
    public partial class Behavior丨标准丨光牙DK : Behavior
    {
        private int bonus_enemy = 4;
        private int bonus_mine = 4;
        
        public override string BehaviorName() { return "丨标准丨光牙DK"; }
        PenalityManager penman = PenalityManager.Instance;

        /// <summary>
        /// 标准光牙DK的留牌策略
        /// </summary>
        /// <param name="cards">起手卡牌列表</param>
        public override void specialMulligan(List<Mulligan.CardIDEntity> cards, HeroEnum enemyHeroClass)
        {
            int 怪异魔蚊 = 0;
            int 暴行祭礼 = 0;
            int 火羽精灵 = 0;
            int 病变虫群 = 0;
            int 鱼人木乃伊 = 0;
            int 恐惧猎犬训练师 = 0;
            int 感染吐息 = 0;
            int 疯狂生物 = 0;
            int 堕寒男爵 = 0;
            int 秘迹观测者 = 0;
            

            foreach (Mulligan.CardIDEntity card in cards)
            {
                CardDB.Card cardCN = CardDB.Instance.getCardDataFromID(card.id);
                if (cardCN.nameCN == CardDB.cardNameCN.怪异魔蚊)
                {
                    怪异魔蚊 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.暴行祭礼)
                {
                    暴行祭礼 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.火羽精灵)
                {
                    火羽精灵 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.病变虫群)
                {
                    病变虫群 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.鱼人木乃伊)
                {
                    鱼人木乃伊 += 1;
                }
                 if (cardCN.nameCN == CardDB.cardNameCN.恐惧猎犬训练师)
                {
                    恐惧猎犬训练师 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.感染吐息)
                {
                    感染吐息 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.疯狂生物)
                {
                    疯狂生物 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.堕寒男爵)
                {
                    堕寒男爵 += 1;
                }
                if (cardCN.nameCN == CardDB.cardNameCN.堕寒男爵)
                {
                    秘迹观测者 += 1;
                }
            }

            foreach (Mulligan.CardIDEntity card in cards)
            {
                CardDB.Card cardCN = CardDB.Instance.getCardDataFromID(card.id);

                if (cardCN.nameCN == CardDB.cardNameCN.怪异魔蚊)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "留一张怪异魔蚊";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = 2;
                                tmp.holdReason = "按规则留第二张怪异魔蚊，对面没解牌就后手压死对手";
                            }
                        }
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.困窘的机械师)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "留一张困窘的机械师";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = 2;
                                tmp.holdReason = "按规则留第二张困窘的机械师";
                            }
                        }
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.反魔法护罩)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不留暴行祭礼，没尸体还亏节奏";
                        
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.展馆茶杯)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "前期不留";

                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.穆克拉)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "前期不留";

                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.梦缚迅猛龙)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "留一张梦缚迅猛龙";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = 2;
                                tmp.holdReason = "按规则留第二张梦缚迅猛龙，对面没解牌就后手压死对手";
                            }
                        }
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.火羽精灵)
                {
                     if (cards.Count >= 3)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "留一张火羽精灵";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张火羽精灵，留一张配合533就好";
                            }
                        }
                    }                  
                }

                if (cardCN.nameCN == CardDB.cardNameCN.病变虫群)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "留一张病变虫群";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = 2;
                                tmp.holdReason = "按规则留第二张病变虫群超展开";
                            }
                        }
                    }
                }

                if (cardCN.nameCN == CardDB.cardNameCN.鱼人木乃伊)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "留一张鱼人木乃伊";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张鱼人木乃伊，两张太呆了";
                            }
                        }
                    }
                    else
                    if (火羽精灵 + 病变虫群 >= 1)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "按特殊规则丢弃鱼人木乃伊，因为有更优质的展开";
                    }              
                }


                if (cardCN.nameCN == CardDB.cardNameCN.恐惧猎犬训练师)
                {
                    if (cards.Count >= 3 && 怪异魔蚊 + 火羽精灵 + 病变虫群 + 鱼人木乃伊 >= 1)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "先后手有能用的1费牌留1张恐惧猎犬训练师";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张卡恐惧猎犬训练师";
                            }
                        }
                    }
                    else
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不符合特殊规则不留恐惧猎犬训练师";
                    }                  
                }

                if (cardCN.nameCN == CardDB.cardNameCN.感染吐息)
                {
                    if (cards.Count >= 3 && 怪异魔蚊 + 火羽精灵 + 病变虫群 + 鱼人木乃伊 >= 1)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "先后手有能用的1费牌留1张感染吐息";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张感染吐息";
                            }
                        }
                    }
                    else
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不符合特殊规则不留感染吐息";
                    }                  
                }
                


                if (cardCN.nameCN == CardDB.cardNameCN.疯狂生物)
                {
                    if (cards.Count >= 3 && 怪异魔蚊 + 火羽精灵 + 病变虫群 + 鱼人木乃伊 >= 1)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "先后手有能用的1费牌留1张疯狂生物";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张疯狂生物";
                            }
                        }
                    }
                    else
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不符合特殊规则不留疯狂生物";
                    }                  
                }

                if (cardCN.nameCN == CardDB.cardNameCN.堕寒男爵)
                {
                    if (cards.Count > 3)
                    {
                        card.holdByRule = 2;
                        card.holdReason = "能按费拍怪留1张堕寒男爵过牌";
                        foreach (Mulligan.CardIDEntity tmp in cards)
                        {
                            if (tmp.entitiy == card.entitiy) continue;
                            if (tmp.id == card.id)
                            {
                                tmp.holdByRule = -2;
                                tmp.holdReason = "按规则丢弃第二张堕寒男爵";
                            }
                        }
                    }
                    else
                    {
                        card.holdByRule = -2;
                        card.holdReason = "不符合特殊规则不留堕寒男爵";
                    }                  
                }

                if (cardCN.nameCN == CardDB.cardNameCN.秘迹观测者)
                {
                    if (cards.Count >= 3)
                    {
                        card.holdByRule = -2;
                        card.holdReason = "前期不留";
                    }                  
                }

                //剩下大于等于4费的卡hb是默认不留的
            }       
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 位计数辅助函数（保持原样）
        private static int CountBits(int value)
        {
            int count = 0;
            while (value != 0)
            {
                count++;
                value &= value - 1;
            }
            return count;
        }
        public override int getComboPenality(CardDB.Card card, Minion target, Playfield p, Handmanager.Handcard nowHandcard)
        {
            // 无法选中
            if (target != null && target.untouchable)
            {
                return 100000;
            }
            int 惩罚值 = 0;
            //int 种族数 = 0;           
            int 手牌有1费牌 = 0;
            int 最大法力值 = p.ownMaxMana;
            int 当前法力值 = p.mana;
            int 我方随从数 = p.ownMinions.Count;
            int 敌方随从数 = 0;
            int 尸体数 = p.getCorpseCount();
            int 我方卡牌数 = p.owncards.Count;
            HashSet<TAG_RACE> uniqueBaseRaces = new HashSet<TAG_RACE>();

            foreach (Minion m in p.ownMinions)
            {
                TAG_RACE raceMask = (TAG_RACE)m.handcard.card.race;
                if (raceMask == TAG_RACE.INVALID) continue;

                // 分解所有种族掩码为独立种族（包括单一种族）
                foreach (TAG_RACE baseRace in Enum.GetValues(typeof(TAG_RACE)))
                {
                    if (baseRace == TAG_RACE.INVALID) continue;

                    if ((raceMask & baseRace) != 0)
                    {
                        uniqueBaseRaces.Add(baseRace);
                    }
                }
            }

            int 种族数 = uniqueBaseRaces.Count;
            //遍历手牌
            foreach (Handmanager.Handcard hc in p.owncards)
			{
                if (hc.manacost == 2) 手牌有1费牌++;
            }
            //遍历敌方随从
            foreach (Minion m in p.enemyMinions)
            {
                if (m.handcard.card.type == CardDB.cardtype.MOB && !m.untouchable && m.dormant <= 0)
                {                    
                    敌方随从数++;
                }
            }
            //此处为单卡描述
            switch (card.nameCN)		
            {   

                case CardDB.cardNameCN.怪异魔蚊:
                    if (最大法力值 == 1 && 手牌有1费牌 < 2) 惩罚值 += 8;
                    惩罚值 -= 2;
                break;

                case CardDB.cardNameCN.病变虫群:
                    if (最大法力值 == 1) 惩罚值 -= 12;           
                    break;

                case CardDB.cardNameCN.困窘的机械师:
                    惩罚值 -= 8;
                    break;

                case CardDB.cardNameCN.反魔法护罩:
                    if (我方随从数 < 3) 惩罚值 += 18;
                    惩罚值 -= 我方随从数 * 4;
                break;

                case CardDB.cardNameCN.梦缚迅猛龙:
                    if (当前法力值 < 2) 惩罚值 += 25;
                    else 惩罚值 -= 8;
                break;

                case CardDB.cardNameCN.暴行祭礼:
                    if (尸体数 < 2) 惩罚值 += 500;
                break;

                case CardDB.cardNameCN.鱼人木乃伊:
                    惩罚值 -= 6;
                break;               
                
                case CardDB.cardNameCN.感染吐息:
                    if (!target.isHero) 惩罚值 -= 5;
                    else 惩罚值 += 5;
                break;

                case CardDB.cardNameCN.堕寒男爵:
                    if (最大法力值 == 3) 惩罚值 -= 8;
                    if (我方卡牌数 <= 3) 惩罚值 -= 11;
                    if (我方卡牌数 >= 7) 惩罚值 += 100;
                break;

                case CardDB.cardNameCN.秘迹观测者:
                    //if (我方随从数 >= 3 && 最大法力值 == 3) 惩罚值 -= 5;
                    惩罚值 -= 我方随从数 * 8;
                    break;

                case CardDB.cardNameCN.恶毒恐魔:
                    if (尸体数 < 4) 惩罚值 += 36;
                    else 惩罚值 -= 1;
                break;
                
                case CardDB.cardNameCN.血虫感染:
                    if (最大法力值 == 4) 惩罚值 -= 15;
                    if (我方卡牌数 <= 3) 惩罚值 -= 20;             
                break;

                case CardDB.cardNameCN.展馆茶壶:
                    if (种族数 < 3) 惩罚值 += 16;
                    else
                        惩罚值 -= 种族数 * 23;
                break;

                case CardDB.cardNameCN.展馆茶杯:
                    if (种族数 < 2) 惩罚值 += 16;
                    else
                    惩罚值 -= 种族数 * 22;
                break;

                case CardDB.cardNameCN.丑恶的残躯:
                    if (我方随从数 >= 4) 惩罚值 += 16;
                    else 惩罚值 -= 18;
                break;

                case CardDB.cardNameCN.气闸破损:
                    if (尸体数 < 5 || 我方随从数 >= 6) 惩罚值 += 999;
                    if (尸体数 >= 5 && 我方随从数 <= 5) 惩罚值 -= 14;
                    if (尸体数 >= 5 && 敌方随从数 >= 我方随从数) 惩罚值 -= 16;
                break;

                case CardDB.cardNameCN.死亡使者萨鲁法尔:
                    惩罚值 -= 5;
                break;
            }
            if (card.type == CardDB.cardtype.MOB)
            {
                惩罚值 -= 2;
            }
            if (card.type == CardDB.cardtype.HEROPWR)
            {
                惩罚值 += 17;
            }
            return 惩罚值;
        }

        // 核心，场面值
        public override float getPlayfieldValue(Playfield p)
        {

            if (p.value > -200000) return p.value;
            float retval = 0;
            retval += getGeneralVal(p);
            // 危险血线
            int hpboarder = 3;
            // 抢脸血线
            int aggroboarder = 12;
            retval += getHpValue(p, hpboarder, aggroboarder);
            // 对手基本随从交换模拟
            retval += enemyTurnPen(p);
            // 出牌序列数量
            int count = p.playactions.Count;
            int ownActCount = 0;
            // 排序问题！！！！
            for (int i = 0; i < count; i++)
            {
                Action a = p.playactions[i];
                ownActCount++;
                switch (a.actionType)
                {
                    case actionEnum.attackWithMinion:
                        retval += 5;
                        continue;
                    case actionEnum.playcard:
                        retval -= 6;
                        break;
                    case actionEnum.useHeroPower:
                        break;
                    default:
                        continue;
                }
                switch (a.card.card.nameCN)
                {
                    case CardDB.cardNameCN.梦缚迅猛龙:
                        retval -= i * 5;
                        break;
                    case CardDB.cardNameCN.堕寒男爵:
                        retval -= i * 2;
                        break;
                    case CardDB.cardNameCN.血虫感染:
                        retval -= i * 1;
                        break;
                    case CardDB.cardNameCN.展馆茶杯:
                    case CardDB.cardNameCN.展馆茶壶:
                        retval += 6;
                        break;
                }
            }
            //retval += getSecretPenality(p); // 奥秘的影响
            //p.value = retval;
            return retval;
        }

        // 发现卡的价值
        public override int getDiscoverVal(CardDB.Card card, Playfield p)
        {
			// Helpfunctions.Instance.logg("发现卡：" + card.nameCN);
			// Helpfunctions.Instance.logg("发现卡类型：" + card.race);
			// Helpfunctions.Instance.logg("发现卡费用：" + card.cost);
            switch (card.nameCN)
            {
    
            //随从

            case CardDB.cardNameCN.萨萨里安:
            case CardDB.cardNameCN.丑恶的残躯:
            case CardDB.cardNameCN.侏儒嚼嚼怪:
            case CardDB.cardNameCN.髓骨使御者:
            return 25;

            case CardDB.cardNameCN.堕寒男爵:
            case CardDB.cardNameCN.尸魔花:
            case CardDB.cardNameCN.喷灯破坏者:
            case CardDB.cardNameCN.团队领袖:
            case CardDB.cardNameCN.大作战狂热玩家:
            case CardDB.cardNameCN.展馆茶杯:
            case CardDB.cardNameCN.秘迹观测者:
            case CardDB.cardNameCN.法瑞克:
            case CardDB.cardNameCN.玩具盗窃恶鬼:
            case CardDB.cardNameCN.泼漆彩鳍鱼人:
            case CardDB.cardNameCN.笨拙的搬运工:
            case CardDB.cardNameCN.血蓟幻术师:
            case CardDB.cardNameCN.躁动的愤怒卫士:
            case CardDB.cardNameCN.达卡莱附魔师:
            case CardDB.cardNameCN.迅猛龙先锋:
            case CardDB.cardNameCN.逃生舱:
            case CardDB.cardNameCN.缝合巨人:
            return 20;

            case CardDB.cardNameCN.死亡金属骑士:
            case CardDB.cardNameCN.毛绒暴暴狗:
            case CardDB.cardNameCN.彩虹裁缝:
            case CardDB.cardNameCN.A3型机械金刚:
            case CardDB.cardNameCN.乌祖尔暴怒者:
            case CardDB.cardNameCN.南海船长:
            case CardDB.cardNameCN.巢群虫后:
            case CardDB.cardNameCN.死亡侍僧:
            case CardDB.cardNameCN.卡牌评级师:
            case CardDB.cardNameCN.可怕的主厨:
            case CardDB.cardNameCN.扮装选手:
            case CardDB.cardNameCN.法夜欺诈者:
            case CardDB.cardNameCN.混蒙畸体:
            case CardDB.cardNameCN.焦油爬行者:
            case CardDB.cardNameCN.爆破工程师:
            case CardDB.cardNameCN.甜蜜雪灵:
            case CardDB.cardNameCN.穆克拉:
            case CardDB.cardNameCN.粗暴的猢狲:
            case CardDB.cardNameCN.红衣指挥官:
            case CardDB.cardNameCN.荆棘帮暴徒:
            case CardDB.cardNameCN.软软多头蛇:
            case CardDB.cardNameCN.锈烂蝰蛇:
            case CardDB.cardNameCN.鱼人领军:           
            return 10;

            case CardDB.cardNameCN.骷髅帮手:
            case CardDB.cardNameCN.恐惧猎犬训练师:
            case CardDB.cardNameCN.蹒跚的僵尸坦克:
            case CardDB.cardNameCN.恶毒恐魔:
            case CardDB.cardNameCN.蛛魔护群守卫:
            case CardDB.cardNameCN.黑棘针线师:
            case CardDB.cardNameCN.死亡使者萨鲁法尔:
            case CardDB.cardNameCN.行程保安:
            case CardDB.cardNameCN.变异生命体:
            case CardDB.cardNameCN.套娃傀儡:
            return 15;

            case CardDB.cardNameCN.扛包收尸人:
            case CardDB.cardNameCN.寒冬先锋:
            case CardDB.cardNameCN.脆骨海盗:
            case CardDB.cardNameCN.滑雪高手:
            case CardDB.cardNameCN.业余傀儡师:
            case CardDB.cardNameCN.小精灵:
            case CardDB.cardNameCN.鱼人木乃伊:
            case CardDB.cardNameCN.狂野炎术师:
            case CardDB.cardNameCN.疯狂的炼金师:
            case CardDB.cardNameCN.血法师萨尔诺斯:
            case CardDB.cardNameCN.黑骑士:
            case CardDB.cardNameCN.冰喉:
            return 5;

            }
            return 0;
        }
        // 敌方随从价值 主要等于（HP + Angr） * 4
        public override int getEnemyMinionValue(Minion m, Playfield p)
        {
            bool dieNextTurn = false;
            foreach (Minion mm in p.enemyMinions)
            {
                if (mm.handcard.card.nameCN == CardDB.cardNameCN.末日预言者)
                {
                    dieNextTurn = true;
                    break;
                }
            }
            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart) dieNextTurn = true;
            if (dieNextTurn)
            {
                return -1;
            }
            if (m.Hp <= 0) return 0;
            int retval = 1;
            // 法强相当于 1.5 点属性
            retval += m.spellpower * 6;
            if (!m.frozen && (!m.cantAttack || m.handcard.card.nameCN == CardDB.cardNameCN.邪刃豹))
            {
                retval += m.Angr * 4;
                if (m.windfury) retval += m.Angr * 2;
            }
            if (m.silenced) return retval;
            if (m.Spellburst) retval += 22;
            if (m.taunt) retval += 2;
            
            if (m.divineshild) retval += m.Angr * 2;
            if (m.divineshild && m.taunt) retval += 5;
            if (m.stealth) retval += 2;
            if (m.name == CardDB.cardNameEN.nerubianegg && m.Angr <= 3 && !m.taunt) retval -= 5; //蛛魔之卵
            // 剧毒价值两点属性
            if (m.poisonous)
            {
                retval += 8;
            }
            if (m.lifesteal) retval += m.Angr * 2;

            switch (m.handcard.card.nameCN)
            {
                // 解不掉游戏结束
                case CardDB.cardNameCN.盲眼神射手:
                case CardDB.cardNameCN.烈焰亡魂:
                case CardDB.cardNameCN.工厂装配机:
                case CardDB.cardNameCN.落难的大法师:
                case CardDB.cardNameCN.巫师学徒:
                case CardDB.cardNameCN.肢体商贩:
                case CardDB.cardNameCN.巨型图腾埃索尔:
                case CardDB.cardNameCN.驻锚图腾:
                case CardDB.cardNameCN.刺豚拳手:
                case CardDB.cardNameCN.空中飞爪:
                case CardDB.cardNameCN.金翼巨龙:
                case CardDB.cardNameCN.安保自动机:
                case CardDB.cardNameCN.机械跃迁者:
                case CardDB.cardNameCN.火焰术士弗洛格尔:
                case CardDB.cardNameCN.对空奥术法师:
                case CardDB.cardNameCN.前沿哨所:
                case CardDB.cardNameCN.战场军官:
                case CardDB.cardNameCN.伯尔纳锤喙:
                case CardDB.cardNameCN.甜水鱼人斥候:
                case CardDB.cardNameCN.塔姆辛罗姆:
                case CardDB.cardNameCN.暗影珠宝师汉纳尔:
                case CardDB.cardNameCN.伦萨克大王:
                case CardDB.cardNameCN.布莱恩铜须:
                case CardDB.cardNameCN.观星者露娜:
                case CardDB.cardNameCN.大法师瓦格斯:
                case CardDB.cardNameCN.火妖:
                case CardDB.cardNameCN.下水道渔人:
                case CardDB.cardNameCN.空中炮艇:
                case CardDB.cardNameCN.船载火炮:
                case CardDB.cardNameCN.火舌图腾:
                case CardDB.cardNameCN.末日预言者:
                case CardDB.cardNameCN.莫尔杉哨所:
                case CardDB.cardNameCN.鱼人领军:
                case CardDB.cardNameCN.南海船长:
                case CardDB.cardNameCN.灭龙弩炮:
                case CardDB.cardNameCN.战马训练师:
                case CardDB.cardNameCN.加基森拍卖师:
                case CardDB.cardNameCN.健谈的调酒师:
                case CardDB.cardNameCN.豪宅管家俄里翁:
                case CardDB.cardNameCN.小鬼骑士:
                case CardDB.cardNameCN.针岩图腾:
                case CardDB.cardNameCN.伴唱机:
                case CardDB.cardNameCN.空气之怒图腾:
                case CardDB.cardNameCN.战场通灵师:
                case CardDB.cardNameCN.纸艺天使:
                case CardDB.cardNameCN.纳亚克海克森:
                case CardDB.cardNameCN.饥饿食客哈姆:
                case CardDB.cardNameCN.箭矢工匠:
                case CardDB.cardNameCN.傀儡大师多里安:
                case CardDB.cardNameCN.脆骨海盗:
                case CardDB.cardNameCN.暗影升腾者:
                case CardDB.cardNameCN.赤红教士:
                case CardDB.cardNameCN.受伤的搬运工:
                case CardDB.cardNameCN.隐藏宝石:
                case CardDB.cardNameCN.剃刀沼泽摇滚明星:
                case CardDB.cardNameCN.虚空协奏者:
                case CardDB.cardNameCN.阿曼苏尔:
                case CardDB.cardNameCN.脱困古神尤格萨隆:
                case CardDB.cardNameCN.复仇者阿格拉玛:
                case CardDB.cardNameCN.雷霆之神高戈奈斯:
                case CardDB.cardNameCN.兵主:
                case CardDB.cardNameCN.灭世泰坦萨格拉斯:
                case CardDB.cardNameCN.悠闲的曲奇:
                case CardDB.cardNameCN.凯恩日怒:
                case CardDB.cardNameCN.粗暴的猢狲:
                case CardDB.cardNameCN.狂飙邪魔:
                case CardDB.cardNameCN.椰子火炮手:
                case CardDB.cardNameCN.卡兹格罗斯:
                case CardDB.cardNameCN.炎魔之王拉格纳罗斯:
                case CardDB.cardNameCN.召唤师达克玛洛:
                case CardDB.cardNameCN.腐臭淤泥波普加:
                case CardDB.cardNameCN.高阶祭司阿门特:
                case CardDB.cardNameCN.伊辛迪奥斯:
                case CardDB.cardNameCN.生命的缚誓者艾欧娜尔:
                case CardDB.cardNameCN.维和者阿米图斯:
                case CardDB.cardNameCN.科隆加恩:
                case CardDB.cardNameCN.游侠斥候:
                case CardDB.cardNameCN.虚灵神谕者:
                case CardDB.cardNameCN.巢群虫后:
                case CardDB.cardNameCN.威拉罗克温布雷:
                case CardDB.cardNameCN.凶恶的滑矛纳迦:
                case CardDB.cardNameCN.神话观测者:
                case CardDB.cardNameCN.圣沙泽尔:
                case CardDB.cardNameCN.海关执法者:
                case CardDB.cardNameCN.笨拙的杂役:
                case CardDB.cardNameCN.食肉格块:
                case CardDB.cardNameCN.全息技师:
                case CardDB.cardNameCN.潜伏者:
                case CardDB.cardNameCN.老腐和老墓:
                case CardDB.cardNameCN.航母:
                case CardDB.cardNameCN.黑暗圣堂武士:
                case CardDB.cardNameCN.高阶圣堂武士:
                case CardDB.cardNameCN.执政官:
                case CardDB.cardNameCN.凶恶的入侵者:
                case CardDB.cardNameCN.淘金客:
                case CardDB.cardNameCN.战列巡航舰:
                case CardDB.cardNameCN.摩摩尔:
                case CardDB.cardNameCN.爆虫:
                case CardDB.cardNameCN.怪异魔蚊:
                case CardDB.cardNameCN.扭曲的织网蛛:
                case CardDB.cardNameCN.梦缚迅猛龙:
                case CardDB.cardNameCN.凋零先驱:
                case CardDB.cardNameCN.鲜花商贩:
                case CardDB.cardNameCN.痴梦树精:
                case CardDB.cardNameCN.尸魔花:
                case CardDB.cardNameCN.活化月亮井:
                case CardDB.cardNameCN.喜悦的枭兽:
                case CardDB.cardNameCN.好奇的积云:
                case CardDB.cardNameCN.林地塑型者:
                case CardDB.cardNameCN.布罗尔熊皮:
                case CardDB.cardNameCN.丑恶的残躯:
                case CardDB.cardNameCN.饱胀水蛭:
                case CardDB.cardNameCN.尼珊德拉甲虫:
                case CardDB.cardNameCN.戈德林:
                case CardDB.cardNameCN.欧恩哈拉:
                case CardDB.cardNameCN.棘嗣幼龙:
                    retval += bonus_enemy * 3;
                    break;
                // 不解巨大劣势
                case CardDB.cardNameCN.安娜科德拉:
                case CardDB.cardNameCN.农夫:
                case CardDB.cardNameCN.旗标骷髅:
                case CardDB.cardNameCN.锋鳞:
                case CardDB.cardNameCN.凯瑞尔罗姆:
                case CardDB.cardNameCN.暗鳞先知:
                case CardDB.cardNameCN.鲨鳍后援:
                case CardDB.cardNameCN.相位追猎者:
                case CardDB.cardNameCN.鱼人宝宝车队:
                case CardDB.cardNameCN.饥饿的秃鹫:
                case CardDB.cardNameCN.锈水海盗:
                case CardDB.cardNameCN.盛装歌手:
                case CardDB.cardNameCN.玛克扎尔的小鬼:
                case CardDB.cardNameCN.发明机器人:
                case CardDB.cardNameCN.宝藏经销商:
                case CardDB.cardNameCN.随船外科医师:
                case CardDB.cardNameCN.玩具船:
                case CardDB.cardNameCN.雏龙牧人:
                case CardDB.cardNameCN.得胜的维库人:
                case CardDB.cardNameCN.电击学徒:
                case CardDB.cardNameCN.感染者:
                case CardDB.cardNameCN.奥术工匠:
                    retval += bonus_enemy * 1;
                    break;
                case CardDB.cardNameCN.白银之手新兵:
                    if (p.enemyHeroAblility.card.nameCN == CardDB.cardNameCN.白银之手)
                        retval += bonus_enemy * 111;
                    break;
                // 算有点用
                case CardDB.cardNameCN.贪婪的书虫:
                case CardDB.cardNameCN.治疗图腾:
                case CardDB.cardNameCN.力量图腾:
                case CardDB.cardNameCN.神秘女猎手:
                case CardDB.cardNameCN.矮人神射手:
                case CardDB.cardNameCN.低阶侍从:
                case CardDB.cardNameCN.战斗邪犬:
                case CardDB.cardNameCN.法力浮龙:
                case CardDB.cardNameCN.飞刀杂耍者:
                case CardDB.cardNameCN.迦顿男爵:
                    retval += bonus_enemy * 1;
                    break;
                case CardDB.cardNameCN.疯狂的科学家:
                    retval -= bonus_enemy * 4;
                    break;
            }
            return retval;
        }

        // 我方随从价值，大致等于主要等于 （HP + Angr） * 4 
        public override int getMyMinionValue(Minion m, Playfield p)
        {
            bool dieNextTurn = false;
            foreach (Minion mm in p.enemyMinions)
            {
                if (mm.handcard.card.nameCN == CardDB.cardNameCN.末日预言者 && !mm.silenced)
                {
                    dieNextTurn = true;
                    break;
                }
            }
            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart) dieNextTurn = true;
            if (dieNextTurn)
            {
                return -1;
            }
            if (m.Hp <= 0) return 0;
            int retval = 2;
            // 军官在场优先攻击
            if (m.numAttacksThisTurn > 1) retval += m.Angr * m.numAttacksThisTurn;
            retval += m.ownBlessingOfWisdom * 4;
            retval += m.Hp * 4;
            retval += m.Angr * 4;
            // 高攻低血是垃圾
            if (m.Angr > m.Hp + 4) retval -= (m.Angr - m.Hp) * 3;
            //if (m.handcard.card.nameCN == CardDB.cardNameCN.疯狂的科学家 && m.Hp <= 0) retval += 2;
            if (m.handcard.card.nameCN == CardDB.cardNameCN.怪异魔蚊 && m.Hp >= 3) retval += 233;
            if (m.handcard.card.nameCN == CardDB.cardNameCN.饱胀水蛭 && m.Hp >= 3) retval += 151;
            // 风怒价值
            if ((!m.playedThisTurn || m.rush == 1 || m.charge == 1) && m.windfury) retval += m.Angr;
            // 圣盾价值
            if (m.divineshild) retval += m.Hp * 3;
            // 潜行价值
            if (m.stealth) retval += m.Angr / 2 + 1;
            if (m.reborn) retval += 6;
            // 吸血
            //if (m.lifesteal) retval += m.Angr / 2 + 1;
            // 圣盾嘲讽
            if (m.divineshild && m.taunt) retval += 4;

            //int bonus = 4;
            switch (m.handcard.card.nameCN)
            {
                case CardDB.cardNameCN.怪异魔蚊:
                    retval += 4 * bonus_mine;
                    break;
                case CardDB.cardNameCN.梦缚迅猛龙:
                    retval += 4 * bonus_mine;
                    break;
                case CardDB.cardNameCN.饱胀水蛭:
                    retval += 3 * bonus_mine;
                    break;
                case CardDB.cardNameCN.丑恶的残躯:
                    retval += 3 * bonus_mine;
                    break;
                case CardDB.cardNameCN.恶毒恐魔:
                    retval += 3 * bonus_mine;
                    break;
            }

            return retval;
        }

        public override int getHpValue(Playfield p, int hpboarder, int aggroboarder)
        {
            int offset_enemy = 0;
            int offset_mine = p.calEnemyTotalAngr() + Hrtprozis.Instance.enemyDirectDmg;

            int retval = 0;
            // 血线安全
            if (p.ownHero.Hp + p.ownHero.armor - offset_mine > hpboarder)
            {
                retval += 5 * (5 + p.ownHero.Hp + p.ownHero.armor - offset_mine - hpboarder) / 2;
            }
            // 快死了
            else if (p.ownHero.Hp + p.ownHero.armor - offset_mine > 0)
            {
                //if (p.nextTurnWin()) retval -= (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor);
                retval -= 5 * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor + offset_mine) * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor + offset_mine);
            }
            else
            {
                retval -= 3 * (hpboarder + 1) * (hpboarder + 1) + 100;
            }
            if (p.ownHero.Hp + p.ownHero.armor - offset_mine < 6 && p.ownHero.Hp + p.ownHero.armor - offset_mine > 0)
            {
                retval -= 80 / (p.ownHero.Hp + p.ownHero.armor - offset_mine);
            }
            // 对手血线安全
            if (p.enemyHero.Hp + p.enemyHero.armor + offset_enemy >= aggroboarder)
            {
                retval += (aggroboarder - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            }
            // 开始打脸
            else
            {
                retval += 3 * (aggroboarder + 1 - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            }
            // 进入斩杀线
            // if (p.enemyHero.Hp + p.enemyHero.armor + offset_enemy <= 5 && p.enemyHero.Hp + p.enemyHero.armor + offset_enemy > 0)
            // {
            //     retval += 300 / (p.enemyHero.Hp + p.enemyHero.armor - offset_enemy);
            // }
            // 场攻+直伤大于对方生命，预计完成斩杀
            if (p.anzEnemyTaunt == 0 && p.calTotalAngr() + p.calDirectDmg(p.mana, false) >= p.enemyHero.Hp + p.enemyHero.armor)
            {
                retval += 2000;
            }
            // 下回合斩杀本回合打脸
            if (p.calDirectDmg(p.ownMaxMana + 1, false, true) >= p.enemyHero.Hp + p.enemyHero.armor)
            {
                retval += 100;
            }
            return retval;
        }

        public override int getSecretPen_CharIsAttacked(Playfield p, SecretItem si, Minion attacker, Minion defender)
        {
            if (attacker.isHero) return 0;
            int pen = 0;
            // 攻击的基本惩罚
            if (si.canBe_explosive && !defender.isHero)
            {
                pen -= 20;
                //pen += (attacker.Hp + attacker.Angr);
                foreach (SecretItem sii in p.enemySecretList)
                {
                    sii.canBe_explosive = false;
                }
            }
            return pen;
        }

    }
}