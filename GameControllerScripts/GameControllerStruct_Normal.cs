using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;

public partial class GameController_Normal : MonoBehaviour
{
    #region enums

    //���� ���� �뷫�� �ܰ�
    public enum NowGameStepEnum
    {

        GameEntrance,
        MainGame_Initialize,
        MainGame_Neutral,
        MainGame_Other,
        MainGame_My,
        EndGame,

    }

    //���� ���� ���� �ܰ�
    public enum NowGameStepDetailEnum
    {
        //���� ���� ���� ��
        GameEntrance_Ready,

        //����� �Բ� ���� ����
        GameEntrance_ShowReady,
        //���� ��
        GameEntrance_Show,

        //���� ���� �� ��ȭ
        GameEntrance_Dialogue,
        //���� ���� �� ���
        GameEntrance_Wait,


        //����� ����
        //���� ������ ����ϰ� ���ӽ��۴��
        MainGame_InitialFirstSetting,
        MainGame_InitialFirstPull,

        MainGame_InitialFirstSelectionSetting,
        MainGame_InitialFirstSelectionReady,
        MainGame_InitialFirstSelectionWaiting,
        
        MainGame_InitialFirstPlayer,
        MainGame_InitialFirstDump,
        MainGame_InitialFirstEnd,

        //�߸� ó��
        //�� �̵� ó��
        MainGame_NeutralTurnCheck,
        //��� ������ ���� �� ��Ʈ��ũ�� Ȯ�� �� �Ѿ��.
        MainGame_NeutralUserAllCheck,
        //���� ������ �߻��� ��� ó��
        MainGame_NeutralError,

        //�� ���� ���� ó��
        //��ȿ�� �� �ǵ� ����� �Ͱ�ȴ�.
        MainGame_NeutralRoundEnd,

        //����÷��� ����--------------------------------

        //��� �غ� Ȯ��
        MainGame_OtherReady,
        //���� 2�� ��ο�
        //MainGame_OtherPull,
        //��� ���������
        MainGame_OtherWait,
        MainGame_OtherIntegration,
        MainGame_OtherDump,
        MainGame_OtherRecover,
        MainGame_OtherUtilize,
        MainGame_OtherEnd,
        //�ϼ� �� ����, End�� �Ѿ��.
        MainGame_OtherCompletion,
        MainGame_OtherAttackStep,

        //�׺�
        MainGame_OtherSurrender,

        //����� ��縦 ���� �ܰ�
        MainGame_OtherShow,
        MainGame_OtherDialogue,


        //�ڽ��÷��� ����--------------------------------

        //�ڽ� �غ� ȣ����
        MainGame_MyReady,
        //���� 2�� ��ο�
        //MainGame_MyPull,
        //�ڽ� ���������
        MainGame_MyWait,
        MainGame_MyIntegration,
        MainGame_MyDump,
        MainGame_MyRecover,
        MainGame_MyUtilize,
        MainGame_MyEnd,
        //�ϼ� �� ����, End�� �Ѿ��.
        MainGame_MyCompletion,
        MainGame_MyAttackStep,

        //�׺�
        MainGame_MySurrender,

        //����� ��縦 ���� �ܰ�
        MainGame_MyShow,
        MainGame_MyDialogue,

        //���� ���� �ܰ�--------------------------------
        EndGame_Show,
        EndGame_Dialogue,
        EndGame_Result,
        EndGame_Exit,

    }

    //custom game�� ������� ������ �÷���
    //noraml game�� 
    public enum GameType
    {
        storyCustomGame,
        storyNormalGame,
        
        versusNormalGame,
    }

    public enum MyControlType
    {
        none,
        dump,
        integration,
        recover,
        recoverDumpSelect,
        completion,
        useArcane,
    }

    public enum StoneForm
    {
        normalRevealed,
        normalConcealed,

        completionStair,
        completionSame,
    }

    public enum StoneType
    {
        arcane,
        element,
        optic,
        mineral,
    }

    //�� ������ ���� �������ε� ����ϴ� ��ġ
    public enum StoneDetailType
    {
        //�ϼ��Ȱ� ������
        completion,

        //165~
        arcane,

        //0~14
        element_fire,
        //15~29
        element_land,
        //30~44
        element_ocean,
        //45~59
        element_forest,
        //60~74
        element_purple,

        //75~89
        optic_light,
        //90~104
        optic_dark,
        //105~119
        optic_chaos,

        //120~134
        mineral_gold,
        //135~149
        mineral_silver,
        //150~164
        mineral_blood,

