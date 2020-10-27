namespace RsCode.DI
{
    /// <summary>
    ///  瞬时（Transient）生命周期服务在它们每次请求时被创建。
    ///  这一生命周期适合轻量级的，无状态的服务
    /// </summary>
    public interface ITransientDependency
    {
    }
}
