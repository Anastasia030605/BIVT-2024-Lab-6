using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_4
    {
        public struct Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;

            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public double Time { get { return _time; } }

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = -1;
            }

            public void Run(double time)
            {
                if (_time == -1 && time > 0)
                {
                    _time = time;
                }
            }
            public void Print() { }
        }

        public struct Group
        {
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name { get { return _name; } }
            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) { return null; }
                    return _sportsmen;
                }
            }

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }
            public Group(Group other)
            {
                _name = other.Name;
                if (other.Sportsmen != null)
                {
                    _sportsmen = new Sportsman[other.Sportsmen.Length];
                    Array.Copy(other.Sportsmen, _sportsmen, _sportsmen.Length);
                } else
                {
                    _sportsmen = new Sportsman[0];
                }
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null) { return; }
                Sportsman[] newArray = new Sportsman[_sportsmen.Length + 1];
                Array.Copy(_sportsmen, newArray, _sportsmen.Length);
                newArray[_sportsmen.Length] = sportsman;
                _sportsmen = newArray;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null || _sportsmen == null) { return; }
                Sportsman[] newArray = new Sportsman[_sportsmen.Length + sportsmen.Length];
                Array.Copy(_sportsmen, newArray, _sportsmen.Length);
                Array.ConstrainedCopy(sportsmen, 0, newArray, _sportsmen.Length, sportsmen.Length);
                _sportsmen = newArray;
            }

            public void Add(Group other)
            {
                if(_sportsmen == null || other.Sportsmen == null) { return; }
                Add(other.Sportsmen);
            }
            
            public void Sort()
            {
                Sportsman[] newArray = _sportsmen
                    .OrderBy(sportsman => sportsman.Time)
                    .ToArray();
                Array.Copy(newArray, _sportsmen, _sportsmen.Length);
            }

            public static Group Merge(Group group1, Group group2)
            {
                Group merged = new Group("Финалисты");
                Sportsman[] gr1, gr2;
                if(group1.Sportsmen == null) { gr1 = new Sportsman[0]; }
                else {  gr1 = group1.Sportsmen; }
                if(group2.Sportsmen == null) { gr2 = new Sportsman[0]; }
                else { gr2 = group2.Sportsmen; }
                merged._sportsmen = new Sportsman[gr1.Length + gr2.Length];
                for (int i = 0, j = 0; i < gr1.Length || j < gr2.Length;)
                {
                    if(i < gr1.Length && j < gr2.Length)
                    {
                        if (gr1[i].Time <= gr2[j].Time)
                        {
                            merged._sportsmen[i + j] = gr1[i++];
                        } else
                        {
                            merged._sportsmen[i + j] = gr2[j++];
                        }
                    } else
                    {
                        if(i < gr1.Length)
                        {
                            Array.ConstrainedCopy(gr1, i, merged._sportsmen, i + j, gr1.Length - i - 1);
                            i = gr1.Length;
                        }
                        if(j < gr2.Length)
                        {
                            Array.ConstrainedCopy(gr2, j, merged._sportsmen, i + j, gr2.Length - j - 1);
                            j = gr2.Length;
                        }
                    }
                }
                return merged;
            }
            public void Print()
            {

            }
        }
    }
}