        //����Ʈ�� ������
        last,

    }

    public enum ArcaneStoneType
    {
        none,

        //�ϴ� �⺻������ 5�� �̻� �ֵ��� �Ѵ�.
        lightningStone,
        reverseStone,
        gravityStone,
        induceStone,
        resonanceStone,
        rippleStone,
        burstStone,

        //�ƹ��� ȿ�� ���� ������
        //�̰� ������ �������� �ȴ�.
        blank,
    }

    //���� ������ List�� ���� �̸�
    public enum MultiplierReferenceName
    {
        multiplier_1111,
        multiplier_a111,

        multiplier_211,
        multiplier_2a1,
        multiplier_A11,

        multiplier_31,
        multiplier_3a,

        multiplier_22,
        multiplier_A2,

        multiplier_4,
    }

    #endregion

    //�ٽ� ���� ������, �پ��� ���� ��ġ-------------------------------------------------------------------------------------------
    [System.Serializable]
    public class MainGameBaseInfo
    {
        //�뷫���� �ܰ�
        public NowGameStepEnum nowGameStepEnum;

        //�������� �������� �ܰ�
        public NowGameStepDetailEnum nowGameStepDetailEnum;

        //���� ������ ����
        public GameType gameType;


        [Space(15)]

        //���� �⺻���� ���� ����
        //�ź��� �����Ѵ�(�⺻������ 165���� �����ȴ�)
        public int baseStoneCount;

        //�ź��� ����(�⺻������ 1->5�� �ȴ�)
        //isArcane�� �翬�� Ȱ��ȭ
        public int arcaneStoneCount;

        //���߿� isArcane�� �رݵȴ�.
        //�� ������ blank �ϳ��� ����.
        public bool isArcane;


        [Space(15)]

        //�ΰ��� story������ ����Ѵ�.
        //�������������� �������� �޾ƿ´�.
        public List<int> PiledStonesForStoryList;

        //story���� �ź� List�� �����.
        public List<int> PiledArcaneStonesRefForStoryList;

        [Space(15)]

        //������ �� ���� ����
        //�⺻������ 6���� �����Ѵ�.
        //�� ���Ŀ��� �ı��ȴ�.
        public int savedDumpedCounts;

        //���� ������ ����
        public List<StoneInfo> DumpedStonesList;

        [Space(15)]
        //���� �ı��� ����
        public List<StoneInfo> DestroyedStonesList;



        [Space(15)]
        [Header("���� ���� �Ϲ� ����")] [Space(5)]
        //���� �÷��̾�
        [ReadOnly] public int firstPlayerNum;
        [ReadOnly] public bool firstPlayerFailedBefore;

        [Space(5)]

        //���� ���
        public int scoreRatio;

        [Space(5)]
        //���� �÷��� ���� ���� ��ȣ
        public int playedTurnThisRound;
        public int nowPlayingPlayer;
        public int nextPlayingPlayer;

        [Space(5)]
        //���� �� ��
        public int nowRounds;

        //��ǥ �� ��
        public int targetRounds;

        //���� ���� �� �� ��
        public int allPlayedTurnCounts;

        [Space(10)]
        public MyControlType nowMyControlType;


        [Space(5)]
        //�ֱ� �ռ��� ������ ����
        //-1�� �ƹ��� �� �� ���� ��
        //�� �ܿ��� 0,1,2,3
        public int latestTurnUser = -1;
        public int latestCompletedUser;

        [Space(5)]
        [Header("�⺸�� ���� ���ǥ")]
        [Space(5)]
        //���� ������ List
        public List<int> CommonMultiplierReferenceList = new List<int> { 2, 4, 3, 6, 12, 5, 10, 5, 20, 20 };
        
        
        #region entrance flow
        [Space(10)]
        [Header("Entrance �帧 ����")] [Space(5)]
        public bool entranceReadyPass;
        public bool entranceShowPass;
        public bool entrancedialoguePass;
        public bool entranceWaitPass;

        [Space(5)]
        [Header("���� �н�")] [Space(5)]
        public bool entranceAllPass;
        #endregion

        #region maingame initial
        [Space(10)]
        [Header("MainGame Initial �帧 ����")] [Space(5)]

        //bool�� �ƴ� int�� �ٲ��.
        public int initialSelectionFinishedPass;
        [Space(5)]
        public List<bool> initialSelectionFinishedList;

