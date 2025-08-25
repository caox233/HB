namespace HREngine.Bots
{
    using System.Collections.Generic;

    public class Handmanager  // 手牌管理
    {

        public class Handcard
        {


    // 新增字段  快枪用
    public bool isQuickdrawActive = false;
    public int drawnTurn = -1;

    public void MarkDrawn(int currentTurn)
    {
        drawnTurn = currentTurn;
        isQuickdrawActive = true;
    }

    public void UpdateQuickdrawStatus(int currentTurn)
    {
        isQuickdrawActive = (drawnTurn == currentTurn);
    }


            public int lockedUntilTurn = 0; // ← 新增字段：锁定回合数
            public int position = 0; //手牌的位置
            public int entity = -1; //炉石传说内部entity编号
            public int manacost = 1000; //花费费用，但获取卡牌费用要用getManaCost(Playfield p)函数
            public int addattack = 0; //增加的攻击力，如风驰电掣的SIM中就使其 +1
            public int addHp = 0; //增加的血量
            public CardDB.Card card; //卡牌，指向CardDB.cs
            public Minion target; //目标
            public int poweredUp = 0; //上回合是否使用元素牌
            public int darkmoon_num = 0; //暗月先知抽牌数：战斗中已触发的奥秘数
            public int extraParam2 = 0; //扩展参数2，可以用来记录一些此卡需要的特殊数据
            public bool extraParam3 = false; //扩展参数3
            public int SCRIPT_DATA_NUM_1 = 0;
            public int TAG_ONE_TURN_EFFECT = 0;
            public int LUNAHIGHLIGHTHINT = 0;
            public int scheme = 1;
            public List<CardDB.cardIDEnum> enchs = new List<CardDB.cardIDEnum>();
            public bool discovered = false;
            //临时卡牌
            public bool temporary = false;
            //条件计数，例如施放过几张法术，英雄技能造成多少伤害等
            public int conditionalCount = 0;
            //条件卡牌，例如施放的法术牌
            public List<CardDB.cardIDEnum> conditionalList = new List<CardDB.cardIDEnum>();

            public string Status
            {
                get
                {
                    return "{" + position + "} " + card.nameCN + " [费用: " + manacost + "] (" + (addattack + card.Attack) + "/" + (addHp + card.Health) + ")";
                }
            }

            public string OnlineCardImage
            {
                get
                {
                    return card.OnlineCardImage;
                }
            }

            public string OnlineCardTile
            {
                get
                {
                    return card.OnlineCardTile;
                }
            }

            public Handcard()
            {
                card = CardDB.Instance.unknownCard;
            }
            public Handcard(Handcard hc)
            {
                this.position = hc.position;
                this.entity = hc.entity;
                this.manacost = hc.manacost;
                this.card = hc.card;
                this.addattack = hc.addattack;
                this.addHp = hc.addHp;
                this.poweredUp = hc.poweredUp;
                this.SCRIPT_DATA_NUM_1 = hc.SCRIPT_DATA_NUM_1;
                this.discovered = hc.discovered;
                this.TAG_ONE_TURN_EFFECT = hc.TAG_ONE_TURN_EFFECT;
                this.LUNAHIGHLIGHTHINT = hc.LUNAHIGHLIGHTHINT;
                this.scheme = hc.scheme;
                this.enchs = hc.enchs;
                //临时卡牌
                this.temporary = hc.temporary;
                //条件计数
                this.conditionalCount = hc.conditionalCount;
                //条件卡牌
                this.conditionalList = hc.conditionalList;
            }
            public Handcard(CardDB.Card c)
            {
                this.position = 0;
                this.entity = -1;
                this.card = c;
                this.addattack = 0;
                this.addHp = 0;
            }
            public void setHCtoHC(Handcard hc)
            {
                this.manacost = hc.manacost;
                this.addattack = hc.addattack;
                this.addHp = hc.addHp;
                this.card = hc.card;
                this.poweredUp = hc.poweredUp;
                this.SCRIPT_DATA_NUM_1 = hc.SCRIPT_DATA_NUM_1;
                this.discovered = hc.discovered;
                this.TAG_ONE_TURN_EFFECT = hc.TAG_ONE_TURN_EFFECT;
                this.LUNAHIGHLIGHTHINT = hc.LUNAHIGHLIGHTHINT;
                this.enchs = hc.enchs;
                //临时卡牌
                this.temporary = hc.temporary;
                //条件计数
                this.conditionalCount = hc.conditionalCount;
                //条件卡牌
                this.conditionalList = hc.conditionalList;
            }

            public int getManaCost(Playfield p)  //读取卡牌法力值
            {
                if (this.enchs.Count > 0 && (this.enchs.Contains(CardDB.cardIDEnum.GDB_118t) || this.enchs.Contains(CardDB.cardIDEnum.GDB_118t2) || this.enchs.Contains(CardDB.cardIDEnum.SW_052t4e) || this.enchs.Contains(CardDB.cardIDEnum.TTN_744e1)))
                {
                    return 1000;
                }
                return this.card.getManaCost(p, this.manacost);
            }
            public bool canplayCard(Playfield p, bool own) //判定卡牌是否能够使用
            {
                // TODO 游戏内无法使用 声光干扰器
                if (this.enchs.Count > 0 && (this.enchs.Contains(CardDB.cardIDEnum.GDB_118t) || this.enchs.Contains(CardDB.cardIDEnum.GDB_118t2) || this.enchs.Contains(CardDB.cardIDEnum.SW_052t4e) || this.enchs.Contains(CardDB.cardIDEnum.TTN_744e1)))
                {
                    return false;
                }
                return this.card.canplayCard(p, this.manacost, own);
            }
        }

        public List<Handcard> handCards = new List<Handcard>();

        public int anzcards = 0;

        public int enemyAnzCards = 0;

        private int ownPlayerController = 0;

        Helpfunctions help;
        CardDB cdb = CardDB.Instance;

        private static Handmanager instance;

        public static Handmanager Instance
        {
            get
            {
                return instance ?? (instance = new Handmanager());
            }
        }


        private Handmanager()
        {
            this.help = Helpfunctions.Instance;

        }

        public void clearAllRecalc()
        {
            this.handCards.Clear();
            this.anzcards = 0;
            this.enemyAnzCards = 0;
            this.ownPlayerController = 0;
        }

        public void setOwnPlayer(int player)
        {
            this.ownPlayerController = player;
        }




        public void setHandcards(List<Handcard> hc, int anzown, int anzenemy)
        {
            this.handCards.Clear();
            foreach (Handcard h in hc)
            {
                this.handCards.Add(new Handcard(h));
            }
            //this.handCards.AddRange(hc);
            this.handCards.Sort((a, b) => a.position.CompareTo(b.position));
            this.anzcards = anzown;
            this.enemyAnzCards = anzenemy;
        }
        
        public void printcards()
        {
            help.logg("Own Handcards: ");
            foreach (Handmanager.Handcard hc in this.handCards)
            {
                string s = "pos " + hc.position + " " + hc.card.nameCN + "(" + hc.card.Attack + "/" + hc.card.Health + ")" + " " + hc.manacost + " entity " + hc.entity + " " + hc.card.cardIDenum + " " + hc.addattack + " " + hc.addHp + " " + hc.poweredUp;
                if(hc.enchs.Count > 0)
                {
                    foreach(CardDB.cardIDEnum cardIDEnum in hc.enchs)
                    {
                        s += " " + cardIDEnum.ToString();
                    }
                }
                help.logg(s);
            }
            help.logg("Enemy cards: " + this.enemyAnzCards);
        }

         // 新增的抽牌方法  快枪用
public void DrawCard(int currentTurn, CardDB.Card newCardData)
{
    // 创建新手牌实例
    Handcard newCard = new Handcard(newCardData)
    {
        position = this.handCards.Count, // 位置设为当前手牌数
        entity = this.getNewEntityID(), // 需要补充实体ID生成方法
        manacost = newCardData.cost // 初始化基础费用
    };

    // 标记快枪状态
    newCard.MarkDrawn(currentTurn);
    
    // 将新卡加入手牌列表
    this.handCards.Add(newCard);
    this.anzcards++;

    // 手牌排序（按position升序）
    this.handCards.Sort((a, b) => a.position.CompareTo(b.position));
}

// 需要补充的实体ID生成方法（在类内添加）
private int entityCounter = 100;
private int getNewEntityID()
{
    return entityCounter++;
}


    }

}