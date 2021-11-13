using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleTests
{
    public class RetryTest
    {
        [Fact]
        public async Task Test()
        {
            int t = 0;
            bool res1 = false, res2 = false;
            try { res1 = await Retry(3, () => Foo(t++)); }
            catch { }
            Assert.False(res1);

            try { res2 = await Retry(3, () => Foo(t++)); }
            catch { }
            Assert.True(res2);
        }

        private Task<bool> Foo(int t)
        {
            if (t < 5)
                throw new Exception();

            return Task.FromResult(true);
        }

        internal static async Task<T> Retry<T>(int retry, Func<Task<T>> action, int retryMillisecondDelay = 500)
        {
            Exception exception = null;
            int tryCount = 0;
            while (tryCount < retry)
            {
                try
                {
                    return await action?.Invoke();
                }
                catch (Exception ex)
                {
                    exception = ex;
                    await Task.Delay(retryMillisecondDelay);
                    tryCount++;
                }
            }

            throw exception;
        }
    }
}
