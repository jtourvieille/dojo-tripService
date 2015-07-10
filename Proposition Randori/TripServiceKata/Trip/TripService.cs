using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        private ITripDAOWrapper _tripDaoWrapper;
        private IUserSessionWrapper _userSessionWrapper;
        public TripService() : this(new TripDAOWrapper(), new UserSessionWrapper())
        {

        }

        public TripService(ITripDAOWrapper tripDAOWrapper, IUserSessionWrapper userSessionWrapper)
        {
            _userSessionWrapper = userSessionWrapper;
            _tripDaoWrapper = tripDAOWrapper;
        }


        public List<Trip> GetTripsByUser(User.User user)
        {
            User.User loggedUser = _userSessionWrapper.GetLoggedInUser();

            if (loggedUser == null)
                throw new UserNotLoggedInException();

            if (user.IsFriendWith(loggedUser))
                return _tripDaoWrapper.FindTripsByUser(user);

            return new List<Trip>();
        }
    }

    public interface ITripDAOWrapper
    {
        List<Trip> FindTripsByUser(User.User user);
    }

    public class TripDAOWrapper : ITripDAOWrapper
    {
        public List<Trip> FindTripsByUser(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }
    }

    public interface IUserSessionWrapper
    {
        User.User GetLoggedInUser();
    }

    public class UserSessionWrapper : IUserSessionWrapper
    {
        public User.User GetLoggedInUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}
