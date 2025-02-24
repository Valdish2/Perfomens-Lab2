using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Rootobject
    {
        public Test[] tests { get; set; }
    }

    public class Test
    {
        public int id { get; set; }
        public string title { get; set; }
        public string value { get; set; }

        public Value[] values { get; set; }
    }

    public class Value
    {
        public int id { get; set; }
        public string title { get; set; }
        public string value { get; set; }
        public Value1[] values { get; set; }
    }

    public class Value1
    {
        public int id { get; set; }
        public string title { get; set; }
        public Value2[] values { get; set; }
    }

    public class Value2
    {
        public int id { get; set; }
        public string title { get; set; }
        public string value { get; set; }
    }




    public class Rootobject1
    {
        public Value3[] values { get; set; }
    }

    public class Value3
    {
        public int id { get; set; }
        public string value { get; set; }
    }
}
