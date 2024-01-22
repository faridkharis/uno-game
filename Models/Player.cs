using UnoGame.Models.Interface;
using UnoGame.Models.Enum;

namespace UnoGame.Models;

public class Player : IPlayer
{
	public int Id { get; }
	public string Name { get; set; }

	public Player(int id, string name)
	{
		Id = id;
		Name = name;
	}
}
