using System.Collections.Generic;
using System;
using System.Linq;
using log4net;
using Logger = Triton.Common.LogUtilities.Logger;

namespace HREngine.Bots
{
    public partial class Behavior丨通用丨沙包战 : Behavior
    {
        private static readonly ILog ilog_0 = Logger.GetLoggerInstanceForType();

        private int bonus_enemy = 4;
        private int bonus_mine = 4;
        // 危险血线
        private int hpboarder = 25;
        // 抢脸血线
        private int aggroboarder = 5;

        private static readonly ILog Log = Logger.GetLoggerInstanceForType();
        

        public override string BehaviorName() { return "丨通用丨沙包战"; }
        PenalityManager penman = PenalityManager.Instance;

        public override int getComboPenality(CardDB.Card card, Minion target, Playfield p, Handmanager.Handcard nowHandcard)
        {
            // 无法选中
            if (target != null && target.untouchable)
            {
                return 100000;
            }

            // 初始惩罚值
            int pen = 0;


            bool 剑刃风暴 = false;
            bool 剃刀沼泽摇滚明星 = false;
            bool 合金顾问 = false;
            switch (card.nameCN)		
            {
                case CardDB.cardNameCN.投入战斗:
                {
                    // 统计手牌中的嘲讽随从
                    int tauntCount = 0;
                    foreach (Handmanager.Handcard hc in p.owncards)
                    {
                        if (hc.card.type == CardDB.cardtype.MOB && hc.card.tank)
                        {
                            tauntCount++;
                        }
                    }
                    
                    // 根据嘲讽随从数量调整优先级
                    if (tauntCount >= 2)
                    {
                        pen -= 70; // 有多个嘲讽随从时，提高优先级
                    }
                    else if (tauntCount == 1)
                    {
                        pen -= 10; // 只有一个嘲讽随从时，小幅提高优先级
                    }
                    else
                    {
                        pen += 100; // 没有嘲讽随从时，给予较大惩罚
                    }
                }
                break;
                case CardDB.cardNameCN.攀上新高:
                if(p.ownMaxMana >= 3)
                {
                    pen -= 100;
                }
                break;        
               case CardDB.cardNameCN.萨隆苦囚:
               foreach (Handmanager.Handcard hhc in p.owncards)
                {
                   if (hhc.card.nameCN == CardDB.cardNameCN.维伦流亡者领袖)
                   {
                        pen -= 1000;
                   }
                }
                    break;
               case CardDB.cardNameCN.维伦流亡者领袖:
                if (p.ownGraveyard.ContainsKey(CardDB.cardIDEnum.ICC_466) || 
                    p.ownGraveyard.ContainsKey(CardDB.cardIDEnum.FB_Champs_ICC_466) || 
                    p.ownGraveyard.ContainsKey(CardDB.cardIDEnum.CORE_ICC_466))
                {
                    pen -= 5000; // 墓地中有萨隆苦囚，大幅提高优先级
                }
                else
                {
                    pen += 200; // 默认优先级
                }
                    break;
                case CardDB.cardNameCN.盾牌格挡:
                {
                    pen -= 80;
                }
                    break;    
                case CardDB.cardNameCN.卑劣的脏鼠:
                if(p.ownMinions.Count >= 3)
                {
                    pen -= 20;
                }
                    break;
                case CardDB.cardNameCN.雷诺杰克逊:
                if (p.ownHero.Hp <= 10)
                {
                    pen -= 10000;
                }
                else if (p.ownHero.Hp > 15)
                {
                    pen += 1000;
                }
                    break;
                case CardDB.cardNameCN.黑石摇滚:
                {
                    pen -= 100;
                }
                    break;
                case CardDB.cardNameCN.乐队经理精英牛头人酋长:
                {
                    pen -= 30;
                }
                    break;
                case CardDB.cardNameCN.补水区:
                if(p.getCorpseCount() < 6 && p.ownMinions.Count > 4)
                {
                    pen += 1000;
                }
                else if(p.getCorpseCount() > 4 && p.ownMinions.Count < 4)
                {
                    pen -= 100;
                }
                break;    
                case CardDB.cardNameCN.剑刃风暴:
                if (p.ownMinions.Count < 1 && p.enemyMinions.Count >= 1)
                {
                    pen -= 50;
                }
                else if(p.ownMinions.Count >= 1 && p.enemyMinions.Count < 1)
                {
                    pen += 100;
                }
                break;
                case CardDB.cardNameCN.绝命乱斗:
                if(p.ownMinions.Count <= 2 && p.enemyMinions.Count >= 5)
                {
                    pen -= 100;
                }
                else if(p.ownMinions.Count >= 3 && p.enemyMinions.Count <= 2)
                {
                    pen += 1000;
                }
                    break;
                case CardDB.cardNameCN.荒芜之地乱斗打手:
                if(p.ownMinions.Count <= 2 && p.enemyMinions.Count >= 5)
                {
                    pen -= 100;
                }
                else if(p.ownMinions.Count >= 3 && p.enemyMinions.Count <= 2)
                {
                    pen += 1000;
                }
                    break;
                case CardDB.cardNameCN.愤怒卫士:
                {
                    pen += 10000;
                }
                    break;
                case CardDB.cardNameCN.凶魔城堡:
                {
                    pen += 10000;
                }
                    break;
                 case CardDB.cardNameCN.孤胆游侠雷诺:
                 if(p.enemyMinions.Count >= 4)
                 {
                     pen -= 1000;
                 }
                    break;
                case CardDB.cardNameCN.基尔加丹:
                    pen -= 5;
                    if (p.ownMinions.Count >= 2 && p.ownHero.Hp >= 17) pen -= 27;
                    if (p.ownMinions.Count >= 3 && p.ownHero.Hp >= 15) pen -= 88;
                    break;
                case CardDB.cardNameCN.商品卖家:
                if (p.enemyDeckSize < 15 || p.ownMinions.Count > 2)
                {
                    pen -= 200; // 当对方牌库少于15张或我方场上有3个以上随从时提高优先级
                }
                else
                {
                    pen += 10; // 不满足条件时给予小幅度惩罚
                }
                    break;
                case CardDB.cardNameCN.铸甲师:
                if (p.ownMinions.Count > 2)
                {
                    pen -= 50;
                }
                else
                {
                    pen += 20;
                }
                    break;
                case CardDB.cardNameCN.奇利亚斯豪华版3000型:
                 {
                     pen -= 500;
                 }
                    break;
                case CardDB.cardNameCN.无界空宇:
                if (p.enemyMinions.Count < 5 && p.ownMinions.Count >= 3)
                {
                    pen += 2000;
                }
                else if (p.enemyMinions.Count > 5 && p.ownMinions.Count < 3)
                {
                    pen -= 300;
                }
                break;
           
                case CardDB.cardNameCN.托尔托拉:
                {
                    pen -= 500; // 提高优先级
                }
                break;
                // 根据手牌数量调整昔时古树的优先级
                case CardDB.cardNameCN.昔时古树:
                {
                    if (p.owncards.Count < 5)
                    {
                        pen -= 400; // 手牌少时提高优先级
                    }
                    else if (p.owncards.Count > 8)
                    {
                        pen += 300; // 手牌多时降低优先级
                    }
                    else
                    {
                        pen -= 50; // 手牌数量适中时给予小幅度提升
                    }
                }
                break;
            }

            if (p.ownMaxMana == 7
                 && 剑刃风暴
                 && 剃刀沼泽摇滚明星
                 && 合金顾问)
            {
                switch (card.nameCN)
                {
                    case CardDB.cardNameCN.剃刀沼泽摇滚明星:
                        pen -= 80;
                        break;
                    case CardDB.cardNameCN.合金顾问:
                        pen -= 50;
                        break;
                    case CardDB.cardNameCN.剑刃风暴:
                        pen -= 30;
                        break;
                }
            }
            return pen;
        }

