namespace ChatNow_WebAPi.Hubs
{
    public interface IChatHub
    {
        /// <summary>
        /// Retorna alguma mensagem, dado ou informação ao cliente
        /// </summary>
        /// <param name="message">Mensagem a ser enviada</param>
        /// <returns>Task</returns>
        Task ReceiveMessage(string message);


        Task ReceiveChatMessage(string senderId, string message, string conversationId);
    }
}
