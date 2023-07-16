using System.Collections;
using UnityEngine;

//�پ��� �ΰ� ��ɼ� �Լ��� ���� ����
//������ ��ư�鵵 ���⿡ �����.
public partial class GameController_Normal : MonoBehaviour
{
    public void MoveToTargetStep(NowGameStepEnum tempStepEnum, NowGameStepDetailEnum tempStepDetailEnum)
    {
        mainGameBaseInfo.nowGameStepEnum = tempStepEnum;
        mainGameBaseInfo.nowGameStepDetailEnum = tempStepDetailEnum;
    }

    //�׽�Ʈ�� ��ư �Լ�
    //first player ���ÿ�
    public void RandomFirstSelect()
    {
        for (int i = 0; i < 2; i++)
        {
            //�Ʒ��� ���� ���� �ʴ� �ڵ���̴�.

            //StoneInfo tempStone = myPlayerGameInfo.holdingStone[0];
            //myPlayerGameInfo.SelectStonesRef.Add(i);

            //myPlayerGameInfo.holdingStone.RemoveAt(0);
        }

        for (int targetNum = 0; targetNum < 3; targetNum++)
        {
            for (int i = 0; i < 2; i++)
            {
                //StoneInfo tempStone = otherPlayerGameInfo[targetNum].holdingStone[0];
                otherPlayerGameInfo[targetNum].SelectStonesRef.Add(i);

                //otherPlayerGameInfo[targetNum].holdingStone.RemoveAt(0);
            }
        }

        mainGameBaseInfo.initialSelectionFinishedPass = 4;
    }

    //���� ��ư���μ� ���� ���̴�.
    public void SelectNowPlaneNum()
    {
        myPlayerUI.returnUI.SetActive(false);

        //Ȥ�ö� �̼����� ������ ��ȿȭ�Ѵ�.
        if (externalGameObjects.SelectionControllerList[0].nowAppliedStoneRefNum == -1 || externalGameObjects.SelectionControllerList[1].nowAppliedStoneRefNum == -1)
        {
            return;
        }

        for (int i = 0; i < 2; i++)
        {

            myPlayerGameInfo.SelectStonesRef.Add(externalGameObjects.SelectionControllerList[i].nowAppliedStoneRefNum);

        }

        RandomFirstSelect();
    }

    //������ ��ư ��� �Ѱ�
    #region Dump Utility

    public void DumpButtonUtilize()
    {

        myPlayerUI.UIAll.SetActive(false);
        myPlayerUI.returnUI.SetActive(true);

        StartCoroutine(DumpUtilizeCor());
    }

    IEnumerator DumpUtilizeCor()
    {
        
        //slider�� Ȱ��ȭ
        SliderHandlerOn(-1, false);
        //darker Ȱ��ȭ
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.darker;

        //���� ����
        CrystalAllControl(0,0);

        yield return null;
        mainGameBaseInfo.nowMyControlType = MyControlType.dump;

        //���� Ȱ��ȭ
        externalGameObjects.SelectionNormalAll.SetActive(true);

        externalGameObjects.SelectionControllerList[0].ColorSetting(1);
        externalGameObjects.SelectionControllerList[0].gameObject.SetActive(true);
        externalGameObjects.SelectionControllerList[0].ResetPlane();
        externalGameObjects.SelectionControllerList[0].planeAltarActive = true;

        externalGameObjects.SelectionControllerList[1].ColorSetting(1);
        externalGameObjects.SelectionControllerList[1].gameObject.SetActive(true);
        externalGameObjects.SelectionControllerList[1].ResetPlane();
        externalGameObjects.SelectionControllerList[1].planeAltarActive = true;

        //�ǵ����� ��ư Ȱ��ȭ

        yield break;
    }

    //������ �Ϸ� ��ư
    public void DumpNowPlaneNum()
    {
        myPlayerUI.returnUI.SetActive(false);

        //Ȥ�ö� �̼����� ������ ��ȿȭ�Ѵ�.
        if (externalGameObjects.SelectionControllerList[0].nowAppliedStoneRefNum == -1 || externalGameObjects.SelectionControllerList[1].nowAppliedStoneRefNum == -1)
        {
            return;
        }

        for (int i = 0; i < 2; i++)
        {

            //�����ϱ�
            myPlayerGameInfo.SelectStonesRef.Add(externalGameObjects.SelectionControllerList[i].nowAppliedStoneRefNum);

        }

        externalGameObjects.dumpExecutionButton.SetActive(false);

        mainGameBaseInfo.nowMyControlType = MyControlType.none;

 
        //������ �ܰ踦 ����
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_MyDump;

        StartCoroutine(DumpExecutionCor());
    }

