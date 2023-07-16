using System.Collections;
using UnityEngine;

//다양한 부가 기능성 함수에 대한 정리
//앞으로 버튼들도 여기에 만든다.
public partial class GameController_Normal : MonoBehaviour
{
    public void MoveToTargetStep(NowGameStepEnum tempStepEnum, NowGameStepDetailEnum tempStepDetailEnum)
    {
        mainGameBaseInfo.nowGameStepEnum = tempStepEnum;
        mainGameBaseInfo.nowGameStepDetailEnum = tempStepDetailEnum;
    }

    //테스트용 버튼 함수
    //first player 선택용
    public void RandomFirstSelect()
    {
        for (int i = 0; i < 2; i++)
        {
            //아래는 이제 쓰지 않는 코드들이다.

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

    //현재 버튼으로서 참조 중이다.
    public void SelectNowPlaneNum()
    {
        myPlayerUI.returnUI.SetActive(false);

        //혹시라도 미선택이 있으면 무효화한다.
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

    //버리기 버튼 기능 총괄
    #region Dump Utility

    public void DumpButtonUtilize()
    {

        myPlayerUI.UIAll.SetActive(false);
        myPlayerUI.returnUI.SetActive(true);

        StartCoroutine(DumpUtilizeCor());
    }

    IEnumerator DumpUtilizeCor()
    {
        
        //slider바 활성화
        SliderHandlerOn(-1, false);
        //darker 활성화
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.darker;

        //보석 끄기
        CrystalAllControl(0,0);

        yield return null;
        mainGameBaseInfo.nowMyControlType = MyControlType.dump;

        //제단 활성화
        externalGameObjects.SelectionNormalAll.SetActive(true);

        externalGameObjects.SelectionControllerList[0].ColorSetting(1);
        externalGameObjects.SelectionControllerList[0].gameObject.SetActive(true);
        externalGameObjects.SelectionControllerList[0].ResetPlane();
        externalGameObjects.SelectionControllerList[0].planeAltarActive = true;

        externalGameObjects.SelectionControllerList[1].ColorSetting(1);
        externalGameObjects.SelectionControllerList[1].gameObject.SetActive(true);
        externalGameObjects.SelectionControllerList[1].ResetPlane();
        externalGameObjects.SelectionControllerList[1].planeAltarActive = true;

        //되돌리기 버튼 활성화

        yield break;
    }

    //버리기 완료 버튼
    public void DumpNowPlaneNum()
    {
        myPlayerUI.returnUI.SetActive(false);

        //혹시라도 미선택이 있으면 무효화한다.
        if (externalGameObjects.SelectionControllerList[0].nowAppliedStoneRefNum == -1 || externalGameObjects.SelectionControllerList[1].nowAppliedStoneRefNum == -1)
        {
            return;
        }

        for (int i = 0; i < 2; i++)
        {

            //선택하기
            myPlayerGameInfo.SelectStonesRef.Add(externalGameObjects.SelectionControllerList[i].nowAppliedStoneRefNum);

        }

        externalGameObjects.dumpExecutionButton.SetActive(false);

        mainGameBaseInfo.nowMyControlType = MyControlType.none;

 
        //버리는 단계를 수행
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_MyDump;

        StartCoroutine(DumpExecutionCor());
    }

    IEnumerator DumpExecutionCor()
    {
        //잠시 모든 돌 이동 불가
        externalGameObjects.gameObjectSpawner.StoneMovableAllFunction(false);

        //모든 버튼 UI 비활성화
        myPlayerUI.UIAll.SetActive(false);

        //녹는 이펙트를 발동시킨다.
        externalGameObjects.SelectionControllerList[0].StartDumpDissolve();
        externalGameObjects.SelectionControllerList[1].StartDumpDissolve();

        yield return new WaitForSeconds(1.4f);

        //현재 생성된 dump area를 2칸 민다.
        //6개를 초과하면 땅 밑으로 떨어진다. (destroy)
        externalGameObjects.dumpController.StartCreate2AndDestroy2();

        //밀리는 동시에 내 돌도 그 곳으로 던진다.
        //select를 활성화 하고 버리기도 한다.

        for (int i = 0; i < 2; i++)
        {
            //Hand의 Stone의 활성화
            myCommonObjects.StoneOnHandBriefsPair[i].gameObject.SetActive(true);
            myCommonObjects.StoneOnHandBriefsPair[i].ActiveShowObjects(-1);
            myCommonObjects.StoneOnHandBriefsPair[i].ImmediateApplyObjects((int)myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[i]].stoneDetailType, myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[i]].stoneNumber);
            myCommonObjects.StoneOnHandBriefsPair[i].SetAbandonTargetTransform(externalGameObjects.dumpController.TargetGameObjectList[i].transform, 1);

            //기존 것은 끄기
            externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[myPlayerGameInfo.SelectStonesRef[i]].gameObject.SetActive(false);

        }

        yield return null;

        //2개의 정보도 버린다.
        DumpSelectedStone(0);

        yield return null;


        //버려지는 연출이 끝날 때 즈음 원래로 되돌리기
        //보석 원래로 되돌리기
        CrystalAllControl(1, 0);

        //불 다시 키기
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.none;

        //최종 단계로 넘긴다.
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_MyEnd;

        yield break;

    }

    #endregion

    //복원 기능 총괄
    #region Recover Utility

    public void RecoverButtonUtilize()
    {
        myPlayerUI.UIAll.SetActive(false);
        myPlayerUI.returnUI.SetActive(true);

        //StartCoroutine(RecoverUtilizeCor());

        SliderHandlerOn(-1, false);
        //darker 활성화
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.darker;

        //보석 끄기
        CrystalAllControl(0, 0);


        mainGameBaseInfo.nowMyControlType = MyControlType.recoverDumpSelect;

        //index개수만큼 발동
        //dump중 사용 가능한 것을 avail시킨다.
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

    //실질적인 Integration으로 넘어감
    public void RecoverToIntegration()
    {
        //일단 돌들 모두 비활성화
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

        //small plane 2번에 세팅
        externalGameObjects.SelectionSmallControllerList[2].nowPlaneStatus = PlaneAltarController.PlaneStatusEnum.selectionReady;

        externalGameObjects.SelectionSmallControllerList[2].EffectPlaneNone();

    }

    /*
    IEnumerator RecoverUtilizeCor()
    {
        //slider바 활성화
        SliderHandlerOn(-1, false);
        //darker 활성화
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.darker;

        //보석 끄기
        CrystalAllControl(0, 0);

        yield return null;

        mainGameBaseInfo.nowMyControlType = MyControlType.recover;

        //small selectio 활성화
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

    //합성 기능 총괄
    #region Integration Utility

    public void IntegrationButtonUtilize()
    {
        myPlayerUI.UIAll.SetActive(false);
        myPlayerUI.returnUI.SetActive(true);

        //StartCoroutine(IntegrationUtilizeCor());

        //slider바 활성화
        SliderHandlerOn(-1, false);
        //darker 활성화
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.darker;

        //보석 끄기
        CrystalAllControl(0, 0);

        mainGameBaseInfo.nowMyControlType = MyControlType.integration;

        //small selectio 활성화
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
        //slider바 활성화
        SliderHandlerOn(-1, false);
        //darker 활성화
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.darker;

        //보석 끄기
        CrystalAllControl(0, 0);

        yield return null;
        mainGameBaseInfo.nowMyControlType = MyControlType.integration;

        //small selectio 활성화
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

    //되돌리기 기능 총괄
    #region Recover Utility

    public void ReturnButtonUtilize()
    {
        myPlayerUI.UIAll.SetActive(true);
        myPlayerUI.returnUI.SetActive(false);

        //StartCoroutine(ReturnUtilizeCor());

        //slider바 활성화
        SliderHandlerOn(-1, false);
        //darker 활성화
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.none;

        //보석 되돌리기
        CrystalAllControl(1, 0);


        if (mainGameBaseInfo.nowMyControlType == MyControlType.dump)
        {
            //일단 내부 값 다 초기화 해둔다.
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

                //돌 원래 위치로 되돌림
                if (mainGameBaseInfo.nowMyControlType == MyControlType.recover)
                {
                    //Debug.Log("되돌리기 중");
                    //Debug.Log(myPlayerGameInfoControl.tempSelectedDumpNum);
                    externalGameObjects.dumpController.TargetGameObjectList[myPlayerGameInfoControl.tempSelectedDumpNum].GetComponent<StoneOnHandBrief>().recoverReturnTimer = 0f;
                    externalGameObjects.dumpController.TargetGameObjectList[myPlayerGameInfoControl.tempSelectedDumpNum].GetComponent<StoneOnHandBrief>().nowStoneStatus = StoneOnHandBrief.StoneStatus.returnRecover;
                    //Debug.Log(externalGameObjects.dumpController.TargetGameObjectList[myPlayerGameInfoControl.tempSelectedDumpNum].GetComponent<StoneOnHandBrief>().nowStoneStatus);
                }

            }

        }

        mainGameBaseInfo.nowMyControlType = MyControlType.none;

        //대형은 일단 나중에...
        /*
        for (int i = 0; i < 5; i++)
        {
            externalGameObjects.SelectionLargeControllerList[i].ResetPlane();
        }
        */

        //제단 비활성화
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
        //slider바 활성화
        SliderHandlerOn(-1, false);
        //darker 활성화
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.none;

        //보석 되돌리기
        CrystalAllControl(1, 0);

        yield return null;

        if(mainGameBaseInfo.nowMyControlType == MyControlType.dump)
        {
            //일단 내부 값 다 초기화 해둔다.
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

        //대형은 일단 나중에...
        
        //for (int i = 0; i < 5; i++)
        //{
        //    externalGameObjects.SelectionLargeControllerList[i].ResetPlane();
        //}

        //제단 비활성화
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

    //현재 내가 완성되었는지 연산한다.
    void MyManufactureCompletion()
    {

    }

    public void ReturnToWait()
    {

    }

}
