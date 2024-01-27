using Godot;
using gtfo_demons.MollysHouse.Presentation;

public partial class BuildingHealthBar : Control
{
	[Export] private int _initialHealth;
	[Export] private string _name;
	[Export] private LevelBuildings _building;

	private ProgressBar _delayedBar;
	private Timer _delayedTimer;
	private ProgressBar _healthBar;
	private Label _buildingName;
	public override void _Ready()
	{
		_healthBar = GetNode<ProgressBar>("Container/CurrentHealthBar");
		_delayedBar = GetNode<ProgressBar>("Container/DelayedBar");
		_delayedTimer = GetNode<Timer>("Container/DelayedBar/DelayedTimer");
		_buildingName = GetNode<Label>("BuildingName");

		_buildingName.Text = _name ?? "LABEL_NOT_ASSIGNED";
		
		_healthBar.MaxValue = _initialHealth;
		_healthBar.Value = _initialHealth;
		
		_delayedBar.MaxValue = _initialHealth;
		_delayedBar.Value = _initialHealth;
		_delayedTimer.Connect(Timer.SignalName.Timeout, Callable.From(OnDelayedBarTimeout));

		_building?.Connect(LevelBuildings.SignalName.OnDamageReceived, Callable.From<int>(OnDamagedReceived));
	}

	private void OnDelayedBarTimeout()
	{
		_delayedBar.Value = _healthBar.Value;
	}

	private void OnDamagedReceived(int amount)
	{
		_healthBar.Value -= amount;
		_delayedTimer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
#if DEBUG
		if (Input.IsActionJustPressed("ui_accept"))
		{
			OnDamagedReceived(10);
		}
#endif
	}
}
