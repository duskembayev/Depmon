namespace Depmon.Server.Collector.ConfigBuilder.Template
{
    public static class JsonTemplate
    {
        public static string CreateGroup()
        {
            return "{'groupCode': 'groupCodeValue', 'resources': [{'resourceCode': 'resourceCodeValue','indicators':[{'indicatorCode': 'indicatorCodeValue', 'command': 'commandValue','args':{} }] }]}";
        }

        public static string CreateResource()
        {
            return "{'resourceCode': 'resourceCodeValue','indicators':[{'indicatorCode': 'indicatorCodeValue', 'command': 'commandValue','args':{} }] }";
        }

        public static string CreateIndicator()
        {
            return "{'indicatorCode': 'indicatorCodeValue', 'command': 'commandValue','args':{} }";

        }
    }
}