    IEnumerator DumpExecutionCor()
    {
        //��� ��� �� �̵� �Ұ�
        externalGameObjects.gameObjectSpawner.StoneMovableAllFunction(false);

        //��� ��ư UI ��Ȱ��ȭ
        myPlayerUI.UIAll.SetActive(false);

        //��� ����Ʈ�� �ߵ���Ų��.
        externalGameObjects.SelectionControllerList[0].StartDumpDissolve();
        externalGameObjects.SelectionControllerList[1].StartDumpDissolve();

        yield return new WaitForSeconds(1.4f);

        //���� ������ dump area�� 2ĭ �δ�.
        //6���� �ʰ��ϸ� �� ������ ��������. (destroy)
        externalGameObjects.dumpController.StartCreate2AndDestroy2();

        //�и��� ���ÿ� �� ���� �� ������ ������.
        //select�� Ȱ��ȭ �ϰ� �����⵵ �Ѵ�.

        for (int i = 0; i < 2; i++)
        {
            //Hand�� Stone�� Ȱ��ȭ
            myCommonObjects.StoneOnHandBriefsPair[i].gameObject.SetActive(true);
            myCommonObjects.StoneOnHandBriefsPair[i].ActiveShowObjects(-1);
            myCommonObjects.StoneOnHandBriefsPair[i].ImmediateApplyObjects((int)myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[i]].stoneDetailType, myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[i]].stoneNumber);
            myCommonObjects.StoneOnHandBriefsPair[i].SetAbandonTargetTransform(externalGameObjects.dumpController.TargetGameObjectList[i].transform, 1);

