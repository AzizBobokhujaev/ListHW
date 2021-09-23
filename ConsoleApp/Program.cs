using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class MyList<T>
    {
        private T[] myList = null;
        private int newIndex = 0;
        private int CountItems = 0;

        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < myList.Length)
                    return myList[index];
                else
                {
                    Console.WriteLine(new ArgumentOutOfRangeException().Message);
                    return myList[0];
                }
            }
            set { myList[index] = value; }
        }

        public MyList()
        {
            this.myList = new T[1];
        }

        public MyList(int count)
        {
            this.myList = new T[count];
        }

        public void Add(T item)
        {
            myList[newIndex] = item;
            newIndex++;
            CountItems++;
        }

        public int Capacity
        {
            get { return myList.Length; }

        }
        public int Count
        {
            get { return CountItems; }
        }
        public IEnumerator GetEnumerator()
        {
            return myList.GetEnumerator();
        }
    }
    class MyDictionary<TKey, TValue>
    {
        private int counter = 0;
        private TKey[] Keylist = new TKey[0];
        private TValue[] Valuelist = new TValue[0];
        public int Counter
        {
            get { return this.counter; }
        }

        public MyDictionary()
        {
            TKey[] Keylist = new TKey[0];
            TValue[] Valuelist = new TValue[0];
            this.Keylist = Keylist;
            this.Valuelist = Valuelist;
        }
        public void Add(TKey Key, TValue Value)
        {
            this.counter++;
            Array.Resize(ref Keylist, counter);
            Keylist[counter - 1] = Key;

            Array.Resize(ref Valuelist, counter);
            Valuelist[counter - 1] = Value;
        }
        public TValue this[TKey Key]
        {
            get
            {
                int index = -1;
                for (int i = 0; i < counter; i++)
                {
                    if (Key.Equals(Keylist[i]))
                        index = i;
                }
                if (index == -1)
                {
                    Console.WriteLine(new NullReferenceException().Message);
                    index++;
                }
                return Valuelist[index];
            }
            set
            {
                for (int i = 0; i < counter; i++)
                    if (Key.Equals(Keylist[i]))
                    {
                        Valuelist[i] = value;
                    }
            }
        }
        public IEnumerable GetItems()
        {
            Pair<TKey, TValue> Item;
            for (int i = 0; i < counter; i++)
            {
                Item = new Pair<TKey, TValue>(Keylist[i], Valuelist[i]);
                yield return Item;
            }
        }
    }
    public class Pair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public Pair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyList<string> CityOfWorldList = new MyList<string>(22);
            MyDictionary<int, string> CityDictionarList = new MyDictionary<int, string>();
            CityOfWorldList.Add("Душанбе");
            CityOfWorldList.Add("Москва");
            CityOfWorldList.Add("Нью-Йорк");
            CityOfWorldList.Add("Мельбурн");
            CityOfWorldList.Add("Бостон");
            CityOfWorldList.Add("Лондон");
            CityOfWorldList.Add("Токио");



            //добавим все элемент из CityOfWorldList в CityDictionarList
            for (int i = 0; i < CityOfWorldList.Count; i++)
            {
                CityDictionarList.Add(i, CityOfWorldList[i]);
            }
            Console.WriteLine("--------------- GetItemOfKey --------------------\n");
            for (int i = 0; i < CityDictionarList.Counter; i++)
            {
                Console.WriteLine(CityDictionarList[i]);
            }
            Console.WriteLine("Key  -  Value \n");
            foreach (Pair<int, string> KeyValue in CityDictionarList.GetItems())
                Console.WriteLine(KeyValue.Key + " - " + KeyValue.Value);
            Console.ReadKey();
        }
    }
}