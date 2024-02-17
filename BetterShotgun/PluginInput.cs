using LethalCompanyInputUtils.Api;
using UnityEngine.InputSystem;

namespace Hypick;

public class Keybinds : LcInputActions
{
	// public static readonly Keybinds Instance = new();

	public InputAction ReloadKey => Asset["Reload"];

	public override void CreateInputActions(in InputActionMapBuilder builder)
	{
		builder.NewActionBinding().WithActionId("Reload").WithActionType(InputActionType.Button).WithKbmPath(Manager.ReloadShotgunKey).WithBindingName("ReloadShotgunKey").Finish();
	}
}
