# unity_book_code_chapter4

> "가장 쉬운 유니티 게임 제작 2판"의 Chapter4 내용을 실습한 코드입니다.\
> 필요한 에셋들은 책 저작자의 [네이버 블로그](https://blog.naver.com/kimluxx/223009736569)에서 다운로드 받을 수 있습니다.\
> 오브젝트의 이동과 충돌에 대해 다룰 것입니다.

## 목표: 간단한 2D 종스크롤 슈팅 게임 만들기

### 순서

1. 배경 움직이기
2. 플레이어 움직이기
3. 총알 발사
4. 적 비행기 프리펩으로 만들기
5. 게임 중간에 적 비행기 생성하기
6. 충돌 만들기
7. 스코어 표시하기
8. 게임오버 화면 만들기(Scene 전환)

### 공통

- 기본 Material은 mainTextureOffset을 지원하지 않는다.
  - 새로운 Material을 만들자!
- Project View에서 CustomSpriteMaterial을 만든다.
- Shader를 Standard에서 UI/Unlit/Text Detail로 설정한다.
  - Text Detail을 검색하자.
- 이하 만드는 모든 게임 오브젝트의 Material은 CustomSpriteMaterial로 설정한다.

### 1. 배경 움직이기

- Hierarchy View에서 Background 게임 오브젝트를 만든다.
  - Sprite를 background_basic으로 설정한다.
- 게임 오브젝트를 화면 크기에 맞춘다.
- C\# 스크립트 이름을 BackgroundScroll로 만든다.
  - `Material  mat, float  speed, float  currentYoffset`을 선언한다.
    - `speed`는 0.08f로 설정했다.
    - `currentYoffset`은 0으로 설정했다.
  - `Start()`에서 SpriteRenderer의 material component를 `Material  mat`에 저장한다.
  - `Update()`에서 `currentYoffset += speed * Time.deltaTime;`으로 Y축의 offset을 바꾼다.
  - `Update()`에서 `mat.mainTextureOffset = new Vector2(0, currentYoffset);`으로 그림을 세로축으로 이동한다.
- Background에 BackgroundScroll을 component에 추가한다.
- background_basic(Background의 Sprite 원본 Asset)을 Project View에서 선택해 Wrap Mode를 Repeat으로 설정하고 \[Apply] 버튼을 눌러 저장한다.
  - 이걸 하지 않으면 그림이 반복되지 않는다!
