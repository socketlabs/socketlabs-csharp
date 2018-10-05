namespace dotNetCoreExample.Examples
{
    public static class ExampleConfig
    {
        //public static int ServerId => Environment.GetEnvironmentVariable("SocketlabsServerId", EnvironmentVariableTarget.User);
        //public static string ApiKey => Environment.GetEnvironmentVariable("SocketlabsApiPassword", EnvironmentVariableTarget.User);
        public static string TargetApi = "https://inject.socketlabs.com/api/v1/email";
 
        public static int ServerId => 0; //your serverId
        public static string ApiKey => "your api key"; 

    }
}
