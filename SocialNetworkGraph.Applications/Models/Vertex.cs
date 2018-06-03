using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkGraph.Models
{
    public class Vertex
    {
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }

            private set
            {
                _id = value;
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }

            private set
            {
                _name = value;
            }
        }

        public Vertex(int id, string lastName, string firstName, string fatherName)
        {
            Id = id;
            Name = string.Format("{0} {1}.{2}.", lastName, firstName[0], fatherName[0]);
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Vertex)
                return Id.Equals((obj as Vertex).Id);
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
