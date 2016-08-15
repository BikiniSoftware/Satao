using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Satao.Tests.Fixtures;

namespace Satao.Tests
{
    [TestClass]
    public class When_adding_an_item
    {
        private ICacheProvider _provider;

        [TestInitialize]
        public void SetUp()
        {
            _provider = new CacheProvider();
        }

        [TestMethod]
        public void Should_retrieve_the_items()
        {
            var id = Guid.NewGuid();
            var name = "Bob";
            var dummy = new DummyClass(id, name);

            var id2 = Guid.NewGuid();
            var name2 = "Bob";
            var dummy2 = new DummyClass(id2, name2);

            _provider.Add(dummy.Id, dummy, DateTime.Now.AddMinutes(1));
            _provider.Add(dummy2.Id, dummy2, DateTime.Now.AddMinutes(33));

            var cachedValue = _provider.Get<Guid, DummyClass>(id);
            var cachedValue2 = _provider.Get<Guid, DummyClass>(id2);

            cachedValue.Id.Should().Be(id);
            cachedValue.Name.Should().Be(name);

            cachedValue2.Id.Should().Be(id2);
            cachedValue2.Name.Should().Be(name2);
        }

        [TestMethod]
        public void Should_retrieve_the_item_class()
        {
            var id = Guid.NewGuid();
            var name = "Bob";
            var dummy = new DummyClass(id, name);

            _provider.Add(dummy.Id, dummy, DateTime.Now.AddMinutes(1));
            var cachedValue = _provider.Get<Guid, DummyClass>(id);

            cachedValue.Id.Should().Be(id);
            cachedValue.Name.Should().Be(name);
        }

        [TestMethod]
        public void Should_retrieve_the_item_string()
        {
            var key = "007";
            var name = "Bob";
            
            _provider.Add(key,name, DateTime.Now.AddMinutes(1));
            var cachedValue = _provider.Get<string, string>(key);

            cachedValue.Should().Be(name);
        }

        [TestMethod]
        public void Should_retrieve_the_item_primitive()
        {
            var key = "007";
            var name = 1;

            _provider.Add<string,int>(key, name, DateTime.Now.AddMinutes(1));
            var cachedValue = _provider.Get<string, int>(key);

            cachedValue.Should().Be(name);
        }

        [TestMethod]
        public void Should_have_deep_clone_the_item()
        {
            var dummy = new DummyClass() { Name = "Bob" };
            _provider.Add(dummy.Id.ToString(), dummy, DateTime.Now.AddMinutes(1));

            dummy.Name = "Toto";
            var cachedValue = _provider.Get<string, DummyClass>(dummy.Id.ToString());

            cachedValue.Id.Should().Be(dummy.Id);
            cachedValue.Name.Should().Be("Bob");
        }

        [TestMethod]
        public void That_already_exists_should_retrieve_the_item()
        {
            var dummy = new DummyClass() { Name = "Bob" };
            _provider.Add(dummy.Id.ToString(), dummy, DateTime.Now.AddMinutes(1));
            _provider.Add(dummy.Id.ToString(), dummy, DateTime.Now.AddMinutes(1));

            var cachedValue = _provider.Get<string, DummyClass>(dummy.Id.ToString());

            dummy.Id.Should().Be(cachedValue.Id);
            dummy.Name.Should().Be(cachedValue.Name);
        }

    }
}
