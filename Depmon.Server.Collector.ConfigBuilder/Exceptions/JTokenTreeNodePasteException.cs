using System;

namespace Depmon.Server.Collector.ConfigBuilder.Exceptions
{
    public class JTokenTreeNodePasteException : AggregateException
    {
        public JTokenTreeNodePasteException(Exception sourceException)
            : base(sourceException)
        {
        }
    }
}