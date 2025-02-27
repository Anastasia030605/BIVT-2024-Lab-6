using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_5
    {
        public struct Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;
            private string[] _response;

            public string Animal { get { return _animal; } }
            public string CharacterTrait { get { return _characterTrait; } }
            public string Concept { get { return _concept; } }

            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
                _response = new string[] {animal, characterTrait, concept};
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null || questionNumber < 1 || questionNumber > 3) { return 0; }
                int count = 0;
                foreach (Response response in responses)
                {
                   if(response._response == null) { continue;  }
                   var resp = response._response;
                   if(resp.Length >= questionNumber && resp[questionNumber - 1] != null) { count++; }
                }
                return count;
            }
            public void Print() 
            {
                Console.WriteLine(_animal + " " + _characterTrait +" " + _concept);
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses;

            public string Name { get { return _name; } }
            public Response[] Responses { get { return _responses; } }

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }

            public void Add(string[] answers)
            {
                if (answers == null || _responses == null) { return; }
                Response[] newArray = new Response[_responses.Length + 1];
                Array.Copy(_responses, newArray, _responses.Length);
                string[] resp = new string[] {null, null, null};
                for(int i = 0; i < Math.Min(answers.Length, 3); ++i)
                {
                    resp[i] = answers[i]; 
                }
                Response answer = new Response(resp[0], resp[1], resp[2]);
                newArray[newArray.Length - 1] = answer;
                _responses = newArray;
            }
            public string[] GetTopResponses(int question)
            {
                if (_responses == null || question < 1 || question > 3) { return null; }
                int[] counts = new int[_responses.Length];
                int currEmptyInd = 0;
                for (int i = 0; i < _responses.Length; ++i) { counts[i] = 0; }
                string[] answers = new string[_responses.Length];
                foreach (Response response in _responses)
                {
                    string[] arr = { response.Animal, response.CharacterTrait, response.Concept };
                    string resp = arr[question - 1];
                    if(resp == null || resp == "") { continue; }
                    int find = Array.IndexOf(answers, resp);
                    if (find == -1) 
                    {
                        answers[currEmptyInd] = resp;
                        counts[currEmptyInd++] = 1;
                    } else
                    {
                        counts[find]++;
                    }
                }
                var zipped = counts.Select((count, index) => new { Count = count, Answer = answers[index] })
                   .OrderByDescending(pair => pair.Count)
                   .ToArray();
                counts = zipped.Select(pair => pair.Count).ToArray();
                answers = zipped.Select(pair => pair.Answer).ToArray();
                string[] result = new string[Math.Min(currEmptyInd, 5)];
                int cur = 0;
                for (int i = 0; i < result.Length; ++i) {
                    result[cur++] = answers[i];
                }
                return result;
            }
            public void Print()
            {
                foreach (Response response in _responses)
                {
                    response.Print();
                }
            }
        }
    }
}
