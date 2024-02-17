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

		public async Task<IEnumerable<MessageModel>> GetAllAsync()
		{
			return (await _Mrepo.GetAllAsync()).OrderByDescending(m => m.Date);
		}

		public async Task<MessageModel?> GetMessageByIdAsync(int id)
		{
			return await _Mrepo.GetMessageByIdAsync(id);
		}

		public async Task<MessageModel?> GetMessageByUsername(string username)
		{
			return await _Mrepo.GetMessageByUsernameAsync(username);
		}

		public async Task<IEnumerable<MessageModel>> CreateMessageAsync(MessageModel newMessage)
		{
			return await _Mrepo.CreateMessageAsync(newMessage);
		}

		public async Task<string> UpdateMessageAsync(int id, string message)
		{
			return await _Mrepo.UpdateMessageAsync(id, message);
		}

		public async Task<bool> DeleteMessageAsync(MessageModel messageToDelete)
		{
			return await _Mrepo.DeleteMessageAsync(messageToDelete);
		}
	}
}
