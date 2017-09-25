using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.UnitTest
{
    [TestClass]
    public class TestAutoMapper
    {
        [TestMethod]
        public void test() {
            Entity2 e1 = new Entity2();
            e1.name = "1";
            e1.pwd = "2";
            //AutoMapper.Mapper.Initialize(cfg=>cfg.CreateMap<Entity1, Entity2>());
            //var s = AutoMapper.Mapper.Map<Entity2>(e1);
            var s = MapToObj<Entity2, Entity1>(e1);
            Console.WriteLine(s.name);
        }
        public static K MapToObj<T, K>(T obj)
        {
            AutoMapper.Mapper.Initialize(t => t.CreateMap<T, K>());
            return AutoMapper.Mapper.Map<K>(obj);
        }
    }

    public class Entity1 {
        public string name { get; set; }
        public string pwd { get; set; }
    }

    public class Entity2 {
        public string name { get; set; }
        public string pwd { get; set; }
    }

}
