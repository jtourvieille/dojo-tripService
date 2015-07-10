namespace TripServiceKata.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using TripServiceKata.Exception;
    using TripServiceKata.Trip;
    using System.Collections.Generic;
    using UserQueJeTest = User.User;
    using TripQueJeTest = TripServiceKata.Trip.Trip;

    [TestClass]
    public class TripServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(UserNotLoggedInException))]
        public void WhenUserNotLoggedInThenRaiseAnException()
        {
            UserQueJeTest GUEST = null;
            TripService ts = new TripService(new DummyTripWrapper(), new DummyUserSessionWrapper(GUEST));
            ts.GetTripsByUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void WhenNullParameterIsUsedItShouldThrow()
        {
            TripService ts = new TripService(new DummyTripWrapper(), new DummyUserSessionWrapper(new UserQueJeTest()));
            var result = ts.GetTripsByUser(null);
        }

        [TestMethod]
        public void WhenUserHasNoFriendItShouldReturnAnEmptyList()
        {
            TripService ts = new TripService(new DummyTripWrapper(), new DummyUserSessionWrapper(new UserQueJeTest()));
            var result = ts.GetTripsByUser(new UserQueJeTest());
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void WhenUserHasOneFriendButNoTrips()
        {
            var loggedInUser = new UserQueJeTest();
            var friend = new UserQueJeTest();
            friend.AddFriend(loggedInUser);

            TripService ts = new TripService(new DummyTripWrapper(), new DummyUserSessionWrapper(loggedInUser));
            var result = ts.GetTripsByUser(friend);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void WhenUserHasOneFriendWithOneTripShouldReturnOneTrip()
        {
            var loggedInUser = new UserQueJeTest();
            var friend = new UserQueJeTest();
            friend.AddTrip(new TripQueJeTest());
            friend.AddFriend(loggedInUser);

            TripService ts = new TripService(new DummyTripWrapper(), new DummyUserSessionWrapper(loggedInUser));
            var result = ts.GetTripsByUser(friend);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
    }

    public class DummyTripWrapper : ITripDAOWrapper
    {
        public List<TripQueJeTest> FindTripsByUser(UserQueJeTest user)
        {
            return user.Trips();
        }
    }

    public class DummyUserSessionWrapper : IUserSessionWrapper
    {
        private UserQueJeTest loggedInUser;

        public DummyUserSessionWrapper(UserQueJeTest loggedInUser)
        {
            this.loggedInUser = loggedInUser;
        }

        public UserQueJeTest GetLoggedInUser()
        {
            return this.loggedInUser;
        }
    }
}
