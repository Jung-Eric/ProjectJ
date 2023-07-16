using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameController_Normal : MonoBehaviour
{

    //�پ��� ���� �⺻����
    [System.Serializable]
    public class PlayerBaseInfo
    {
        //���� ������ ���� ����
        public int avatarNum;
        public int droneNum;
        public int stageNum;

        //true�� �Ϲ� ����, �ƴϸ� AI
        public bool isPlayer;

        //�Ϲ� ������ �ƴϸ� ������� �������̴�.
        //�⺻������ �����κ��� �޾ƿ��� �ȴ�.
        public List<RecentHistory> recentHistoryList;

    }

    [System.Serializable]
    public class MyPlayerUI
    {

        //�ϴ� ���� UI
        /*
        //���� ��ư
        public GameObject finishButton;

        //�ռ� ��ư���� ������ ������ ��ȯ�� �� �ִ�.
        public GameObject integrationButtonIdentical;
        public GameObject integrationButtonIdentical_Large;
        public GameObject integrationButtonSequential;
        public GameObject integrationButtonSequential_Large;

        */

        //��ü UI GameObject
        public GameObject UIAll;

        //�ռ��Ѵ�.
        public GameObject integrationButton;

        //���ٴϴ� 6���� �� �� �ϳ��� �����Ѵ�.
        public GameObject recoverButton;


        //�ź��� ������ ó�� �� 
        //�ź� Ȱ�� ��ư
        //slelect 0������ �� �ϳ� ��� ������.
        //public GameObject utilizeButton;
        

        //������ ��ư(select Ȱ��ȭ)
        public GameObject dumpButton;
        //�ٷ� ������
        public GameObject dumpExecutionButton;

        [Space(10)]
        //�ǵ����� ��ư
        public GameObject returnUI;

        /*
        //�ٸ� ��ɵ��� ������ �� �⺻ ���·� ���ƿ´�.
        //���ƿ��� ��ư
        public GameObject returnButton;

        //���ġ
        //�̴� ���ϴܿ� ��ġ�Ǿ��ִ�.
        public GameObject rearrangeButton;
        */
    }

    //�پ��� ���� �⺻����
    [System.Serializable]
    public class PlayerGameInfo
    {
        //���� ȹ�� ����
        public int nowScore;

        [Space(5)]
        public bool isConnected;

        [Space(5)]
        //���� ����ִ���
        public bool isAlive;

        [Space(5)]
        //���� �ϼ��� ���� ����
        public int nowCompletedStoneCount;
        //���� ������ �ź� ����
        public int nowHoldingArcaneCount;


        [Space(10)]
        //���� �ش� ����� ��������


        [Space(5)]
        //���� ������ �ִ� ���� ���� ���� ����
        public List<StoneInfo> HoldingStone;

        //������ list
        //������ ��ġ�� ���� ������ ���ϰ� ����
        //�⺻�����δ� 0���θ� �̷��� �ִ�.
        //�ϼ�(1) �ź�(1) �⺻(5+3+3) 1+1+5+3+3 13����
        [Space(10)]
        [Header("������ reference / 0 : �ϼ� / 1 : �ź� / 2~6 : ���Ҽ� / 7~9 : ���ϼ� / 10~12 : ö����")]
        [Space(5)]
        public List<int> HoldingStoneReferenceCounts;

        //holding stone�� �⺻���� reference�� �����Ѵ�.
        //my ������ �̸� ����Ѵ�.
        #region rearrange


        //�⺻���� arrange
        //�ϴ� �̰� �������� �۾��ϵ��� ����
        //�̰� ���ӿ� �������� �����̴�.
        [Space(5)]
        public List<int> HoldingStoneArrangeReference_Base;

        [Space(5)]
        bool isHoldStoneArrangeCalculated_Element;
        //�������� ����
        public List<int> HoldingStoneArrangeReference_Element;

        //�ϴ� ������ �Ǿ����� Ȯ���Ѵ�.
        bool isHoldStoneArrangeCalculated_Optic;
        //�������� ����
        public List<int> HoldingStoneArrangeReference_Optic;

        //�ϴ� ������ �Ǿ����� Ȯ���Ѵ�.
        bool isHoldStoneArrangeCalculated_Mineral;
        //�������� ����
        public List<int> HoldingStoneArrangeReference_Mineral;


        //���߿� �ϼ��� ���� ���� reference�� �����.
        bool isHoldStoneArrangeCalculated_AmountDescending;
        //�������� ����
        public List<int> HoldingStoneArrangeReference_AmountDescending;
        #endregion


        //������� ���� ���� ����(���� ���۷��� ����)
        public int accumulatedCounts;

        [Space(5)]
        //�⺻������ ������ ����
        public List<int> SelectStonesRef;
        //public List<StoneInfo> selectedStones;

        //�� ���� ����(first player selection)
        public int firstPlayerScore;


        //�ϼ��� �پ��� ���� ����
        [Space(10)]
        [Header("���� ���� �Ϲ� ����")]
        [Space(5)]
        
        public List<int> RecoverAvailIndexList;
        public List<int> RecoverDumpIndexRefList;
        [Space(5)]
        public List<int> IntegrationIndexList;

        [Space(5)]
        public CompletionTypeArcane completionTypeArcane;
        public List<CompletionTypeElement> CompletionTypeElementsList;
        public List<CompletionTypeOpticMineral> CompletionTypeOpticList;
        public List<CompletionTypeOpticMineral> CompletionTypeMineralList;

        //�ϼ� ���� ����
        [Space(10)]
        [Header("���� ���� ���� ����")]
        [Space(5)]
        //���� ���� �Ѱ� ����
        public CompletedStonesInfo completedStonesInfo;


        //�⺻���� ������.
        public PlayerGameInfo()
        {
            nowCompletedStoneCount = 0;
        }

        //���� �ռ� ���� ��
        public void PlayerStoneIntegration()
        {
            //���� ����
            nowCompletedStoneCount++;
        }

        //first selection ���� ����
        public void CalFirstPlayerScore()
        {

            //����� ���� ���а� ���� ��
            //Ȥ�ö� ���� ����� �� �������� -99���.
            if (SelectStonesRef.Count == 2)
            {
                //���� ���� Ÿ���� ���ٸ�
                if (HoldingStone[SelectStonesRef[0]].stoneDetailType == HoldingStone[SelectStonesRef[1]].stoneDetailType)
                {
                    //���� ��ȣ
                    if (HoldingStone[SelectStonesRef[0]].stoneNumber == HoldingStone[SelectStonesRef[1]].stoneNumber)
                    {
                        firstPlayerScore = 80;
                    }
                    //���ӵ�(����)
                    else if (HoldingStone[SelectStonesRef[0]].stoneType == StoneType.element)
                    {
                        if (HoldingStone[SelectStonesRef[0]].stoneNumber + 1 == HoldingStone[SelectStonesRef[1]].stoneNumber)
                        {
                            firstPlayerScore = 60;
                        }
                        //���ӵ�
                        else if (HoldingStone[SelectStonesRef[0]].stoneNumber == HoldingStone[SelectStonesRef[1]].stoneNumber + 1)
                        {
                            firstPlayerScore = 60;
                        }
                        else
                        {
                            firstPlayerScore = 40;
                        }
                    }
                    //�� �� ���� ö�� ����
                    else
                    {
                        firstPlayerScore = 40;
                    }

                }
                else if (HoldingStone[SelectStonesRef[0]].stoneType == HoldingStone[SelectStonesRef[1]].stoneType)
                {
                    firstPlayerScore = 20;
                    CalFirstPlayerScoreSub();
                }
                else
                {
                    firstPlayerScore = 0;
                    CalFirstPlayerScoreSub();
                }

            }
            else
            {
                //Ȥ�ó� ����ó��
                firstPlayerScore = -99;
            }

        }

        //�Լ� �����
        public void CalFirstPlayerScoreSub()
        {
            //���� ���� ũ�� ����
            int tempStoneNum0 = 0;
            int tempStoneNum1 = 0;

            //���Ҹ� ���ڸ� �ݿ��Ѵ�.
            //���� �ź�� ������ 1�� ����Ѵ�.
            if (HoldingStone[SelectStonesRef[0]].stoneType == StoneType.element)
            {
                tempStoneNum0 = HoldingStone[SelectStonesRef[0]].stoneNumber + 1;
            }
            else
            {
                tempStoneNum0 = 1;
            }


            if (HoldingStone[SelectStonesRef[1]].stoneType == StoneType.element)
            {
                tempStoneNum1 = HoldingStone[SelectStonesRef[1]].stoneNumber + 1;
            }
            else
            {
                tempStoneNum1 = 1;
            }

            firstPlayerScore = firstPlayerScore + tempStoneNum0 + tempStoneNum1;
        }

        //=============================================================================================
        //���� �ʱ�ȭ �Լ��� ���� �ʱ�ȭ �Լ�

        #region clear function

        //���� �ʰ�ȭ �Լ�
        public void ClearPlayerGameInfo()
        {
            nowScore = 0;
            isAlive = true;

            nowCompletedStoneCount = 0;
            nowHoldingArcaneCount = 0;
            accumulatedCounts = 0;

            HoldingStone.Clear();
            ResetHoldingStoneReferenceCounts();

            SelectStonesRef.Clear();
            firstPlayerScore = 0;

            //�ΰ����� ��ġ �ʱ�ȭ
            ResetIsHoldStoneArrangeCalculated();
            ResetHoldingArrangeReference();

            //Ž�� �ʱ�ȭ
            CompletionReset();

        }

        //���� �ʱ�ȭ
        //������ �ʱ�ȭ �ϸ� �ȵȴ�.
        //���� ���µ� ������ �ʿ� ����.
        public void ClearPlayerRoundInfo()
        {

            nowCompletedStoneCount = 0;
            nowHoldingArcaneCount = 0;
            accumulatedCounts = 0;

            HoldingStone.Clear();
            ResetHoldingStoneReferenceCounts();

            SelectStonesRef.Clear();
            firstPlayerScore = 0;

            //�ΰ����� ��ġ �ʱ�ȭ
            ResetIsHoldStoneArrangeCalculated();
            ResetHoldingArrangeReference();

            //Ž�� �ʱ�ȭ
            CompletionReset();

        }
        #endregion

        //=============================================================================================

        //���� ������ ����ִ� ���� ������� ����
        //���� ����ϴ� �Լ�
        public void RearrangeHoldingStones()
        {

            for (int i = 0; i < HoldingStone.Count; i++)
            {

                StoneInfo tempStone = HoldingStone[i];

                for (int j = 0; j < i; j++)
                {
                    if (tempStone.stoneArrangeReference < HoldingStone[j].stoneArrangeReference)
                    {
                        HoldingStone.RemoveAt(i);

                        HoldingStone.Insert(j, tempStone);

                        break;
                    }
                }

            }
        }


        public void RearrangeHoldingStonesMinor()
        {

            //HoldingStone.Insert(null);

        }

        //reference ���� �ʱ�ȭ
        public void ResetIsHoldStoneArrangeCalculated()
        {
            isHoldStoneArrangeCalculated_Element = false;
            isHoldStoneArrangeCalculated_Optic = false;
            isHoldStoneArrangeCalculated_Mineral = false;
            isHoldStoneArrangeCalculated_AmountDescending = false;
        }

        public void ResetHoldingArrangeReference()
        {
            HoldingStoneArrangeReference_Element.Clear();
            HoldingStoneArrangeReference_Optic.Clear();
            HoldingStoneArrangeReference_Mineral.Clear();
            HoldingStoneArrangeReference_AmountDescending.Clear();
        }

        //=============================================================================================
        //��ü���� completion check �� �Լ���

        #region Completion Check

        //�ϼ� ������ ������ ����
        //PullOneStone(0) �̰��� �� �Լ��� �ִ´�.
        //��ο� �� ���� ������ ���� ���ġ�Ѵ�.
        //�ϴ� �� �Լ��� ����
        public void RenewCompletionInfo(StoneDetailType targetDetailType)
        {
            int tempNum = (int)targetDetailType;

            int tempMainType;

            //�ش� Ÿ�� �� ���°�� �ش��ϴ���
            //land : 1 / chaos : 2 / 
            int tempSubTypeNum;

            #region typeIfElse
            if(tempNum == 0)
            {
                //�ϼ��� ���� ���
                tempMainType = -1;
                tempSubTypeNum = -1;
            }
            else if (tempNum == 1)
            {
                tempMainType = 0;
                tempSubTypeNum = tempNum;
            }
            else if (tempNum < 7)
            {
                tempMainType = 1;
                tempSubTypeNum = tempNum - 2;
            }
            else if (tempNum < 10)
            {
                tempMainType = 2;
                tempSubTypeNum = tempNum - 7;
            }
            else if (tempNum < 13)
            {
                tempMainType = 3;
                tempSubTypeNum = tempNum - 10;
            }
            else
            {
                tempMainType = -1;
                tempSubTypeNum = 0;
            }
            #endregion


            switch (tempMainType)
            {
                //�ź�
                case 0:
                    CalculateAracaneReference();
                    break;

                //���Ҽ�
                case 1:
                    CalculateElementReference(tempSubTypeNum, tempNum);
                    break;

                //���ϼ�
                case 2:
                    CalculateOpticReference(tempSubTypeNum, tempNum);
                    break;

                //ö����
                case 3:
                    CalculateMineralReference(tempSubTypeNum, tempNum);
                    break;

                default:

                    break;

            }

        }

        //�ʱ⿡ ��ü ����
        //�ϴ� ���� ������ ����, �� �Լ��� ���� �⺻ �����Լ�
        public void RenewCompletionInfoAll()
        {
            RecoverAvailIndexList.Clear();
            RecoverDumpIndexRefList.Clear();

            IntegrationIndexList.Clear();

            CalculateAracaneReference();

            //���Ҽ� ����
            for(int i=(int)(StoneDetailType.element_fire); i< (int)(StoneDetailType.optic_light); i++)
            {
                CalculateElementReference(i- (int)(StoneDetailType.element_fire), i);
            }

            //���ϼ� ����
            for (int i = (int)(StoneDetailType.optic_light); i < (int)(StoneDetailType.mineral_gold); i++)
            {
                CalculateOpticReference(i - (int)(StoneDetailType.optic_light), i);
            }

            //ö���� ����
            for (int i = (int)(StoneDetailType.mineral_gold); i < (int)(StoneDetailType.last); i++)
            {
                CalculateMineralReference(i - (int)(StoneDetailType.mineral_gold), i);
            }

        }

        public void CalculateAracaneReference()
        {
            //int startValue = HoldingStoneReferenceCountsAccumulation(1);

            if (HoldingStoneReferenceCounts[1] > 1)
            {

                if (HoldingStoneReferenceCounts[1] == 2)
                {
                    //1���� ����ִ� ����
                    completionTypeArcane.completionSame = 0;
                    completionTypeArcane.completionSameLarge = -2;

                    RecoverAvailIndexList.Add(0);
                }
                else if(HoldingStoneReferenceCounts[1] == 3)
                {
                    //�ϼ��� ����
                    completionTypeArcane.completionSame = -1;
                    completionTypeArcane.completionSameLarge = -2;

                    RecoverAvailIndexList.Add(0);

                    IntegrationIndexList.Add(0);
                }
                else if(HoldingStoneReferenceCounts[1] == 4)
                {
                    completionTypeArcane.completionSame = -1;
                    completionTypeArcane.completionSameLarge = 0;

                    RecoverAvailIndexList.Add(0);
                    RecoverAvailIndexList.Add(1);

                    IntegrationIndexList.Add(0);
                }
                else if (HoldingStoneReferenceCounts[1] >= 5)
                {
                    completionTypeArcane.completionSame = -1;
                    completionTypeArcane.completionSameLarge = -1;

                    RecoverAvailIndexList.Add(0);
                    RecoverAvailIndexList.Add(1);

                    IntegrationIndexList.Add(0);
                    IntegrationIndexList.Add(1);
                }
                else
                {
                    completionTypeArcane.completionSame = -2;
                    completionTypeArcane.completionSameLarge = -2;
                }

                return;
            }
            else
            {
                return;
            }

        }

        public void CalculateElementReference(int subNum, int originRef)
        {
            int startValue = HoldingStoneReferenceCountsAccumulation(originRef);

            //2�� �̻��� ������ Ȱ��ȭ
            //�� �ܿ��� �ߵ��ʿ� ����.
            if (HoldingStoneReferenceCounts[originRef] > 1)
            {

                //completionSameList�� ���� ����
                //completionSameList�� ���� SameCheckList�� ����

                #region completion same
                List<int> SameCheckList = new List<int>() { 0,0,0,0,0 };

                for (int i = startValue; i < startValue + HoldingStoneReferenceCounts[originRef]; i++)
                {

                    SameCheckList[HoldingStone[i].stoneNumber]++;

                }

                //5���� completionSameList
                for (int i=0; i<5; i++)
                {
                    if(SameCheckList[i] > 1)
                    {
                        if(SameCheckList[i] == 2)
                        {
                            //same check list�� ���� �����Ѵ�.
                            //�̿ϼ��� �κ�
                            CompletionTypeElementsList[subNum].completionSameList[i] = i;

                            RecoverAvailIndexList.Add(100 + subNum * 10 + i);

                        }
                        else if(SameCheckList[i] >= 3)
                        {
                            CompletionTypeElementsList[subNum].completionSameList[i] = -1;

                            RecoverAvailIndexList.Add(100 + subNum * 10 + i);
                            IntegrationIndexList.Add(100 + subNum * 10 + i);
                        }
                        else
                        {
                            CompletionTypeElementsList[subNum].completionSameList[i] = -2;
                        }
                    }
                }
                #endregion

                //=======================================================================================

                //completionStairList / completionStairLarge �� ���� ����
                // 012 / 123 / 234 / 01234 �� ���� ����

                #region completion stair
                List<bool> StairCheckList = new List<bool>() { false, false, false, false, false };
                int tempCount = 0;
                int tempRef = -2;

                for (int i = startValue; i < startValue + HoldingStoneReferenceCounts[originRef]; i++)
                {

                    StairCheckList[HoldingStone[i].stoneNumber] = true;

                }


                // 012  123  234 �� ����
                for (int subRef = 0; subRef < 3; subRef++)
                {
                    tempCount = 0;
                    tempRef = -2;

                    for (int i = 0 + subRef; i < 3 + subRef; i++)
                    {
                        if (StairCheckList[i])
                        {
                            tempCount++;
                        }
                        else
                        {
                            tempRef = i;
                        }
                    }

                    if(tempCount == 3)
                    {
                        CompletionTypeElementsList[subNum].completionStairList[subRef] = -1;

                        RecoverAvailIndexList.Add(100 + subNum * 10 + subRef + 5);
                        IntegrationIndexList.Add(100 + subNum * 10 + subRef + 5);
                    }
                    else if(tempCount == 2)
                    {
                        CompletionTypeElementsList[subNum].completionStairList[subRef] = tempRef;

                        RecoverAvailIndexList.Add(100 + subNum * 10 + subRef + 5);
                    }
                    else
                    {
                        CompletionTypeElementsList[subNum].completionStairList[subRef] = -2;
                    }
                }

                // 01234 �� ����
                tempCount = 0;
                tempRef = -2;

                for (int i = 0; i < 5; i++)
                {
                    if (StairCheckList[i])
                    {
                        tempCount++;
                    }
                    else
                    {
                        tempRef = i;
                    }
                }

                if(tempCount == 5)
                {
                    CompletionTypeElementsList[subNum].completionStairLarge = -1;

                    RecoverAvailIndexList.Add(100 + subNum * 10 + 8);
                    IntegrationIndexList.Add(100 + subNum * 10 + 8);
                }
                else if(tempCount == 4)
                {
                    CompletionTypeElementsList[subNum].completionStairLarge = tempRef;

                    RecoverAvailIndexList.Add(100 + subNum * 10 + 8);
                }
                else
                {
                    CompletionTypeElementsList[subNum].completionStairLarge = -2;
                }


                #endregion

            }

        }

        public void CalculateOpticReference(int subNum, int originRef)
        {
            int startValue = HoldingStoneReferenceCountsAccumulation(originRef);
            //Debug.Log(startValue);

            //2�� �̻��� ������ Ȱ��ȭ
            //�� �ܿ��� �ߵ��ʿ� ����.
            if (HoldingStoneReferenceCounts[originRef] > 1)
            {

                //completionSameList�� ���� ����
                //completionSameList�� ���� SameCheckList�� ����

                #region completion same
                List<int> SameCheckList = new List<int>() { 0, 0, 0 };

                for (int i = startValue; i < startValue + HoldingStoneReferenceCounts[originRef]; i++)
                {

                    SameCheckList[HoldingStone[i].stoneNumber]++;

                }

                //3���� ����
                for (int i = 0; i < 3; i++)
                {
                    if (SameCheckList[i] > 1)
                    {
                        if (SameCheckList[i] == 2)
                        {
                            CompletionTypeOpticList[subNum].completionSameList[i] = i;
                            //CompletionTypeOpticList[subNum].completionSameList[i] = subNum;
                            CompletionTypeOpticList[subNum].completionSameLargeList[i] = -2;

                            RecoverAvailIndexList.Add(200 + subNum * 10 + i);
                        }
                        else if (SameCheckList[i] == 3)
                        {
                            CompletionTypeOpticList[subNum].completionSameList[i] = -1;
                            CompletionTypeOpticList[subNum].completionSameLargeList[i] = -2;

                            RecoverAvailIndexList.Add(200 + subNum * 10 + i);
                            IntegrationIndexList.Add(200 + subNum * 10 + i);
                        }
                        else if (SameCheckList[i] == 4)
                        {
                            CompletionTypeOpticList[subNum].completionSameList[i] = -1;
                            //CompletionTypeOpticList[subNum].completionSameLargeList[i] = subNum;
                            CompletionTypeOpticList[subNum].completionSameLargeList[i] = i;

                            RecoverAvailIndexList.Add(200 + subNum * 10 + i);
                            RecoverAvailIndexList.Add(200 + subNum * 10 + 4 + i);
                            IntegrationIndexList.Add(200 + subNum * 10 + i);
                        }
                        else if (SameCheckList[i] >= 5)
                        {
                            CompletionTypeOpticList[subNum].completionSameList[i] = -1;
                            CompletionTypeOpticList[subNum].completionSameLargeList[i] = -1;

                            RecoverAvailIndexList.Add(200 + subNum * 10 + i);
                            RecoverAvailIndexList.Add(200 + subNum * 10 + 4 + i);
                            IntegrationIndexList.Add(200 + subNum * 10 + i);
                            IntegrationIndexList.Add(200 + subNum * 10 + 4 + i);
                        }
                        else
                        {
                            CompletionTypeOpticList[subNum].completionSameList[i] = -2;
                            CompletionTypeOpticList[subNum].completionSameLargeList[i] = -2;
                        }
                    }
                }
                #endregion

                //=======================================================================================

                //completionStairList �� ���� ����
                // 012 �� ���� ����

                #region completion stair
                List<bool> StairCheckList = new List<bool>() { false, false, false };
                int tempCount = 0;
                int tempRef = -2;

                for (int i = startValue; i < startValue + HoldingStoneReferenceCounts[originRef]; i++)
                {

                    StairCheckList[HoldingStone[i].stoneNumber] = true;

                }

                // 012  �� ����
                tempCount = 0;
                tempRef = -2;

                for (int i = 0; i < 3; i++)
                {
                    if (StairCheckList[i])
                    {
                        tempCount++;
                    }
                    else
                    {
                        tempRef = i;
                    }
                }

                if (tempCount == 3)
                {
                    CompletionTypeOpticList[subNum].completionStair = -1;

                    RecoverAvailIndexList.Add(200 + subNum * 10 + 3);
                    IntegrationIndexList.Add(200 + subNum * 10 + 3);

                }
                else if (tempCount == 2)
                {
                    CompletionTypeOpticList[subNum].completionStair = tempRef;

                    RecoverAvailIndexList.Add(200 + subNum * 10 + 3);
                }
                else
                {
                    CompletionTypeOpticList[subNum].completionStair = -2;
                }
                

                #endregion

            }


        }

        public void CalculateMineralReference(int subNum, int originRef)
        {
            int startValue = HoldingStoneReferenceCountsAccumulation(originRef);

            //2�� �̻��� ������ Ȱ��ȭ
            //�� �ܿ��� �ߵ��ʿ� ����.
            if (HoldingStoneReferenceCounts[originRef] > 1)
            {

                //completionSameList�� ���� ����
                //completionSameList�� ���� SameCheckList�� ����

                #region completion same
                List<int> SameCheckList = new List<int>() { 0, 0, 0 };

                for (int i = startValue; i < startValue + HoldingStoneReferenceCounts[originRef]; i++)
                {

                    SameCheckList[HoldingStone[i].stoneNumber]++;

                }

                //3���� ����
                for (int i = 0; i < 3; i++)
                {
                    if (SameCheckList[i] > 1)
                    {
                        if (SameCheckList[i] == 2)
                        {
                            CompletionTypeMineralList[subNum].completionSameList[i] = i;
                            //CompletionTypeMineralList[subNum].completionSameList[i] = subNum;
                            CompletionTypeMineralList[subNum].completionSameLargeList[i] = -2;

                            RecoverAvailIndexList.Add(300 + subNum * 10 + i);
                        }
                        else if (SameCheckList[i] == 3)
                        {
                            CompletionTypeMineralList[subNum].completionSameList[i] = -1;
                            CompletionTypeMineralList[subNum].completionSameLargeList[i] = -2;

                            RecoverAvailIndexList.Add(300 + subNum * 10 + i);
                            IntegrationIndexList.Add(300 + subNum * 10 + i);
                        }
                        else if (SameCheckList[i] == 4)
                        {
                            CompletionTypeMineralList[subNum].completionSameList[i] = -1;
                            //CompletionTypeMineralList[subNum].completionSameLargeList[i] = subNum;
                            CompletionTypeMineralList[subNum].completionSameLargeList[i] = i;

                            RecoverAvailIndexList.Add(300 + subNum * 10 + i);
                            RecoverAvailIndexList.Add(300 + subNum * 10 + 4 + i);
                            IntegrationIndexList.Add(300 + subNum * 10 + i);
                        }
                        else if (SameCheckList[i] >= 5)
                        {
                            CompletionTypeMineralList[subNum].completionSameList[i] = -1;
                            CompletionTypeMineralList[subNum].completionSameLargeList[i] = -1;

                            RecoverAvailIndexList.Add(300 + subNum * 10 + i);
                            RecoverAvailIndexList.Add(300 + subNum * 10 + 4 + i);
                            IntegrationIndexList.Add(300 + subNum * 10 + i);
                            IntegrationIndexList.Add(300 + subNum * 10 + 4 + i);
                        }
                        else
                        {
                            CompletionTypeMineralList[subNum].completionSameList[i] = -2;
                            CompletionTypeMineralList[subNum].completionSameLargeList[i] = -2;
                        }
                    }
                }
                #endregion

                //=======================================================================================

                //completionStairList �� ���� ����
                // 012 �� ���� ����

                #region completion stair
                List<bool> StairCheckList = new List<bool>() { false, false, false };
                int tempCount = 0;
                int tempRef = -2;

                for (int i = startValue; i < startValue + HoldingStoneReferenceCounts[originRef]; i++)
                {

                    StairCheckList[HoldingStone[i].stoneNumber] = true;

                }

                // 012  �� ����
                tempCount = 0;
                tempRef = -2;

                for (int i = 0; i < 3; i++)
                {
                    if (StairCheckList[i])
                    {
                        tempCount++;
                    }
                    else
                    {
                        tempRef = i;
                    }
                }

                if (tempCount == 3)
                {
                    CompletionTypeMineralList[subNum].completionStair = -1;

                    RecoverAvailIndexList.Add(300 + subNum * 10 + 3);
                    IntegrationIndexList.Add(300 + subNum * 10 + 3);
                }
                else if (tempCount == 2)
                {
                    CompletionTypeMineralList[subNum].completionStair = tempRef;

                    RecoverAvailIndexList.Add(300 + subNum * 10 + 3);
                }
                else
                {
                    CompletionTypeMineralList[subNum].completionStair = -2;
                }


                #endregion

            }

        }


        public void CompletionReset()
        {
            //�� �ʱ�ȭ
            CompletionTypeElementsList.Clear();
            CompletionTypeOpticList.Clear();
            CompletionTypeMineralList.Clear();

            RecoverAvailIndexList.Clear();
            IntegrationIndexList.Clear();

            //���� �� �ʱ�ȭ
            completionTypeArcane.CompletionTypeArcaneReset();

            for (int i = 0; i < 5; i++)
            {
                CompletionTypeElement tempElement = new CompletionTypeElement();
                tempElement.CompletionTypeElementReset();
                CompletionTypeElementsList.Add(tempElement);
            }

            for (int i = 0; i < 3; i++)
            {
                CompletionTypeOpticMineral tempElement = new CompletionTypeOpticMineral();
                tempElement.CompletionTypeOpticMineralReset();
                CompletionTypeOpticList.Add(tempElement);
            }

            for (int i = 0; i < 3; i++)
            {
                CompletionTypeOpticMineral tempElement = new CompletionTypeOpticMineral();
                tempElement.CompletionTypeOpticMineralReset();
                CompletionTypeMineralList.Add(tempElement);
            }
        }


        #endregion

        //=============================================================================================


        //Ư�� ��ǥ���� �������� �޾ƿ´�.
        public int HoldingStoneReferenceCountsAccumulation(int tempRef)
        {
            int tempRes = 0;

            for (int i = 0; i < tempRef; i++)
            {
                tempRes = tempRes + HoldingStoneReferenceCounts[i];
            }

            return tempRes;
        }

        public void ResetHoldingStoneReferenceCounts()
        {
            HoldingStoneReferenceCounts.Clear();

            for (int i = 0; i < 13; i++)
            {
                HoldingStoneReferenceCounts.Add(0);
            }
        }

    }

    //�߰����� ������ ��Ʈ�ѿ� ���� ����
    //�̰� my player���� �ش��ϴ� �����̴�.
    [System.Serializable]
    public class PlayerGameInfoControl
    {

        //���� �巡�� ���� ������Ʈ ��ȣ
        // -1�� �������� ����
        // -2�� ���� ���ư��� ��
        // 0~9 �� �������� �� ����
        public int nowDraggingObjectNum;
        [Space(5)]
        public int tempSelectedDumpNum = -1;
        public PlayerGameInfoControl()
        {
            nowDraggingObjectNum = -1;
        }

        public void InitializePlayerGameInfoControl()
        {
            nowDraggingObjectNum = -1;
        }

    }

    //������ ���� ���� ���� ����
    //���� �÷����� �ٽ����� class
    [System.Serializable]
    public class PlayerStoneInfo
    {


    }
}
