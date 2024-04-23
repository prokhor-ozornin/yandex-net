﻿using System.Runtime.Serialization;
using Catharsis.Commons;
using Catharsis.Extensions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Json;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="TranslationPairsResult"/>.</para>
/// </summary>
public sealed class TranslationPairsResponseTests : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslationPairsResult(IEnumerable{string})"/>
  /// <seealso cref="TranslationPairsResult(TranslationPairsResult.Info)"/>
  /// <seealso cref="TranslationPairsResult(object)"/>
  [Fact]
  public void Constructors()
  {
    typeof(TranslationPairsResult).Should().BeDerivedFrom<object>();

    var response = new TranslationPairsResult([]);
    response.Pairs.Should().BeEmpty();

    response = new TranslationPairsResult(new TranslationPairsResult.Info());
    response.Pairs.Should().BeEmpty();

    response = new TranslationPairsResult(new {});
    response.Pairs.Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationPairsResult.Pairs"/> property.</para>
  /// </summary>
  [Fact]
  public void Pairs_Property()
  {
    var response = new TranslationPairsResult(new { });
    response.Pairs.Should().BeEmpty();

    var pairs = response.Pairs.To<List<string>>();
    pairs.Add("pair");
    response.Pairs.Should().Equal("pair");
    pairs.Remove("pair");
    response.Pairs.Should().BeEmpty();
  }
}

/// <summary>
///   <para>Tests set for class <see cref="TranslationPairsResult.Info"/>.</para>
/// </summary>
public sealed class TranslationPairsResponseInfoTests
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslationPairsResult.Info()"/>
  [Fact]
  public void Constructors()
  {
    typeof(TranslationPairsResult.Info).Should().BeDerivedFrom<object>().And.Implement<IResultable<TranslationPairsResult>>().And.BeDecoratedWith<DataContractAttribute>();

    var info = new TranslationPairsResult.Info();
    info.Pairs.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationPairsResult.Info.Pairs"/> property.</para>
  /// </summary>
  [Fact]
  public void Pairs_Property()
  {
    var pairs = new List<string>();
    new TranslationPairsResult.Info { Pairs = pairs }.Pairs.Should().BeSameAs(pairs);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationPairsResult.Info.ToResult()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToResult_Method()
  {
    using (new AssertionScope())
    {
      Validate(new TranslationPairsResult([]), new TranslationPairsResult.Info());
    }

    return;

    static void Validate(TranslationPairsResult result, TranslationPairsResult.Info info)
    {
      var pairsResult = info.ToResult();

      pairsResult.Should().BeOfType<TranslationPairsResult>();
      pairsResult.Pairs.Should().Equal(result.Pairs);
    }
  }

  /// <summary>
  ///   <para>Performs testing of serialization/deserialization process.</para>
  /// </summary>
  [Fact]
  public void Serialization()
  {
    using (new AssertionScope())
    {
      Validate(new TranslationPairsResult.Info());
    }

    return;

    static void Validate(object instance) => instance.Should().BeDataContractSerializable().And.BeXmlSerializable().And.BeJsonSerializable();
  }
}