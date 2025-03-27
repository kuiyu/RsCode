using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.Coze
{
    public static class JsonToObjectRouterExtension
    {
        public static Type Route(string json)
        {
            var apiResponse = JsonSerializer.Deserialize<ObjectBaseResponse>(json);

            return apiResponse?.ObjectTypeName switch
            {
                "thread.run.step" => typeof(RunStepResponse),
                "thread.run" => typeof(RunResponse),
                "thread.message" => typeof(MessageResponse),
                "thread.message.delta" => typeof(MessageResponse),
                _ => typeof(BaseResponse)
            };
        }
    }
}
