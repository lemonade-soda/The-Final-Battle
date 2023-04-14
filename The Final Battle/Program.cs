using The_Final_Battle;

var game = new Game();

Console.WriteLine("Do you want to play or watch the computers duke it out?");
var input = Console.ReadLine();

game.WantToPlay(input);

game.Start();