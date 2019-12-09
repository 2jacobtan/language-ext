﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using LanguageExt.ClassInstances;
using LanguageExt.TypeClasses;

namespace LanguageExt.Benchmarks
{
    [RPlotExporter, RankColumn]
    [GenericTypeArguments(typeof(int), typeof(EqInt))]
    [GenericTypeArguments(typeof(string), typeof(EqString))]
    public class HashMapRandomRemovalBenchmarks<T, TEq>
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

            immutableMap = ValuesGenerator.SysColImmutableDictionarySetup(values);
            immutableSortedMap = ValuesGenerator.SysColImmutableSortedDictionarySetup(values);
            dictionary = ValuesGenerator.SysColDictionarySetup(values);
            hashMap = ValuesGenerator.LangExtHashMapSetup<T, TEq>(values);
            map = ValuesGenerator.LangExtMapSetup(values);
        }

        [Benchmark]
        public bool SysColImmutableDictionary()
        {
            var localImmutableMap = immutableMap;
            foreach (var key in keys)
            {
                localImmutableMap = localImmutableMap.Remove(key);
            }

            return localImmutableMap.IsEmpty;
        }

        [Benchmark]
        public bool SysColImmutableSortedDictionary()
        {
            var localSortedMap = immutableSortedMap;
            foreach (var key in keys)
            {
                localSortedMap = localSortedMap.Remove(key);
            }

            return localSortedMap.IsEmpty;
        }

        [Benchmark]
        public bool SysColDictionary()
        {
            // NB! no local variable - mutating field instance
            foreach (var key in keys)
            {
                dictionary.Remove(key);
            }

            return dictionary.Count == 0;
        }

        [Benchmark]
        public bool LangExtHashMap()
        {
            var localHashMap = hashMap;
            foreach (var key in keys)
            {
                localHashMap = localHashMap.Remove(key);
            }

            return localHashMap.IsEmpty;
        }

        [Benchmark]
        public bool LangExtMap()
        {
            var localMap = map;
            foreach (var key in keys)
            {
                localMap = localMap.Remove(key);
            }

            return localMap.IsEmpty;
        }
    }
}
