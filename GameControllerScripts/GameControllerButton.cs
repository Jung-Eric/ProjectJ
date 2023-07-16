using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


[CustomEditor(typeof(GameController_Normal))]
[System.Serializable]
public class GameControllerButton : Editor
{
    //��ư �Լ�
    public override void OnInspectorGUI()
    {
        //���� Ÿ�� ����
        GameController_Normal gameTrigger = (GameController_Normal)target;

        EditorGUILayout.BeginHorizontal();
        //���⼭���� UI
        if (GUILayout.Button("���� ���� �ʱ�ȭ", GUILayout.Width(200), GUILayout.Height(20)))
        {
            //�������� ��� ����
            gameTrigger.InitailizeGameSettings();

        }

        if (GUILayout.Button("Initial First Selection�̵�_���ӽõ�", GUILayout.Width(200), GUILayout.Height(20)))
        {
            //�������� ��� ����
            gameTrigger.MoveToTargetStep(GameController_Normal.NowGameStepEnum.MainGame_Initialize, GameController_Normal.NowGameStepDetailEnum.MainGame_InitialFirstSetting);

        }

        GUILayout.FlexibleSpace();  // ������ ������ �ֽ��ϴ�.
        EditorGUILayout.EndHorizontal();  // ���� ���� ��

        //--------------------------------------------------------------------------------------------------------
        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("��� ���� �ϳ��� �̱�", GUILayout.Width(200), GUILayout.Height(20)))
        {
            //�������� ��� ����
            /*
            gameTrigger.PullOneStone(0);
            gameTrigger.PullOneStone(1);
            gameTrigger.PullOneStone(2);
            gameTrigger.PullOneStone(3);
            */
        }

        if (GUILayout.Button("�ٸ� ����� 2���� �̾� �켱�� ����", GUILayout.Width(200), GUILayout.Height(20)))
        {
            //�������� ��� ����
            gameTrigger.RandomFirstSelect();
        }

        GUILayout.FlexibleSpace();  // ������ ������ �ֽ��ϴ�.
        EditorGUILayout.EndHorizontal();  // ���� ���� ��

        //--------------------------------------------------------------------------------------------------------
        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("��� ���� �ϳ��� �̱�", GUILayout.Width(200), GUILayout.Height(20)))
        {
            //�������� ��� ����
            /*
            gameTrigger.PullOneStone(0);
            gameTrigger.PullOneStone(1);
            gameTrigger.PullOneStone(2);
            gameTrigger.PullOneStone(3);
            */
        }


        //--------------------------------------------------------------------------------------------------------
        GUILayout.FlexibleSpace();  // ������ ������ �ֽ��ϴ�.
        EditorGUILayout.EndHorizontal();  // ���� ���� ��
        base.OnInspectorGUI();

        
    }
}
