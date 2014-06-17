using System;
using System.Globalization;
using System.Linq;
using Catharsis.Commons;
using Xunit;

namespace Yandex.Translator
{
  public abstract class UnitTestsBase<T>
  {
    protected void TestCompareTo<PROPERTY>(string property, PROPERTY lower, PROPERTY greater)
    {
      Assertion.NotEmpty(property);

      var first = typeof(T).NewInstance().To<IComparable<T>>();
      var second = typeof(T).NewInstance().To<T>();

      first.Property(property, lower);
      second.Property(property, lower);

      Assert.Equal(0, first.CompareTo(second));
      second.Property(property, greater);
      Assert.True(first.CompareTo(second) < 0);
    }

    protected void TestEquality<PROPERTY>(string property, PROPERTY oldValue, PROPERTY newValue)
    {
      Assertion.NotEmpty(property);

      var entity = typeof(T).NewInstance();

      Assert.False(entity.Equals(null));
      Assert.True(entity.Equals(entity));
      Assert.True(entity.Equals(typeof(T).NewInstance()));

      Assert.True(typeof(T).NewInstance().Property(property, oldValue).Equals(typeof(T).NewInstance().Property(property, oldValue)));
      Assert.False(typeof(T).NewInstance().Property(property, oldValue).Equals(typeof(T).NewInstance().Property(property, newValue)));
    }

    protected void TestHashCode<PROPERTY>(string property, PROPERTY oldValue, PROPERTY newValue)
    {
      Assertion.NotEmpty(property);

      var entity = typeof(T).NewInstance();

      Assert.True(entity.GetHashCode() == entity.GetHashCode());
      Assert.True(entity.GetHashCode() == typeof(T).NewInstance().GetHashCode());

      Assert.True(typeof(T).NewInstance().Property(property, oldValue).GetHashCode() == typeof(T).NewInstance().Property(property, oldValue).GetHashCode());
      Assert.True(typeof(T).NewInstance().Property(property, oldValue).GetHashCode() != typeof(T).NewInstance().Property(property, newValue).GetHashCode());
    }

    protected void TestJson(T instance, object attributes)
    {
      Assertion.NotNull(instance);

      var json = attributes.GetType().GetProperties().Select(property =>
      {
        var value = property.GetValue(attributes, null);
        if (value is bool)
        {
          value = value.ToString().ToLowerInvariant();
        }
        else if (value is DateTime)
        {
          value = @"""{0}""".FormatSelf(value.To<DateTime>().ISO8601());
        }
        else if (value.GetType().IsPrimitive)
        {
          value = string.Format(CultureInfo.InvariantCulture, "{0}", value);
        }
        else if (value is string)
        {
          value = @"""{0}""".FormatSelf(value);
        }
        else
        {
          var jsonValue = value.Json();
          value = jsonValue.Substring(0, jsonValue.Length);
        }
        return @"""{0}"":{1}".FormatSelf(property.Name, value);
      }).Join(",");

      Assert.Equal(attributes == null ? "{}" : @"{{{0}}}".FormatSelf(json), instance.Json());
    }

    protected void TestXml(T instance, string root, object attributes)
    {
      Assertion.NotNull(instance);
      Assertion.NotEmpty(root);

      var tags = attributes.GetType().GetProperties().Select(property =>
      {
        var value = property.GetValue(attributes, null);

        if (value is bool)
        {
          value = value.ToString().ToLowerInvariant();
        }
        else if (value is DateTime)
        {
          value = value.To<DateTime>().AsXml();
        }

        return string.Format(CultureInfo.InvariantCulture, "<{0}>{1}</{0}>", property.Name, value);
      });

      var xml = instance.Xml();
      Assert.True(xml.Contains(@"<?xml version=""1.0"" encoding=""utf-16""?>"));
      if (attributes == null)
      {
        Assert.True(xml.Contains(@"<{0} xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" />".FormatSelf(root)));
      }
      else
      {
        Assert.True(xml.Contains(@"<{0} xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">".FormatSelf(root)));
        Assert.True(xml.Contains("</{0}>".FormatSelf(root)));
        foreach (var tag in tags)
        {
          Assert.True(xml.Contains(tag));
        }
      }
    }
  }
}