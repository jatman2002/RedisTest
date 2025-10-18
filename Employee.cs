using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisTest
{
    internal class Employee
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }

        public override String ToString() {
            return $"id={Id}\tname={Name}\tage={Age}\tposition={Position}"; 
        }

    }
}
