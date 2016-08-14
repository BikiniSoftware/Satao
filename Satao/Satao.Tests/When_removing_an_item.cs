using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Satao.Tests.Fixtures;

namespace Satao.Tests
{
    [TestClass]
    public class When_removing_an_item
    {
        private CacheProvider _provider;

        [TestInitialize]
        public void SetUp()
        {
            _provider = new CacheProvider();
        }

        [TestMethod]
        public void Should_be_null_when_trying_to_get_it()
        {
            var dummy = new DummyClass() { Id = 0, Name = "Bob" };
            _provider.Add(dummy.Id, dummy, DateTime.Now.AddMinutes(1));
            _provider.Remove(dummy.Id);

            var cachedValue = _provider.Get<int, DummyClass>(dummy.Id);
            cachedValue.Should().BeNull();
        }

        [TestMethod]
        public void Should_be_null_when_trying_removing_an_unexisting_one()
        {
            var cachedValue = _provider.Get<int, DummyClass>(1);
            cachedValue.Should().BeNull();
        }
    }
}
