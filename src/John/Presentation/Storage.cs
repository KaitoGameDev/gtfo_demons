using Godot;
using gtfo_demons.John.Domain;
using gtfo_demons.John.Factory;

namespace gtfo_demons.John.Presentation;

public partial class Storage : Node3D
{
	
	[Export] private int _capacity;
	[Export] private int _overloadedCapacity;
	[Export] private ProgressBar _progressBar;
	[Export] private StyleBoxFlat _barStyle;
	[Export] private AnimationPlayer _storageBarAnimator;

	private int _totalCapturedDemons = 0;
	private IPlayerInteractions _playerInteractions;

	public override void _Ready() {
		_playerInteractions = PlayerServicesFactory.GetPlayerInteractions();
		_progressBar.MaxValue = _capacity;
		_progressBar.Value = 0;
	}

	public override void _Process(double delta) {
		#if DEBUG
		if (Input.IsKeyPressed(Key.S)) {
			SaveDemon(5);
		}
		#endif
	}

	public void SaveDemon(int amount) {
		_totalCapturedDemons += amount;
		_progressBar.Value = _totalCapturedDemons;
		
		if (_totalCapturedDemons == _capacity + _overloadedCapacity) {
			_playerInteractions.Overloaded();
		}

		CheckBarBehaviour();
	}

	private void CheckBarBehaviour() {
		if (_totalCapturedDemons < _capacity) {
			_barStyle.BgColor = Color.FromString("f0c14a", Colors.White);
			if (_storageBarAnimator.IsPlaying())
				_storageBarAnimator.Stop();
		}
		else {
			_barStyle.BgColor = Color.FromString("b80816", Colors.White);
			if (_storageBarAnimator.CurrentAnimation != "overloaded") {
				_storageBarAnimator.Play("overloaded");
			}
		}
	}

	public void ReleaseDemon() {
		if (_totalCapturedDemons == 0) return;
		
		_totalCapturedDemons--;
		
		if (_totalCapturedDemons <= _capacity) {
			_progressBar.Value = _totalCapturedDemons;
		}
	}
}