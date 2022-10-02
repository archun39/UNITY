# 5주차

## 목표 구현

1. 플레이어들이 경찰과 도둑의 역할을 배정받을 수 있다.
2. 배정받은 역할에 따른 기능들을 구현한다.


### [Scripts]
Player Prefab에 PoliceControl.cs와 TheifControl.cs를 추가하였다.
각 포탈에 경찰에는 BePolice.cs와 도둑에는 BeTheif.cs를 추가하였다.

22.10.02.  
(TheifControl.cs)
- Safeopen() : 디텍팅 후 금고를 발견하면 'F'키(상호작용키)를 통해 금고를 열 수 있다.

(PoliceControl.cs)
- Catch() : 디택팅 후 도둑플레이어(BeTheif)를 발견하면 'F'키(상호작용키)를 통해 도둑을 감옥(Jail)로 보낼 수 있다.



### [Map]
- 맵에 포탈 2곳을 만들어 각각의 player들이 경찰(Tag = "BePolice") 과 도둑(Tag = "BeTheif") 의 역할을 선택할 수 있게 하였다.
- 금고를 만들었다 (Tag = "Safe") 22.09.30.
- 감옥을 만들었다 (Tag = "Jail") 22.10.02.