        // 核心，场面值
        public override float getPlayfieldValue(Playfield p)
        {
            if (p.value > -200000) return p.value;
            float retval = 0;
            retval += getGeneralVal(p);            
            retval += getHpValue(p, hpboarder, aggroboarder);
            // 出牌序列数量
            int count = p.playactions.Count;
            int ownActCount = 0;
            bool useAb = false;
            // 排序问题！！！！
            for (int i = 0; i < count; i++)
            {
                Action a = p.playactions[i];
                ownActCount++;
                switch (a.actionType)
                {
                    case actionEnum.trade://交易
                        retval += 20;
                        continue;
                    case actionEnum.useLocation://地标
                        retval += 20;
                        continue;
                    case actionEnum.useTitanAbility://泰坦
                        retval += 20;
                        continue;
                    case actionEnum.forge://锻造
                        retval += 20;
                        continue;
                        // 随从攻击
                    case actionEnum.attackWithMinion:
                        continue;
                    // 英雄攻击
                    case actionEnum.attackWithHero:
                        continue;
                    case actionEnum.useHeroPower:
                        useAb = true;
                    if (p.ownHeroName == HeroEnum.deathknight && p.ownMinions.Count == 7) //DK
                    {
                        retval -= 10000;
                    }
                    if (p.ownHeroName == HeroEnum.shaman && p.ownMinions.Count == 7) //萨满
                    {
                        retval -= 10000;
                    }  
                        continue;
                    case actionEnum.playcard:
                        break;
                    default:
                        continue;
                }
                switch (a.card.card.nameCN)
                {
                    case CardDB.cardNameCN.幸运币:
                        retval -= i;
                        break;
                }
            }
            // 对手基本随从交换模拟
            retval += enemyTurnPen(p);
            retval -= p.lostDamage;
            retval += getSecretPenality(p); // 奥秘的影响
            retval -= p.enemyWeapon.Angr * 3 + p.enemyWeapon.Durability * 3;
            return retval;
        }


