using Data;

namespace Business
{
    public class UsersManager
    {
        private readonly UserRepository _userRepository;

        public UsersManager(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int AddNewUser(string userName)
        {
            var user = GetUserByUserName(userName);
            if (user != null)
                throw new Exception($"Errore: utente {userName} già esistente");

            var newUserId = _userRepository.InsertNewUser(userName);
            return newUserId;
        }

        public UserDto GetUserById(int id)
        {
            if (id <= 0)
                throw new Exception("Id non valido");

            return _userRepository.GetById(id);
        }

        public UserDto? GetUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new Exception("UserName non valido");

            return _userRepository.GetByUserName(userName);
        }

        public int DeleteUserById(int id)
        {
            if (id <= 0)
                throw new Exception("Id non valido");

            var userId = _userRepository.DeleteUser(id);
            return userId ?? throw new Exception("Errore cancellazione utente");
        }
    }
}