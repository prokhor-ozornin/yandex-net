namespace Yandex.Translator;

/// <summary>
///   <para>Voting search results.</para>
/// </summary>
public interface IResultable<T> where T : class
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <returns></returns>
  T Result();
}