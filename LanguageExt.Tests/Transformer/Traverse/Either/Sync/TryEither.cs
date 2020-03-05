using System;
using Xunit;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace LanguageExt.Tests.Transformer.Traverse.Either.Sync
{
    public class TryEither
    {
        [Fact]
        public void WithErrorLeft_FailIsLeft()
        {
            var ma = Try<Either<Error, int>>(new Exception("fail"));
            var mb = ma.Sequence();
            var mc = Left<Error, Try<int>>(Error.New("fail"));

            var mr = mb == mc;
            
            Assert.True(mr);
        }
        
        [Fact]
        public void WithExceptionLeft_FailIsLeft()
        {
            var ma = Try<Either<Exception, int>>(new Exception("fail"));
            var mb = ma.Sequence();
            var mc = Left<Exception, Try<int>>(new Exception("fail"));

            var mr = mb.LeftToSeq().Head().Message == mc.LeftToSeq().Head().Message;
            
            Assert.True(mr);
        }
        
        
        [Fact]
        public void WithUnknownLeft_FailIsLeft()
        {
            var ma = Try<Either<string, int>>(new Exception("fail"));
            var mb = ma.Sequence();

            Assert.True(mb.IsBottom);
        }        
        
        [Fact]
        public void SuccLeftIsLeft()
        {
            var ma = Try<Either<Error, int>>(Left(Error.New("alt")));
            var mb = ma.Sequence();
            var mc = Left<Error, Try<int>>(Error.New("alt"));

            var mr = mb == mc;
            
            Assert.True(mr);
        }
        
        [Fact]
        public void SuccRightIsRight()
        {
            var ma = Try<Either<Error, int>>(Right(1234));
            var mb = ma.Sequence();
            var mc = Right<Error, Try<int>>(Try(1234));

            var mr = (from tb in mb
                      from tc in mc
                      select tb.Try().Equals(tc.Try())).IfLeft(false);
            
            Assert.True(mr);
        }
    }
}
