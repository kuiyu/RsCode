# 扣子api SDK

C#版扣子API封装

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
 var configs = CozeServiceBase.GetConfig();

 //使用指定AppId创建token  
  await CozeServiceBase.GetAccessTokenAsync("1175343662622");
  //创建会话
    EnterMessageObject enterMessageObject = new EnterMessageObject();
    enterMessageObject.Role = "user";
    enterMessageObject.Type = "question";//默认question
    enterMessageObject.Content = dto.Content;
    enterMessageObject.ContentType = "text";
    var ret= await ConversationService.CreateAsync(new EnterMessageObject[] { enterMessageObject });
    var conversationId=ret.Data.Id;

  //发送消息
  await MessageService.CreateAsync(conversationId, new MessageCreateRequest
            {
                Content = "你好RsCode",
                ContentType = dto.ContentType,
                role = "user",
            });
``