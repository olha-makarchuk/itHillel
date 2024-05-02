namespace Application.Interfaces
{
	public interface IRabbitMQProducer
	{
		public void SendProductMessage<T>(T message, string queue);
	}
}
