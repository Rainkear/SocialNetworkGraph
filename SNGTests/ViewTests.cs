using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkGraph.Models;
using SocialNetworkGraph.ViewModels;
using SocialNetworkGraph;

namespace SNGTests
{
    [TestClass]
    public class ViewTests
    {
        [TestMethod]
        public void PersonWindowTest()
        {
            Person dPerson = new Person(1, "Ivanov", "Ivan", "Ivanovich", new DateTime(1995, 1, 1),
                new Sex(1, "Male"), new Locality(1, "Moscow"), new Locality(10, "Voronezh"), "+79066666666");
            dPerson.Hobbies.Add(new Hobby(1, "TestHobby"));

            Person dFriendPerson = new Person(1, "Sidorov", "Petr", "Ivanovich", new DateTime(1995, 1, 1),
                new Sex(1, "Male"), new Locality(1, "Moscow"), new Locality(10, "Voronezh"), "+79066666667");
            dPerson.LFriends.Add(dFriendPerson);
            dPerson.RFriends.Add(dFriendPerson);

            PersonWindowViewModel vm = new PersonWindowViewModel(dPerson);
            PersonWindow window = new PersonWindow();
            window.DataContext = vm;
            window.ShowDialog();
        }
    }
}