        // -1�� ���� ���� �� �� ����
        // 0�� ������
        // 1�� ���� ���� �� �� �̵�
        public judgeType judgeBonus = judgeType.none;
        public enum judgeType
        {
            none,
            benefit,
            penalty,
        }


        #endregion


        #region maingame neutral
        [Space(10)]
        [Header("MainGame Initial �帧 ����")]
        [Space(5)]
        public List<bool> neutralNetworkCheckingList;
        #endregion

        //#region maingame my avail functions


        //���� ���� ���� �� ����, ��ǥ ó���� �ʱ�ȭ
        public void ResetGameBaseInfo(int tempTargetRounds)
        {
            //��ǥ�� ���� ��
            targetRounds = tempTargetRounds;

            nowRounds = 0;

            
            playedTurnThisRound = 0;
            allPlayedTurnCounts = 0;
        }

        public void ResetRoundBaseInfo()
        {
            nowRounds++;
            playedTurnThisRound = 0;
        }

    }
    //----------------------------------------------------------------------------------------------------------------------------

    //���տ� class
    // -2�� ������, -1�� �ϼ�, 0~4�� �ش� ���� ������

    [System.Serializable]
    public class CompletionTypeElement
    {
        [ReadOnly] public List<int> completionSameList;

        [ReadOnly] public List<int> completionStairList;

        [ReadOnly] public int completionStairLarge;


        //�ʱ�ȭ
        public void CompletionTypeElementReset()
        {
            completionSameList = new List<int>() { -2, -2, -2, -2, -2 };

            completionStairList = new List<int>() { -2, -2, -2 };

            completionStairLarge = -2;
        }
    }


    [System.Serializable]
    public class CompletionTypeOpticMineral
    {
        [ReadOnly] public List<int> completionSameList;

        [ReadOnly] public int completionStair;

        [ReadOnly] public List<int> completionSameLargeList;


        //�ʱ�ȭ
        public void CompletionTypeOpticMineralReset()
        {
            completionSameList = new List<int>() { -2, -2, -2};

            completionStair = -2;

            completionSameLargeList = new List<int>() { -2, -2, -2 };
        }

    }

    [System.Serializable]
    public class CompletionTypeArcane
    {
        [ReadOnly] public int completionSame;

        [ReadOnly] public int completionSameLarge;

        public void CompletionTypeArcaneReset()
        {
            completionSame = -2;

            completionSameLarge = -2;
        }
    }

    [System.Serializable]
    public class CompletedStonesInfo
    {
        //�ڽ� �ռ� Ƚ��
        [ReadOnly] public int integratedStoneCount;

        //�ڽ� ���� Ƚ��
        [ReadOnly] public int recoveredStoneCount;


        [Space(5)]

        //���� ������ �ִ� ����
        //����� �ռ�, ������ ���� ������ �߻����� �ʴ´�.
        //�� ������ �̿��� �⺸���� ���� max���� �����´�.
        [ReadOnly] public int maxAvailScore;

        [Space(5)]
        //���� ������ �ߺ��ȴٸ� sub reference�� ����ִ´�.
        [ReadOnly] public int resultReference;

        [Space(5)]
        //�ߺ��� ���� ��� subreference�� ����ִ´�.
        [ReadOnly] public List<int> subReference;

        [Space(5)]
        //���� ����� reference
        public List<availableGemListInfo> AvailableGemList;

    }

    [System.Serializable]
    public class availableGemListInfo
    {
        //���� 4���� Gem�� ���ؼ� ����

        [ReadOnly] public List<Gem> availableGems;

        [Space(5)]
        [ReadOnly] public int calculatedScore;

        //����� ����
        [ReadOnly] public int resultMultiplier;

        //��� stair���� üũ
        [ReadOnly] public bool isAllStair;
        [ReadOnly] public bool isAllSame;

        [Space(5)]
        //���߿� ���� ������ �ؾ��ϴϱ� �Լ��� ���� ������ �ʴ´�.
        //���߿� �Լ��� ���� �����ϵ��� �����.
        [ReadOnly] public int[] TempMultiplierReferenceList = new int[10] { 2, 4, 3, 6, 12, 5, 10, 5, 20, 20};

        [Space(5)]
        //��Ÿ �پ��� ����
        [ReadOnly] public int baseGemScore = 2;

        //���ʽ� ����
        //���߿� ���� �����ϴ�.
        [ReadOnly] public int sameGemBonusScore = 1;
        [ReadOnly] public int stairGemBonusScore = 0;

