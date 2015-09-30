﻿using System.IO;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector
{
    public interface ICsvReader
    {
        Fact[] Read(Stream data);
    }
}