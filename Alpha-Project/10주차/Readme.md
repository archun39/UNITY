# 10주차

### 8주차 에러해결

- client에서 coroutine이 적용되지 않는 현상
    - → client 조건을 걸어서 해결

# 아이템

> 화면에 퀵슬롯을 두어 플레이어가 아이템을 획득했을 때 퀵슬롯을 통해 빠르게 사용하도록 함
> 
1. 퀵슬롯의 갯수는 2개이다.
2. 플레이어는 아이템을 다가가서 습득한다.
3. 퀵슬롯은 터치를 통해서 사용할 수 있다.
4. 같은 아이템도 다른 슬롯에 등록한다.
5. 아이템은 랜덤박스를 통해 랜덤으로 획득된다.

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/7f591b07-0fb6-43ea-bb79-e1dc438f23b1/Untitled.png)

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/c996df35-dc91-42bf-a7ab-f04a54bdb7b0/Untitled.png)

                                         먹기 전

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/005f51c7-fc3c-46f9-809c-fe813176fa83/Untitled.png)

                                       먹은 후

# 아이템 종류

1. 이동속도 증가 ( 공용 )
2. 열쇠 ( 도둑 )

---

< 구현 예정 >

1. 곤봉 ( 경찰 )
2. 위치추적기 ( 경찰 )
3. 테이저건 ( 경찰 )
4. 덫 ( 경찰 )

### 구현

→ 디자인 패턴을 한눈에 볼 수 있도록 구현함

모든 아이템에 들어가는 스크립트

1. PickUp.cs
2. Slot.cs 
3. UseItem.cs

---

 PnTItemManager (서버)를 통해서 기능 구현

## 에러 발생

1. 한 플레이어가 습득한 아이템이 공통적으로 인식된다.
    1. 그러나 사용은 각자 된다.
    2. 해결 : clientRpc를 통해서 해당에서만 되게끔
2. Destroy
3. 사용 아이템을 destroy하면 코루틴이 중단됨
    1. → ItemManager를 사용함
4. 클라이언트는 사용이안됨
