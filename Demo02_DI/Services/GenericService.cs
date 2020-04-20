using System;

namespace Demo02_DI.Services
{
    public class GenericService<T> : IGenericService<T>
    {
        private readonly T _item;

        public GenericService(T item)
        {
            _item = item;
            Console.WriteLine(typeof(T).Name);
        }
    }
}