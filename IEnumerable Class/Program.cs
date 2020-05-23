using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace UseEnumerable
{
    class QueueWrapper<T> : IEnumerable<T>
    {
        private Queue<T> queue = new Queue<T>();

        public void AddMyData(T data)
        {
            queue.Enqueue(data);
        }

        public IEnumerable<output> ChangeDataType<output>()             // Generic Method 
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            foreach (var item in queue)
            {
                var result = converter.ConvertTo(item,typeof(output));
                yield return (output)result;
            }
        }

        public IEnumerator<T> GetEnumerator()
        { 
            foreach (var item in queue)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()                     // Implementation if IEnumerable
        {             
            return GetEnumerator();
        }
    }

    class Consumer
    {
        private static QueueWrapper<int> wrapper = new QueueWrapper<int>();

        static int Main()
        {
            for (int i = 0; i < 10; i++)
                wrapper.AddMyData(i);

            foreach (var item in wrapper)
                Console.Write("{0} ", item);
            Console.WriteLine();

            var myDouble = wrapper.ChangeDataType<DateTime>();
            foreach (var item in wrapper)
                Console.Write("{0} ", item);
            Console.WriteLine();

            Console.ReadLine();
            return 0;
        }
    }
}

