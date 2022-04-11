namespace Data
{
    public class UserRepository
    {
        private readonly List<UserDto> _users;

        public UserRepository()
        {
            if (_users == null)
                _users = new List<UserDto>();
        }

        public UserDto? GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public UserDto? GetByUserName(string userName)
        {
            var user = _users.FirstOrDefault(x => x.UserName == userName);
            return user;
        }

        public int InsertNewUser(string userName)
        {
            var newUser = new UserDto
            {
                UserName = userName.Trim(),
                Id = _users.Count + 1
            };

            _users.Add(newUser);
            return newUser.Id;
        }

        public int? DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            if (user == null) return null;
            var removed = _users.Remove(user);
            return removed ? id : null;
        }
    }
}
