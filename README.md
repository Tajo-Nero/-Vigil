# 8번출구 모작 프로젝트

## 프로젝트 소개
Unity를 활용한 "8번출구" 게임의 모작 프로젝트입니다. 무한 루프 공간에서 미묘한 변화를 감지하여 탈출하는 공포 퍼즐 게임을 구현했습니다.

## 게임 특징
- 무한 루프 지하철 통로 시스템
- 동적 이상현상 생성 시스템
- 실시간 플레이어 선택 추적
- 분위기 있는 사운드 디자인

## 기술 스택
- **Engine**: Unity 2022.3 LTS
- **Language**: C# 10.0
- **Architecture**: Component-based Architecture
- **Version Control**: Git & GitHub

<details>
<summary><b>최현민 (CHM) - 이상현상 시스템 및 사운드/애니메이션</b></summary>

### 담당 업무
- 게임 내 이상현상 구현 및 트리거 시스템 개발
- NPC AI 및 애니메이션 상태 관리
- 환경 사운드 및 인터랙션 사운드 구현
- 공포 분위기 연출을 위한 시각/청각 효과 개발

### 주요 스크립트
| 스크립트명 | 기능 설명 |
|-----------|----------|
| **이상현상 시스템** | |
| `RoomThreeTrigger.cs` | 플레이어 움직임에 따른 방 색상 변화 이상현상 구현 |
| `DoorSoundController.cs` | 플레이어 접근 시 문이 자동으로 열리는 이상현상 |
| **NPC AI 시스템** | |
| `Slender_Ctrl.cs` | 슬렌더맨 AI - 플레이어 감지 및 추적, 깜빡임 효과 |
| `CDO_Blood_Ctrl.cs` | 피 묻은 군인 AI - 플레이어 추적 및 크기 증가 효과 |
| `CDO_Crying.cs` | 우는 군인 - 주기적 울음소리 및 플레이어 감지 시 기립 |
| `CDO_Soldier.cs` | 일반 군인 - 경례, 엎드려, 차렷 등 군인 동작 구현 |

### 기술적 특징
- 코루틴을 활용한 시간 기반 이벤트 제어
- Animator Controller를 통한 복잡한 애니메이션 상태 관리
- AudioSource를 활용한 3D 공간 사운드 구현
- Physics.OverlapSphere를 이용한 효율적인 플레이어 감지
- 다양한 트리거 시스템으로 인터랙티브한 공포 요소 구현

</details>

<details>
<summary><b>이시온 (ZL) - 유틸리티 라이브러리 및 최적화</b></summary>

### 담당 업무
- 공통 유틸리티 라이브러리 개발
- 성능 최적화를 위한 확장 메서드 구현
- 디자인 패턴 인터페이스 제공
- 컬렉션 및 타입 변환 유틸리티 개발

### 주요 스크립트
| 스크립트명 | 기능 설명 |
|-----------|----------|
| **디자인 패턴** | |
| `ISingleton.cs` | 제네릭 싱글톤 패턴 인터페이스 구현 |
| **컬렉션 확장** | |
| `ArrayExtensions.cs` | 배열을 LinkedList로 변환하는 확장 메서드 |
| `LinkedListExtensions.cs` | LinkedList의 PopFirst/PopLast 기능 추가 |
| **문자열 처리** | |
| `CharExtensions.cs` | Span을 활용한 고성능 문자열 연결 |
| `StringBuilderExtensions.cs` | StringBuilder의 효율적인 Concat 메서드 제공 |
| **타입 변환** | |
| `EnumExtensions.cs` | unsafe 코드를 활용한 Enum↔int 고속 변환 |
| `EnumUnion.cs` | Enum 변환을 위한 Union 타입 구조체 |
| **수학 유틸리티** | |
| `MathEx.cs` | 각도-라디안 변환 상수 (Deg2Rad, Rad2Deg) |
| `MathFEx.cs` | float 수학 상수 및 오디오 데시벨 변환 메서드 |

### 기술적 특징
- `unsafe` 코드와 포인터를 활용한 고성능 구현
- `Span<T>`를 사용한 메모리 효율적인 문자열 처리
- 제네릭을 활용한 재사용 가능한 유틸리티
- 확장 메서드를 통한 직관적인 API 제공

</details>

<details>
<summary><b>윤종현 (YJH) - VR 인터랙션 및 튜토리얼 시스템</b></summary>

### 담당 업무
- VR 핸드 트래킹 및 제스처 인식 시스템 구현
- 튜토리얼 진행 로직 및 대화 시스템 개발
- 플레이어 이동 컨트롤러 구현
- UI/UX 매니저 시스템 구축

### 주요 스크립트
| 스크립트명 | 기능 설명 |
|-----------|----------|
| **튜토리얼 시스템** | |
| `TutorialDialog.cs` | 튜토리얼 진행 로직 및 제스처 학습 시퀀스 관리 |
| `DialogueManager.cs` | 대화창 UI 표시 및 텍스트 출력 관리 |
| **VR 컨트롤러** | |
| `LeftHandMotion.cs` | 왼손 제스처 인식 및 이동 제어 (전진/후진/달리기) |
| `RightHandMotion.cs` | 오른손 제스처 인식 및 상호작용 제어 |
| **트리거 시스템** | |
| `Trigger.cs` | 플레이어 충돌 감지 및 이벤트 발생 |

### 기술적 특징
- Unity XR Hands를 활용한 핸드 트래킹 구현
- 코루틴을 활용한 순차적 튜토리얼 진행
- 제스처 기반 이동 시스템 (엄지/검지/새끼손가락 인식)
- UnityEvent를 활용한 유연한 이벤트 시스템

</details>

## 시작하기

### 요구사항
- Unity 2022.3.XX LTS 이상
- Git

### 설치 방법
```bash
# 저장소 클론
git clone https://github.com/Tajo-Nero/-Vigil.git

# Unity Hub에서 프로젝트 열기
# File > Open Project > 클론한 폴더 선택
