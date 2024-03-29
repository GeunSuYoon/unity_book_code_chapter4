# unity_book_code_chapter4

> "가장 쉬운 유니티 게임 제작 2판"의 Chapter4 내용을 실습한 코드입니다.\
> 필요한 에셋들은 책 저작자의 [네이버 블로그](https://blog.naver.com/kimluxx/223009736569)에서 다운로드 받을 수 있습니다.\
> 오브젝트의 이동과 충돌에 대해 다룰 것입니다.

## 목표: 간단한 2D 종스크롤 슈팅 게임 만들기

### 순서

1. 배경(Backgrond) 설정
2. 플레이어(Player) 설정
3. 총알(Bullet) 설정
4. 적 비행기(Enemy) 설정
5. 스코어 표시하기
6. 게임오버 화면 만들기(Scene 전환)

### 공통

- 기본 Material은 mainTextureOffset을 지원하지 않는다.
  - 새로운 Material을 만들자!
- Project View에서 CustomSpriteMaterial을 만든다.
- Shader를 Standard에서 UI/Unlit/Text Detail로 설정한다.
  - Text Detail을 검색하자.
- 이하 만드는 모든 게임 오브젝트의 Material은 CustomSpriteMaterial로 설정한다.
- Component 타입으로 선언한 변수는 `Start()`에서 정의한다.

### 1. 배경(Backgrond) 설정

- Background 게임 오브젝트를 만든다.
  - Sprite를 background_basic으로 설정한다.
- Background를 화면 크기에 맞춘다.
- C\# 스크립트 이름을 [BackgroundScroll](./Assets/BackgroundScroll.cs)로 만든다.
  - `Material  mat, float  speed, float  currentYoffset`을 선언한다.
    - `speed`는 0.08f로 설정했다.
    - `currentYoffset`은 0으로 설정했다.
  - `Update()`에서 `currentYoffset += speed * Time.deltaTime;`으로 Y축의 offset을 바꾼다.
  - `Update()`에서 `mat.mainTextureOffset = new Vector2(0, currentYoffset);`으로 그림을 세로축으로 이동한다.
- BackgroundScroll을 component에 추가한다.
- background_basic(Background의 Sprite 원본 Asset)을 Project View에서 선택해 Wrap Mode를 Repeat으로 설정하고 \[Apply] 버튼을 눌러 저장한다.
  - 이걸 하지 않으면 그림이 반복되지 않는다!

### 2. 플레이어(Player) 설정

- Hierarchy View에서 Player 게임 오브젝트를 만든다.
  - Square로 만들면 스프라이트 렌더러가 포함돼 편하다!
  - Sprite를 Plane으로 설정한다.
- Circle Collider 2D를 component에 추가해 Radius를 비행기 크기에 맞게 조정한다.
  - Poligon Collider 2D가 더 정확하나 로딩이 오래 걸린다. 간단한 실습이니 Circle로 하자...
  - Is Trigger를 체크해 `OnTriggerEnter2D()`함수를 사용할 수 있게 한다.
- C\# 스크립트 이름을 [PlayerMove](./Assets/PlayerMove.cs)로 만든다.
  - 이하 요소들을 선언한다.\
    `public float  speed;`\
    `Transform  tr;`\
    `Vector2  mousePosition;`\
    `public Vector2  limitPoint1;`\
    `public Vector2  limitPoint2;`\
    `public GameObject	prefaBullet;`\
    `int  hitCounter;`
    - `speed, limitPoint1, limitPoint2`를 게임 플레이하며 확인하며 조정한다.
      - `limitPoint1, limitPoint2`는 player의 이동 제한 범위다.
    - `prefaBullet`은 이후에 만들어 추가한다.
  - 이하 함수들을 추가한다.\
    `void OnDrawGizmos();`\
    `IEnumerator FireBullet();`\
    `void	OnTriggerEnter2D(Collider2D collision);`
  - `Start()`에서 `StartCoroutine(FireBullet());`으로 총알을 발사하게 만든다.
    - `IEnumerator` 타입 함수들은 `StartCoroutine()` 함수를 통해 프레임 단위로 실행하는 `Update()`와 따로 설정에 맞춰 실행할 수 있다!
  - `Update()`에서 마우스를 클릭한 곳을 따라 게임 오브젝트를 이동하게 만든다.
    - `limitPoint1, limitPoint2`를 기준으로 그 이상, 혹은 이하로 갈 수 없게 만든다.
  - `void OnDrawGizmos()` 함수에서는 `limitPoint1, limitPoint2`를 빨간 선으로 표시한다.
  - `IEnumerator FireBullet()` 함수에서는 일정 시간동안 `prefaBullet`이 생성되게 만든다.
    - `Instantiate()` 함수는 인자의 개수와 내용에 따라 만들 오브젝트의 초기 설정을 변경할 수 있다.
      - `prefaBullet` 게임 오브젝트를 `tr.position`에서 `Quaternion.identity`(회전 없이) 생성한다.
    - `yield return new WaitForSeconds(0.3f);`로 0.3f마다 반복한다.
  - `void	OnTriggerEnter2D(Collider2D collision)` 함수에서 충돌이 일어날 때 GameOver Scenen으로 넘긴다.
- PlayerMove를 component에 추가한다.

### 3. 총알(Bullet) 설정

- Bullet 게임 오브젝트를 만든다.
  - Sprite를 bullet으로 설정한다.
- Position이 (0, 0, 0)이 아닐 경우 Transform 바 우측 설정(세로 점 세 개)을 눌러 Reset을 클릭한다.
- Box Collider 2D를 component에 추가한다.
- C\# 스크립트 이름을 [BulletMove](./Assets/BulletMove.cs)로 만든다.
  - 이하 요소들을 선언한다.\
    `public float	speed;`\
    `public float	waitSecond;`\
    `Transform		tr;`
    - `waitSecond`는 bullet이 없어질 시간이다.
  - 이하 함수를 추가한다.\
    `IEnumerator DestroySelf()`
  - `Start()`에서 `StartCoroutine(DestroySelf());`으로 `waitSecond` 시간 후 bullet이 파괴되도록 한다.
  - `Update()`에서 `tr.Translate(Vector2.up * speed);`으로 bullet의 높이를 매 프레임 speed만큼 높인다.
  - `IEnumerator DestroySelf()`에서 `yield return new WaitForSeconds(waitSecond);, Destroy(this.gameObject);`로 waitSecond마다 bullet이 파괴도록 한다.
- BulletMove를 component에 추가한다.
- Bullet 게임 오브젝트를 Project View로 드래그해 Prefab으로 만든다.
  - 파란 상자 아이콘으로 바뀌면 성공한 것이다.

### 4. 적 비행기(Enemy) 설정

- Enemy 게임 오브젝트를 만든다.
  - Sprite를 Plane_Enemy로 설정한다.
- Circle Collider 2D를 component에 추가해 충돌 범위를 적절히 설정한다.
- Rigidbody 2D를 component에 추가하고, Gravity Scale을 0으로 바꿔 중력 현상을 안 받도록 한다.
- C\# 스크립트 이름을 [EnemyMove](./Assets/EnemyMove.cs)로 만든다.
  - Enemy는 Bullet과 비슷한 움직임을 보인다.
  - BulletMove Script를 복사해 EnemyMove Script로 만들자.
  - `int  hitCounter = 0;` 변수를 추가하자.
    - 개인적으로 추가해본 부분이다.
    - hitCounter는 collision이 일어날 때 증가하며, 특정 값이 되면 게임 오브젝트를 파괴한다.
  - speed와 waitSecond 값을 적절히 바꾼다.
  - `void		OnTriggerEnter2D(Collider2D collision);` 함수를 추가로 사용한다.
    - PlayerMove 스크립트 내 이름이 같은 함수와 비슷한 역할을 한다.
    - hitCounter를 증가시키고, 충돌한 오브젝트를 삭제한다.
    - 만일 hitCounter가 일정 수 이상이 되면, Enemy 오브젝트를 Destroy 한다.
    - Destroy 후 GameManger 오브젝트의 Score를 10 올린다.
- EnemyMove를 component에 추가한다.
- Enemy 게임 오브젝트를 Project View로 드래그해 Prefab으로 만든다!

### 5. 스코어 표시하기
### 6. 게임오버 화면 만들기(Scene 전환)
