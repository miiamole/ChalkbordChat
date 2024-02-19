using ChackBoard.Data.Model;
using ChackBoard.Data.Repositories;

namespace Chalkboard.App
{
    public class MessageServices
    {
        private MessageRepository _Mrepo;

        public MessageServices(MessageRepository Mrepo)
        {
            _Mrepo = Mrepo;
        }

        public async Task<IEnumerable<MessageModel>> GetAll()
        {
            return await _Mrepo.GetAllAsync();
        }

        public async Task<MessageModel?> GetMessageById(int id)
        {
            return await _Mrepo.GetMessageById(id);
        }

        public async Task<MessageModel?> GetMessageByUsername(string username)
        {
            return await _Mrepo.GetMessageByUsername(username);
        }

        public async Task<IEnumerable<MessageModel>> CreateMessage(MessageModel newMessage)
        {
            return await _Mrepo.CreateMessage(newMessage);
        }

        public async Task<string> UpdateMessage(int id, string message)
        {
            return await _Mrepo.UpdateMessage(id, message);
        }

        public bool DeleteMessage(MessageModel messageToDelete)
        {
            return _Mrepo.DeleteMessage(messageToDelete);
        }
    }
}
