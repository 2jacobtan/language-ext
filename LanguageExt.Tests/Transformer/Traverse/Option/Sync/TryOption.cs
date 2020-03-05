using System;
using Xunit;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace LanguageExt.Tests.Transformer.Traverse.Option.Sync
{
    public class TryOption
    {
        [Fact]
        public void FailIsNone()
        {
            var ma = Try<Option<int>>(new Exception("fail"));
            var mb = ma.Sequence();
            var mc = Option<Try<int>>.None;

            var mr = mb == mc;
            
            Assert.True(mr);
        }
        
        [Fact]
        public void SuccNoneIsNone()
        {
            var ma = Try<Option<int>>(None);
            var mb = ma.Sequence();
            var mc = Option<Try<int>>.None;

            var mr = mb == mc;
            
            Assert.True(mr);
        }
        
        [Fact]
        public void SuccSomeIsSomeSucc()
        {
            var ma = Try(Some(1234));
            var mb = ma.Sequence();
            var mc = Some(Try(1234));

            var mr = (from tb in mb
                      from tc in mc
                      select tb.Try().Equals(tc.Try())).IfNone(false);
            
            Assert.True(mr);
        }
    }
}
