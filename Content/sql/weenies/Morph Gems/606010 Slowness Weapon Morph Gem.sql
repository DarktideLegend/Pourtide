DELETE FROM `weenie` WHERE `class_Id` = 606010;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (606010, 'ace606010-slownessweaponmorphgem', 38, '2024-07-05 06:06:02') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (606010,   1,       2048) /* ItemType - Gem */
     , (606010,   5,         10) /* EncumbranceVal */
     , (606010,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (606010,  18,          0) /* UiEffects - Undef */
     , (606010,  19,          1) /* Value */
     , (606010,  53,        101) /* PlacementPosition - Resting */
     , (606010,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (606010,  94,      33025) /* TargetType - WeaponOrCaster */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (606010,  11, True ) /* IgnoreCollisions */
     , (606010,  13, True ) /* Ethereal */
     , (606010,  14, True ) /* GravityStatus */
     , (606010,  19, True ) /* Attackable */
     , (606010,  69, False) /* IsSellable */
     , (606010, 60001, True );

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (606010, 60001,       0);

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (606010,   1, 'Slowness Weapon Morph Gem') /* Name */
     , (606010,  14, 'Use this morph gem on any loot-generated weapon or caster to give it slowness capabilities.') /* Use */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (606010,   1,   33555677) /* Setup */
     , (606010,   3,  536870932) /* SoundTable */
     , (606010,   8,  100673819) /* Icon */
     , (606010,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-07-04T23:05:14.4164878-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [],
  "UserChangeSummary": "Weenie exported from ACEmulator world database using ACE.Adapter",
  "IsDone": true
}
*/