        // 敌方随从价值 主要等于 （HP + Angr） * 4  
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
            foreach (CardDB.cardIDEnum s in p.ownSecretsIDList)
            {
                if (s == CardDB.cardIDEnum.EX1_610 || s == CardDB.cardIDEnum.VAN_EX1_610)
                {
                    if (m.Hp <= 2)
                    {
                        dieNextTurn = true;
                        break;
                    }
                }
            }
            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart) dieNextTurn = true;
            if (dieNextTurn)
            {
                return -1;
            }
            if (m.Hp <= 0) return 0;
            int retval = 4;
            if (m.Angr > 0 || p.enemyHeroStartClass == TAG_CLASS.PRIEST)
                retval += m.Hp * bonus_enemy;
            retval += m.spellpower * bonus_enemy * 3 / 2;
            if (!m.frozen && !m.cantAttack)
            {
                retval += m.Angr * bonus_enemy;
                if (m.windfury) retval += m.Angr * bonus_enemy / 2;
            }
            if (m.silenced) return retval;

            if (m.taunt) retval += 2;
            if (m.divineshild) retval += m.Angr * 2;
            if (m.divineshild && m.taunt) retval += 5;
            if (m.stealth) retval += 2;

            if (m.lifesteal) retval += m.Angr * bonus_enemy;

