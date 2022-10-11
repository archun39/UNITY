# 기능 구현
## [검거]
### 힘들었던 점
1. target의 고유 id 받아오기
- 고유 id에는 'OwnerClientId'와 'NetworkObjectId'가 있다.
- 'OwnerClientId'를 통해 ClientRpc를 전송할 때 원하는 client에게 보낼 수 있다.  
ex) ClientRpcParams clientRpcParams = new ClientRpcParams  
            {  
                Send = new ClientRpcSendParams  
                {  
                    TargetClientIds = new ulong[]{targetId}  
                }  
            };  
 
- 'NetworkObjectId'를 통해 해당 networkobject를 가져올 수 있다.  
ex) GetNetworkObject(playerobjectId).GetComponent<TheifControl>().GoJail();  


| id를 통해 object를 가져오는 것이 힘들었다.
