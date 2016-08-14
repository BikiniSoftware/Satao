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
    public class When_absolute_time_expires
    {
        private CacheProvider _provider;

        [TestInitialize]
        public void SetUp()
        {
            _provider = new CacheProvider();
        }

        [TestMethod]
        public async Task Should_be_null()
        {
            var dummy = new DummyClass() { Id = 0, Name = "Bob" };
            _provider.Add(dummy.Id, dummy, DateTime.Now.AddSeconds(5));

            await Task.Delay(TimeSpan.FromSeconds(10)); // To simultate expiry.

            var cachedValue = _provider.Get<int, DummyClass>(dummy.Id);
            cachedValue.Should().BeNull("Absolute expiry expires");
        }
    }
}
