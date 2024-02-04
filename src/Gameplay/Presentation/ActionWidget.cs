using System.Linq;
using Godot;
using gtfo_demons.John.Domain;
using gtfo_demons.John.Factory;

namespace gtfo_demons.Gameplay.Presentation;

public partial class ActionWidget : Control
{
    private NinePatchRect _rakeIcon;
    private NinePatchRect _paradoxIcon;
    private Label _buttonKey;

    private IPlayerInteractions _playerInteractions;

    public override void _Ready() {
        _buttonKey = GetNode<Label>("Panel/Key/ButtonKey");
        _rakeIcon = GetNode<NinePatchRect>("Panel/RakeIcon");
        _paradoxIcon = GetNode<NinePatchRect>("Panel/ParadoxIcon");

        _playerInteractions = PlayerServicesFactory.GetPlayerInteractions();
    }

    private void CheckUserController() {
        var inputs = Input.GetConnectedJoypads();
        if (inputs.Count > 0) {
            var deviceName = Input.GetJoyName(inputs.First()).ToLower();
            _buttonKey.Text = deviceName.Contains("nintendo") ? "Y" :
                deviceName.Contains("xinput") ? "X" : $"{(char)9634}";
        }
        else {
            _buttonKey.Text = "X";
        }
    }

    public override void _Process(double delta) {
        CheckUserController();
        if (_playerInteractions.IsInParadoxArea() && !_paradoxIcon.Visible) {
            _paradoxIcon.Visible = true;
            _rakeIcon.Visible = false;
            return;
        }

        if (!_playerInteractions.IsInParadoxArea() && !_rakeIcon.Visible) {
            _rakeIcon.Visible = true;
            _paradoxIcon.Visible = false;
        }
    }
}