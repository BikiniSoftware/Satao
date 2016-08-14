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
    public class When_purging_normal_priorities
    {
        private CacheProvider _provider;

        [TestInitialize]
        public void SetUp()
        {
            _provider = new CacheProvider();
            for (int i = 0; i < 100; i++)
            {
                _provider.Add(i, new DummyClass() { Id = i, Name = "Bobby" }, TimeSpan.FromSeconds(5), CacheItemPriority.Normal);
            }

            for (int i = 100; i < 150; i++)
            {
                _provider.Add(i, new DummyClass() { Id = i, Name = "Vanessa" }, TimeSpan.FromSeconds(5), CacheItemPriority.High);
            }
        }

        [TestMethod]
        public void Should_only_get_hight_priority()
        {
            _provider.PurgeNormalPriorities();
            foreach (int key in _provider.Keys())
            {
                var cachedValue = _provider.Get<int, DummyClass>(key);
                cachedValue.Id.Should().BeGreaterOrEqualTo(100).And.BeLessThan(150);
            }
        }
    }
}