        [Space(5)]
        //���� ���� ���ʽ� ����
        //2��,3��,4�� �� ������ ������ �ο�
        [ReadOnly] public List<int> elementIterationBonusScore = new List<int>{ 1, 2, 4 };

        [Space(5)]
        //���� �� ����Ϸ��� ����
        [ReadOnly] public StoneDetailType mainStoneDetailType;


        //multiplier �� ����
        //AllStair �� AllSame �� ����
        //ElementIteration
        public void CalculateScoreInfo()
        {
            // 1 �ź�
            // 2~6 ���Ҽ�
            // 7~9 ���ϼ�
            // 10~12 ö����
            //StoneDetailType
            
            //12��
            List<int> tempList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


            // ElementIteration ������
            // 9���� completion ����
            // ���߿� 2�� 3�� 4���� Ȯ���Ѵ�.
            List<int> tempElementIteration = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };



            //3���� �����ϳ�
            bool is3Iterated = false;
            bool is4Iterated = false;

            isAllStair = true;
            isAllSame = true;

            //Gem ������ �����ؼ� stair same ����, ��ø������ üũ�Ѵ�.
            //tempElementIteration �� ä���.
            for (int i=0; i< availableGems.Count; i++)
            {
                if(availableGems[i].isSame == 1)
                {
                    isAllStair = false;
                }
                //��� �Ǵ� �ź�
                else
                {
                    isAllSame = false;
                }

                int tempRef = ((int)(availableGems[i].detailType)) - 1;

                if (availableGems[i].isLarge)
                {
                    tempList[tempRef] += 2;
                }
                else
                {
                    tempList[tempRef] += 1;
                }

                //element Ÿ���̸�
                if(availableGems[i].elementSubType != -1)
                {
                    //�ϳ��� ����
                    tempElementIteration[availableGems[i].elementSubType]++;
                }

            }

            //���� ��ü������ 1���̸� �׳� ��� false ó���Ѵ�.
            if (availableGems.Count < 2)
            {
                isAllStair = false;
                isAllSame = false;
            }


            //3��ø, 4��ø�� ��츦 Ȯ���Ѵ�.
            for (int i = 0; i < tempList.Count; i++)
            {
                if (tempList[i] == 3)
                {
                    is3Iterated = true;
                }
                else if (tempList[i] == 4)
                {
                    is4Iterated = true;
                }
            }


