using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TripServiceKata.Tests
{
    [TestClass]
    public class UserTest
    {

        User.User user;

        [TestInitialize]
        public void Init()
        {
            user = new User.User();
        }

        [TestMethod]
        public void ShouldAddAFriend()
        {
            user.AddFriend(new User.User());
            Assert.AreEqual(1, user.GetFriends().Count());
        }

        [TestMethod]
        public void IsFriendWith()
        {
            Assert.AreEqual(false, user.IsFriendWith(new User.User()));
        }


    }
}
