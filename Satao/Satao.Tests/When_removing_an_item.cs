using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Satao.Tests.Fixtures;

namespace Satao.Tests
{
    [TestClass]
    public class When_removing_an_item
    {
        private ICacheProvider _provider;

        [TestInitialize]
        public void SetUp()
        {
            _provider = new CacheProvider();
        }

        [TestMethod]
        public void Should_be_null_when_trying_to_get_it()
        {
            var dummy = new DummyClass() { Name = "Bob" };
            _provider.Add(dummy.Id, dummy, DateTime.Now.AddMinutes(1));
            _provider.Remove(dummy.Id);

            var cachedValue = _provider.Get<Guid, DummyClass>(dummy.Id);
            cachedValue.Should().BeNull();
        }

        [TestMethod]
        public void Should_be_null_when_trying_removing_an_unexisting_one()
        {
            var cachedValue = _provider.Get<Guid, DummyClass>(Guid.NewGuid());
            cachedValue.Should().BeNull();
        }

        [TestMethod]
        public void Should_launch_the_event_handler()
        {
            var _cacheProvider = new CacheProvider();

            var dummy = new DummyClass() { Name = "Bob" };
            _cacheProvider.Add(dummy.Id, dummy, DateTime.Now.AddMinutes(1));
            _cacheProvider.KeyRemoved += (s,e) => _cacheProvider.Add("TOTO", 123456789, DateTime.Now.AddDays(5));
            _cacheProvider.Remove(dummy.Id);

            var cachedValue = _cacheProvider.Get<string, int>("TOTO");
            cachedValue.Should().Be(123456789);
        }
    }
}
