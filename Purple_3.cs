﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_3
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            private int _judgements;

            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public double[] Marks
            {
                get
                {
                    if (_marks == null) { return default(double[]); }
                    var newArray = new double[_marks.Length];
                    Array.Copy(_marks, newArray, newArray.Length);
                    return newArray;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null) { return default(int[]); }
                    var newArray = new int[_places.Length];
                    Array.Copy(_places, newArray, newArray.Length);
                    return newArray;
                }
            }
            public int Score
            {
                get
                {
                    if (_places == null) { return 0; }
                    int score = 0;
                    for (int i = 0; i < _places.Length; ++i)
                    {
                        score += _places[i];
                    }
                    return score;
                }
            }
            public int HighestPlace
            {
                get
                {
                    if (_places == null) { return 0; }
                    int highestPlace = int.MaxValue;
                    for (int i = 0; i < _places.Length; ++i)
                    {
                        if (_places[i] < highestPlace)
                        {
                            highestPlace = _places[i];
                        }
                    }
                    return highestPlace;
                }
            }
            public double MarksSum
            {
                get
                {
                    if (_marks == null) { return 0; }
                    double marksSum = 0;
                    foreach (double x in _marks)
                    {
                        marksSum += x;
                    }
                    return marksSum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                for (int i = 0; i < _places.Length; ++i)
                {
                    _marks[i] = 0;
                    _places[i] = 0;
                }
                _judgements = 0;
            }

            public void Evaluate(double result)
            {
                if (_marks == null || _judgements == _marks.Length) { return; }
                _marks[_judgements] = result;
                _judgements++;
            }
            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) { return; }
                for (int i = 0; i < 7; ++i)
                {
                    Participant[] array = (from participant in participants
                                           orderby participant.Marks[i] descending
                                           orderby participant.Places[participant.Places.Length - 1] ascending
                                           select participant).ToArray();
                    Array.Copy(array, participants, participants.Length);
                    for (int j = 1; j <= participants.Length; ++j)
                    {
                        participants[j - 1]._places[i] = j;
                    }
                }
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) { return; }

                Participant[] arr =   array
                         .OrderBy(participant => participant.Score)
                         .ThenBy(participant => participant.HighestPlace)
                         .ThenByDescending(participant => participant.MarksSum)
                         .ToArray();
                Array.Copy(arr, array, arr.Length);
            }
            public void Print() { }
        }
    }
}