            //���� ���� ����
            externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[myPlayerGameInfo.SelectStonesRef[i]].gameObject.SetActive(false);

        }

        yield return null;

        //2���� ������ ������.
        DumpSelectedStone(0);

        yield return null;


        //�������� ������ ���� �� ���� ������ �ǵ�����
        //���� ������ �ǵ�����
        CrystalAllControl(1, 0);

        //�� �ٽ� Ű��
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.none;

        //���� �ܰ�� �ѱ��.
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_MyEnd;

        yield break;

    }

    #endregion

    //���� ��� �Ѱ�
    #region Recover Utility

    public void RecoverButtonUtilize()
    {
        myPlayerUI.UIAll.SetActive(false);
        myPlayerUI.returnUI.SetActive(true);

        //StartCoroutine(RecoverUtilizeCor());

        SliderHandlerOn(-1, false);
        //darker Ȱ��ȭ
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.darker;

        //���� ����
        CrystalAllControl(0, 0);


        mainGameBaseInfo.nowMyControlType = MyControlType.recoverDumpSelect;

        //index������ŭ �ߵ�
        //dump�� ��� ������ ���� avail��Ų��.
        for(int i=0; i<myPlayerGameInfo.RecoverDumpIndexRefList.Count; i++)
        {
            int tempNum = (myPlayerGameInfo.RecoverDumpIndexRefList[i] % 10);

            if(tempNum % 2 == 0)
            {
                externalGameObjects.dumpController.TargetGameObjectList[4 - tempNum].GetComponent<StoneOnHandBrief>().SetRecoverSetting(true, 4 - tempNum);
                //myPlayerGameInfoControl.tempSelectedDumpNum = 4 - tempNum;
            }
            else
            {
                externalGameObjects.dumpController.TargetGameObjectList[6 - tempNum].GetComponent<StoneOnHandBrief>().SetRecoverSetting(true, 6 - tempNum);
                //myPlayerGameInfoControl.tempSelectedDumpNum = 6 - tempNum;
            }
        }
    }

    //�������� Integration���� �Ѿ
    public void RecoverToIntegration()
    {
        //�ϴ� ���� ��� ��Ȱ��ȭ
        for (int i = 0; i < myPlayerGameInfo.RecoverDumpIndexRefList.Count; i++)
        {
            int tempNum = (myPlayerGameInfo.RecoverDumpIndexRefList[i] % 10);

            if (tempNum % 2 == 0)
            {
                externalGameObjects.dumpController.TargetGameObjectList[4 - tempNum].GetComponent<StoneOnHandBrief>().nowStoneStatus = StoneOnHandBrief.StoneStatus.waiting;
            }
            else
            {
                externalGameObjects.dumpController.TargetGameObjectList[6 - tempNum].GetComponent<StoneOnHandBrief>().nowStoneStatus = StoneOnHandBrief.StoneStatus.waiting;
                //externalGameObjects.dumpController.TargetGameObjectList[6 - tempNum].GetComponent<StoneOnHandBrief>().SetRecoverSetting(true, 6 - tempNum);
            }
        }


        externalGameObjects.SelectionSmallAll.SetActive(true);

        mainGameBaseInfo.nowMyControlType = MyControlType.recover;

        for (int i = 0; i < 3; i++)
        {
            externalGameObjects.SelectionSmallControllerList[i].ColorSetting(0);
            externalGameObjects.SelectionSmallControllerList[i].gameObject.SetActive(true);
            externalGameObjects.SelectionSmallControllerList[i].ResetPlane();
            externalGameObjects.SelectionSmallControllerList[i].planeAltarActive = true;
        }

        //small plane 2���� ����
        externalGameObjects.SelectionSmallControllerList[2].nowPlaneStatus = PlaneAltarController.PlaneStatusEnum.selectionReady;

        externalGameObjects.SelectionSmallControllerList[2].EffectPlaneNone();

    }

    /*
    IEnumerator RecoverUtilizeCor()
    {
        //slider�� Ȱ��ȭ
        SliderHandlerOn(-1, false);
        //darker Ȱ��ȭ
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.darker;

        //���� ����
        CrystalAllControl(0, 0);

        yield return null;

        mainGameBaseInfo.nowMyControlType = MyControlType.recover;

        //small selectio Ȱ��ȭ
        externalGameObjects.SelectionSmallAll.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            externalGameObjects.SelectionSmallControllerList[i].ColorSetting(0);
            externalGameObjects.SelectionSmallControllerList[i].gameObject.SetActive(true);
            externalGameObjects.SelectionSmallControllerList[i].ResetPlane();
            externalGameObjects.SelectionSmallControllerList[i].planeAltarActive = true;
        }

        yield return null;
    }
    */

    #endregion

    //�ռ� ��� �Ѱ�
    #region Integration Utility

    public void IntegrationButtonUtilize()
    {
        myPlayerUI.UIAll.SetActive(false);
        myPlayerUI.returnUI.SetActive(true);

        //StartCoroutine(IntegrationUtilizeCor());

        //slider�� Ȱ��ȭ
        SliderHandlerOn(-1, false);
        //darker Ȱ��ȭ
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.darker;

        //���� ����
        CrystalAllControl(0, 0);

        mainGameBaseInfo.nowMyControlType = MyControlType.integration;

        //small selectio Ȱ��ȭ
        externalGameObjects.SelectionSmallAll.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            externalGameObjects.SelectionSmallControllerList[i].ColorSetting(0);
            externalGameObjects.SelectionSmallControllerList[i].gameObject.SetActive(true);
            externalGameObjects.SelectionSmallControllerList[i].ResetPlane();
            externalGameObjects.SelectionSmallControllerList[i].planeAltarActive = true;
        }
    }


    /*
    IEnumerator IntegrationUtilizeCor()
    {
        //slider�� Ȱ��ȭ
        SliderHandlerOn(-1, false);
        //darker Ȱ��ȭ
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.darker;

        //���� ����
        CrystalAllControl(0, 0);

        yield return null;
        mainGameBaseInfo.nowMyControlType = MyControlType.integration;

        //small selectio Ȱ��ȭ
        externalGameObjects.SelectionSmallAll.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            externalGameObjects.SelectionSmallControllerList[i].ColorSetting(0);
            externalGameObjects.SelectionSmallControllerList[i].gameObject.SetActive(true);
            externalGameObjects.SelectionSmallControllerList[i].ResetPlane();
            externalGameObjects.SelectionSmallControllerList[i].planeAltarActive = true;
        }

        yield return null;
    }
    */
    #endregion

    //�ǵ����� ��� �Ѱ�
    #region Recover Utility

    public void ReturnButtonUtilize()
    {
        myPlayerUI.UIAll.SetActive(true);
        myPlayerUI.returnUI.SetActive(false);

        //StartCoroutine(ReturnUtilizeCor());

        //slider�� Ȱ��ȭ
        SliderHandlerOn(-1, false);
        //darker Ȱ��ȭ
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.none;

        //���� �ǵ�����
        CrystalAllControl(1, 0);


        if (mainGameBaseInfo.nowMyControlType == MyControlType.dump)
        {
            //�ϴ� ���� �� �� �ʱ�ȭ �صд�.
            for (int i = 0; i < 2; i++)
            {
                externalGameObjects.SelectionControllerList[i].ResetPlane();
            }
        }
        else
        {
            if (mainGameBaseInfo.nowMyControlType == MyControlType.recoverDumpSelect || mainGameBaseInfo.nowMyControlType == MyControlType.recover)
            {
                /*
                for (int i = 0; i < 3; i++)
                {
                    externalGameObjects.SelectionSmallControllerList[i].ResetPlane();
                }
                */

                for (int i = 0; i < 6; i++)
                {
                    externalGameObjects.dumpController.TargetGameObjectList[i].GetComponent<StoneOnHandBrief>().SetRecoverSetting(false, -1);
                }

                //�� ���� ��ġ�� �ǵ���
                if (mainGameBaseInfo.nowMyControlType == MyControlType.recover)
                {
                    //Debug.Log("�ǵ����� ��");
                    //Debug.Log(myPlayerGameInfoControl.tempSelectedDumpNum);
                    externalGameObjects.dumpController.TargetGameObjectList[myPlayerGameInfoControl.tempSelectedDumpNum].GetComponent<StoneOnHandBrief>().recoverReturnTimer = 0f;
                    externalGameObjects.dumpController.TargetGameObjectList[myPlayerGameInfoControl.tempSelectedDumpNum].GetComponent<StoneOnHandBrief>().nowStoneStatus = StoneOnHandBrief.StoneStatus.returnRecover;
                    //Debug.Log(externalGameObjects.dumpController.TargetGameObjectList[myPlayerGameInfoControl.tempSelectedDumpNum].GetComponent<StoneOnHandBrief>().nowStoneStatus);
                }

            }

        }

        mainGameBaseInfo.nowMyControlType = MyControlType.none;

        //������ �ϴ� ���߿�...
        /*
        for (int i = 0; i < 5; i++)
        {
            externalGameObjects.SelectionLargeControllerList[i].ResetPlane();
        }
        */

        //���� ��Ȱ��ȭ
        externalGameObjects.SelectionNormalAll.SetActive(false);

        externalGameObjects.SelectionSmallAll.SetActive(false);
        externalGameObjects.SelectionSmallEffect.SetActive(false);

        externalGameObjects.dumpExecutionButton.SetActive(false);
        externalGameObjects.recoverExecutionButton.SetActive(false);
        externalGameObjects.integrationExecutionButton.SetActive(false);

        ResetHoldingStones();

        mainGameBaseInfo.nowMyControlType = MyControlType.none;
    }

    /*
    IEnumerator ReturnUtilizeCor()
    {
        //slider�� Ȱ��ȭ
        SliderHandlerOn(-1, false);
        //darker Ȱ��ȭ
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.none;

        //���� �ǵ�����
        CrystalAllControl(1, 0);

        yield return null;

        if(mainGameBaseInfo.nowMyControlType == MyControlType.dump)
        {
            //�ϴ� ���� �� �� �ʱ�ȭ �صд�.
            for (int i = 0; i < 2; i++)
            {
                externalGameObjects.SelectionControllerList[i].ResetPlane();
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                externalGameObjects.SelectionSmallControllerList[i].ResetPlane();
            }
        }

        mainGameBaseInfo.nowMyControlType = MyControlType.none;

        //������ �ϴ� ���߿�...
        
        //for (int i = 0; i < 5; i++)
        //{
        //    externalGameObjects.SelectionLargeControllerList[i].ResetPlane();
        //}

        //���� ��Ȱ��ȭ
        externalGameObjects.SelectionNormalAll.SetActive(false);
        externalGameObjects.SelectionSmallAll.SetActive(false);

        externalGameObjects.dumpExecutionButton.SetActive(false);
        externalGameObjects.recoverExecutionButton.SetActive(false);
        externalGameObjects.integrationExecutionButton.SetActive(false);


        yield return null;
        
        ResetHoldingStones();

        mainGameBaseInfo.nowMyControlType = MyControlType.none;
    }
    */

    public void ResetHoldingStones()
    {

        for(int i=0; i< 10; i++)
        {
            externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i].stoneNowStatus = StoneOnHand.StoneNowStatusEnum.normalDragControl;
        }
        
    }
    

    #endregion

    //���� ���� �ϼ��Ǿ����� �����Ѵ�.
    void MyManufactureCompletion()
    {

    }

    public void ReturnToWait()
    {

    }

}
