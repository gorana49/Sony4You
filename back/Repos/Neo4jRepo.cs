using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using back.IRepos;
namespace back.Repos
{
    public class Neo4jRepo : INeo4jRepo 
    {
        private readonly IDriver _driverNeo4J;
        public Neo4jRepo(IDriver driverNeo4J)
        {
           _driverNeo4J = driverNeo4J;
        }
    }
}