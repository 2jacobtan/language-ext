using Xunit;
using Xunit.Abstractions;
using static LanguageExt.Prelude;

namespace LanguageExt.Tests.Transformer.Traverse.Lst.Collections
{
    public class HashSetLst
    {
        private readonly ITestOutputHelper _log;
        public HashSetLst(ITestOutputHelper log) => _log = log;

        [Fact]
        public void EmptyEmptyIsEmptyEmpty()
        {
            HashSet<Lst<int>> ma = Empty;

            var mb = ma.Sequence();

            var mc = Lst<HashSet<int>>.Empty;
            
            Assert.True(mb == mc);
        }
        
        // TODO: failing
        [Fact]
        public void HashSetHashSetCrossProduct()
        {
            var ma = HashSet(List(1, 2), List(10, 20, 30));

            var mb = ma.Sequence();

            var mc = List(HashSet(1, 10), HashSet(1, 20), HashSet(1, 30), HashSet(2, 10), HashSet(2, 20), HashSet(2, 30));
            
            _log.WriteLine(mb.ToFullString());
            _log.WriteLine(mc.ToFullString());
            
            Assert.True(mb == mc);
        }
        
                
        [Fact]
        public void HashSetOfEmptiesAndNonEmptiesIsEmpty()
        {
            var ma = HashSet(List<int>(), List(1, 2, 3));

            var mb = ma.Sequence();

            var mc = Lst<HashSet<int>>.Empty;
            
            Assert.True(mb == mc);
        }
        
        [Fact]
        public void HashSetOfEmptiesIsEmpty()
        {
            var ma = HashSet(List<int>(), List<int>());

            var mb = ma.Sequence();

            var mc = Lst<HashSet<int>>.Empty;
            
            Assert.True(mb == mc);
        }
    }
}
