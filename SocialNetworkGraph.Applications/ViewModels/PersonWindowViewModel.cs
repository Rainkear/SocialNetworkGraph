using SocialNetworkGraph.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkGraph.ViewModels
{
    public class PersonWindowViewModel : BaseViewModel
    {
        private Person _person;

        public int ID
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
                NotifyPropertyChanged("ID");
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public List<string> Hobby
        {
            get
            {
                return _hobby;
            }

            set
            {
                _hobby = value;
                NotifyPropertyChanged("Hobby");
            }
        }
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }

            set
            {
                _birthDate = value;
                NotifyPropertyChanged("BirthDate");
            }
        }
        public string Sex
        {
            get
            {
                return _sex;
            }

            set
            {
                _sex = value;
                NotifyPropertyChanged("Sex");
            }
        }
        public string BirthPlace
        {
            get
            {
                return _birthPlace;
            }

            set
            {
                _birthPlace = value;
                NotifyPropertyChanged("BirthPlace");
            }
        }
        public string LivePlace
        {
            get
            {
                return _livePlace;
            }

            set
            {
                _livePlace = value;
                NotifyPropertyChanged("LivePlace");
            }
        }
        public string Phone
        {
            get
            {
                return _phone;
            }

            set
            {
                _phone = value;
                NotifyPropertyChanged("Phone");
            }
        }
        public List<string> Friends
        {
            get
            {
                return _friends;
            }

            set
            {
                _friends = value;
                NotifyPropertyChanged("Friends");
            }
        }

        private int _id;
        private string _name;
        private List<string> _hobby;
        private DateTime _birthDate;
        private string _sex;
        private string _birthPlace;
        private string _livePlace;
        private string _phone;
        private List<string> _friends;

        public PersonWindowViewModel(Person person)
        {
            _person = person;
            ID = person.Id;
            Name = string.Format("{0} {1} {2}", person.LastName, 
                person.FirstName, person.FatherName);
            Hobby = person.Hobbies.Select(x => x.Name).ToList();
            Friends = person.LFriends.Select(p =>
                    string.Format("{0} {1}. {2}.", p.LastName, p.FirstName[0], p.FatherName[0]))
                .ToList();
            BirthDate = person.BirthDate;
            LivePlace = person.LivePlace.Name;
            BirthPlace = person.BirthPlace.Name;
            Sex = person.Sex.Name;
            Phone = person.Phone;
        }
    }
}
