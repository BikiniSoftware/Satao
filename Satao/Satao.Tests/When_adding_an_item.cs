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
    public class When_adding_an_item
    {
        private CacheProvider _provider;

        [TestInitialize]
        public void SetUp()
        {
            _provider = new CacheProvider();
        }

        [TestMethod]
        public void Should_retrieve_the_item()
        {
            var dummy = new DummyClass() { Id = 0, Name = "Bob" };
            _provider.Add(dummy.Id, dummy, DateTime.Now.AddMinutes(1));

            var cachedValue = _provider.Get<int, DummyClass>(dummy.Id);

            dummy.Id.Should().Be(cachedValue.Id);
            dummy.Name.Should().Be(cachedValue.Name);
        }

        [TestMethod]
        public void That_already_exists_should_retrieve_the_item()
        {
            var dummy = new DummyClass() { Id = 0, Name = "Bob" };
            _provider.Add(dummy.Id, dummy, DateTime.Now.AddMinutes(1));
            _provider.Add(dummy.Id, dummy, DateTime.Now.AddMinutes(1));

            var cachedValue = _provider.Get<int, DummyClass>(dummy.Id);

            dummy.Id.Should().Be(cachedValue.Id);
            dummy.Name.Should().Be(cachedValue.Name);
        }
    }
}