            if (m.poisonous)
            {
                retval += 4;
                if (p.ownMinions.Count < p.enemyMinions.Count) retval += 10;
            }
            // 异能价值
            switch (m.handcard.card.nameCN)
            {
                // 解不掉游戏结束
                case CardDB.cardNameCN.对空奥术法师:
                case CardDB.cardNameCN.火焰术士弗洛格尔:
                case CardDB.cardNameCN.黑眼:
                case CardDB.cardNameCN.聒噪怪:
                case CardDB.cardNameCN.鲨鱼之灵:
                case CardDB.cardNameCN.农夫:
                case CardDB.cardNameCN.尼鲁巴蛛网领主:
                case CardDB.cardNameCN.前沿哨所:
                case CardDB.cardNameCN.洛萨:
                case CardDB.cardNameCN.考内留斯罗姆:
                case CardDB.cardNameCN.战场军官:
                case CardDB.cardNameCN.大领主弗塔根:
                case CardDB.cardNameCN.圣殿蜡烛商:
                case CardDB.cardNameCN.伯尔纳锤喙:
                case CardDB.cardNameCN.魅影歹徒:
                case CardDB.cardNameCN.灵魂窃贼:
                case CardDB.cardNameCN.甜水鱼人斥候:
                case CardDB.cardNameCN.原野联络人:
                case CardDB.cardNameCN.狂欢报幕员:
                case CardDB.cardNameCN.巫师学徒:
                case CardDB.cardNameCN.塔姆辛罗姆:
                case CardDB.cardNameCN.导师火心:
                case CardDB.cardNameCN.伊纳拉碎雷:
                case CardDB.cardNameCN.暗影珠宝师汉纳尔:
                case CardDB.cardNameCN.伦萨克大王:
                case CardDB.cardNameCN.洛卡拉:
                case CardDB.cardNameCN.布莱恩铜须:
                case CardDB.cardNameCN.观星者露娜:
                case CardDB.cardNameCN.大法师瓦格斯:
                case CardDB.cardNameCN.火妖:
                case CardDB.cardNameCN.下水道渔人:
                case CardDB.cardNameCN.空中炮艇:
                case CardDB.cardNameCN.船载火炮:
                case CardDB.cardNameCN.团伙核心:
                case CardDB.cardNameCN.巡游领队:
                case CardDB.cardNameCN.科卡尔驯犬者:
                case CardDB.cardNameCN.火舌图腾:
                    retval += bonus_enemy * 8;
                    break;
                // 不解巨大劣势
                case CardDB.cardNameCN.拍卖师亚克森:
                case CardDB.cardNameCN.法师猎手:
                case CardDB.cardNameCN.萨特监工:
                case CardDB.cardNameCN.甩笔侏儒:
                case CardDB.cardNameCN.精英牛头人酋长金属之神:
                case CardDB.cardNameCN.莫尔杉哨所:
                case CardDB.cardNameCN.凯瑞尔罗姆:
                case CardDB.cardNameCN.鱼人领军:
                case CardDB.cardNameCN.南海船长:
                case CardDB.cardNameCN.坎雷萨德埃伯洛克:
                case CardDB.cardNameCN.人偶大师多里安:
                case CardDB.cardNameCN.暗鳞先知:
                case CardDB.cardNameCN.灭龙弩炮:
                case CardDB.cardNameCN.神秘女猎手:
                case CardDB.cardNameCN.鲨鳍后援:
                case CardDB.cardNameCN.怪盗图腾:
                case CardDB.cardNameCN.矮人神射手:
                case CardDB.cardNameCN.任务达人:
                case CardDB.cardNameCN.贪婪的书虫:
                case CardDB.cardNameCN.战马训练师:
                case CardDB.cardNameCN.相位追猎者:
                case CardDB.cardNameCN.鱼人宝宝车队:
                case CardDB.cardNameCN.科多兽骑手:
                case CardDB.cardNameCN.奥秘守护者:
                case CardDB.cardNameCN.获救的流民:
                case CardDB.cardNameCN.白银之手新兵:
                case CardDB.cardNameCN.低阶侍从:
                    retval += bonus_enemy * 3;
                    break;
                // 算有点用
                case CardDB.cardNameCN.幽灵狼前锋:
                case CardDB.cardNameCN.战斗邪犬:
                case CardDB.cardNameCN.饥饿的秃鹫:
                case CardDB.cardNameCN.法力浮龙:
                case CardDB.cardNameCN.加基森拍卖师:
                case CardDB.cardNameCN.飞刀杂耍者:
                case CardDB.cardNameCN.锈水海盗:
                case CardDB.cardNameCN.大法师安东尼达斯:
                    retval += bonus_enemy * 2;
                    break;
            }
            // 血量越低，解怪优先度越高
            if (p.ownHero.Hp <= 15)
            {
                retval += (16 - p.ownHero.Hp) * 3;
                if (p.ownHero.Hp <= 6) retval *= 2;
            }
            return retval;
        }
        /// <summary>
        /// 我方随从价值
        /// </summary>
        /// <param name="m"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public override int getMyMinionValue(Minion m, Playfield p)
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
            int retval = 5;
            if (m.Hp <= 0) return 0;
            retval += m.Hp * bonus_mine;
            retval += m.Angr * bonus_mine;
            if (m.Hp <= 1 && !m.divineshild) retval -= (m.Angr - 1) * (bonus_mine - 1);
            // 高攻低血是垃圾
            if (m.Angr > m.Hp + 4) retval -= (m.Angr - m.Hp) * (bonus_mine - 1);
            // 风怒价值
            if ((!m.playedThisTurn || m.rush == 1 || m.charge == 1) && m.windfury) retval += m.Angr;
            // 圣盾价值
            if (m.divineshild) retval += m.Angr * 3;
            // 潜行价值
            if (m.stealth) retval += m.Angr / 2 + 1;
            // 吸血
            if (m.lifesteal) retval += m.Angr / 2 + 1;
            // 圣盾嘲讽
            if (m.divineshild && m.taunt) retval += 4;
            // 嘲讽
            if (m.taunt) retval += 3;
            switch (m.handcard.card.nameCN)
            {
                case CardDB.cardNameCN.黑眼:
                    break;
            }
            if (m.dormant > 0)
            {
                retval -= bonus_mine * m.dormant;
            }
            return retval;
        }

        public override int getSirFinleyPriority(List<Handmanager.Handcard> discoverCards)
        {

            return -1; //comment out or remove this to set manual priority
            int sirFinleyChoice = -1;
            int tmp = int.MinValue;
            for (int i = 0; i < discoverCards.Count; i++)
            {
                CardDB.cardNameEN name = discoverCards[i].card.nameEN;
                if (SirFinleyPriorityList.ContainsKey(name) && SirFinleyPriorityList[name] > tmp)
                {
                    tmp = SirFinleyPriorityList[name];
                    sirFinleyChoice = i;
                }
            }
            return sirFinleyChoice;
        }
        public override int getSirFinleyPriority(CardDB.Card card)
        {
            return SirFinleyPriorityList[card.nameEN];
        }

        private Dictionary<CardDB.cardNameEN, int> SirFinleyPriorityList = new Dictionary<CardDB.cardNameEN, int>
        {
            //{HeroPowerName, Priority}, where 0-9 = manual priority
            { CardDB.cardNameEN.lesserheal, 0 },
            { CardDB.cardNameEN.shapeshift, 6 },
            { CardDB.cardNameEN.fireblast, 7 },
            { CardDB.cardNameEN.totemiccall, 1 },
            { CardDB.cardNameEN.lifetap, 9 },
            { CardDB.cardNameEN.daggermastery, 5 },
            { CardDB.cardNameEN.reinforce, 4 },
            { CardDB.cardNameEN.armorup, 2 },
            { CardDB.cardNameEN.steadyshot, 8 }
        };

        public override int getHpValue(Playfield p, int hpboarder, int aggroboarder)
        {
            int offset_enemy = 0;

            int retval = 0;
            // 血线安全
            if (p.ownHero.Hp + p.ownHero.armor > hpboarder)
            {
                retval += (5 + p.ownHero.Hp + p.ownHero.armor - hpboarder);
            }
            // 快死了
            else
            {
                //if (p.nextTurnWin()) retval -= (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor);
                retval -= 5 * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor) * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor);
            }
            if (p.ownHero.Hp + p.ownHero.armor < 10 && p.ownHero.Hp + p.ownHero.armor > 0)
            {
                retval -= 200 / (p.ownHero.Hp + p.ownHero.armor);
            }
            // 对手血线安全
            if (p.enemyHero.Hp + p.enemyHero.armor + offset_enemy >= aggroboarder)
            {
                retval += 2 * (aggroboarder - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            }
            // 开始打脸
            else
            {
                retval += 4 * (aggroboarder + 1 - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            }
            // 场攻+直伤大于对方生命，预计完成斩杀
            if (p.anzEnemyTaunt == 0 && p.calTotalAngr() + p.calDirectDmg(p.mana, false) >= p.enemyHero.Hp + p.enemyHero.armor)
            {
                retval += 2000;
            }
            return retval;
        }

        /// <summary>
        /// 获取使用地标的惩罚值
        /// </summary>
        /// <param name="m"></param>
        /// <param name="target"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public override int getUseLocationPenality(Minion m, Minion target, Playfield p)
        {
            int penalty = 0; // 初始惩罚值为 0

            switch (m.handcard.card.nameCN.ToString())
            {
                case "树篱迷宫":
                    penalty = -47; // 优先级数值转为负值作为惩罚值
                    break;
                case "远足步道":
                    penalty = -49;
                    break;
                case "尤格萨隆的监狱":
                    penalty = -52;
                    break;
                case "惊险悬崖":
                    penalty = -68;
                    break;
                case "鹦鹉乐园":
                    penalty = -59;
                    break;
                case "大地之末号":
                    penalty = -59;
                    break;
                case "潮汐之地":
                    penalty = -50;
                    break;
                case "小玩物小屋":
                    penalty = -48;
                    break;
                default:
                    penalty = 0; // 如果卡牌名称不匹配，使用初始惩罚值
                    break;
            }

            return (int)penalty;
        }

        

        /// <summary>
        /// 获取使用泰坦技能的惩罚值
        /// </summary>
        /// <param name="m"></param>
        /// <param name="target"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public override int getUseTitanAbilityPenality(Minion m, Minion target, Playfield p)
        {
            
                return 0;
        }

        /// <summary>
        /// 发现卡的价值
        /// </summary>
        /// <param name="card"></param>
        /// <param name="p"></param>
        /// <returns></returns>
          public override int getDiscoverVal(CardDB.Card card, Playfield p)
        {
            switch (card.nameCN)
            {
            case CardDB.cardNameCN.基尔加丹:
            if(p.ownDeckSize >= 20)
            {
                return 0;
            }
            else if(p.ownDeckSize <= 15)
            {
                return 50;
            }
            else if(p.ownDeckSize <= 10)
            {
                return 100;
            }
            else if(p.ownDeckSize <= 5)
            {
                return 300;
            }
            return 10;
            case CardDB.cardNameCN.补水区:
            if(p.getCorpseCount() >= 6 && p.ownMaxMana >= 8)
            {
                return 80;
            }
            return 20; 
            case CardDB.cardNameCN.黑石摇滚:
            return 50; 
            }     

           try
              {
                //初始化Hsreplay数据
                Hsreplay hs = Hsreplay.Instance;

        // 1. 检查数据加载状态
            if (Hsreplay.AllCardStats == null || Hsreplay.AllCardStats.Count == 0)
            {
                Helpfunctions.Instance.logg("Hsreplay 数据未加载或为空");
                return 0;
            }
            else
            {
                Helpfunctions.Instance.logg("已加载 " + Hsreplay.AllCardStats.Count.ToString() + " 条卡牌数据");
            }

                // 2. 打印调试信息
                Helpfunctions.Instance.logg(string.Format("查询卡牌：{0} (DBF ID: {1})", card.nameCN, card.dbfId));
        
            // 3. 添加类型转换确保一致
            var cardStats = Hsreplay.AllCardStats.FirstOrDefault(c => c.DbfId == card.dbfId);

            if (cardStats != null)
            {
               Helpfunctions.Instance.logg("匹配成功 - 胜率: " + cardStats.WinrateWhenDrawn.ToString());
               Helpfunctions.Instance.logg("getDiscoverVal - 使用Hsreplay数据比对" + card.nameCN.ToString() + " => " + cardStats.WinrateWhenDrawn);
               ilog_0.Info("getDiscoverVal - 使用Hsreplay数据比对" + card.nameCN.ToString() + " => " + cardStats.WinrateWhenDrawn);
               // 返回 WinrateWhenDrawn 的整数部分
               return (int)cardStats.WinrateWhenDrawn;
            }

               // 4. 输出样例数据辅助调试
               var sampleIds = Hsreplay.AllCardStats.Take(10).Select(c => c.DbfId).ToList();
               Helpfunctions.Instance.logg("未找到匹配数据，样例 DbfIds: " + string.Join(", ", sampleIds));
               return 0;
            }
            catch (Exception ex)
            {
               Helpfunctions.Instance.logg("异常发生: " + ex.Message);
               //ilog_0.Error(ex, "getDiscoverVal 执行错误");
               return 0;
            }
        }

        
    }
}