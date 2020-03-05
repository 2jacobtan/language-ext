using Xunit;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace LanguageExt.Tests.Transformer.Traverse.Option.Sync
{
    public class OptionOption
    {
        [Fact]
        public void NoneIsNone()
        {
            var ma = Option<Option<int>>.None;
            var mb = ma.Sequence();
            var mc = Option<Option<int>>.None;

            var mr = mb == mc;
            
            Assert.True(mr);
        }
        
        [Fact]
        public void SomeNoneIsNone()
        {
            var ma = Some<Option<int>>(None);
            var mb = ma.Sequence();
            var mc = Option<Option<int>>.None;

            var mr = mb == mc;
            
            Assert.True(mr);
        }
        
        [Fact]
        public void SomeSomeIsSomeSome()
        {
            var ma = Some(Some(1234));
            var mb = ma.Sequence();
            var mc = Some(Some(1234));

            var mr = mb == mc;
            
            Assert.True(mr);
        }
    }
}