            //�ź� ���� ���
            if (tempList[0] == 0)
            {
                if(availableGems.Count == 4)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_1111];
                }
                else if (availableGems.Count == 3)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_211];
                }
                else if (availableGems.Count == 2)
                {
                    //3���� ������ multiplier_31
                    if (is3Iterated)
                    {
                        resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_31];
                    }
                    //������ multiplier_22
                    else
                    {
                        resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_22];
                    }
                }
                else if(availableGems.Count == 1)
                {
                    //��� �ǹ̴� ���µ� Ȥ�ó� �ؼ� �ִ´�.
                    if (is4Iterated)
                    {
                        resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_4];
                    }
                    else
                    {
                        resultMultiplier = 1;
                    }
                }
                else
                {
                    resultMultiplier = 1;
                }
            }
            //�ź� 1��
            else if(tempList[0] == 1)
            {
                if (availableGems.Count == 4)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_a111];
                }
                else if (availableGems.Count == 3)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_2a1];
                }
                else if(availableGems.Count == 2)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_3a];
                }
            }
            //�ź� 2��
            else if (tempList[0] == 2)
            {
                if (availableGems.Count == 3)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_A11];
                }
                else if(availableGems.Count == 2)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_A2];
                }
                else
                {
                    resultMultiplier = 1;
                }
            }
            else
            {
                //���� ���
                resultMultiplier = 1;
            }


            //-------------------------------------------------------------------

            //element bonus ����
            // 1�� �ƹ� �ǹ� ����. 2,3,4�� ���� bonus ������ ó���Ѵ�.
            for (int i=0; i< tempElementIteration.Count; i++)
            {
                //2�� ��ø
                if (tempElementIteration[i] == 2)
                {
                    CheackAndChangeBonusScore(i, elementIterationBonusScore[0]);
                }
                //3�� ��ø
                else if (tempElementIteration[i] == 3)
                {
                    CheackAndChangeBonusScore(i, elementIterationBonusScore[1]);
                }
                //4�� ��ø
                else if (tempElementIteration[i] == 4)
                {
                    CheackAndChangeBonusScore(i, elementIterationBonusScore[2]);
                }
            }

        }

        //�Լ� ���� ���� ������ �и��Ѵ�.
        public void CheackAndChangeBonusScore(int tagetElementSubType, int targetScore)
        {
            for (int i = 0; i < availableGems.Count; i++)
            {
                if(availableGems[i].elementSubType == tagetElementSubType)
                {
                    availableGems[i].elementBonusScore = targetScore;
                }
            }
        }
    }


    #region Script Stucts

    [System.Serializable]
    public class Gem
    {
        //false�� ����, true�� ����
        public bool isLarge;

        // 1~12
        public StoneDetailType detailType;

        // same = 1, stair = 0, arcane = -1;
        public int isSame;

        // sub type
        // element ���ʽ� ������ ����
        // -1�� �� ���� ���, 0~8�� element completion ����
        public int elementSubType = -1;

        // ������ ���� �߰� ���� �ο�
        public int elementBonusScore = 0;

    }

    [System.Serializable]
    public class AvailableStoneList
    {

    }

    [System.Serializable]
    public struct EntranceFlowControl
    {

    }

    [System.Serializable]
    public struct SelectionUtility
    {

    }

    [System.Serializable]
    public class StoneInfo
    {
        //���� ����
        [ReadOnly] public StoneForm stoneForm;
        //���� �ϼ��� ���� ��쿡�� ������ �ο��Ѵ�.
        [ReadOnly] public int completedSeq;

        //4��
        [ReadOnly] public StoneType stoneType;

        //��������
        [ReadOnly] public StoneDetailType stoneDetailType;

        //0~4 or 0~2
        [ReadOnly] public int stoneNumber;

        //�̰� ���� �߰��� ���ɼ��� ����.
        [ReadOnly] public ArcaneStoneType arcaneStoneType;

        //Ž���� ��ǥ
        //�ϼ��� �� 0~9
        //�ź� 10~19
        //�Ϲ� 100 + (stone detail+1) x 10 + stone number
        [ReadOnly] public int stoneArrangeReference;

        //�׳� ���ݱ��� ���� ����
        [ReadOnly] public int stoneAccumulatedReference;

        //������
        public StoneInfo(StoneDetailType tempStoneDetailTypeEnum, int tempStoneNumber, int subReference, int accReference)
        {
            //�ϴ� ���� �������� ����
            stoneDetailType = tempStoneDetailTypeEnum;

            //���� �������� ������ ���� �뷫 �������� ������ �����Ѵ�.
            //completion�� ���
            if (tempStoneDetailTypeEnum == StoneDetailType.completion)
            {
                //�ϴ� ��� ����
            }
            //�ź�
            else if (tempStoneDetailTypeEnum == StoneDetailType.arcane)
            {
                stoneType = StoneType.arcane;
                stoneNumber = -2;
                arcaneStoneType = (ArcaneStoneType)tempStoneNumber;
                stoneArrangeReference = 10 + subReference;
            }
            //���Ҽ�
            else if ((int)tempStoneDetailTypeEnum < (int)StoneDetailType.optic_light)
            {
                stoneType = StoneType.element;
                stoneNumber = tempStoneNumber % 5;
                arcaneStoneType = ArcaneStoneType.none;
                stoneArrangeReference = 100 + (((int)stoneDetailType - (int)(StoneDetailType.element_fire)) * 10) + stoneNumber;
            }
            //���ϼ�
            else if ((int)tempStoneDetailTypeEnum < (int)StoneDetailType.mineral_gold)
            {
                stoneType = StoneType.optic;
                stoneNumber = tempStoneNumber % 3;
                arcaneStoneType = ArcaneStoneType.none;
                stoneArrangeReference = 200 + (((int)stoneDetailType - (int)(StoneDetailType.optic_light)) * 10) + stoneNumber;
            }
            //������
            else if ((int)tempStoneDetailTypeEnum < (int)StoneDetailType.last)
            {
                stoneType = StoneType.mineral;
                stoneNumber = tempStoneNumber % 3;
                arcaneStoneType = ArcaneStoneType.none;
                stoneArrangeReference = 300 + (((int)stoneDetailType - (int)(StoneDetailType.mineral_gold)) * 10) + stoneNumber;
            }

            //���� ���� ���
            stoneAccumulatedReference = accReference;
        }

    }

    [System.Serializable]
    public struct RecentHistory
    {
        //��� ����
        public int rank;

        //���(1~4)
        public int grade;

        //�ְ� ����
        public int maxDamage;
    }

    #endregion
}

