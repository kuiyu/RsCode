using DeepSeek.Core;

namespace RsCode.AI
{
    public static class DeepSeekHelper
    {
        static DeepSeekClient client;
        public static DeepSeekClient Client {
            get { 
                if(client==null)
                {
                    var apiKey = ConfigurationHelper.GetValue("DeepSeek:ApiKey");
                    client = new DeepSeekClient(apiKey);
                }
               return client; 
            } }
    }
}
