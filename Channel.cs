using System.Collections.Generic;
using System.Linq;

namespace rocket_bot
{
    public class Channel<T> where T : class
    {
        private readonly List<T> collection = new List<T>();

        /// <summary>
        /// Возвращает количество элементов в коллекции
        /// </summary>
        public int Count
        {
            get
            {
                lock (collection)
                {
                    return collection.Count;
                }
            }
        }

        /// <summary>
        /// Возвращает элемент по индексу или null, если такого элемента нет.
        /// При присвоении удаляет все элементы после.
        /// Если индекс в точности равен размеру коллекции, работает как Append.
        /// </summary>
        public T this[int index]
        {
            get
            {
                lock (collection)
                {
                    try
                    {
                        return collection[index];
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            set
            {
                lock (collection)
                {
                    if (index == collection.Count)
                         collection.Add(value);

                    collection[index] = value;
                    var count = collection.Count;
                    for (var i = index + 1; i < count; i++)
                    {
                        collection.RemoveAt(index + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает последний элемент или null, если такого элемента нет
        /// </summary>
        public T LastItem()
        {
            lock (collection)
            {
                return collection.LastOrDefault();
            }
        }

        /// <summary>
        /// Добавляет item в конец только если lastItem является последним элементом
        /// </summary>
        public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
        {
            lock (collection)
            {
                if (LastItem() == knownLastItem)
                    collection.Add(item);
            }
        }
    }
}