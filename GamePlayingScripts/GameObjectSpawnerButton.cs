using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


[CustomEditor(typeof(GameObjectSpawner))]
[System.Serializable]
public class GameObjectSpawnerButton : Editor
{
    //��ư �Լ�
    public override void OnInspectorGUI()
    {
        //���� Ÿ�� ����
        GameObjectSpawner gameTrigger = (GameObjectSpawner)target;

        EditorGUILayout.BeginHorizontal();
        //���⼭���� UI
        if (GUILayout.Button("stone ����", GUILayout.Width(200), GUILayout.Height(20)))
        {
            //�������� ��� ����
            //gameTrigger.InitailizeGameSettings();

            //gameTrigger.StoneCreationFunction();

        }

        if (GUILayout.Button("stone ����", GUILayout.Width(200), GUILayout.Height(20)))
        {
            //�������� ��� ����
            //gameTrigger.MoveToTargetStep(GameController_Normal.NowGameStepEnum.MainGame_Initialize, GameController_Normal.NowGameStepDetailEnum.MainGame_initialFirstSetting);
            //gameTrigger.StoneAppearFunction();
        }

        GUILayout.FlexibleSpace();  // ������ ������ �ֽ��ϴ�.
        EditorGUILayout.EndHorizontal();  // ���� ���� ��

        //--------------------------------------------------------------------------------------------------------

        //GUILayout.Space(5);
        //EditorGUILayout.BeginHorizontal();

        //if (GUILayout.Button("��� ���� �ϳ��� �̱�", GUILayout.Width(200), GUILayout.Height(20)))
        //{
        //    //�������� ��� ����
        //    /*
        //    gameTrigger.PullOneStone(0);
        //    gameTrigger.PullOneStone(1);
        //    gameTrigger.PullOneStone(2);
        //    gameTrigger.PullOneStone(3);
        //    */
        //}

        //if (GUILayout.Button("���� 2���� �̾� �켱�� ����", GUILayout.Width(200), GUILayout.Height(20)))
        //{
        //    //�������� ��� ����
        //    //gameTrigger.RandomFirstSelect();
        //}

        //GUILayout.FlexibleSpace();  // ������ ������ �ֽ��ϴ�.
        //EditorGUILayout.EndHorizontal();  // ���� ���� ��

        //--------------------------------------------------------------------------------------------------------
        base.OnInspectorGUI();


    }
}
