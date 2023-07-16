using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


[CustomEditor(typeof(GameController_Normal))]
[System.Serializable]
public class GameControllerButton : Editor
{
    //버튼 함수
    public override void OnInspectorGUI()
    {
        //최초 타겟 선언
        GameController_Normal gameTrigger = (GameController_Normal)target;

        EditorGUILayout.BeginHorizontal();
        //여기서부터 UI
        if (GUILayout.Button("설정 완전 초기화", GUILayout.Width(200), GUILayout.Height(20)))
        {
            //본격적인 명령 수행
            gameTrigger.InitailizeGameSettings();

        }

        if (GUILayout.Button("Initial First Selection이동_게임시동", GUILayout.Width(200), GUILayout.Height(20)))
        {
            //본격적인 명령 수행
            gameTrigger.MoveToTargetStep(GameController_Normal.NowGameStepEnum.MainGame_Initialize, GameController_Normal.NowGameStepDetailEnum.MainGame_InitialFirstSetting);

        }

        GUILayout.FlexibleSpace();  // 고정된 여백을 넣습니다.
        EditorGUILayout.EndHorizontal();  // 가로 생성 끝

        //--------------------------------------------------------------------------------------------------------
        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("모든 유저 하나씩 뽑기", GUILayout.Width(200), GUILayout.Height(20)))
        {
            //본격적인 명령 수행
            /*
            gameTrigger.PullOneStone(0);
            gameTrigger.PullOneStone(1);
            gameTrigger.PullOneStone(2);
            gameTrigger.PullOneStone(3);
            */
        }

        if (GUILayout.Button("다른 사람들 2개씩 뽑아 우선권 결정", GUILayout.Width(200), GUILayout.Height(20)))
        {
            //본격적인 명령 수행
            gameTrigger.RandomFirstSelect();
        }

        GUILayout.FlexibleSpace();  // 고정된 여백을 넣습니다.
        EditorGUILayout.EndHorizontal();  // 가로 생성 끝

        //--------------------------------------------------------------------------------------------------------
        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("모든 유저 하나씩 뽑기", GUILayout.Width(200), GUILayout.Height(20)))
        {
            //본격적인 명령 수행
            /*
            gameTrigger.PullOneStone(0);
            gameTrigger.PullOneStone(1);
            gameTrigger.PullOneStone(2);
            gameTrigger.PullOneStone(3);
            */
        }


        //--------------------------------------------------------------------------------------------------------
        GUILayout.FlexibleSpace();  // 고정된 여백을 넣습니다.
        EditorGUILayout.EndHorizontal();  // 가로 생성 끝
        base.OnInspectorGUI();

        
    }
}
