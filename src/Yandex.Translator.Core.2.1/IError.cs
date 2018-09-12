namespace Yandex.Translator
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public interface IError
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
}