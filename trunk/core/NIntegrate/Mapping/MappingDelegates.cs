namespace NIntegrate.Mapping
{
    internal delegate void InternalMapperProxy(object fac, object from, ref object to);

    public delegate TTo Mapper<TFrom, TTo>(TFrom from);

    internal delegate void InternalMapper<TFrom, TTo>(MapperFactory fac, TFrom from, ref TTo to);

    public delegate TValue MappingFrom<TFrom, TValue>(TFrom from);

    public delegate TTo MappingTo<TTo, TValue>(TTo to, TValue value);
}
