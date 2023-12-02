using System.Threading.Tasks;
using Godot;

namespace Matt.Godot.Tutorial2D;

// ReSharper disable once InconsistentNaming
public partial class HUD : CanvasLayer
{
    [Signal]
    public delegate void StartGameEventHandler();

    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
    }

    /// <summary>
    /// Update the score label in the HUD to the specified value.
    /// </summary>
    /// <param name="score"></param>
    public void UpdateScore(int score) => GetNode<Label>("ScoreLabel").Text = score.ToString();

    /// <summary>
    /// Shows the game over message, and prepares for the next game.
    /// </summary>
    public async Task ShowGameOver()
    {
        ShowMessage("Game Over!");

        var messageTimer = GetNode<Timer>("MessageTimer");
        await ToSignal(messageTimer, Timer.SignalName.Timeout);

        var message = GetNode<Label>("Message");
        message.Text = "Dodge the creeps!";
        message.Show();

        await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
        GetNode<Button>("StartButton").Show();
    }

    /// <summary>
    /// Shows a message on the HUD for a short while.
    /// </summary>
    /// <param name="text">The message to display</param>
    private void ShowMessage(string text)
    {
        var message = GetNode<Label>("Message");
        message.Text = text;
        message.Show();

        GetNode<Timer>("MessageTimer").Start();
    }

    /// <summary>
    /// Handle starting up the game when the button is pressed.
    /// </summary>
    private void OnStartButtonPressed()
    {
        GetNode<Button>("StartButton").Hide();
        EmitSignal(SignalName.StartGame);
    }

    /// <summary>
    /// Show the message when the timer lapses.
    /// </summary>
    private void OnMessageTimerTimeout() => GetNode<Label>("Message").Hide();
}