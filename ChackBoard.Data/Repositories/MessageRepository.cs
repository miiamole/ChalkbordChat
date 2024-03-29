﻿using ChackBoard.Data.Database;
using ChackBoard.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ChackBoard.Data.Repositories
{
	public class MessageRepository
	{
		private readonly ChalkboardDbContext _context;
		public string StatusMessage { get; set; }
		public MessageRepository(ChalkboardDbContext context)
		{
			_context = context;
			StatusMessage = "no status message";
		}

		public async Task<IEnumerable<MessageModel>> GetAllAsync()
		{
			return await _context.Messages.ToListAsync();
		}

		public async Task<MessageModel?> GetMessageByIdAsync(int id)
		{
			return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
		}

		public async Task<MessageModel?> GetMessageByUsernameAsync(string username)
		{
			return await _context.Messages.FirstOrDefaultAsync(m => m.Username == username);
		}

		public async Task<IEnumerable<MessageModel>> CreateMessageAsync(MessageModel newMessage)
		{
			try
			{
				await _context.Messages.AddAsync(newMessage);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{

				//catch error
				StatusMessage = ex.Message;
			}
			return await GetAllAsync();
		}

		public async Task<string> UpdateMessageAsync(int id, string message)
		{
			MessageModel? messagetoUpdate = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);

			if (messagetoUpdate == null)
			{
				return "something went wrong, check your variables";
			}
			messagetoUpdate.Message = message;
			messagetoUpdate.Date = DateTime.Now;
			await _context.SaveChangesAsync();

			return messagetoUpdate.Message;
		}

		public async Task<bool> DeleteMessageAsync(MessageModel messageToDelete)
		{

			try
			{
				_context.Messages.Remove(messageToDelete);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				StatusMessage = ex.Message;
				return false;
			}
			return true;
		}



	}
}
