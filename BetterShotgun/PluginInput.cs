using LethalCompanyInputUtils.Api;
using UnityEngine.InputSystem;

namespace Hypick;

public class Keybinds : LcInputActions
{
	public InputAction ReloadKey => Asset["Reload"];

	public override void CreateInputActions(in InputActionMapBuilder builder)
	{
		builder.NewActionBinding().WithActionId("Reload").WithActionType(InputActionType.Button).WithBindingName("ReloadShotgunKey").Finish();
	}
}
