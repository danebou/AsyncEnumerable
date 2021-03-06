﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dasync.Collections;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class EnumeratorLinqStyleExtensionsTests
    {
        [Test]
        public async Task Select()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.Select(x => x.ToString()).ToArrayAsync();
            var expectedResult = new string[] { "1", "2", "3" };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task SelectWithIndex()
        {
            var collection = new int[] { 1, 1, 1 }.GetAsyncEnumerator();
            var actualResult = await collection.Select((x, i) => x + i).ToArrayAsync();
            var expectedResult = new long[] { 1, 2, 3 };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task First()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.FirstAsync();
            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void First_Empty()
        {
            var collection = AsyncEnumerable<int>.Empty;
            Assert.ThrowsAsync<InvalidOperationException>(() => collection.FirstAsync());
        }

        [Test]
        public async Task First_Predicate()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.FirstAsync(x => x > 1);
            Assert.AreEqual(2, actualResult);
        }

        [Test]
        public void First_Predicate_Empty()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            Assert.ThrowsAsync<InvalidOperationException>(() => collection.FirstAsync(x => x > 3));
        }

        [Test]
        public async Task FirstOrDefault()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.FirstAsync();
            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public async Task FirstOrDefault_Empty()
        {
            var collection = AsyncEnumerable<int>.Empty;
            var actualResult = await collection.FirstOrDefaultAsync();
            Assert.AreEqual(0, actualResult);
        }

        [Test]
        public async Task FirstOrDefault_Predicate()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.FirstOrDefaultAsync(x => x > 1);
            Assert.AreEqual(2, actualResult);
        }

        [Test]
        public async Task FirstOrDefault_Predicate_Empty()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.FirstOrDefaultAsync(x => x > 3);
            Assert.AreEqual(0, actualResult);
        }

        [Test]
        public async Task Take()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.Take(2).ToArrayAsync();
            var expectedResult = new int[] { 1, 2 };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Take_Zero()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.Take(0).ToArrayAsync();
            var expectedResult = new int[] { };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Take_More()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.Take(int.MaxValue).ToArrayAsync();
            var expectedResult = new int[] { 1, 2, 3 };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task TakeWhile()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.TakeWhile(x => x < 3).ToArrayAsync();
            var expectedResult = new int[] { 1, 2 };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task TakeWhile_None()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.TakeWhile(x => x < 1).ToArrayAsync();
            var expectedResult = new int[] { };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task TakeWhile_All()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.TakeWhile(x => x > 0).ToArrayAsync();
            var expectedResult = new int[] { 1, 2, 3 };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Skip()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.Skip(2).ToArrayAsync();
            var expectedResult = new int[] { 3 };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Skip_Zero()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.Skip(0).ToArrayAsync();
            var expectedResult = new int[] { 1, 2, 3 };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Skip_More()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.Skip(1000).ToArrayAsync();
            var expectedResult = new int[] { };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task SkipWhile()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.SkipWhile(x => x < 3).ToArrayAsync();
            var expectedResult = new int[] { 3 };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task SkipWhile_None()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.SkipWhile(x => x > 3).ToArrayAsync();
            var expectedResult = new int[] { 1, 2, 3 };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task SkipWhile_All()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.SkipWhile(x => x > 0).ToArrayAsync();
            var expectedResult = new int[] { };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Where()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.Where(x => x != 2).ToArrayAsync();
            var expectedResult = new int[] { 1, 3 };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Where_None()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.Where(x => x > 3).ToArrayAsync();
            var expectedResult = new int[] { };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Where_All()
        {
            var collection = new int[] { 1, 2, 3 }.GetAsyncEnumerator();
            var actualResult = await collection.Where(x => x > 0).ToArrayAsync();
            var expectedResult = new int[] { 1, 2, 3 };
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task WhereWithIndex()
        {
            var collection = new int[] { 1, 2, 1 }.GetAsyncEnumerator();
            var actualResult = await collection.Where((x, i) => (x + i) != 3).ToArrayAsync();
            var expectedResult = new int[] { 1 };
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}