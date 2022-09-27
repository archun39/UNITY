# 4주차 - 다양한 기능 구현


## PlayerController : 플레이어 이동


## Joystic asset을 이용한 플레이어 이동 구현

<img width="510" alt="image" src="https://user-images.githubusercontent.com/39714917/192506768-f2128a2c-0ee6-4df4-b351-606d14ed3d64.png">

Control 영역 : Control영역에서 터치입력시 조이스틱이 나타나며 플레이어를 조작할 수 있습니다.

Rotate 영역 :  Rotate영역 터치시 메인카메라가 회전을 할 수 있게 하였습니다


## NetCode를 이용한 멀티플레이어 호스트와 클라이언트 오브젝트 생성 구현

[배운내용] 
1. Input.GetAxisRaw() : "Horizontal" 과 "Vertical"을 사용하여 받는다.
2. Time.deltaTime을 통해 각 플레이어의 프레임에 따른 속도를 맞춰줄 수 있음.
3. Rigidbody - collision Detection -> Continous를 통해서 충돌 방지
4. transform.LookAt() : 

<Animator>
- animator의 변경 조건은 parameter를 통한다.
