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

		public IEnumerable<MessageModel> GetAll()
		{
			return _Mrepo.GetAllAsync().Result.OrderByDescending(m => m.Date);
		}

		public MessageModel? GetMessageById(int id)
		{
			return _Mrepo.GetMessageById(id).Result;
		}

		public MessageModel? GetMessageByUsername(string username)
		{
			return _Mrepo.GetMessageByUsername(username).Result;
		}

		public IEnumerable<MessageModel> CreateMessage(MessageModel newMessage)
		{
			return _Mrepo.CreateMessage(newMessage).Result;
		}

		public string UpdateMessage(int id, string message)
		{
			return _Mrepo.UpdateMessage(id, message).Result;
		}

		public async Task<bool> DeleteMessage(MessageModel messageToDelete)
		{
			return await _Mrepo.DeleteMessage(messageToDelete);
		}
	}
}
