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
    public class When_using_sliding_expiry
    {
        private ICacheProvider _provider;

        [TestInitialize]
        public void SetUp()
        {
            _provider = new CacheProvider();
        }

        [TestMethod]
        public async Task Should_retrieve_null_when_expires()
        {
            var dummy = new DummyClass() { Name = "Bob" };
            _provider.Add(dummy.Id, dummy, TimeSpan.FromSeconds(5));

            await Task.Delay(TimeSpan.FromSeconds(10)); // To simultate expiry.

            var cachedValue = _provider.Get<Guid, DummyClass>(dummy.Id);
            cachedValue.Should().BeNull("Relative expiry expires");
        }

        [TestMethod]
        public async Task Should_retrieve_the_item_after_getting_it()
        {
            var dummy = new DummyClass() { Name = "Bob" };
            _provider.Add(dummy.Id, dummy, TimeSpan.FromSeconds(5));
            
            await Task.Delay(TimeSpan.FromSeconds(3)); // To simultate expiry.

            var cachedValue = _provider.Get<Guid, DummyClass>(dummy.Id);
            cachedValue.Id.Should().Be(dummy.Id);

            await Task.Delay(TimeSpan.FromSeconds(6)); // To simultate expiry.

            var cachedValue2 = _provider.Get<Guid, DummyClass>(dummy.Id);
            cachedValue2.Should().BeNull("Relative expiry expires");
        }
    }
}
