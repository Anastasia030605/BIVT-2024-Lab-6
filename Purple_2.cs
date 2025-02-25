using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;

            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int Distance { get { return _distance; } }      
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return default(int[]);

                    var newArray = new int[_marks.Length];
                    Array.Copy(_marks, newArray, _marks.Length);
                    return newArray;
                }
            }
            public int Result
            {
                get
                {
                    if (_marks == null) return 0;
                    int result = 0;
                    int best = int.MinValue, worst = int.MaxValue;
                    for (int i = 0; i < _marks.Length; ++i)
                    {
                        result += _marks[i];
                        if (_marks[i] > best) { best = _marks[i]; }
                        if (_marks[i] < worst) { worst = _marks[i]; }
                    }
                    result -= worst + best;
                    result += 60 + (_distance - 120) * 2;
                    return result >= 0 ? result : 0;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                for (int i = 0; i < _marks.Length; ++i) { 
                    _marks[i] = 0;
                }
            }

            public void Jump(int distance, int[] marks)
            {
                if(distance < 0 || marks == null || _marks == null || marks.Length != _marks.Length) return;
                _distance = distance;
                for(int i = 0; i < _marks.Length; ++i)
                {
                    _marks[i] = marks[i];
                }
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) { return; }
                Participant[] query = (from participant in array
                                       orderby participant.Result descending
                                       select participant).ToArray();
                Array.Copy(query, array, array.Length);
            }
            public void Print() { }
        }
    }
}
