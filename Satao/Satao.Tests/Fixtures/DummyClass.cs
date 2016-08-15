using System;

namespace Satao.Tests.Fixtures
{
    public class DummyClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DummyClass()
        {
            
        }
        public DummyClass(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
