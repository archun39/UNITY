# 5주차

## 목표 구현

플레이어들이 경찰과 도둑의 역할을 배정받을 수 있다.
배정받은 역할에 따른 기능들을 구현한다.


### [Scripts]
Player Prefab에 PoliceControl.cs와 TheifControl.cs를 추가하였다.
각 포탈에 경찰에는 BePolice.cs와 도둑에는 BeTheif.cs를 추가하였다.

### [Map]
- 맵에 포탈 2곳을 만들어 각각의 player들이 경찰(Tag = "BePolice") 과 도둑(Tag = "BeTheif") 의 역할을 선택할 수 있게 하였다.
- 금고를 만들었다 (Tag = "Safe")
