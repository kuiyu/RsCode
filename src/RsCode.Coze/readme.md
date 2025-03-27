# 扣子api SDK

C#版扣子API封装,支持多个应用调用

appsettings.json中配置应用信息
```json
  "ByteDance": {
    "Coze": [
      {
        "AppId": "", //应用id
        "PublicKey": "", //公钥指纹
        "PrivateKeyPath": "private_key.pem" //私钥文件相对路径
      }
    ] //应用信息
  },
```

代码中调用

```csharp
//添加服务 
services.AddCoze();

//构造中注入
CozeManager cozeManager;ConversationService conversationService;
public ChatController(CozeManager cozeManager, ConversationService conversationService)
{
        this.cozeManager = cozeManager;
    var configs = cozeManager.CozeConfigs;
    appId = configs.First().AppId;
    this.conversationService = conversationService;    
}

//获取会话id
public async Task<object> CreateAsync(ChatCreateDto dto)
{
    string appId="";
    cozeManager.RefreshToken(appId); //刷新token
           
    EnterMessageObject enterMessageObject = new EnterMessageObject();
    enterMessageObject.Role = "user";
    enterMessageObject.Type = "question";//默认question
    enterMessageObject.Content = dto.Content;
    enterMessageObject.ContentType = "text";
    return await conversationService.CreateAsync(new EnterMessageObject[] { enterMessageObject });
}
