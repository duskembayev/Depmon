using System;

namespace Depmon.Server.Collector.ConfigBuilder.Exceptions
{
    public class JTokenTreeNodeDeleteException : AggregateException
    {
        public JTokenTreeNodeDeleteException(Exception sourceException)
            : base(sourceException)
        {
        }
    }
}