using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Volare.Models;

namespace Volare.Service
{
    public interface ISeedingService
    {
        void Seed();
    }
}