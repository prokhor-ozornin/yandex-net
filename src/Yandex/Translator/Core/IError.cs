namespace Yandex.Translator;

/// <summary>
///   <para></para>
/// </summary>
public interface IError : IComparable<IError>, IEquatable<IError>
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  int Code { get; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  string Text { get; }
}