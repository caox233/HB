using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_CORE_EX1_407 : SimTemplate //* Brawl
    {
        // Destroy all minions except one. (chosen randomly) �ƻ����й��ޣ�����������

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // ��ȡ��ǰ���ϵ���������б�
            List<Minion> allMinions = new List<Minion>();

            foreach (var minion in p.ownMinions)
                allMinions.Add(minion);

            foreach (var minion in p.enemyMinions)
                allMinions.Add(minion);

            // �������û����ӣ�ֱ�ӷ��أ���Ȼ�����Ѿ�������
            if (allMinions.Count < 1)
                return;

            // ���ѡ��һ����ӱ���
            Random random = new Random();
            int indexToKeep = random.Next(allMinions.Count);
            Minion chosen = allMinions[indexToKeep];

            // ɾ�������������
            foreach (var minion in allMinions)
            {
                if (minion != chosen)
                {
                    if (minion.own) // ����Ǽ�����ӣ�ʹ�÷ֽ��߼�
                        p.minionGetDestroyed(minion);
                    else // ����ǵз���ӣ�ֱ���Ƴ�
                        p.minionGetDestroyed(minion);
                }
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINIMUM_TOTAL_MINIONS, 4),
            };
        }
    }
}