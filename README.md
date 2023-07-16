<img src="https://img.shields.io/badge/Unity-FFFFFF?style=flat-square&logo=unity&logoColor=black"/> <img src="https://img.shields.io/badge/CSharp-239120?style=flat-square&logo=CSharp&logoColor=black"/> <img src="https://img.shields.io/badge/WebRTC-333333?style=flat-square&logo=WebRTC&logoColor=white"/>

![header](https://capsule-render.vercel.app/api?type=waving&color=timeGradient&text=Project%20LM&animation=twinkling&fontSize=60&fontAlignY=40&fontAlign=70&height=200)

ProjectLM(가제)은 개인적으로 개발중인 Unity Puzzle Game입니다.  
보석과 신화를 테마로 했으며 게임의 시대상은 근미래를 배경으로 하고 있습니다.  


ProjectLM(Working Title)is individually diveloping Unity Puzzle Game.  
It's themes are based on jewel and mythology and the era of the game is set in the near future.  


**현재 제 코딩 스타일 소개용 포트폴리오로도 활용하고 있습니다.**  
**현재 세부 내용은 현재 구현 중에 있습니다.**  

**This project is also using as my coding style portfolio.**  
**Detailed codes are under implementation.**  

![ProjectLM_logo_small_v3](https://github.com/Jung-Eric/ProjectLM/assets/56705742/d5324b1b-e572-4fdc-87e2-39f5a64d5047)

</br>
게임의 플레이는 크게 3가지 플레이 방식으로 구분됩니다.  
Game play is divied into 3 types.  
</br>  
  
  1. 보석을 테마로 한 퍼즐게임
     </br>
     Jewel theme puzzle game  

  2. 다양한 종류의 제조 시스템과 경영게임
     </br>
     Various type of manufacturing system and mangament game  

  3. 게임내 캐릭터들과의 대화 시뮬레이션 게임
     </br>
     Interactive visual novel  

</br>

  이 3가지의 게임은 서로 연관되어있고 서로의 플레이를 변화시킵니다.  
  계속 변화하고 상호작용하는 플레이 경험을 제공하는 것이 제 게임의 최우선 목표라고 할 수 있습니다.

</br>

  These 3 games are related and change each other's playng style.  
  The top priority of this game is providing interactive and variable game experience.  

</br>

## Project Spec

프로젝트 LM의 기본적인 프로젝트 구성 및 기본 코드들을 설명합니다.  
Explaining projectLM's base project composition and base codes.

</br>

### GameManager Class

게임의 모든 진행을 관리하는 class  
DontDestroyOnLoad와 Singleton 상속을 통해 단일로 지속적으로 유지됩니다.  
</br>
플레이어 정보 관리(불러오기, 관련 데이터 처리 등)  
게임 내 기본 진행 관리(로딩, 메뉴, 퍼즐 게임, 경영 게임, 대화 시뮬레이션 등등)  
scene 초기화, scene 이동, scene 로딩 관리(Asyncronous loading)  
게임 기본 세팅 관리(언어와 음향 등), 네트워크 상태 관리
</br>

이러한 주요 요소들을 총괄적으로 관리하고 있습니다.

![GameManager](https://github.com/Jung-Eric/ProjectLM/assets/56705742/4807975c-f094-4045-8b4e-3bfbae722f8a)

</br>
(이 class의 세부 code는 게임 내 핵심 요소들의 연관 관계 및 네트워크 연결 정보 및 key값들을 담고 있어서 공개하지 않습니다. 양해 부탁드립니다...)

</br>
</br>

### GameController_Normal Class

NormalMainGameScene(Puzzle 목적의 Game Scene) 에서 Game 플레이를 총괄하는 class.  

[GameController_Normal.cs](https://github.com/Jung-Eric/ProjectLM/blob/master/GameControllerScripts/GameController_Normal.cs)를 베이스로 기능들에 따라 여러 문서들로 나뉘어서 구현했습니다.  

</br>

[GameController_Normal.cs](https://github.com/Jung-Eric/ProjectLM/blob/master/GameControllerScripts/GameController_Normal.cs)는 Update의 GamePlay 함수를 기반으로 퍼즐 게임의 다양한 진행 단계를 수행합니다.  

[GameControllerStruct_Normal.cs](https://github.com/Jung-Eric/ProjectLM/blob/master/GameControllerScripts/GameControllerStruct_Normal.cs)는 Puzzle Game 내 진행 step과 다양한 게임적 요소들에 대한 정의를 가지고 있습니다.  

[GameControllerStruct_PlayerInfo.cs](https://github.com/Jung-Eric/ProjectLM/blob/master/GameControllerScripts/GameControllerStruct_PlayerInfo.cs) 는 유저 기본 정보에 대한 구조, 유저의 게임 내 다양한 변수(점수, 생존 여부, 퍼즐 관련 정보 등)를 가지고 있습니다.  

[GameController_Normal_Functions.cs](https://github.com/Jung-Eric/ProjectLM/blob/master/GameControllerScripts/GameController_Normal_Functions.cs) 는 기본적인 게임 구동 매커니즘과 관련한 대부분의 함수를 가지고 있습니다.  

[GameControllerUtility_Normal.cs](https://github.com/Jung-Eric/ProjectLM/blob/master/GameControllerScripts/GameControllerUtility_Normal.cs) 는 게임 구동 중 UI(Button 등)과 상호작용하는 함수에 대한 함수를 가지고 있습니다.  



</br>

위 c# script들은 모두 GameController_Normal Class의 partial class 입니다.  
또한 별도로 GameControllerButton.cs 는 GameController_Normal의 기능을 확장시키는 클래스입니다.
(Insepctor에 편의성을 위한 UI 기능들을 부여합니다)  
해당 Inspector UI에는 GameControllerUtility_Normal.cs의 함수들이 연동되어 있기도 합니다.

해당 Inspector UI를 통해 설정 초기화, step 건너뛰기, 상대 임의 플레이 등의 simulation이 가능합니다.

![GameControllerInspector_small](https://github.com/Jung-Eric/ProjectLM/assets/56705742/f1dee5b1-0a10-4db8-b74b-51f2fda3cf9d)

</br>

### GameObjectSpawner Class

게임 내 퍼즐 관련 오브젝트의 전체적인 관리를 담당하는 class 입니다.  

GameObjectSpawner.cs 를 통해 구현되어 있습니다.  

</br>

퍼즐게임 특성 상 퍼즐 오브젝트의 생성과 파괴가 잦습니다.  
하지만 해당 클래스는 오브젝트를 생성하거나 파괴하는 대신 변형(이미지 변경, 크기 변경 등)해서  
오브젝트를 생성 파괴하지 않고도 퍼즐을 진행시킬 수 있습니다.  

</br>
이 곳에도 Inspector UI를 추가해 임의의 추가 simulation이 가능합니다.

![GameObjectSpawner_small](https://github.com/Jung-Eric/ProjectLM/assets/56705742/39321442-c5f8-4981-bd55-1fc21ffff7b7)

</br>

### DumpController Class

퍼즐 관련 object의 Instantiate 및 Destroy를 담당하는 class 입니다.  

DumpController.cs 를 통해서 구현되어 있습니다.  

</br>
최소한의 일부 퍼즐 오브젝트는 생성 및 파괴가 불가피하기에 이를 관리합니다. (최대 8개)  
오브젝트를 생성, 파괴하고 개수를 관리합니다.  

</br>
</br>
### NodeBasedEditor & DialogueMaker

현재 프로젝트에서 대화 형식의 게임 (비쥬얼 노벨)은 핵심적인 역할을 맡고 있습니다.  
이를 위해 별도의 개발 툴이 필요했고 NodeBasedEditor를 직접 제작해 사용하고 있습니다.  


![NodeBasedEditor](https://github.com/Jung-Eric/ProjectLM/assets/56705742/21b37578-da61-4251-8add-864b73070a77)

![Dialogue_1](https://github.com/Jung-Eric/ProjectLM/assets/56705742/97a9abd4-697b-4a45-bf81-d28be2ad0408)  

(남성의 이미지는 테스트용 임시 이미지입니다)

![Dialogue_2](https://github.com/Jung-Eric/ProjectLM/assets/56705742/e22900f9-b7be-4549-8671-d796668e1bc6)


NodeBasedEditor에서 편집한 대로 캐릭터의 대화창이 구성되는것을 볼 수 있습니다.  
NodeBasedEditor은 Node와 Link의 형태로 대화를 생성하여 유기적인 대화를 구성할 수 있습니다.  
Node는 대화와 선택지들, Link는 이들의 유기적인 연결을 의미합니다.  
위 정보를 DialogueContainer Class 파일로 저장할 수 있습니다.(Save Data로 저장 가능)  
추후 Load Data 기능을 이용해 불러와 편집할 수 잇습니다.  

</br>

이러한 DialogueContainer 파일을 해석해 즉각적으로 게임 내 대화창을 구성할 수도 있습니다.  
DialogueCanvase에 적용해둔 DialogueMaker에 의해 대화창이 구성되고 진행됩니다.  

![DialogueMaker](https://github.com/Jung-Eric/ProjectLM/assets/56705742/97488804-6797-4d63-90b6-e2b94e491599)

</br>


