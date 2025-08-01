# 미니게임 프로젝트

태그:  
C#, SQLite, Unity, WPF

notion 링크:  
https://www.notion.so/23f86b77e264810891dbfb04143b46ae?source=copy_link

프로젝트 시작:  
2025년 5월 26일

마감 일자:  
2025년 6월 9일

인원:  
김도영, 김철수, 이승민

# 🎨디자인

---

# 로그인 UI

![image.png](Assets/image.png)

![image.png](Assets/image%201.png)

![image.png](Assets/image%202.png)

![image.png](Assets/image%203.png)

# 행성 UI

![image.png](Assets/image%204.png)

![image.png](Assets/image%205.png)

![image.png](Assets/image%206.png)

![image.png](Assets/image%207.png)

![image.png](Assets/image%208.png)

![image.png](Assets/image%209.png)

![image.png](Assets/image%2010.png)

![image.png](Assets/image%2011.png)

# Unity UI

![image.png](Assets/image%2012.png)

![image.png](Assets/image%2013.png)

# 🧠기획

---

## <시스템>

## 게임의 전반적인 흐름

몬도샤인에서 적과 싸울 때, 필요한 물품을 요구 → 제한 시간 안에 구한 물품들을 생산 및 조합하여 납품

## 제5원소(아리스토텔레스 4원소설+제5원소)

 흙, 물, 공기, 불 → 흙(Sand)의 나라, 물(Water)의 나라, 바람(Wind)의 나라, 불(Fire)의 나라

제5원소 → 4가지 원소의 조합을 통해 악을 물리칠 무언가를 만든다.

## 데이터 조회

몬도샤인 → 몬도샤인이라는 나라에 대한 정보만 조회 가능

흙, 물, 바람, 불 → 생산 가능한 목록에 대해서만 조회 가능

![image.png](Assets/image%2014.png)

## <타겟>

![image.png](Assets/image%2015.png)

## <타임라인>

![image.png](Assets/image%2016.png)

## <역할>

김도영 : Project planning , 2D Graphic design , Implement login UI and features
김철수 : Interlock between Unity and WPF , Combination implementation
**이승민 : Create game UI , Implement production, combination, and delete features**

# 🏗️전체 구조

---

- **SQLite ↔ C#(WPF) ↔ Unity**
- **DB구조**
    
    ![image.png](Assets/image%2017.png)
    

![image.png](Assets/image%2018.png)

# 📄기능 정의서

---

| 기능명 | 설명 | 우선 순위 | 담당자 |
| --- | --- | --- | --- |
| 로그인 기능 | 게임 시작 전 사용자를 확인하고 게임을 시작할 수 있게 하는 기능 | 1 | 김도영 |
| NPC의 물품 요구 기능 | 발주처에서 원하는 물품을 퀘스트 형식으로 유저에게 하달하는 기능 | 1 | 이승민 |
| 데이터 생산 기능 | 4개의 나라에서 유저가 원하는 데이터 생산 | 1 | 김도영 |
| 데이터 조합 기능 | 얻은 데이터를 조합하여 새로운 물품을 조합, 데이터 조합시 강화 이벤트를 통해 성공/실패 여부를 결정 | 1 | 김철수 |
| 데이터 삭제 기능 | 데이터 관리를 위해 필요없는 물품들은 버리기가 가능함 | 1 | 김도영 |
| 생산 진행시 그래프, 시간 기능 | 생산 요청 시 각 데이터마다 생산에 걸리는 시간을 시각적으로 보여줌 | 1 | 이승민 |
| 데이터 가속 기능 | 데이터 생산 요청 후 생산 시간을 기다리지 않고 즉시 생산할 수 있게 해주는 기능(단, 가속 시 미니게임을 통해 즉시 생산 여부를 판단) | 2 | 김철수 |
| 데이터 자동 삭제 기능 | 각 물품 마다 유지 시간(유통 기한)을 두어 일정 시간 후 데이터가 삭제되게 처리(재고 관리) | 1 | 이승민 |
| 데이터 사전(등록) | 처음에는 재료에 대한 정보를 제공하지 않다가 새로운 물질을 발견 시 도감에 자동 등록되어 물품에 대한 정보를 보여주는 기능(일반적인 게임들의 도감 기능과 유사) | 2 | 김철수 |
| 업적 달성 | 특정한 미션을 통과하였을 경우, 업적 성공을 알림과 동시에 인터페이스의 변화가 생김 | 2 | 김철수 |

# 🌿브랜치 전략

---

| 브랜치명 | 작업 영역 | 역할 | 전략 |
| --- | --- | --- | --- |
| main |  | 최종 결과물 |  |
| Develop |  | 테스트 | 각 브랜치에서 merge |
| Production | 생산 | 생산 기능 | 생산, 유통기한, 조합, 삭제, 사전 등의 기능 |
| Interface | UI | UI 및 로그인 | WPF UI, 로그인 기능 |

# 🛠️트러블 슈팅

---

## UserControl 전환 시 타이머나 데이터 상태가 초기화되고, 버튼도 가려지는 문제가 발생함

**원인:**

`new UserControl()`로 매번 ViewModel 없이 새로 생성해서 이전 상태가 초기화됨. 또한 MainGrid 설정이 없어 버튼이 사라짐

**해결:**

공유 ViewModel을 생성 후 UserControl에 전달하고, 전용 `MainGrid`를 만들어 UI와 분리함

## UI에 바인딩된 유통기한 타이머 값이, 실제 생산이 완료되기도 전에 미리 표시되는 현상발생함

**원인:**

생산 시작 시점에 유통기한을 설정 + UI 바인딩이 무조건 ExpirationTime을 표시

또한 MainGrid 설정이 없어 버튼이 사라짐

**해결:**

조건부 바인딩으로

public bool IsProduced => Quantity > 0;
<TextBlock Text="{Binding ExpirationTime}" Visibility="{Binding IsProduced, Converter={StaticResource BoolToVisibilityConverter}}" />

모델에     public bool IsProduced => Quantity > 0; 작성

Quantity가 0보다 크면 IsProduced는 true → UI에 보임

Quantity가 0이면 IsProduced는 false → UI에 안보임

## 보유재료에서 UI에 반영된 데이터가 정렬되면서 빈칸이 생김 + 배열로 바인딩되어있어 배열이 정렬되면서 아이템의 위치가 바뀜

**원인:** 
ProductionItems 리스트로 뷰, 생산라인 데이터를 같이 수행, 정렬시 라인번호에 따라 고정되어야 할 UI가 변경이 됨

**해결:**

생산 리스트를 분리
ProducedItems / ProductionItems

# 💡회고록

---

## 기획의 한계

- 공장 시뮬레이션 또는 무역 게임으로 진행된 프로젝트지만 코딩과 기획 실력이 미흡했고 애매한 프로젝트가 되었다고 생각합니다. 하지만 C# WPF로 UI, DB연동, 데이터 바인딩 등을 통해 WPF에 대해 깊게 이해하게 되는 시간이 되었습니다.
결과적으로 프로젝트의 완성도는 아쉬웠지만, 그 과정에서 C# WPF 기반의 UI 구성, 데이터 바인딩, DB 연동 등 실제 개발 기술을 다루는 데 집중할 수 있었고, C#과 MVVM 구조에 대한 실질적인 이해도는 분명히 향상되었습니다.
이번 경험을 통해 기획 단계의 중요성과 팀원 간 소통의 필요성을 절감했고, 앞으로는 현실적인 구조 설계와 개발 친화적인 기획을 목표로 삼게 되었습니다.
