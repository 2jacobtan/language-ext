using System;
using Xunit;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace LanguageExt.Tests.Transformer.Traverse.Try.Sync
{
    public class EitherTry
    {
        [Fact]
        public void LeftIsFail()
        {
            var ma = Left<Error, Try<int>>(Error.New("alt"));
            var mb = ma.Sequence();

            var mc = TryFail<Either<Error, int>>(new Exception("alt"));

            Assert.True(default(EqTry<Either<Error, int>>).Equals(mb, mc));
        }
        
        [Fact]
        public void RightFailIsFail()
        {
            var ma = Right<Error, Try<int>>(TryFail<int>(new Exception("fail")));
            var mb = ma.Sequence();
            var mc = TryFail<Either<Error, int>>(new Exception("fail"));

            Assert.True(default(EqTry<Either<Error, int>>).Equals(mb, mc));
        }
        
        [Fact]
        public void RightSuccIsSuccRight()
        {
            var ma = Right<Error, Try<int>>(TrySucc(1234));
            var mb = ma.Sequence();
            var mc = TrySucc(Right<Error, int>(1234));
            
            Assert.True(default(EqTry<Either<Error, int>>).Equals(mb, mc));
        }
    }
}
