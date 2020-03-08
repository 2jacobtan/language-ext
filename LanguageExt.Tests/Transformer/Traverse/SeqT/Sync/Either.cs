using LanguageExt.Common;
using Xunit;
using static LanguageExt.Prelude;

namespace LanguageExt.Tests.Transformer.Traverse.SeqT.Sync
{
    public class EitherSeq
    {
        [Fact]
        public void LeftIsEmpty()
        {
            var ma = Left<Error, Seq<int>>(Error.New("alt"));

            var mb = ma.Sequence();

            Assert.True(mb == Empty);
        }

        [Fact]
        public void RightEmptyIsEmpty()
        {
            var ma = Right<Error, Seq<int>>(Empty);

            var mb = ma.Sequence();

            Assert.True(mb == Empty);
        }

        [Fact]
        public void RightSeqIsSeqRight()
        {
            var ma = Right<Error, Seq<int>>(Seq(1, 2, 3));

            var mb = ma.Sequence();

            Assert.True(mb == Seq(Right<Error, int>(1), Right<Error, int>(2), Right<Error, int>(3)));
        }
    }
}
