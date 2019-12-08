﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using LanguageExt.ClassInstances;
using LanguageExt.TypeClasses;
using static LanguageExt.Prelude;

namespace LanguageExt.Benchmarks
{
    [RPlotExporter, RankColumn]
    [GenericTypeArguments(typeof(int), typeof(EqInt))]
    [GenericTypeArguments(typeof(string), typeof(EqString))]
    public class HashMapRandomReadBenchmark<T, TEq>
        where TEq : struct, Eq<T>
    {
        [Params(100, 1000, 10000, 100000)]
        public int N;

        T[] keys;

        ImmutableDictionary<T, T> immutableMap;
        ImmutableSortedDictionary<T, T> immutableSortedMap;
        Dictionary<T, T> dictionary;
        HashMap<TEq, T, T> hashMap;
        Map<T, T> map;

        [GlobalSetup]
        public void Setup()
        {
            var values = ValuesGenerator.Default.GenerateDictionary<T, T>(N);
            keys = values.Keys.ToArray();

            immutableMap = SysColImmutableDictionarySetup(values);
            immutableSortedMap = SysColImmutableSortedDictionarySetup(values);
            dictionary = SysColDictionarySetup(values);
            hashMap = LangExtHashMapSetup(values);
            map = LangExtMapSetup(values);
        }

        [Benchmark]
        public int SysColImmutableDictionary()
        {
            int result = default;
            foreach (var key in keys)
            {
                result ^= immutableMap[key].GetHashCode();
            }

            return result;
        }

        [Benchmark]
        public int SysColImmutableSortedDictionary()
        {
            int result = default;
            foreach (var key in keys)
            {
                result ^= immutableSortedMap[key].GetHashCode();
            }

            return result;
        }

        [Benchmark]
        public int SysColDictionary()
        {
            int result = default;
            foreach (var key in keys)
            {
                result ^= dictionary[key].GetHashCode();
            }

            return result;
        }

        [Benchmark]
        public int LangExtHashMap()
        {
            int result = default;
            foreach (var key in keys)
            {
                result ^= hashMap[key].GetHashCode();
            }

            return result;
        }

        [Benchmark]
        public int LangExtMap()
        {
            int result = default;
            foreach (var key in keys)
            {
                result ^= map[key].GetHashCode();
            }

            return result;
        }

        public ImmutableDictionary<T, T> SysColImmutableDictionarySetup(Dictionary<T, T> values)
        {
            var immutableMap = ImmutableDictionary.Create<T, T>();
            foreach (var kvp in values)
            {
                immutableMap = immutableMap.Add(kvp.Key, kvp.Value);
            }

            return immutableMap;
        }

        public ImmutableSortedDictionary<T, T> SysColImmutableSortedDictionarySetup(Dictionary<T, T> values)
        {
            var immutableMap = ImmutableSortedDictionary.Create<T, T>();
            foreach (var kvp in values)
            {
                immutableMap = immutableMap.Add(kvp.Key, kvp.Value);
            }

            return immutableMap;
        }

        public Dictionary<T, T> SysColDictionarySetup(Dictionary<T, T> values)
        {
            var dictionary = new Dictionary<T, T>();
            foreach (var kvp in values)
            {
                dictionary.Add(kvp.Key, kvp.Value);
            }

            return dictionary;
        }

        public HashMap<TEq, T, T> LangExtHashMapSetup(Dictionary<T, T> values)
        {
            var hashMap = HashMap<TEq, T, T>();
            foreach (var kvp in values)
            {
                hashMap = hashMap.Add(kvp.Key, kvp.Value);
            }

            return hashMap;
        }

        private Map<T, T> LangExtMapSetup(Dictionary<T, T> values)
        {
            var hashMap = Map<T, T>();
            foreach (var kvp in values)
            {
                hashMap = hashMap.Add(kvp.Key, kvp.Value);
            }

            return hashMap;
        }
    }
}
